/* Copyright (c) 1996-2022 The OPC Foundation. All rights reserved.
   The source code in this file is covered under a dual-license scenario:
     - RCL: for OPC Foundation Corporate Members in good-standing
     - GPL V2: everybody else
   RCL license terms accompanied with this source code. See http://opcfoundation.org/License/RCL/1.00/
   GNU General Public License as published by the Free Software Foundation;
   version 2 of the License are accompanied with this source code. See http://opcfoundation.org/License/GPLv2
   This source code is distributed in the hope that it will be useful,
   but WITHOUT ANY WARRANTY; without even the implied warranty of
   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
*/

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Opc.Ua.Bindings
{
    /// <summary>
    /// Creates a transport channel for the ITransportChannel interface.
    /// Implements the UA-SC security and UA Binary encoding.
    /// The socket layer requires a IMessageSocketFactory implementation.
    /// </summary>
    public class UaSCUaBinaryTransportChannel : ITransportChannel, IMessageSocketChannel
    {
        private const int kChannelCloseDefault = 1_000;

        #region Constructors
        /// <summary>
        /// Create a transport channel from a message socket factory.
        /// </summary>
        /// <param name="messageSocketFactory">The message socket factory.</param>
        public UaSCUaBinaryTransportChannel(IMessageSocketFactory messageSocketFactory)
        {
            m_messageSocketFactory = messageSocketFactory;
        }
        #endregion

        #region IDisposable Members
        /// <summary>
        /// Frees any unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// An overrideable version of the Dispose.
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                var channel = Interlocked.Exchange(ref m_channel, null);
                Utils.SilentDispose(channel);
            }
        }
        #endregion

        #region IMessageSocketChannel Members
        /// <summary>
        /// Returns the channel's underlying message socket if connected / available.
        /// </summary>
        public IMessageSocket Socket
        {
            get { return m_channel?.Socket; }
        }
        #endregion

        #region ITransportChannel Members

        /// <summary>
        /// Called when the token changes
        /// </summary>
        public event ChannelTokenActivatedEventHandler OnTokenActivated
        {
            add => m_OnTokenActivated += value;
            remove => m_OnTokenActivated -= value;
        }

        /// <summary>
        /// A masking indicating which features are implemented.
        /// </summary>
        public TransportChannelFeatures SupportedFeatures =>
            TransportChannelFeatures.Open | TransportChannelFeatures.BeginOpen |
            TransportChannelFeatures.BeginSendRequest | TransportChannelFeatures.SendRequestAsync |
            (Socket?.MessageSocketFeatures ?? 0);

        /// <summary>
        /// Gets the description for the endpoint used by the channel.
        /// </summary>
        public EndpointDescription EndpointDescription => m_settings.Description;

        /// <summary>
        /// Gets the configuration for the channel.
        /// </summary>
        public EndpointConfiguration EndpointConfiguration => m_settings.Configuration;

        /// <summary>
        /// Gets the context used when serializing messages exchanged via the channel.
        /// </summary>
        public IServiceMessageContext MessageContext => m_quotas.MessageContext;

        /// <summary>
        ///  Gets the the channel's current security token.
        /// </summary>
        public ChannelToken CurrentToken
        {
            get { return m_channel?.CurrentToken; }
        }

        /// <summary>
        /// Gets or sets the default timeout for requests send via the channel.
        /// </summary>
        public int OperationTimeout
        {
            get { return m_operationTimeout; }
            set { m_operationTimeout = value; }
        }

        /// <summary>
        /// Initializes a secure channel with the endpoint identified by the URL.
        /// </summary>
        /// <param name="url">The URL for the endpoint.</param>
        /// <param name="settings">The settings to use when creating the channel.</param>
        /// <exception cref="ServiceResultException">Thrown if any communication error occurs.</exception>
        public void Initialize(
            Uri url,
            TransportChannelSettings settings)
        {
            SaveSettings(url, settings);
            Interlocked.Exchange(ref m_channel, CreateChannel());
        }

        /// <summary>
        /// Initializes a secure channel with the endpoint identified by the connection.
        /// </summary>
        /// <param name="connection">The connection to use.</param>
        /// <param name="settings">The settings to use when creating the channel.</param>
        /// <exception cref="ServiceResultException">Thrown if any communication error occurs.</exception>
        public void Initialize(
            ITransportWaitingConnection connection,
            TransportChannelSettings settings)
        {
            SaveSettings(connection.EndpointUrl, settings);
            Interlocked.Exchange(ref m_channel, CreateChannel(connection));
        }

        /// <summary>
        /// Opens a secure channel with the endpoint identified by the URL.
        /// </summary>
        /// <exception cref="ServiceResultException">Thrown if any communication error occurs.</exception>
        public void Open()
        {
            // opens when the first request is called to preserve previous behavior.
        }

        /// <summary>
        /// Begins an asynchronous operation to open a secure channel with the endpoint identified by the URL.
        /// </summary>
        /// <param name="callback">The callback to call when the operation completes.</param>
        /// <param name="callbackData">The callback data to return with the callback.</param>
        /// <returns>
        /// The result which must be passed to the EndOpen method.
        /// </returns>
        /// <exception cref="ServiceResultException">Thrown if any communication error occurs.</exception>
        /// <seealso cref="Open"/>
        public IAsyncResult BeginOpen(AsyncCallback callback, object callbackData)
        {
            lock (m_lock)
            {
                // create the channel.
                Interlocked.Exchange(ref m_channel, CreateChannel(null));

                // begin connect operation.
                return m_channel.BeginConnect(this.m_url, m_operationTimeout, callback, callbackData);
            }
        }

        /// <summary>
        /// Completes an asynchronous operation to open a secure channel.
        /// </summary>
        /// <param name="result">The result returned from the BeginOpen call.</param>
        /// <exception cref="ServiceResultException">Thrown if any communication error occurs.</exception>
        /// <seealso cref="Open"/>
        public void EndOpen(IAsyncResult result)
        {
            m_channel.EndConnect(result);
        }

        /// <summary>
        /// Closes any existing secure channel and opens a new one.
        /// </summary>
        /// <exception cref="ServiceResultException">Thrown if any communication error occurs.</exception>
        /// <remarks>
        /// Calling this method will cause outstanding requests over the current secure channel to fail.
        /// </remarks>
        public void Reconnect() => Reconnect(null);

        /// <summary>
        /// Closes any existing secure channel and opens a new one.
        /// </summary>
        /// <param name="connection">A reverse connection, null otherwise.</param>
        /// <exception cref="ServiceResultException">Thrown if any communication error occurs.</exception>
        /// <remarks>
        /// Calling this method will cause outstanding requests over the current secure channel to fail.
        /// </remarks>
        public void Reconnect(ITransportWaitingConnection connection)
        {
            Utils.LogInfo("TransportChannel RECONNECT: Reconnecting to {0}.", m_url);

            lock (m_lock)
            {
                // the new channel must be created first because WinSock will reuse sockets and this
                // can result in messages sent over the old socket arriving as messages on the new socket.
                // if this happens the new channel is shutdown because of a security violation.
                UaSCUaBinaryClientChannel channel = Interlocked.Exchange(ref m_channel, null);

                try
                {
                    // reconnect.
                    Interlocked.Exchange(ref m_channel, CreateChannel(connection));

                    // begin connect operation.
                    IAsyncResult result = m_channel.BeginConnect(m_url, m_operationTimeout, null, null);
                    m_channel.EndConnect(result);
                }
                finally
                {
                    // close existing channel.
                    if (channel != null)
                    {
                        try
                        {
                            channel.Close(kChannelCloseDefault);
                        }
                        catch (Exception e)
                        {
                            // do nothing.
                            Utils.LogTrace(e, "Ignoring exception while closing transport channel during Reconnect.");
                        }
                        finally
                        {
                            channel.Dispose();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Begins an asynchronous operation to close the existing secure channel and open a new one.
        /// </summary>
        /// <param name="callback">The callback to call when the operation completes.</param>
        /// <param name="callbackData">The callback data to return with the callback.</param>
        /// <returns>
        /// The result which must be passed to the EndReconnect method.
        /// </returns>
        /// <exception cref="ServiceResultException">Thrown if any communication error occurs.</exception>
        /// <seealso cref="Reconnect()"/>
        public IAsyncResult BeginReconnect(AsyncCallback callback, object callbackData)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Completes an asynchronous operation to close the existing secure channel and open a new one.
        /// </summary>
        /// <param name="result">The result returned from the BeginReconnect call.</param>
        /// <exception cref="ServiceResultException">Thrown if any communication error occurs.</exception>
        /// <seealso cref="Reconnect()"/>
        public void EndReconnect(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Closes the secure channel.
        /// </summary>
        /// <exception cref="ServiceResultException">Thrown if any communication error occurs.</exception>
        public void Close()
        {
            UaSCUaBinaryClientChannel channel = Interlocked.Exchange(ref m_channel, null);
            channel?.Close(kChannelCloseDefault);
        }

        /// <summary>
        /// Closes the secure channel (async).
        /// </summary>
        /// <exception cref="ServiceResultException">Thrown if any communication error occurs.</exception>
        public Task CloseAsync(CancellationToken ct)
        {
            UaSCUaBinaryClientChannel channel = Interlocked.Exchange(ref m_channel, null);
            if (channel != null)
            {
                return channel.CloseAsync(kChannelCloseDefault, ct);
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Begins an asynchronous operation to close the secure channel.
        /// </summary>
        /// <param name="callback">The callback to call when the operation completes.</param>
        /// <param name="callbackData">The callback data to return with the callback.</param>
        /// <returns>
        /// The result which must be passed to the EndClose method.
        /// </returns>
        /// <exception cref="ServiceResultException">Thrown if any communication error occurs.</exception>
        /// <seealso cref="Close"/>
        public IAsyncResult BeginClose(AsyncCallback callback, object callbackData)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Completes an asynchronous operation to close the secure channel.
        /// </summary>
        /// <param name="result">The result returned from the BeginClose call.</param>
        /// <exception cref="ServiceResultException">Thrown if any communication error occurs.</exception>
        /// <seealso cref="Close"/>
        public void EndClose(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sends a request over the secure channel.
        /// </summary>
        /// <param name="request">The request to send.</param>
        /// <returns>The response returned by the server.</returns>
        /// <exception cref="ServiceResultException">Thrown if any communication error occurs.</exception>
        public IServiceResponse SendRequest(IServiceRequest request)
        {
            IAsyncResult result = BeginSendRequest(request, null, null);
            return EndSendRequest(result);
        }

        /// <summary>
        /// Sends a request over the secure channel (async version).
        /// </summary>
        /// <param name="request">The request to send.</param>
        /// <param name="ct">The cancellation token.</param>
        /// <returns>The response returned by the server.</returns>
        /// <exception cref="ServiceResultException">Thrown if any communication error occurs.</exception>
        public Task<IServiceResponse> SendRequestAsync(IServiceRequest request, CancellationToken ct)
        {
            var operation = BeginSendRequest(request, null, null);
            return EndSendRequestAsync(operation, ct);
        }

        /// <summary>
        /// Begins an asynchronous operation to send a request over the secure channel.
        /// </summary>
        /// <param name="request">The request to send.</param>
        /// <param name="callback">The callback to call when the operation completes.</param>
        /// <param name="callbackData">The callback data to return with the callback.</param>
        /// <returns>
        /// The result which must be passed to the EndSendRequest method.
        /// </returns>
        /// <exception cref="ServiceResultException">Thrown if any communication error occurs.</exception>
        /// <seealso cref="SendRequest"/>
        public IAsyncResult BeginSendRequest(IServiceRequest request, AsyncCallback callback, object callbackData)
        {
            UaSCUaBinaryClientChannel channel = m_channel;

            if (channel == null)
            {
                channel = CreateChannel();
                var currentChannel = Interlocked.CompareExchange(ref m_channel, channel, null);
                if (currentChannel != null)
                {
                    Utils.SilentDispose(channel);
                    channel = currentChannel;
                }
            }

            return channel.BeginSendRequest(request, m_operationTimeout, callback, callbackData);
        }

        /// <summary>
        /// Completes an asynchronous operation to send a request over the secure channel.
        /// </summary>
        /// <param name="result">The result returned from the BeginSendRequest call.</param>
        /// <returns></returns>
        /// <exception cref="ServiceResultException">Thrown if any communication error occurs.</exception>
        /// <seealso cref="SendRequest"/>
        public IServiceResponse EndSendRequest(IAsyncResult result)
        {
            UaSCUaBinaryClientChannel channel = m_channel;

            if (channel == null)
            {
                throw ServiceResultException.Create(StatusCodes.BadSecureChannelClosed, "Channel has been closed.");
            }

            return channel.EndSendRequest(result);
        }

        /// <summary>
        /// Completes an asynchronous operation to send a request over the secure channel.
        /// </summary>
        /// <param name="result">The result returned from the BeginSendRequest call.</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        /// <exception cref="ServiceResultException">Thrown if any communication error occurs.</exception>
        /// <seealso cref="SendRequest"/>
        public Task<IServiceResponse> EndSendRequestAsync(IAsyncResult result, CancellationToken ct)
        {
            UaSCUaBinaryClientChannel channel = m_channel;

            if (channel == null)
            {
                throw ServiceResultException.Create(StatusCodes.BadSecureChannelClosed, "Channel has been closed.");
            }

            return channel.EndSendRequestAsync(result, ct);
        }

        /// <summary>
        /// Saves the settings so the channel can be opened later.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="settings">The settings.</param>
        private void SaveSettings(Uri url, TransportChannelSettings settings)
        {
            // save the settings.
            m_url = url;
            m_settings = settings;
            m_operationTimeout = settings.Configuration.OperationTimeout;

            // initialize the quotas.
            EndpointConfiguration configuration = m_settings.Configuration;
            m_quotas = new ChannelQuotas {
                MaxBufferSize = configuration.MaxBufferSize,
                MaxMessageSize = TcpMessageLimits.AlignRoundMaxMessageSize(configuration.MaxMessageSize),
                ChannelLifetime = configuration.ChannelLifetime,
                SecurityTokenLifetime = configuration.SecurityTokenLifetime,
                MessageContext = new ServiceMessageContext() {
                    MaxArrayLength = configuration.MaxArrayLength,
                    MaxByteStringLength = configuration.MaxByteStringLength,
                    MaxMessageSize = TcpMessageLimits.AlignRoundMaxMessageSize(configuration.MaxMessageSize),
                    MaxStringLength = configuration.MaxStringLength,
                    MaxEncodingNestingLevels = configuration.MaxEncodingNestingLevels,
                    MaxDecoderRecoveries = configuration.MaxDecoderRecoveries,
                    NamespaceUris = m_settings.NamespaceUris,
                    ServerUris = new StringTable(),
                    Factory = m_settings.Factory
                },

                CertificateValidator = settings.CertificateValidator
            };

            // create the buffer manager.
            m_bufferManager = new BufferManager("Client", settings.Configuration.MaxBufferSize);
        }

        /// <summary>
        /// Opens the channel before sending the request.
        /// </summary>
        /// <param name="connection">A reverse connection, null otherwise.</param>
        private UaSCUaBinaryClientChannel CreateChannel(ITransportWaitingConnection connection = null)
        {
            IMessageSocket socket = null;
            if (connection != null)
            {
                socket = connection.Handle as IMessageSocket;
                if (socket == null)
                {
                    throw new ArgumentException("Connection Handle is not of type IMessageSocket.");
                }
            }

            // create the channel.
            var channel = new UaSCUaBinaryClientChannel(
                Guid.NewGuid().ToString(),
                m_bufferManager,
                m_messageSocketFactory,
                m_quotas,
                m_settings.ClientCertificate,
                m_settings.ClientCertificateChain,
                m_settings.ServerCertificate,
                m_settings.Description);

            // use socket for reverse connections, ignore otherwise
            if (socket != null)
            {
                channel.Socket = socket;
                channel.Socket.ChangeSink(channel);
                channel.ReverseSocket = true;
            }

            // Register the token changed event handler with the internal channel
            channel.OnTokenActivated =
                (current, previous) => m_OnTokenActivated?.Invoke(this, current, previous);
            return channel;
        }
        #endregion

        #region Private Fields
        private readonly object m_lock = new object();
        private Uri m_url;
        private int m_operationTimeout;
        private TransportChannelSettings m_settings;
        private ChannelQuotas m_quotas;
        private BufferManager m_bufferManager;
        private UaSCUaBinaryClientChannel m_channel;
        private event ChannelTokenActivatedEventHandler m_OnTokenActivated;
        private IMessageSocketFactory m_messageSocketFactory;
        #endregion
    }
}
