/* ========================================================================
 * Copyright (c) 2005-2020 The OPC Foundation, Inc. All rights reserved.
 *
 * OPC Foundation MIT License 1.00
 *
 * Permission is hereby granted, free of charge, to any person
 * obtaining a copy of this software and associated documentation
 * files (the "Software"), to deal in the Software without
 * restriction, including without limitation the rights to use,
 * copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the
 * Software is furnished to do so, subject to the following
 * conditions:
 *
 * The above copyright notice and this permission notice shall be
 * included in all copies or substantial portions of the Software.
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
 * OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
 * NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
 * HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
 * WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
 * OTHER DEALINGS IN THE SOFTWARE.
 *
 * The complete license agreement can be found here:
 * http://opcfoundation.org/License/MIT/1.00/
 * ======================================================================*/

using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Opc.Ua.Server
{
    /// <summary>
    /// An interface used by the monitored items to signal the subscription.
    /// </summary>
    public interface ISubscription
    {
        /// <summary>
        /// The session that owns the monitored item.
        /// </summary>
        Session Session { get; }

        /// <summary>
        /// The subscriptions owner identity.
        /// </summary>
        IUserIdentity EffectiveIdentity { get; }

        /// <summary>
        /// The identifier for the item that is unique within the server.
        /// </summary>
        uint Id { get; }

        /// <summary>
        /// Called when a monitored item is ready to publish.
        /// </summary>
        void ItemReadyToPublish(IMonitoredItem monitoredItem);

        /// <summary>
        /// Called when a monitored item is ready to publish.
        /// </summary>
        void ItemNotificationsAvailable(IMonitoredItem monitoredItem);

        /// <summary>
        /// Called when a value of monitored item is discarded in the monitoring queue.
        /// </summary>
        void QueueOverflowHandler();
    }

    /// <summary>
    /// Manages a subscription created by a client.
    /// </summary>
    public class Subscription : ISubscription, IDisposable
    {
        #region Constructors
        /// <summary>
        /// Initializes the object.
        /// </summary>
        public Subscription(
            IServerInternal server,
            Session session,
            uint subscriptionId,
            double publishingInterval,
            uint maxLifetimeCount,
            uint maxKeepAliveCount,
            uint maxNotificationsPerPublish,
            byte priority,
            bool publishingEnabled,
            uint maxMessageCount)
        {
            if (server == null) throw new ArgumentNullException(nameof(server));
            if (session == null) throw new ArgumentNullException(nameof(session));

            m_server = server;
            m_session = session;
            m_id = subscriptionId;
            m_publishingInterval = publishingInterval;
            m_maxLifetimeCount = maxLifetimeCount;
            m_maxKeepAliveCount = maxKeepAliveCount;
            m_maxNotificationsPerPublish = maxNotificationsPerPublish;
            m_publishingEnabled = publishingEnabled;
            m_priority = priority;
            m_publishTimerExpiry = HiResClock.TickCount64 + (long)publishingInterval;
            m_keepAliveCounter = maxKeepAliveCount;
            m_lifetimeCounter = 0;
            m_waitingForPublish = false;
            m_maxMessageCount = maxMessageCount;
            m_sentMessages = new List<NotificationMessage>();
            m_supportsDurable = m_server.MonitoredItemQueueFactory.SupportsDurableQueues;
            m_isDurable = false;

            m_monitoredItems = new Dictionary<uint, LinkedListNode<IMonitoredItem>>();
            m_itemsToCheck = new LinkedList<IMonitoredItem>();
            m_itemsToPublish = new LinkedList<IMonitoredItem>();
            m_itemsToTrigger = new Dictionary<uint, List<ITriggeredMonitoredItem>>();

            // m_itemsReadyToPublish         = new Queue<IMonitoredItem>();
            // m_itemsNotificationsAvailable = new LinkedList<IMonitoredItem>();
            m_sequenceNumber = 1;

            // initialize diagnostics.
            m_diagnostics = new SubscriptionDiagnosticsDataType {
                SessionId = m_session.Id,
                SubscriptionId = m_id,
                Priority = priority,
                PublishingInterval = publishingInterval,
                MaxKeepAliveCount = maxKeepAliveCount,
                MaxLifetimeCount = maxLifetimeCount,
                MaxNotificationsPerPublish = maxNotificationsPerPublish,
                PublishingEnabled = publishingEnabled,
                ModifyCount = 0,
                EnableCount = 0,
                DisableCount = 0,
                RepublishMessageRequestCount = 0,
                RepublishMessageCount = 0,
                TransferRequestCount = 0,
                TransferredToSameClientCount = 0,
                TransferredToAltClientCount = 0,
                PublishRequestCount = 0,
                DataChangeNotificationsCount = 0,
                EventNotificationsCount = 0,
                NotificationsCount = 0,
                LatePublishRequestCount = 0,
                CurrentKeepAliveCount = 0,
                CurrentLifetimeCount = 0,
                UnacknowledgedMessageCount = 0,
                DiscardedMessageCount = 0,
                MonitoredItemCount = 0,
                DisabledMonitoredItemCount = 0,
                MonitoringQueueOverflowCount = 0,
                NextSequenceNumber = (uint)m_sequenceNumber
            };

            ServerSystemContext systemContext = m_server.DefaultSystemContext.Copy(session);

            m_diagnosticsId = server.DiagnosticsNodeManager.CreateSubscriptionDiagnostics(
                systemContext,
                m_diagnostics,
                OnUpdateDiagnostics);

            TraceState(LogLevel.Information, TraceStateId.Config, "CREATED");
        }

        /// <summary>
        /// Initialize subscription after a restart from a template
        /// </summary>
        public Subscription(IServerInternal server,
            IStoredSubscription storedSubscription)
        {
            if (server.IsRunning)
            {
                throw new InvalidOperationException("Subscription restore can only occur on startup");
            }

            m_server = server;
            m_session = null;
            m_id = storedSubscription.Id;
            m_publishingInterval = storedSubscription.PublishingInterval;
            m_maxLifetimeCount = storedSubscription.MaxLifetimeCount;
            m_lifetimeCounter = storedSubscription.LifetimeCounter;
            m_maxKeepAliveCount = storedSubscription.MaxKeepaliveCount;
            m_maxNotificationsPerPublish = storedSubscription.MaxNotificationsPerPublish;
            m_publishingEnabled = false;
            m_priority = storedSubscription.Priority;
            m_publishTimerExpiry = HiResClock.TickCount64 + (long)storedSubscription.PublishingInterval;
            m_keepAliveCounter = storedSubscription.MaxKeepaliveCount;
            m_waitingForPublish = false;
            m_maxMessageCount = storedSubscription.MaxMessageCount;
            m_sentMessages = storedSubscription.SentMessages;
            m_supportsDurable = m_server.MonitoredItemQueueFactory.SupportsDurableQueues;
            m_isDurable = storedSubscription.IsDurable;
            m_savedOwnerIdentity = new UserIdentity(storedSubscription.UserIdentityToken);
            m_sequenceNumber = storedSubscription.SequenceNumber;
            m_lastSentMessage = storedSubscription.LastSentMessage;

            m_monitoredItems = new Dictionary<uint, LinkedListNode<IMonitoredItem>>();
            m_itemsToCheck = new LinkedList<IMonitoredItem>();
            m_itemsToPublish = new LinkedList<IMonitoredItem>();
            m_itemsToTrigger = new Dictionary<uint, List<ITriggeredMonitoredItem>>();

            // initialize diagnostics.
            m_diagnostics = new SubscriptionDiagnosticsDataType {
                SubscriptionId = m_id,
                Priority = m_priority,
                PublishingInterval = m_publishingInterval,
                MaxKeepAliveCount = m_maxKeepAliveCount,
                MaxLifetimeCount = m_maxLifetimeCount,
                MaxNotificationsPerPublish = m_maxNotificationsPerPublish,
                PublishingEnabled = m_publishingEnabled,
                ModifyCount = 0,
                EnableCount = 0,
                DisableCount = 0,
                RepublishMessageRequestCount = 0,
                RepublishMessageCount = 0,
                TransferRequestCount = 0,
                TransferredToSameClientCount = 0,
                TransferredToAltClientCount = 0,
                PublishRequestCount = 0,
                DataChangeNotificationsCount = 0,
                EventNotificationsCount = 0,
                NotificationsCount = 0,
                LatePublishRequestCount = 0,
                CurrentKeepAliveCount = 0,
                CurrentLifetimeCount = 0,
                UnacknowledgedMessageCount = 0,
                DiscardedMessageCount = 0,
                MonitoredItemCount = 0,
                DisabledMonitoredItemCount = 0,
                MonitoringQueueOverflowCount = 0,
                NextSequenceNumber = (uint)m_sequenceNumber
            };

            ServerSystemContext systemContext = m_server.DefaultSystemContext.Copy();

            m_diagnosticsId = server.DiagnosticsNodeManager.CreateSubscriptionDiagnostics(
                systemContext,
                m_diagnostics,
                OnUpdateDiagnostics);

            TraceState(LogLevel.Information, TraceStateId.Config, "RESTORED");

            RestoreMonitoredItems(storedSubscription.MonitoredItems);
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
                lock (m_lock)
                {
                    foreach (KeyValuePair<uint, LinkedListNode<IMonitoredItem>> monitoredItemKVP in m_monitoredItems)
                    {
                        Utils.SilentDispose(monitoredItemKVP.Value?.Value);
                    }

                    m_monitoredItems.Clear();
                    m_sentMessages.Clear();
                    m_itemsToCheck.Clear();
                    m_itemsToPublish.Clear();
                }
            }
        }
        #endregion

        #region ISubscription Members
        /// <summary>
        /// The session that owns the monitored item.
        /// </summary>
        public Session Session
        {
            get { return m_session; }
        }

        /// <summary>
        /// The unique identifier assigned to the subscription.
        /// </summary>
        public uint Id
        {
            get { return m_id; }
        }

        /// <summary>
        /// The subscriptions owner identity.
        /// </summary>
        public IUserIdentity EffectiveIdentity
        {
            get { return (m_session != null) ? m_session.EffectiveIdentity : m_savedOwnerIdentity; }
        }

        /// <summary>
        /// Queues an item that is ready to publish.
        /// </summary>
        public void ItemReadyToPublish(IMonitoredItem monitoredItem)
        {
            /*
            lock (m_itemsReadyToPublish)
            {
                m_itemsReadyToPublish.Enqueue(monitoredItem);
            }
            */
        }

        /// <summary>
        /// Tells the subscription that notifications are available but the item is not ready to publish.
        /// </summary>
        public void ItemNotificationsAvailable(IMonitoredItem monitoredItem)
        {
            /*
            lock (m_itemsReadyToPublish)
            {
                m_itemsNotificationsAvailable.AddLast(monitoredItem);
            }
            */
        }
        #endregion

        #region Public Interface
        /// <summary>
        /// The identifier for the session that owns the subscription.
        /// </summary>
        public NodeId SessionId
        {
            get
            {
                lock (m_lock)
                {
                    if (m_session == null)
                    {
                        return null;
                    }

                    return m_session.Id;
                }
            }
        }

        /// <summary>
        /// True if the subscription is set to durable and supports long lifetime and queue size
        /// </summary>
        public bool IsDurable => m_isDurable;

        /// <summary>
        /// Gets the lock that must be acquired before accessing the contents of the Diagnostics property.
        /// </summary>
        public object DiagnosticsLock
        {
            get
            {
                return m_diagnostics;
            }
        }

        /// <summary>
        /// Gets the lock that must be acquired before updating the contents of the Diagnostics property.
        /// </summary>
        public object DiagnosticsWriteLock
        {
            get
            {
                // mark diagnostic nodes dirty
                if (m_server != null && m_server.DiagnosticsNodeManager != null)
                {
                    m_server.DiagnosticsNodeManager.ForceDiagnosticsScan();
                }
                return DiagnosticsLock;
            }
        }

        /// <summary>
        /// Gets the current diagnostics for the subscription.
        /// </summary>
        public SubscriptionDiagnosticsDataType Diagnostics
        {
            get
            {
                return m_diagnostics;
            }
        }

        /// <summary>
        /// The publishing rate for the subscription.
        /// </summary>
        public double PublishingInterval
        {
            get
            {
                lock (m_lock)
                {
                    return m_publishingInterval;
                }
            }
        }

        /// <summary>
        /// The number of monitored items.
        /// </summary>
        public int MonitoredItemCount
        {
            get
            {
                lock (m_lock)
                {
                    return m_monitoredItems.Count;
                }
            }
        }

        /// <summary>
        /// The priority assigned to the subscription.
        /// </summary>
        public byte Priority
        {
            get
            {
                return m_priority;
            }
        }

        /// <summary>
        /// Deletes the subscription.
        /// </summary>
        public void Delete(OperationContext context)
        {
            // delete the diagnostics.
            if (m_diagnosticsId != null && !m_diagnosticsId.IsNullNodeId)
            {
                ServerSystemContext systemContext = m_server.DefaultSystemContext.Copy(m_session);
                m_server.DiagnosticsNodeManager.DeleteSubscriptionDiagnostics(systemContext, m_diagnosticsId);
            }

            lock (m_lock)
            {
                try
                {
                    TraceState(LogLevel.Information, TraceStateId.Deleted, "DELETED");

                    // the context may be null if the server is cleaning up expired subscriptions.
                    // in this case we create a context with a dummy request and use the current session.
                    if (context == null)
                    {
                        RequestHeader requestHeader = new RequestHeader();
                        requestHeader.ReturnDiagnostics = (uint)(int)DiagnosticsMasks.OperationSymbolicIdAndText;
                        context = new OperationContext(requestHeader, RequestType.Unknown);
                    }

                    StatusCodeCollection results;
                    DiagnosticInfoCollection diagnosticInfos;

                    DeleteMonitoredItems(
                        context,
                        new UInt32Collection(m_monitoredItems.Keys),
                        true,
                        out results,
                        out diagnosticInfos);
                }
                catch (Exception e)
                {
                    Utils.LogError(e, "Delete items for subscription failed.");
                }
            }
        }

        /// <summary>
        /// Checks if the subscription is ready to publish.
        /// </summary>
        public PublishingState PublishTimerExpired()
        {
            lock (m_lock)
            {
                long currentTime = HiResClock.TickCount64;

                // check if publish interval has elapsed.
                if (m_publishTimerExpiry >= currentTime)
                {
                    // check if waiting for publish.
                    if (m_waitingForPublish)
                    {
                        return PublishingState.WaitingForPublish;
                    }

                    return PublishingState.Idle;
                }

                // set next expiry time.
                while (m_publishTimerExpiry < currentTime)
                {
                    m_publishTimerExpiry += (long)m_publishingInterval;
                }

                // check lifetime has elapsed.
                if (m_waitingForPublish)
                {
                    m_lifetimeCounter++;

                    lock (DiagnosticsWriteLock)
                    {
                        m_diagnostics.LatePublishRequestCount++;
                        m_diagnostics.CurrentLifetimeCount = m_lifetimeCounter;
                    }

                    if (m_lifetimeCounter >= m_maxLifetimeCount)
                    {
                        TraceState(LogLevel.Information, TraceStateId.Deleted, "EXPIRED");
                        return PublishingState.Expired;
                    }
                }

                // increment keep alive counter.
                m_keepAliveCounter++;

                lock (DiagnosticsWriteLock)
                {
                    m_diagnostics.CurrentKeepAliveCount = m_keepAliveCounter;
                }

                // check for monitored items.
                if (m_publishingEnabled && m_session != null)
                {
                    // check for monitored items that are ready to publish.
                    LinkedListNode<IMonitoredItem> current = m_itemsToCheck.First;
                    bool itemsTriggered = false;

                    while (current != null)
                    {
                        LinkedListNode<IMonitoredItem> next = current.Next;
                        IMonitoredItem monitoredItem = current.Value;

                        // check if the item is ready to publish.
                        if (monitoredItem.IsReadyToPublish || monitoredItem.IsResendData)
                        {
                            m_itemsToCheck.Remove(current);
                            m_itemsToPublish.AddLast(current);
                        }

                        // update any triggered items.
                        List<ITriggeredMonitoredItem> triggeredItems = null;

                        if (monitoredItem.IsReadyToTrigger)
                        {
                            if (m_itemsToTrigger.TryGetValue(current.Value.Id, out triggeredItems))
                            {
                                for (int ii = 0; ii < triggeredItems.Count; ii++)
                                {
                                    if (triggeredItems[ii].SetTriggered())
                                    {
                                        itemsTriggered = true;
                                    }
                                }

                                // clear ReadyToTrigger flag after trigger
                                monitoredItem.IsReadyToTrigger = false;
                            }
                        }

                        current = next;
                    }

                    // need to go through the list again if items were triggered.
                    if (itemsTriggered)
                    {
                        current = m_itemsToCheck.First;

                        while (current != null)
                        {
                            LinkedListNode<IMonitoredItem> next = current.Next;
                            IMonitoredItem monitoredItem = current.Value;

                            if (monitoredItem.IsReadyToPublish)
                            {
                                m_itemsToCheck.Remove(current);
                                m_itemsToPublish.AddLast(current);
                            }

                            current = next;
                        }
                    }

                    if (m_itemsToPublish.Count > 0)
                    {
                        if (!m_waitingForPublish)
                        {
                            // TraceState(LogLevel.Trace, TraceStateId.Deleted, "READY TO PUBLISH");
                        }

                        m_waitingForPublish = true;
                        return PublishingState.NotificationsAvailable;
                    }
                }

                // check if keep alive expired.
                if (m_keepAliveCounter >= m_maxKeepAliveCount)
                {
                    if (!m_waitingForPublish)
                    {
                        // TraceState(LogLevel.Trace, TraceStateId.Items, "READY TO KEEPALIVE");
                    }

                    m_waitingForPublish = true;
                    return PublishingState.NotificationsAvailable;
                }

                // do nothing.
                return PublishingState.Idle;
            }
        }

        /// <summary>
        /// Transfers the subscription to a new session.
        /// </summary>
        /// <param name="context">The session to which the subscription is transferred.</param>
        /// <param name="sendInitialValues">Whether the first Publish response shall contain current values.</param> 
        public void TransferSession(OperationContext context, bool sendInitialValues)
        {
            // locked by caller
            m_session = context.Session;

            var monitoredItems = m_monitoredItems.Select(v => v.Value.Value).ToList();
            var errors = new List<ServiceResult>(monitoredItems.Count);
            for (int ii = 0; ii < monitoredItems.Count; ii++)
            {
                errors.Add(null);
            }

            m_server.NodeManager.TransferMonitoredItems(context, sendInitialValues, monitoredItems, errors);

            int badTransfers = 0;
            for (int ii = 0; ii < errors.Count; ii++)
            {
                if (ServiceResult.IsBad(errors[ii]))
                {
                    badTransfers++;
                }
            }

            if (badTransfers > 0)
            {
                Utils.LogTrace("Failed to transfer {0} Monitored Items", badTransfers);
            }

            lock (DiagnosticsWriteLock)
            {
                m_diagnostics.SessionId = m_session.Id;
            }
        }

        /// <summary>
        /// Initiates resending of all data monitored items in a Subscription
        /// </summary>
        /// <param name="context"></param>
        public void ResendData(OperationContext context)
        {
            // check session.
            VerifySession(context);
            lock (m_lock)
            {
                var monitoredItems = m_monitoredItems.Select(v => v.Value.Value).ToList();
                foreach (IMonitoredItem monitoredItem in monitoredItems)
                {
                    monitoredItem.SetupResendDataTrigger();
                }
            }
        }

        /// <summary>
        /// Tells the subscription that the owning session is being closed.
        /// </summary>
        public void SessionClosed()
        {
            lock (m_lock)
            {
                if (m_session != null)
                {
                    m_savedOwnerIdentity = m_session.EffectiveIdentity;
                    m_session = null;
                }
            }

            lock (DiagnosticsWriteLock)
            {
                m_diagnostics.SessionId = null;
            }
        }

        /// <summary>
        /// Resets the keepalive counter.
        /// </summary>
        private void ResetKeepaliveCount()
        {
            m_keepAliveCounter = 0;

            lock (DiagnosticsWriteLock)
            {
                m_diagnostics.CurrentKeepAliveCount = 0;
            }
        }

        /// <summary>
        /// Resets the lifetime count.
        /// </summary>
        private void ResetLifetimeCount()
        {
            m_lifetimeCounter = 0;

            lock (DiagnosticsWriteLock)
            {
                m_diagnostics.CurrentLifetimeCount = 0;
            }
        }

        /// <summary>
        /// Update the monitoring queue overflow count.
        /// </summary>
        public void QueueOverflowHandler()
        {
            lock (DiagnosticsWriteLock)
            {
                m_diagnostics.MonitoringQueueOverflowCount++;
            }
        }

        /// <summary>
        /// Removes a message from the message queue.
        /// </summary>
        public ServiceResult Acknowledge(OperationContext context, uint sequenceNumber)
        {
            lock (m_lock)
            {
                // check session.
                VerifySession(context);

                // clear lifetime counter.
                ResetLifetimeCount();

                // find message in queue.
                for (int ii = 0; ii < m_sentMessages.Count; ii++)
                {
                    if (m_sentMessages[ii].SequenceNumber == sequenceNumber)
                    {
                        if (m_lastSentMessage > ii)
                        {
                            m_lastSentMessage--;
                        }

                        m_sentMessages.RemoveAt(ii);
                        return null;
                    }
                }

                if (sequenceNumber == 0)
                {
                    return StatusCodes.BadSequenceNumberInvalid;
                }

                // TraceState(LogLevel.Trace, TraceStateId.Items, "ACK " + sequenceNumber.ToString());

                // message not found.
                return StatusCodes.BadSequenceNumberUnknown;
            }
        }

        /// <summary>
        /// Returns all available notifications.
        /// </summary>
        public NotificationMessage Publish(
            OperationContext context,
            out UInt32Collection availableSequenceNumbers,
            out bool moreNotifications)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            NotificationMessage message = null;

            lock (m_lock)
            {
                moreNotifications = false;
                availableSequenceNumbers = null;

                // check if expired.
                if (m_expired)
                {
                    return null;
                }

                try
                {
                    // update diagnostics.
                    lock (DiagnosticsWriteLock)
                    {
                        m_diagnostics.PublishRequestCount++;
                    }

                    message = InnerPublish(context, out availableSequenceNumbers, out moreNotifications);

                    lock (DiagnosticsWriteLock)
                    {
                        m_diagnostics.UnacknowledgedMessageCount = (uint)availableSequenceNumbers.Count;
                    }
                }
                finally
                {
                    // clear counters on success.
                    if (message != null)
                    {
                        // TraceState(LogLevel.Trace, TraceStateId.Items, Utils.Format("PUBLISH #{0}", message.SequenceNumber));
                        ResetKeepaliveCount();
                        m_waitingForPublish = moreNotifications;
                        ResetLifetimeCount();
                    }
                }
            }

            return message;
        }

        /// <summary>
        /// Publishes a timeout status message.
        /// </summary>
        public NotificationMessage PublishTimeout()
        {
            NotificationMessage message = null;

            lock (m_lock)
            {
                m_expired = true;

                message = new NotificationMessage {
                    SequenceNumber = (uint)m_sequenceNumber,
                    PublishTime = DateTime.UtcNow
                };

                Utils.IncrementIdentifier(ref m_sequenceNumber);

                lock (DiagnosticsWriteLock)
                {
                    m_diagnostics.NextSequenceNumber = (uint)m_sequenceNumber;
                }

                StatusChangeNotification notification = new StatusChangeNotification {
                    Status = StatusCodes.BadTimeout
                };
                message.NotificationData.Add(new ExtensionObject(notification));
            }

            return message;
        }

        /// <summary>
        /// Publishes a SubscriptionTransferred status message.
        /// </summary>
        public NotificationMessage SubscriptionTransferred()
        {
            NotificationMessage message = null;

            lock (m_lock)
            {
                message = new NotificationMessage {
                    SequenceNumber = (uint)m_sequenceNumber,
                    PublishTime = DateTime.UtcNow
                };

                Utils.IncrementIdentifier(ref m_sequenceNumber);

                lock (DiagnosticsWriteLock)
                {
                    m_diagnostics.NextSequenceNumber = (uint)m_sequenceNumber;
                }

                var notification = new StatusChangeNotification {
                    Status = StatusCodes.GoodSubscriptionTransferred
                };
                message.NotificationData.Add(new ExtensionObject(notification));
            }

            return message;
        }


        /// <summary>
        /// Returns all available notifications.
        /// </summary>
        private NotificationMessage InnerPublish(
            OperationContext context,
            out UInt32Collection availableSequenceNumbers,
            out bool moreNotifications)
        {
            // check session.
            VerifySession(context);

            // TraceState(LogLevel.Trace, TraceStateId.Items, "PUBLISH");

            // check if a keep alive should be sent if there is no data.
            bool keepAliveIfNoData = m_keepAliveCounter >= m_maxKeepAliveCount;

            availableSequenceNumbers = new UInt32Collection();

            moreNotifications = false;

            if (m_lastSentMessage < m_sentMessages.Count)
            {
                // return the available sequence numbers.
                for (int ii = 0; ii <= m_lastSentMessage && ii < m_sentMessages.Count; ii++)
                {
                    availableSequenceNumbers.Add(m_sentMessages[ii].SequenceNumber);
                }

                moreNotifications = m_waitingForPublish = m_lastSentMessage < m_sentMessages.Count - 1;

                // TraceState(LogLevel.Trace, TraceStateId.Items, "PUBLISH QUEUED MESSAGE");
                return m_sentMessages[m_lastSentMessage++];
            }

            List<NotificationMessage> messages = new List<NotificationMessage>();

            if (m_publishingEnabled)
            {
                DateTime start1 = DateTime.UtcNow;

                // collect notifications to publish.
                Queue<EventFieldList> events = new Queue<EventFieldList>();
                Queue<MonitoredItemNotification> datachanges = new Queue<MonitoredItemNotification>();
                Queue<DiagnosticInfo> datachangeDiagnostics = new Queue<DiagnosticInfo>();

                // check for monitored items that are ready to publish.
                LinkedListNode<IMonitoredItem> current = m_itemsToPublish.First;

                //Limit the amount of values a monitored item publishes at once
                uint maxNotificationsPerMonitoredItem = m_maxNotificationsPerPublish == 0 ? uint.MaxValue : m_maxNotificationsPerPublish * 3;

                while (current != null)
                {
                    LinkedListNode<IMonitoredItem> next = current.Next;
                    IMonitoredItem monitoredItem = current.Value;
                    bool hasMoreValuesToPublish;

                    if ((monitoredItem.MonitoredItemType & MonitoredItemTypeMask.DataChange) != 0)
                    {
                        hasMoreValuesToPublish = ((IDataChangeMonitoredItem)monitoredItem).Publish(context, datachanges, datachangeDiagnostics, maxNotificationsPerMonitoredItem);
                    }
                    else
                    {
                        hasMoreValuesToPublish = ((IEventMonitoredItem)monitoredItem).Publish(context, events, maxNotificationsPerMonitoredItem);
                    }

                    // if item has more values to publish leave it at the front of the list
                    // to execute publish in next cycle, no checking needed
                    // if no more values to publish are left add it to m_itemsToCheck
                    // to check status on next publish cylce
                    if (!hasMoreValuesToPublish)
                    {
                        m_itemsToPublish.Remove(current);
                        m_itemsToCheck.AddLast(current);
                    }

                    // check there are enough notifications for a message.
                    if (m_maxNotificationsPerPublish > 0 && events.Count + datachanges.Count > m_maxNotificationsPerPublish)
                    {
                        // construct message.
                        int notificationCount;
                        int eventCount = events.Count;
                        int dataChangeCount = datachanges.Count;

                        NotificationMessage message = ConstructMessage(
                             events,
                             datachanges,
                             datachangeDiagnostics,
                             out notificationCount);

                        // add to list of messages to send.
                        messages.Add(message);

                        lock (DiagnosticsWriteLock)
                        {
                            m_diagnostics.DataChangeNotificationsCount += (uint)(dataChangeCount - datachanges.Count);
                            m_diagnostics.EventNotificationsCount += (uint)(eventCount - events.Count);
                            m_diagnostics.NotificationsCount += (uint)notificationCount;
                        }

                        //stop fetching messages from MIs when message queue is full to avoid discards
                        // use m_maxMessageCount - 2 to put remaining values into the last allowed message (each MI is allowed to publish 3 up to messages at once)
                        if (messages.Count >= m_maxMessageCount - 2)
                        {
                            break;
                        }
                    }

                    current = next;
                }

                // publish the remaining notifications.
                while (events.Count + datachanges.Count > 0)
                {
                    // construct message.
                    int notificationCount;
                    int eventCount = events.Count;
                    int dataChangeCount = datachanges.Count;

                    NotificationMessage message = ConstructMessage(
                        events,
                        datachanges,
                        datachangeDiagnostics,
                        out notificationCount);

                    // add to list of messages to send.
                    messages.Add(message);

                    lock (DiagnosticsWriteLock)
                    {
                        m_diagnostics.DataChangeNotificationsCount += (uint)(dataChangeCount - datachanges.Count);
                        m_diagnostics.EventNotificationsCount += (uint)(eventCount - events.Count);
                        m_diagnostics.NotificationsCount += (uint)notificationCount;
                    }
                }

                // check for missing notifications.
                if (!keepAliveIfNoData && messages.Count == 0)
                {
                    Utils.LogError("Oops! MonitoredItems queued but no notifications available.");

                    m_waitingForPublish = false;

                    return null;
                }

                DateTime end1 = DateTime.UtcNow;

                double delta1 = ((double)(end1.Ticks - start1.Ticks)) / TimeSpan.TicksPerMillisecond;

                if (delta1 > 200)
                {
                    TraceState(LogLevel.Trace, TraceStateId.Publish, Utils.Format("PUBLISHING DELAY ({0}ms)", delta1));
                }
            }

            if (messages.Count == 0)
            {
                // create a keep alive message.
                NotificationMessage message = new NotificationMessage();

                // use the sequence number for the next message.
                message.SequenceNumber = (uint)m_sequenceNumber;
                message.PublishTime = DateTime.UtcNow;

                // return the available sequence numbers.
                for (int ii = 0; ii <= m_lastSentMessage && ii < m_sentMessages.Count; ii++)
                {
                    availableSequenceNumbers.Add(m_sentMessages[ii].SequenceNumber);
                }

                // TraceState(LogLevel.Trace, TraceStateId.Items, "PUBLISH KEEPALIVE");
                return message;
            }

            // have to drop unsent messages if out of queue space.
            int overflowCount = messages.Count - (int)m_maxMessageCount;
            if (overflowCount > 0)
            {
                Utils.LogWarning(
                    "WARNING: QUEUE OVERFLOW. Dropping {0} Messages. Increase MaxMessageQueueSize. SubId={1}, MaxMessageQueueSize={2}",
                    overflowCount, m_id, m_maxMessageCount);
                messages.RemoveRange(0, overflowCount);
            }

            // remove old messages if queue is full.
            if (m_sentMessages.Count > m_maxMessageCount - messages.Count)
            {
                lock (DiagnosticsWriteLock)
                {
                    m_diagnostics.UnacknowledgedMessageCount += (uint)messages.Count;
                }

                if (m_maxMessageCount <= messages.Count)
                {
                    m_sentMessages.Clear();
                }
                else
                {
                    m_sentMessages.RemoveRange(0, messages.Count);
                }
            }

            // save new message
            m_lastSentMessage = m_sentMessages.Count;
            m_sentMessages.AddRange(messages);

            // check if there are more notifications to send.
            moreNotifications = m_waitingForPublish = messages.Count > 1;

            // return the available sequence numbers.
            for (int ii = 0; ii <= m_lastSentMessage && ii < m_sentMessages.Count; ii++)
            {
                availableSequenceNumbers.Add(m_sentMessages[ii].SequenceNumber);
            }

            // TraceState(LogLevel.Trace, TraceStateId.Items, "PUBLISH NEW MESSAGE");
            return m_sentMessages[m_lastSentMessage++];
        }

        /// <summary>
        /// Returns the available sequence numbers for retransmission
        /// For example used in Transfer Subscription
        /// </summary>
        public UInt32Collection AvailableSequenceNumbersForRetransmission()
        {
            var availableSequenceNumbers = new UInt32Collection();
            // Assumption we do not check lastSentMessage < sentMessages.Count because
            // in case of subscription transfer original client might have crashed by handling message,
            // therefor new client should have to chance to process all available messages
            for (int ii = 0; ii < m_sentMessages.Count; ii++)
            {
                availableSequenceNumbers.Add(m_sentMessages[ii].SequenceNumber);
            }
            return availableSequenceNumbers;
        }

        /// <summary>
        /// Construct a message from the queues.
        /// </summary>
        private NotificationMessage ConstructMessage(
            Queue<EventFieldList> events,
            Queue<MonitoredItemNotification> datachanges,
            Queue<DiagnosticInfo> datachangeDiagnostics,
            out int notificationCount)
        {
            notificationCount = 0;

            NotificationMessage message = new NotificationMessage();

            message.SequenceNumber = (uint)m_sequenceNumber;
            message.PublishTime = DateTime.UtcNow;

            Utils.IncrementIdentifier(ref m_sequenceNumber);

            lock (DiagnosticsWriteLock)
            {
                m_diagnostics.NextSequenceNumber = (uint)m_sequenceNumber;
            }

            // add events.
            if (events.Count > 0 && notificationCount < m_maxNotificationsPerPublish)
            {
                EventNotificationList notification = new EventNotificationList();

                while (events.Count > 0 && notificationCount < m_maxNotificationsPerPublish)
                {
                    notification.Events.Add(events.Dequeue());
                    notificationCount++;
                }

                message.NotificationData.Add(new ExtensionObject(notification));
            }

            // add datachanges (space permitting).
            if (datachanges.Count > 0 && notificationCount < m_maxNotificationsPerPublish)
            {
                bool diagnosticsExist = false;
                DataChangeNotification notification = new DataChangeNotification();

                notification.MonitoredItems = new MonitoredItemNotificationCollection(datachanges.Count);
                notification.DiagnosticInfos = new DiagnosticInfoCollection(datachanges.Count);

                while (datachanges.Count > 0 && notificationCount < m_maxNotificationsPerPublish)
                {
                    MonitoredItemNotification datachange = datachanges.Dequeue();
                    notification.MonitoredItems.Add(datachange);

                    DiagnosticInfo diagnosticInfo = datachangeDiagnostics.Dequeue();

                    if (diagnosticInfo != null)
                    {
                        diagnosticsExist = true;
                    }

                    notification.DiagnosticInfos.Add(diagnosticInfo);

                    notificationCount++;
                }

                // clear diagnostics if not used.
                if (!diagnosticsExist)
                {
                    notification.DiagnosticInfos.Clear();
                }

                message.NotificationData.Add(new ExtensionObject(notification));
            }

            return message;
        }

        /// <summary>
        /// Returns a cached notification message.
        /// </summary>
        public NotificationMessage Republish(
            OperationContext context,
            uint retransmitSequenceNumber)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            lock (DiagnosticsWriteLock)
            {
                m_diagnostics.RepublishMessageRequestCount++;
            }

            lock (m_lock)
            {
                // check session.
                VerifySession(context);

                // clear lifetime counter.
                ResetLifetimeCount();

                lock (DiagnosticsWriteLock)
                {
                    m_diagnostics.RepublishRequestCount++;
                    m_diagnostics.RepublishMessageRequestCount++;
                }

                // find message.
                foreach (NotificationMessage sentMessage in m_sentMessages)
                {
                    if (sentMessage.SequenceNumber == retransmitSequenceNumber)
                    {
                        lock (DiagnosticsWriteLock)
                        {
                            m_diagnostics.RepublishMessageCount++;
                        }

                        return sentMessage;
                    }
                }

                // message not available.
                throw new ServiceResultException(StatusCodes.BadMessageNotAvailable);
            }
        }

        /// <summary>
        /// Updates the publishing parameters for the subscription.
        /// </summary>
        public void Modify(
            OperationContext context,
            double publishingInterval,
            uint maxLifetimeCount,
            uint maxKeepAliveCount,
            uint maxNotificationsPerPublish,
            byte priority)
        {
            lock (m_lock)
            {
                // check session.
                VerifySession(context);

                // clear lifetime counter.
                ResetLifetimeCount();

                m_maxLifetimeCount = maxLifetimeCount;

                // update publishing interval.
                if (publishingInterval != m_publishingInterval)
                {
                    m_publishingInterval = publishingInterval;
                    m_publishTimerExpiry = HiResClock.TickCount64 + (long)publishingInterval;
                    ResetKeepaliveCount();
                }

                // update keep alive count.
                if (maxKeepAliveCount != m_maxKeepAliveCount)
                {
                    m_maxKeepAliveCount = maxKeepAliveCount;
                }

                m_maxNotificationsPerPublish = maxNotificationsPerPublish;

                // update priority.
                m_priority = priority;

                // update diagnostics
                lock (DiagnosticsWriteLock)
                {
                    m_diagnostics.ModifyCount++;
                    m_diagnostics.PublishingInterval = m_publishingInterval;
                    m_diagnostics.MaxKeepAliveCount = m_maxKeepAliveCount;
                    m_diagnostics.MaxLifetimeCount = m_maxLifetimeCount;
                    m_diagnostics.Priority = m_priority;
                    m_diagnostics.MaxNotificationsPerPublish = m_maxNotificationsPerPublish;
                }

                TraceState(LogLevel.Information, TraceStateId.Config, "MODIFIED");
            }
        }

        /// <summary>
        /// Enables/disables publishing for the subscription.
        /// </summary>
        public void SetPublishingMode(
            OperationContext context,
            bool publishingEnabled)
        {
            lock (m_lock)
            {
                // check session.
                VerifySession(context);

                // clear lifetime counter.
                ResetLifetimeCount();

                // update publishing interval.
                if (publishingEnabled != m_publishingEnabled)
                {
                    m_publishingEnabled = publishingEnabled;

                    // update diagnostics
                    lock (DiagnosticsWriteLock)
                    {
                        m_diagnostics.PublishingEnabled = m_publishingEnabled;

                        if (m_publishingEnabled)
                        {
                            m_diagnostics.EnableCount++;
                        }
                        else
                        {
                            m_diagnostics.DisableCount++;
                        }
                    }
                }

                TraceState(LogLevel.Information, TraceStateId.Config, (publishingEnabled) ? "ENABLED" : "DISABLED");
            }
        }

        /// <summary>
        /// Updates the triggers for the monitored item.
        /// </summary>
        public void SetTriggering(
            OperationContext context,
            uint triggeringItemId,
            UInt32Collection linksToAdd,
            UInt32Collection linksToRemove,
            out StatusCodeCollection addResults,
            out DiagnosticInfoCollection addDiagnosticInfos,
            out StatusCodeCollection removeResults,
            out DiagnosticInfoCollection removeDiagnosticInfos)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (linksToAdd == null) throw new ArgumentNullException(nameof(linksToAdd));
            if (linksToRemove == null) throw new ArgumentNullException(nameof(linksToRemove));

            // allocate results.
            bool diagnosticsExist = false;
            addResults = new StatusCodeCollection();
            addDiagnosticInfos = null;
            removeResults = new StatusCodeCollection();
            removeDiagnosticInfos = null;

            if ((context.DiagnosticsMask & DiagnosticsMasks.OperationAll) != 0)
            {
                addDiagnosticInfos = new DiagnosticInfoCollection();
                removeDiagnosticInfos = new DiagnosticInfoCollection();
            }

            // build list of items to modify.
            lock (m_lock)
            {
                // check session.
                VerifySession(context);

                // clear lifetime counter.
                ResetLifetimeCount();

                // look up triggering item.
                LinkedListNode<IMonitoredItem> triggerNode = null;

                if (!m_monitoredItems.TryGetValue(triggeringItemId, out triggerNode))
                {
                    throw new ServiceResultException(StatusCodes.BadMonitoredItemIdInvalid);
                }

                // lookup existing list.
                List<ITriggeredMonitoredItem> triggeredItems = null;

                if (!m_itemsToTrigger.TryGetValue(triggeringItemId, out triggeredItems))
                {
                    m_itemsToTrigger[triggeringItemId] = triggeredItems = new List<ITriggeredMonitoredItem>();
                }

                // remove old links.
                for (int ii = 0; ii < linksToRemove.Count; ii++)
                {
                    removeResults.Add(StatusCodes.Good);

                    bool found = false;

                    for (int jj = 0; jj < triggeredItems.Count; jj++)
                    {
                        if (triggeredItems[jj].Id == linksToRemove[ii])
                        {
                            found = true;
                            triggeredItems.RemoveAt(jj);
                            break;
                        }
                    }

                    if (!found)
                    {
                        removeResults[ii] = StatusCodes.BadMonitoredItemIdInvalid;

                        // update diagnostics.
                        if ((context.DiagnosticsMask & DiagnosticsMasks.OperationAll) != 0)
                        {
                            DiagnosticInfo diagnosticInfo = ServerUtils.CreateDiagnosticInfo(m_server, context, removeResults[ii]);
                            diagnosticsExist = true;
                            removeDiagnosticInfos.Add(diagnosticInfo);
                        }

                        continue;
                    }

                    // update diagnostics.
                    if ((context.DiagnosticsMask & DiagnosticsMasks.OperationAll) != 0)
                    {
                        removeDiagnosticInfos.Add(null);
                    }
                }

                // add new links.
                for (int ii = 0; ii < linksToAdd.Count; ii++)
                {
                    addResults.Add(StatusCodes.Good);

                    LinkedListNode<IMonitoredItem> node = null;

                    if (!m_monitoredItems.TryGetValue(linksToAdd[ii], out node))
                    {
                        addResults[ii] = StatusCodes.BadMonitoredItemIdInvalid;

                        // update diagnostics.
                        if ((context.DiagnosticsMask & DiagnosticsMasks.OperationAll) != 0)
                        {
                            DiagnosticInfo diagnosticInfo = ServerUtils.CreateDiagnosticInfo(m_server, context, addResults[ii]);
                            diagnosticsExist = true;
                            addDiagnosticInfos.Add(diagnosticInfo);
                        }

                        continue;
                    }

                    // check if triggering interface is supported.
                    ITriggeredMonitoredItem triggeredItem = node.Value as ITriggeredMonitoredItem;

                    if (triggeredItem == null)
                    {
                        addResults[ii] = StatusCodes.BadNotSupported;

                        // update diagnostics.
                        if ((context.DiagnosticsMask & DiagnosticsMasks.OperationAll) != 0)
                        {
                            DiagnosticInfo diagnosticInfo = ServerUtils.CreateDiagnosticInfo(m_server, context, addResults[ii]);
                            diagnosticsExist = true;
                            addDiagnosticInfos.Add(diagnosticInfo);
                        }

                        continue;
                    }

                    // add value if not already in list.
                    bool found = false;

                    for (int jj = 0; jj < triggeredItems.Count; jj++)
                    {
                        if (triggeredItems[jj].Id == triggeredItem.Id)
                        {
                            found = true;
                            break;
                        }
                    }

                    if (!found)
                    {
                        triggeredItems.Add(triggeredItem);
                    }

                    // update diagnostics.
                    if ((context.DiagnosticsMask & DiagnosticsMasks.OperationAll) != 0)
                    {
                        addDiagnosticInfos.Add(null);
                    }
                }

                // remove an empty list.
                if (triggeredItems.Count == 0)
                {
                    m_itemsToTrigger.Remove(triggeringItemId);
                }

                // clear diagnostics if not required.
                if (!diagnosticsExist)
                {
                    if (addDiagnosticInfos != null) addDiagnosticInfos.Clear();
                    if (removeDiagnosticInfos != null) removeDiagnosticInfos.Clear();
                }
            }
        }

        /// <summary>
        /// Adds monitored items to a subscription.
        /// </summary>
        public void CreateMonitoredItems(
            OperationContext context,
            TimestampsToReturn timestampsToReturn,
            MonitoredItemCreateRequestCollection itemsToCreate,
            out MonitoredItemCreateResultCollection results,
            out DiagnosticInfoCollection diagnosticInfos)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (itemsToCreate == null) throw new ArgumentNullException(nameof(itemsToCreate));

            int count = itemsToCreate.Count;

            lock (m_lock)
            {
                // check session.
                VerifySession(context);

                // clear lifetime counter.
                ResetLifetimeCount();
            }

            // create the monitored items.
            List<IMonitoredItem> monitoredItems = new List<IMonitoredItem>(count);
            List<ServiceResult> errors = new List<ServiceResult>(count);
            List<MonitoringFilterResult> filterResults = new List<MonitoringFilterResult>(count);

            for (int ii = 0; ii < count; ii++)
            {
                monitoredItems.Add(null);
                errors.Add(null);
                filterResults.Add(null);
            }

            m_server.NodeManager.CreateMonitoredItems(
            context,
            this.m_id,
            m_publishingInterval,
            timestampsToReturn,
            itemsToCreate,
            errors,
            filterResults,
            monitoredItems,
            m_isDurable);

            // allocate results.
            bool diagnosticsExist = false;
            results = new MonitoredItemCreateResultCollection(count);
            diagnosticInfos = null;

            if ((context.DiagnosticsMask & DiagnosticsMasks.OperationAll) != 0)
            {
                diagnosticInfos = new DiagnosticInfoCollection(count);
            }

            lock (m_lock)
            {
                // check session again after CreateMonitoredItems.
                VerifySession(context);

                for (int ii = 0; ii < errors.Count; ii++)
                {
                    // update results.
                    MonitoredItemCreateResult result = null;

                    if (ServiceResult.IsBad(errors[ii]))
                    {
                        result = new MonitoredItemCreateResult();
                        result.StatusCode = errors[ii].Code;

                        if (filterResults[ii] != null)
                        {
                            result.FilterResult = new ExtensionObject(filterResults[ii]);
                        }
                    }
                    else
                    {
                        IMonitoredItem monitoredItem = monitoredItems[ii];

                        if (monitoredItem != null)
                        {
                            monitoredItem.SubscriptionCallback = this;

                            LinkedListNode<IMonitoredItem> node = m_itemsToCheck.AddLast(monitoredItem);
                            m_monitoredItems.Add(monitoredItem.Id, node);

                            errors[ii] = monitoredItem.GetCreateResult(out result);

                            // update sampling interval diagnostics.
                            AddItemToSamplingInterval(result.RevisedSamplingInterval, itemsToCreate[ii].MonitoringMode);
                        }
                    }

                    results.Add(result);

                    // update diagnostics.
                    if ((context.DiagnosticsMask & DiagnosticsMasks.OperationAll) != 0)
                    {
                        DiagnosticInfo diagnosticInfo = null;

                        if (errors[ii] != null && errors[ii].Code != StatusCodes.Good)
                        {
                            diagnosticInfo = ServerUtils.CreateDiagnosticInfo(m_server, context, errors[ii]);
                            diagnosticsExist = true;
                        }

                        diagnosticInfos.Add(diagnosticInfo);
                    }
                }

                // clear diagnostics if not required.
                if (!diagnosticsExist && diagnosticInfos != null)
                {
                    diagnosticInfos.Clear();
                }

                TraceState(LogLevel.Information, TraceStateId.Items, "ITEMS CREATED");
            }
        }

        /// <summary>
        /// Adds an item to the sampling interval.
        /// </summary>
        private void AddItemToSamplingInterval(
            double samplingInterval,
            MonitoringMode monitoringMode)
        {
            // update diagnostics
            lock (DiagnosticsWriteLock)
            {
                if (monitoringMode == MonitoringMode.Disabled)
                {
                    m_diagnostics.DisabledMonitoredItemCount++;
                }
                m_diagnostics.MonitoredItemCount++;
            }
        }

        /// <summary>
        /// Adds an item to the sampling interval.
        /// </summary>
        private void ModifyItemSamplingInterval(
            double oldInterval,
            double newInterval,
            MonitoringMode monitoringMode)
        {
            // TBD
        }

        /// <summary>
        /// Removes an item from the sampling interval.
        /// </summary>
        private void RemoveItemToSamplingInterval(
            double samplingInterval,
            MonitoringMode monitoringMode)
        {
            // update diagnostics
            lock (DiagnosticsWriteLock)
            {
                if (monitoringMode == MonitoringMode.Disabled)
                {
                    m_diagnostics.DisabledMonitoredItemCount--;
                }
                m_diagnostics.MonitoredItemCount--;
            }
        }

        /// <summary>
        /// Changes the monitoring mode for an item.
        /// </summary>
        private void ModifyItemMonitoringMode(
            double samplingInterval,
            MonitoringMode oldMode,
            MonitoringMode newMode)
        {
            if (newMode != oldMode)
            {
                // update diagnostics
                lock (DiagnosticsWriteLock)
                {
                    if (newMode == MonitoringMode.Disabled)
                    {
                        m_diagnostics.DisabledMonitoredItemCount++;
                    }
                    else
                    {
                        m_diagnostics.DisabledMonitoredItemCount--;
                    }
                }
            }
        }

        /// <summary>
        /// Modifies monitored items in a subscription.
        /// </summary>
        public void ModifyMonitoredItems(
            OperationContext context,
            TimestampsToReturn timestampsToReturn,
            MonitoredItemModifyRequestCollection itemsToModify,
            out MonitoredItemModifyResultCollection results,
            out DiagnosticInfoCollection diagnosticInfos)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (itemsToModify == null) throw new ArgumentNullException(nameof(itemsToModify));

            int count = itemsToModify.Count;

            // allocate results.
            bool diagnosticsExist = false;
            results = new MonitoredItemModifyResultCollection(count);
            diagnosticInfos = null;

            if ((context.DiagnosticsMask & DiagnosticsMasks.OperationAll) != 0)
            {
                diagnosticInfos = new DiagnosticInfoCollection(count);
            }

            // build list of items to modify.
            List<IMonitoredItem> monitoredItems = new List<IMonitoredItem>(count);
            List<ServiceResult> errors = new List<ServiceResult>(count);
            List<MonitoringFilterResult> filterResults = new List<MonitoringFilterResult>(count);
            double[] originalSamplingIntervals = new double[count];

            bool validItems = false;

            lock (m_lock)
            {
                // check session.
                VerifySession(context);

                // clear lifetime counter.
                ResetLifetimeCount();

                for (int ii = 0; ii < count; ii++)
                {
                    filterResults.Add(null);

                    LinkedListNode<IMonitoredItem> node = null;

                    if (!m_monitoredItems.TryGetValue(itemsToModify[ii].MonitoredItemId, out node))
                    {
                        monitoredItems.Add(null);
                        errors.Add(StatusCodes.BadMonitoredItemIdInvalid);

                        // update diagnostics.
                        if ((context.DiagnosticsMask & DiagnosticsMasks.OperationAll) != 0)
                        {
                            DiagnosticInfo diagnosticInfo = ServerUtils.CreateDiagnosticInfo(m_server, context, errors[ii]);
                            diagnosticsExist = true;
                            diagnosticInfos.Add(diagnosticInfo);
                        }

                        continue;
                    }

                    IMonitoredItem monitoredItem = node.Value;
                    monitoredItems.Add(monitoredItem);
                    originalSamplingIntervals[ii] = monitoredItem.SamplingInterval;

                    errors.Add(null);
                    validItems = true;

                    // update diagnostics.
                    if ((context.DiagnosticsMask & DiagnosticsMasks.OperationAll) != 0)
                    {
                        diagnosticInfos.Add(null);
                    }
                }
            }

            // update items.
            if (validItems)
            {
                m_server.NodeManager.ModifyMonitoredItems(
                    context,
                    timestampsToReturn,
                    monitoredItems,
                    itemsToModify,
                    errors,
                    filterResults);
            }

            lock (m_lock)
            {
                // create results.
                for (int ii = 0; ii < errors.Count; ii++)
                {
                    ServiceResult error = errors[ii];

                    MonitoredItemModifyResult result = null;

                    if (ServiceResult.IsGood(error))
                    {
                        error = monitoredItems[ii].GetModifyResult(out result);
                    }

                    if (result == null)
                    {
                        result = new MonitoredItemModifyResult();
                    }

                    if (error == null)
                    {
                        result.StatusCode = StatusCodes.Good;
                    }
                    else
                    {
                        result.StatusCode = error.StatusCode;
                    }

                    // update diagnostics.
                    if (ServiceResult.IsGood(error))
                    {
                        ModifyItemSamplingInterval(originalSamplingIntervals[ii], result.RevisedSamplingInterval, monitoredItems[ii].MonitoringMode);
                    }

                    if (filterResults[ii] != null)
                    {
                        result.FilterResult = new ExtensionObject(filterResults[ii]);
                    }

                    results.Add(result);

                    if ((context.DiagnosticsMask & DiagnosticsMasks.OperationAll) != 0)
                    {
                        if (error != null && error.Code != StatusCodes.Good)
                        {
                            diagnosticInfos[ii] = ServerUtils.CreateDiagnosticInfo(m_server, context, error);
                            diagnosticsExist = true;
                        }
                    }
                }

                // clear diagnostics if not required.
                if (!diagnosticsExist && diagnosticInfos != null)
                {
                    diagnosticInfos.Clear();
                }

                TraceState(LogLevel.Information, TraceStateId.Items, "ITEMS MODIFIED");
            }
        }

        /// <summary>
        /// Deletes the monitored items in a subscription.
        /// </summary>
        public void DeleteMonitoredItems(
            OperationContext context,
            UInt32Collection monitoredItemIds,
            out StatusCodeCollection results,
            out DiagnosticInfoCollection diagnosticInfos)
        {
            DeleteMonitoredItems(context, monitoredItemIds, false, out results, out diagnosticInfos);
        }

        /// <summary>
        /// Deletes the monitored items in a subscription.
        /// </summary>
        private void DeleteMonitoredItems(
            OperationContext context,
            UInt32Collection monitoredItemIds,
            bool doNotCheckSession,
            out StatusCodeCollection results,
            out DiagnosticInfoCollection diagnosticInfos)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (monitoredItemIds == null) throw new ArgumentNullException(nameof(monitoredItemIds));

            int count = monitoredItemIds.Count;

            bool diagnosticsExist = false;
            results = new StatusCodeCollection(count);
            diagnosticInfos = null;

            if ((context.DiagnosticsMask & DiagnosticsMasks.OperationAll) != 0)
            {
                diagnosticInfos = new DiagnosticInfoCollection(count);
            }

            // build list of items to modify.
            List<IMonitoredItem> monitoredItems = new List<IMonitoredItem>(count);
            List<ServiceResult> errors = new List<ServiceResult>(count);
            double[] originalSamplingIntervals = new double[count];
            MonitoringMode[] originalMonitoringModes = new MonitoringMode[count];

            bool validItems = false;

            lock (m_lock)
            {
                // check session.
                if (!doNotCheckSession)
                {
                    VerifySession(context);
                }

                // clear lifetime counter.
                ResetLifetimeCount();

                for (int ii = 0; ii < count; ii++)
                {
                    LinkedListNode<IMonitoredItem> node = null;

                    if (!m_monitoredItems.TryGetValue(monitoredItemIds[ii], out node))
                    {
                        monitoredItems.Add(null);
                        errors.Add(StatusCodes.BadMonitoredItemIdInvalid);

                        // update diagnostics.
                        if ((context.DiagnosticsMask & DiagnosticsMasks.OperationAll) != 0)
                        {
                            DiagnosticInfo diagnosticInfo = ServerUtils.CreateDiagnosticInfo(m_server, context, errors[ii]);
                            diagnosticsExist = true;
                            diagnosticInfos.Add(diagnosticInfo);
                        }

                        continue;
                    }

                    IMonitoredItem monitoredItem = node.Value;
                    monitoredItems.Add(monitoredItem);

                    // remove the item from the internal lists.
                    m_monitoredItems.Remove(monitoredItemIds[ii]);
                    m_itemsToTrigger.Remove(monitoredItemIds[ii]);

                    //remove the links towards the deleted monitored item
                    List<ITriggeredMonitoredItem> triggeredItems = null;
                    foreach (KeyValuePair<uint, List<ITriggeredMonitoredItem>> item in m_itemsToTrigger)
                    {
                        triggeredItems = item.Value;
                        for (int jj = 0; jj < triggeredItems.Count; jj++)
                        {
                            if (triggeredItems[jj].Id == monitoredItemIds[ii])
                            {
                                triggeredItems.RemoveAt(jj);
                                break;
                            }
                        }
                    }

                    if (node.List != null)
                    {
                        node.List.Remove(node);
                    }

                    originalSamplingIntervals[ii] = monitoredItem.SamplingInterval;
                    originalMonitoringModes[ii] = monitoredItem.MonitoringMode;

                    errors.Add(null);
                    validItems = true;

                    // update diagnostics.
                    if ((context.DiagnosticsMask & DiagnosticsMasks.OperationAll) != 0)
                    {
                        diagnosticInfos.Add(null);
                    }
                }
            }

            // update items.
            if (validItems)
            {
                m_server.NodeManager.DeleteMonitoredItems(
                    context,
                    m_id,
                    monitoredItems,
                    errors);
            }

            //dispose monitored Items
            foreach (IMonitoredItem monitoredItem in monitoredItems)
            {
                monitoredItem?.Dispose();
            }

            lock (m_lock)
            {
                // update diagnostics.
                for (int ii = 0; ii < errors.Count; ii++)
                {
                    ServiceResult error = errors[ii];

                    if (error == null)
                    {
                        results.Add(StatusCodes.Good);
                    }
                    else
                    {
                        results.Add(error.StatusCode);
                    }

                    // update diagnostics.
                    if (ServiceResult.IsGood(error))
                    {
                        RemoveItemToSamplingInterval(originalSamplingIntervals[ii], originalMonitoringModes[ii]);
                    }

                    if ((context.DiagnosticsMask & DiagnosticsMasks.OperationAll) != 0)
                    {
                        if (error != null && error.Code != StatusCodes.Good)
                        {
                            diagnosticInfos[ii] = ServerUtils.CreateDiagnosticInfo(m_server, context, error);
                            diagnosticsExist = true;
                        }
                    }
                }

                // clear diagnostics if not required.
                if (!diagnosticsExist && diagnosticInfos != null)
                {
                    diagnosticInfos.Clear();
                }

                TraceState(LogLevel.Information, TraceStateId.Items, "ITEMS DELETED");
            }
        }

        /// <summary>
        /// Changes the monitoring mode for a set of items.
        /// </summary>
        public void SetMonitoringMode(
            OperationContext context,
            MonitoringMode monitoringMode,
            UInt32Collection monitoredItemIds,
            out StatusCodeCollection results,
            out DiagnosticInfoCollection diagnosticInfos)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (monitoredItemIds == null) throw new ArgumentNullException(nameof(monitoredItemIds));

            int count = monitoredItemIds.Count;

            bool diagnosticsExist = false;
            results = new StatusCodeCollection(count);
            diagnosticInfos = null;

            if ((context.DiagnosticsMask & DiagnosticsMasks.OperationAll) != 0)
            {
                diagnosticInfos = new DiagnosticInfoCollection(count);
            }

            // build list of items to modify.
            List<IMonitoredItem> monitoredItems = new List<IMonitoredItem>(count);
            List<ServiceResult> errors = new List<ServiceResult>(count);
            MonitoringMode[] originalMonitoringModes = new MonitoringMode[count];

            bool validItems = false;

            lock (m_lock)
            {
                // check session.
                VerifySession(context);

                // clear lifetime counter.
                ResetLifetimeCount();

                for (int ii = 0; ii < count; ii++)
                {
                    LinkedListNode<IMonitoredItem> node = null;

                    if (!m_monitoredItems.TryGetValue(monitoredItemIds[ii], out node))
                    {
                        monitoredItems.Add(null);
                        errors.Add(StatusCodes.BadMonitoredItemIdInvalid);

                        // update diagnostics.
                        if ((context.DiagnosticsMask & DiagnosticsMasks.OperationAll) != 0)
                        {
                            DiagnosticInfo diagnosticInfo = ServerUtils.CreateDiagnosticInfo(m_server, context, errors[ii]);
                            diagnosticsExist = true;
                            diagnosticInfos.Add(diagnosticInfo);
                        }

                        continue;
                    }

                    IMonitoredItem monitoredItem = node.Value;
                    monitoredItems.Add(monitoredItem);
                    originalMonitoringModes[ii] = monitoredItem.MonitoringMode;

                    errors.Add(null);
                    validItems = true;

                    // update diagnostics.
                    if ((context.DiagnosticsMask & DiagnosticsMasks.OperationAll) != 0)
                    {
                        diagnosticInfos.Add(null);
                    }
                }
            }

            // update items.
            if (validItems)
            {
                m_server.NodeManager.SetMonitoringMode(
                    context,
                    monitoringMode,
                    monitoredItems,
                    errors);
            }

            lock (m_lock)
            {
                // update diagnostics.
                for (int ii = 0; ii < errors.Count; ii++)
                {
                    ServiceResult error = errors[ii];

                    if (error == null)
                    {
                        results.Add(StatusCodes.Good);
                    }
                    else
                    {
                        results.Add(error.StatusCode);
                    }

                    // update diagnostics.
                    if (ServiceResult.IsGood(error))
                    {
                        ModifyItemMonitoringMode(monitoredItems[ii].SamplingInterval, originalMonitoringModes[ii], monitoringMode);
                    }

                    if ((context.DiagnosticsMask & DiagnosticsMasks.OperationAll) != 0)
                    {
                        if (error != null && error.Code != StatusCodes.Good)
                        {
                            diagnosticInfos[ii] = ServerUtils.CreateDiagnosticInfo(m_server, context, error);
                            diagnosticsExist = true;
                        }
                    }
                }

                // clear diagnostics if not required.
                if (!diagnosticsExist && diagnosticInfos != null)
                {
                    diagnosticInfos.Clear();
                }

                if (monitoringMode == MonitoringMode.Disabled)
                {
                    TraceState(LogLevel.Information, TraceStateId.Monitor, "MONITORING DISABLED");
                }
                else if (monitoringMode == MonitoringMode.Reporting)
                {
                    TraceState(LogLevel.Information, TraceStateId.Monitor, "REPORTING");
                }
                else
                {
                    TraceState(LogLevel.Information, TraceStateId.Monitor, "SAMPLING");
                }
            }
        }

        /// <summary>
        /// Verifies that a condition refresh operation is permitted.
        /// </summary>
        public void ValidateConditionRefresh(OperationContext context)
        {
            lock (m_lock)
            {
                VerifySession(context);

                if (m_refreshInProgress)
                {
                    throw new ServiceResultException(StatusCodes.BadRefreshInProgress);
                }
            }
        }

        /// <summary>
        /// Verifies that a condition refresh operation is permitted.
        /// </summary>
        public void ValidateConditionRefresh2(OperationContext context, uint monitoredItemId)
        {
            ValidateConditionRefresh(context);

            lock (m_lock)
            {
                if (!m_monitoredItems.ContainsKey(monitoredItemId))
                {
                    throw new ServiceResultException(StatusCodes.BadMonitoredItemIdInvalid,
                        "Cannot refresh conditions for a monitored item that does not exist.");
                }
            }
        }


        /// <summary>
        /// Refreshes the conditions.
        /// </summary>
        public void ConditionRefresh()
        {
            List<IEventMonitoredItem> monitoredItems = new List<IEventMonitoredItem>();

            lock (m_lock)
            {
                // build list of items to refresh.
                foreach (LinkedListNode<IMonitoredItem> monitoredItem in m_monitoredItems.Values)
                {
                    MonitoredItem eventMonitoredItem = monitoredItem.Value as MonitoredItem;

                    if (eventMonitoredItem != null && eventMonitoredItem.EventFilter != null)
                    {
                        // add to list that gets reported to the NodeManagers.
                        monitoredItems.Add(eventMonitoredItem);
                    }
                }

                // nothing to do if no event subscriptions.
                if (monitoredItems.Count == 0)
                {
                    return;
                }
            }

            ConditionRefresh(monitoredItems, 0);
        }

        /// <summary>
        /// Refreshes the conditions.
        /// </summary>
        public void ConditionRefresh2(uint monitoredItemId)
        {
            List<IEventMonitoredItem> monitoredItems = new List<IEventMonitoredItem>();

            lock (m_lock)
            {
                // build list of items to refresh.
                if (m_monitoredItems.ContainsKey(monitoredItemId))
                {
                    LinkedListNode<IMonitoredItem> monitoredItem = m_monitoredItems[monitoredItemId];

                    MonitoredItem eventMonitoredItem = monitoredItem.Value as MonitoredItem;

                    if (eventMonitoredItem != null && eventMonitoredItem.EventFilter != null)
                    {
                        // add to list that gets reported to the NodeManagers.
                        monitoredItems.Add(eventMonitoredItem);
                    }
                }
                else
                {
                    throw new ServiceResultException(StatusCodes.BadMonitoredItemIdInvalid,
                        "Cannot refresh conditions for a monitored item that does not exist.");
                }

                // nothing to do if no event subscriptions.
                if (monitoredItems.Count == 0)
                {
                    return;
                }
            }

            ConditionRefresh(monitoredItems, monitoredItemId);
        }

        /// <summary>
        /// Refreshes the conditions.  Works for both ConditionRefresh and ConditionRefresh2
        /// </summary>
        private void ConditionRefresh(List<IEventMonitoredItem> monitoredItems, uint monitoredItemId)
        {
            ServerSystemContext systemContext = m_server.DefaultSystemContext.Copy(m_session);

            string messageTemplate = Utils.Format("Condition refresh {{0}} for subscription {0}.", m_id);
            if (monitoredItemId > 0)
            {
                messageTemplate = Utils.Format("Condition refresh {{0}} for subscription {0}, monitored item {1}.", m_id, monitoredItemId);
            }

            lock (m_lock)
            {
                // generate start event.
                RefreshStartEventState e = new RefreshStartEventState(null);

                TranslationInfo message = null;

                message = new TranslationInfo(
                    "RefreshStartEvent",
                    "en-US",
                    Utils.Format(messageTemplate, "started"));

                e.Initialize(
                    systemContext,
                    null,
                    EventSeverity.Low,
                    new LocalizedText(message));

                e.SetChildValue(systemContext, BrowseNames.SourceNode, m_diagnosticsId, false);
                e.SetChildValue(systemContext, BrowseNames.SourceName, Utils.Format("Subscription/{0}", m_id), false);
                e.SetChildValue(systemContext, BrowseNames.ReceiveTime, DateTime.UtcNow, false);

                // build list of items to refresh.
                foreach (IEventMonitoredItem monitoredItem in monitoredItems)
                {
                    MonitoredItem eventMonitoredItem = monitoredItem as MonitoredItem;

                    if (eventMonitoredItem != null && eventMonitoredItem.EventFilter != null)
                    {
                        // queue start refresh event.
                        eventMonitoredItem.QueueEvent(e, true);
                    }
                }

                // nothing to do if no event subscriptions.
                if (monitoredItems.Count == 0)
                {
                    return;
                }
            }

            // tell the NodeManagers to report the current state of the conditions.
            try
            {
                m_refreshInProgress = true;

                OperationContext operationContext = new OperationContext(m_session, DiagnosticsMasks.None);
                m_server.NodeManager.ConditionRefresh(operationContext, monitoredItems);
            }
            finally
            {
                m_refreshInProgress = false;
            }

            lock (m_lock)
            {
                // generate start event.
                RefreshEndEventState e = new RefreshEndEventState(null);

                TranslationInfo message = null;

                message = new TranslationInfo(
                    "RefreshEndEvent",
                    "en-US",
                    Utils.Format(messageTemplate, "completed"));

                e.Initialize(
                    systemContext,
                    null,
                    EventSeverity.Low,
                    new LocalizedText(message));

                e.SetChildValue(systemContext, BrowseNames.SourceNode, m_diagnosticsId, false);
                e.SetChildValue(systemContext, BrowseNames.SourceName, Utils.Format("Subscription/{0}", m_id), false);
                e.SetChildValue(systemContext, BrowseNames.ReceiveTime, DateTime.UtcNow, false);

                // send refresh end event.
                for (int ii = 0; ii < monitoredItems.Count; ii++)
                {
                    MonitoredItem monitoredItem = monitoredItems[ii] as MonitoredItem;

                    if (monitoredItem.EventFilter != null)
                    {
                        monitoredItem.QueueEvent(e, true);
                    }
                }

                // TraceState("CONDITION REFRESH");
            }
        }

        /// <summary>
        /// Sets the subscription to durable mode.
        /// </summary>
        public ServiceResult SetSubscriptionDurable(uint maxLifetimeCount)
        {
            lock (m_lock)
            {
                if (!m_supportsDurable)
                {
                    Utils.LogError("SetSubscriptionDurable requested for subscription with id {0}, but no IMonitoredItemQueueFactory that supports durable queues was registered", m_id);
                    TraceState(LogLevel.Information, TraceStateId.Config, "SetSubscriptionDurable Failed");
                    return StatusCodes.BadNotSupported;
                }

                m_isDurable = true;

                // clear lifetime counter.
                ResetLifetimeCount();

                m_maxLifetimeCount = maxLifetimeCount;


                // update diagnostics
                lock (DiagnosticsWriteLock)
                {
                    m_diagnostics.ModifyCount++;
                    m_diagnostics.MaxLifetimeCount = m_maxLifetimeCount;

                }

                TraceState(LogLevel.Information, TraceStateId.Config, "SET DURABLE");

                return StatusCodes.Good;
            }
        }

        /// <summary>
        /// Gets the monitored items for the subscription.
        /// </summary>
        public void GetMonitoredItems(out uint[] serverHandles, out uint[] clientHandles)
        {
            lock (m_lock)
            {
                serverHandles = new uint[m_monitoredItems.Count];
                clientHandles = new uint[m_monitoredItems.Count];

                int ii = 0;

                foreach (KeyValuePair<uint, LinkedListNode<IMonitoredItem>> entry in m_monitoredItems)
                {
                    serverHandles[ii] = entry.Key;
                    clientHandles[ii] = entry.Value.Value.ClientHandle;
                    ii++;
                }
            }
        }

        /// <summary>
        /// Return a StorableSubscription for restore after a server restart
        /// </summary>
        public IStoredSubscription ToStorableSubscription()
        {
            var monitoredItemsToStore = new List<IStoredMonitoredItem>();

            foreach (KeyValuePair<uint, LinkedListNode<IMonitoredItem>> kvp in m_monitoredItems)
            {
                monitoredItemsToStore.Add(kvp.Value.Value.ToStorableMonitoredItem());
            }

            return new StoredSubscription {
                SentMessages = m_sentMessages,
                Id = Id,
                SequenceNumber = m_sequenceNumber,
                LastSentMessage = m_lastSentMessage,
                LifetimeCounter = m_lifetimeCounter,
                MaxKeepaliveCount = m_maxKeepAliveCount,
                MaxLifetimeCount = m_maxLifetimeCount,
                MaxMessageCount = m_maxMessageCount,
                MaxNotificationsPerPublish = m_maxNotificationsPerPublish,
                Priority = Priority,
                PublishingInterval = PublishingInterval,
                UserIdentityToken = EffectiveIdentity?.GetIdentityToken(),
                MonitoredItems = monitoredItemsToStore,
                IsDurable = IsDurable,
            };
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Restore MonitoredItems after a Server restart
        /// </summary>
        protected virtual void RestoreMonitoredItems(IEnumerable<IStoredMonitoredItem> storedMonitoredItems)
        {
            int count = storedMonitoredItems.Count();

            // create the monitored items.
            List<IMonitoredItem> monitoredItems = new List<IMonitoredItem>(count);

            for (int ii = 0; ii < count; ii++)
            {
                monitoredItems.Add(null);
            }

            m_server.NodeManager.RestoreMonitoredItems(
                 storedMonitoredItems.ToList(),
                 monitoredItems,
                 m_savedOwnerIdentity);

            lock (m_lock)
            {
                foreach (IMonitoredItem monitoredItem in monitoredItems)
                {
                    // skip MonitoredItem if recreation failed
                    if (monitoredItem == null)
                    {
                        continue;
                    }
                    monitoredItem.SubscriptionCallback = this;

                    LinkedListNode<IMonitoredItem> node = m_itemsToCheck.AddLast(monitoredItem);
                    m_monitoredItems.Add(monitoredItem.Id, node);

                    // update sampling interval diagnostics.
                    AddItemToSamplingInterval(monitoredItem.SamplingInterval, monitoredItem.MonitoringMode);
                }

                TraceState(LogLevel.Information, TraceStateId.Items, "ITEMS RESTORED");
            }
        }
        /// <summary>
        /// Returns a copy of the current diagnostics.
        /// </summary>
        private ServiceResult OnUpdateDiagnostics(
            ISystemContext context,
            NodeState node,
            ref object value)
        {
            lock (DiagnosticsLock)
            {
                value = Utils.Clone(m_diagnostics);
            }

            return ServiceResult.Good;
        }

        /// <summary>
        /// Throws an exception if the session is not the owner.
        /// </summary>
        private void VerifySession(OperationContext context)
        {
            if (m_expired)
            {
                throw new ServiceResultException(StatusCodes.BadSubscriptionIdInvalid);
            }

            if (!Object.ReferenceEquals(context.Session, m_session))
            {
                throw new ServiceResultException(StatusCodes.BadSubscriptionIdInvalid, "Subscription belongs to a different session.");
            }
        }

        /// <summary>
        /// The states to log.
        /// </summary>
        private enum TraceStateId
        {
            Config,
            Items,
            Monitor,
            Publish,
            Deleted
        };

        /// <summary>
        /// Dumps the current state of the session queue.
        /// </summary>
        private void TraceState(LogLevel logLevel, TraceStateId id, string context)
        {
            const string deletedMessage = "Subscription {0}, SessionId={1}, Id={2}, SeqNo={3}, MessageCount={4}";
            const string configMessage = "Subscription {0}, SessionId={1}, Id={2}, Priority={3}, Publishing={4}, KeepAlive={5}, LifeTime={6}, MaxNotifications={7}, Enabled={8}";
            const string monitorMessage = "Subscription {0}, Id={1}, KeepAliveCount={2}, LifeTimeCount={3}, WaitingForPublish={4}, SeqNo={5}, ItemCount={6}, ItemsToCheck={7}, ItemsToPublish={8}, MessageCount={9}";
            const string itemsMessage = "Subscription {0}, Id={1}, ItemCount={2}, ItemsToCheck={3}, ItemsToPublish={4}";

            if (!Utils.Logger.IsEnabled(logLevel))
            {
                return;
            }

            // save counters
            Monitor.Enter(m_lock);

            long sequenceNumber = m_sequenceNumber;
            int itemsToCheck = m_itemsToCheck.Count;
            int monitoredItems = m_monitoredItems.Count;
            int itemsToPublish = m_itemsToPublish.Count;
            int sentMessages = m_sentMessages.Count;
            bool publishingEnabled = m_publishingEnabled;
            bool waitingForPublish = m_waitingForPublish;

            Monitor.Exit(m_lock);

            switch (id)
            {
                case TraceStateId.Deleted:
                    Utils.Log(logLevel, deletedMessage, context, m_session?.Id, m_id,
                        sequenceNumber, sentMessages);
                    break;

                case TraceStateId.Config:
                    Utils.Log(logLevel, configMessage, context, m_session?.Id, m_id,
                        m_priority, m_publishingInterval, m_maxKeepAliveCount,
                        m_maxLifetimeCount, m_maxNotificationsPerPublish, publishingEnabled);
                    break;

                case TraceStateId.Items:
                    Utils.Log(logLevel, itemsMessage, context, m_id,
                        monitoredItems, itemsToCheck, itemsToPublish);
                    break;

                case TraceStateId.Publish:
                case TraceStateId.Monitor:
                    Utils.Log(logLevel, monitorMessage, context, m_id, m_keepAliveCounter, m_lifetimeCounter,
                        waitingForPublish, sequenceNumber, monitoredItems, itemsToCheck,
                        itemsToPublish, sentMessages);
                    break;
            }
        }
        #endregion

        #region Private Fields
        private readonly object m_lock = new object();
        private IServerInternal m_server;
        private Session m_session;
        private uint m_id;
        private IUserIdentity m_savedOwnerIdentity;
        private double m_publishingInterval;
        private uint m_maxLifetimeCount;
        private uint m_maxKeepAliveCount;
        private uint m_maxNotificationsPerPublish;
        private bool m_publishingEnabled;
        private byte m_priority;
        private long m_publishTimerExpiry;
        private uint m_keepAliveCounter;
        private uint m_lifetimeCounter;
        private bool m_waitingForPublish;
        private List<NotificationMessage> m_sentMessages;
        private int m_lastSentMessage;
        private long m_sequenceNumber;
        private uint m_maxMessageCount;
        private Dictionary<uint, LinkedListNode<IMonitoredItem>> m_monitoredItems;
        private LinkedList<IMonitoredItem> m_itemsToCheck;
        private LinkedList<IMonitoredItem> m_itemsToPublish;
        private NodeId m_diagnosticsId;
        private SubscriptionDiagnosticsDataType m_diagnostics;
        private bool m_refreshInProgress;
        private bool m_expired;
        private Dictionary<uint, List<ITriggeredMonitoredItem>> m_itemsToTrigger;
        private bool m_supportsDurable;
        private bool m_isDurable;
        #endregion
    }
}
