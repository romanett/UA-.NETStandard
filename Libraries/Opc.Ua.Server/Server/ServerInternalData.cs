/* ========================================================================
 * Copyright (c) 2005-2022 The OPC Foundation, Inc. All rights reserved.
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
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using Opc.Ua.Security.Certificates;

#pragma warning disable 0618

namespace Opc.Ua.Server
{
    /// <summary>
    /// A class that stores the globally accessible state of a server instance.
    /// </summary>
    /// <remarks>
    /// This is a readonly class that is initialized when the server starts up. It provides
    /// access to global objects and data that different parts of the server may require.
    /// It also defines some global methods.
    /// 
    /// This object is constructed is three steps:
    /// - the configuration is provided.
    /// - the node managers et. al. are provided.
    /// - the session/subscription managers are provided.
    /// 
    /// The server is not running until all three steps are complete.
    /// 
    /// The references returned from this object do not change after all three states are complete. 
    /// This ensures the object is thread safe even though it does not use a lock.
    /// Objects returned from this object can be assumed to be threadsafe unless otherwise stated.
    /// </remarks>
    public class ServerInternalData : IServerInternal, IDisposable
    {
        #region Constructors
        /// <summary>
        /// Initializes the datastore with the server configuration.
        /// </summary>
        /// <param name="serverDescription">The server description.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="messageContext">The message context.</param>
        /// <param name="certificateValidator">The certificate validator.</param>
        /// <param name="instanceCertificateProvider">The certificate type provider.</param>
        public ServerInternalData(
            ServerProperties serverDescription,
            ApplicationConfiguration configuration,
            IServiceMessageContext messageContext,
            CertificateValidator certificateValidator,
            CertificateTypesProvider instanceCertificateProvider)
        {
            m_serverDescription = serverDescription;
            m_configuration = configuration;
            m_messageContext = messageContext;

            m_endpointAddresses = new List<Uri>();

            foreach (string baseAddresses in m_configuration.ServerConfiguration.BaseAddresses)
            {
                Uri url = Utils.ParseUri(baseAddresses);

                if (url != null)
                {
                    m_endpointAddresses.Add(url);
                }
            }

            m_namespaceUris = m_messageContext.NamespaceUris;
            m_factory = m_messageContext.Factory;

            m_serverUris = new StringTable();
            m_typeTree = new TypeTable(m_namespaceUris);

            // add the server uri to the server table.
            m_serverUris.Append(m_configuration.ApplicationUri);

            // create the default system context.
            m_defaultSystemContext = new ServerSystemContext(this);
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
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Utils.SilentDispose(m_resourceManager);
                Utils.SilentDispose(m_requestManager);
                Utils.SilentDispose(m_aggregateManager);
                Utils.SilentDispose(m_nodeManager);
                Utils.SilentDispose(m_sessionManager);
                Utils.SilentDispose(m_subscriptionManager);
                Utils.SilentDispose(m_monitoredItemQueueFactory);
            }
        }
        #endregion

        #region Public Interface
        /// <summary>
        /// The session manager to use with the server.
        /// </summary>
        /// <value>The session manager.</value>
        public SessionManager SessionManager
        {
            get { return m_sessionManager; }
        }

        /// <summary>
        /// The subscription manager to use with the server.
        /// </summary>
        /// <value>The subscription manager.</value>
        public SubscriptionManager SubscriptionManager
        {
            get { return m_subscriptionManager; }
        }

        /// <summary>
        /// Stores the MasterNodeManager and the CoreNodeManager
        /// </summary>
        /// <param name="nodeManager">The node manager.</param>
        public void SetNodeManager(MasterNodeManager nodeManager)
        {
            m_nodeManager = nodeManager;
            m_diagnosticsNodeManager = nodeManager.DiagnosticsNodeManager;
            m_coreNodeManager = nodeManager.CoreNodeManager;
        }

        /// <summary>
        /// Sets the EventManager, the ResourceManager, the RequestManager and the AggregateManager.
        /// </summary>
        /// <param name="eventManager">The event manager.</param>
        /// <param name="resourceManager">The resource manager.</param>
        /// <param name="requestManager">The request manager.</param>
        public void CreateServerObject(
            EventManager eventManager,
            ResourceManager resourceManager,
            RequestManager requestManager)
        {
            m_eventManager = eventManager;
            m_resourceManager = resourceManager;
            m_requestManager = requestManager;

            // create the server object.
            CreateServerObject();
        }

        /// <summary>
        /// Stores the SessionManager, the SubscriptionManager in the datastore.
        /// </summary>
        /// <param name="sessionManager">The session manager.</param>
        /// <param name="subscriptionManager">The subscription manager.</param>
        public void SetSessionManager(
            SessionManager sessionManager,
            SubscriptionManager subscriptionManager)
        {
            m_sessionManager = sessionManager;
            m_subscriptionManager = subscriptionManager;
        }

        /// <summary>
        /// Stores the MonitoredItemQueueFactory in the datastore.
        /// </summary>
        /// <param name="monitoredItemQueueFactory">The MonitoredItemQueueFactory.</param>
        public void SetMonitoredItemQueueFactory(
            IMonitoredItemQueueFactory monitoredItemQueueFactory)
        {
            m_monitoredItemQueueFactory = monitoredItemQueueFactory;
        }

        /// <summary>
        /// Stores the Subscriptionstore in the datastore.
        /// </summary>
        /// <param name="subscriptionStore">The subscriptionstore.</param>
        public void SetSubscriptionStore(
            ISubscriptionStore subscriptionStore)
        {
            m_subscriptionStore = subscriptionStore;
        }
        #endregion

        #region IServerInternal Members

        /// <summary>
        /// The endpoint addresses used by the server.
        /// </summary>
        /// <value>The endpoint addresses.</value>
        public IEnumerable<Uri> EndpointAddresses
        {
            get { return m_endpointAddresses; }
        }


        /// <summary>
        /// The context to use when serializing/deserializing extension objects.
        /// </summary>
        /// <value>The message context.</value>
        public IServiceMessageContext MessageContext
        {
            get { return m_messageContext; }
        }

        /// <summary>
        /// The default system context for the server.
        /// </summary>
        /// <value>The default system context.</value>
        public ServerSystemContext DefaultSystemContext
        {
            get { return m_defaultSystemContext; }
        }

        /// <summary>
        /// The table of namespace uris known to the server.
        /// </summary>
        /// <value>The namespace URIs.</value>
        public NamespaceTable NamespaceUris
        {
            get { return m_namespaceUris; }
        }

        /// <summary>
        /// The table of remote server uris known to the server.
        /// </summary>
        /// <value>The server URIs.</value>
        public StringTable ServerUris
        {
            get { return m_serverUris; }
        }

        /// <summary>
        /// The factory used to create encodeable objects that the server understands.
        /// </summary>
        /// <value>The factory.</value>
        public IEncodeableFactory Factory
        {
            get { return m_factory; }
        }

        /// <summary>
        /// The datatypes, object types and variable types known to the server.
        /// </summary>
        /// <value>The type tree.</value>
        /// <remarks>
        /// The type tree table is a global object that all components of a server have access to.
        /// Node managers must populate this table with all types that they define.
        /// This object is thread safe.
        /// </remarks>
        public TypeTable TypeTree
        {
            get { return m_typeTree; }
        }

        /// <summary>
        /// The master node manager for the server.
        /// </summary>
        /// <value>The node manager.</value>
        public MasterNodeManager NodeManager
        {
            get { return m_nodeManager; }
        }

        /// <summary>
        /// The internal node manager for the servers.
        /// </summary>
        /// <value>The core node manager.</value>
        public CoreNodeManager CoreNodeManager
        {
            get { return m_coreNodeManager; }
        }

        /// <summary>
        /// Returns the node manager that managers the server diagnostics.
        /// </summary>
        /// <value>The diagnostics node manager.</value>
        public DiagnosticsNodeManager DiagnosticsNodeManager
        {
            get { return m_diagnosticsNodeManager; }
        }

        /// <summary>
        /// The manager for events that all components use to queue events that occur.
        /// </summary>
        /// <value>The event manager.</value>
        public EventManager EventManager
        {
            get { return m_eventManager; }
        }

        /// <summary>
        /// A manager for localized resources that components can use to localize text.
        /// </summary>
        /// <value>The resource manager.</value>
        public ResourceManager ResourceManager
        {
            get { return m_resourceManager; }
        }

        /// <summary>
        /// A manager for outstanding requests that allows components to receive notifications if the timeout or are cancelled.
        /// </summary>
        /// <value>The request manager.</value>
        public RequestManager RequestManager
        {
            get { return m_requestManager; }
        }

        /// <summary>
        /// A manager for aggregate calculators supported by the server.
        /// </summary>
        /// <value>The aggregate manager.</value>
        public AggregateManager AggregateManager
        {
            get { return m_aggregateManager; }
            set { m_aggregateManager = value; }
        }

        /// <summary>
        /// The manager for active sessions.
        /// </summary>
        /// <value>The session manager.</value>
        ISessionManager IServerInternal.SessionManager
        {
            get { return m_sessionManager; }
        }

        /// <summary>
        /// The manager for active subscriptions.
        /// </summary>
        ISubscriptionManager IServerInternal.SubscriptionManager
        {
            get { return m_subscriptionManager; }
        }


        /// <summary>
        /// The factory for durable monitored item queues
        /// </summary>
        public IMonitoredItemQueueFactory MonitoredItemQueueFactory
        {
            get { return m_monitoredItemQueueFactory; }
        }

        /// <summary>
        /// The store to persist and retrieve subscriptions
        /// </summary>
        public ISubscriptionStore SubscriptionStore
        {
            get { return m_subscriptionStore; }
        }

        /// <summary>
        /// Returns the status object for the server.
        /// </summary>
        /// <value>The status.</value>
        public ServerStatusValue Status
        {
            get { return m_serverStatus; }
        }

        /// <summary>
        /// Gets or sets the current state of the server.
        /// </summary>
        /// <value>The state of the current.</value>
        public ServerState CurrentState
        {
            get
            {
                lock (m_serverStatus.Lock)
                {
                    return m_serverStatus.Value.State;
                }
            }

            set
            {
                lock (m_serverStatus.Lock)
                {
                    m_serverStatus.Value.State = value;
                }
            }
        }

        /// <summary>
        /// Returns the Server object node
        /// </summary>
        /// <value>The Server object node.</value>
        public ServerObjectState ServerObject
        {
            get { return m_serverObject; }
        }

        /// <summary>
        /// Used to synchronize access to the server diagnostics.
        /// </summary>
        /// <value>The diagnostics lock.</value>
        public object DiagnosticsLock
        {
            get { return m_dataLock; }
        }

        /// <summary>
        /// Used to synchronize write access to
        /// the server diagnostics.
        /// </summary>
        /// <value>The diagnostics lock.</value>
        public object DiagnosticsWriteLock
        {
            get
            {
                // implicitly force diagnostics update
                if (DiagnosticsNodeManager != null)
                {
                    DiagnosticsNodeManager.ForceDiagnosticsScan();
                }
                return DiagnosticsLock;
            }
        }

        /// <summary>
        /// Returns the diagnostics structure for the server.
        /// </summary>
        /// <value>The server diagnostics.</value>
        public ServerDiagnosticsSummaryDataType ServerDiagnostics
        {
            get { return m_serverDiagnostics; }
        }

        /// <summary>
        /// Whether the server is currently running.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is running; otherwise, <c>false</c>.
        /// </value>
        /// <remarks>
        /// This flag is set to false when the server shuts down. Threads running should check this flag whenever
        /// they return from a blocking operation. If it is false the thread should clean up and terminate.
        /// </remarks>
        public bool IsRunning
        {
            get
            {
                if (m_serverStatus == null)
                {
                    return false;
                }

                lock (m_serverStatus.Lock)
                {
                    if (m_serverStatus.Value.State == ServerState.Running)
                        return true;

                    if (m_serverStatus.Value.State == ServerState.Shutdown &&
                        m_serverStatus.Value.SecondsTillShutdown > 0)
                        return true;

                    return false;
                }
            }
        }

        /// <summary>
        /// Whether the server is collecting diagnostics.
        /// </summary>
        /// <value><c>true</c> if diagnostics are enabled; otherwise, <c>false</c>.</value>
        public bool DiagnosticsEnabled
        {
            get
            {
                if (m_diagnosticsNodeManager == null)
                {
                    return false;
                }

                return m_diagnosticsNodeManager.DiagnosticsEnabled;
            }
        }

        /// <summary>
        /// Closes the specified session.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="sessionId">The session identifier.</param>
        /// <param name="deleteSubscriptions">if set to <c>true</c> subscriptions are to be deleted.</param>
        public void CloseSession(OperationContext context, NodeId sessionId, bool deleteSubscriptions)
        {
            m_nodeManager.SessionClosing(context, sessionId, deleteSubscriptions);
            m_subscriptionManager.SessionClosing(context, sessionId, deleteSubscriptions);
            m_sessionManager.CloseSession(sessionId);
        }

        /// <summary>
        /// Deletes the specified subscription.
        /// </summary>
        /// <param name="subscriptionId">The subscription identifier.</param>
        public void DeleteSubscription(uint subscriptionId)
        {
            m_subscriptionManager.DeleteSubscription(null, subscriptionId);
        }

        /// <summary>
        /// Called by any component to report a global event.
        /// </summary>
        /// <param name="e">The event.</param>
        public void ReportEvent(IFilterTarget e)
        {
            ReportEvent(DefaultSystemContext, e);
        }

        /// <summary>
        /// Called by any component to report a global event.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="e">The event.</param>
        public void ReportEvent(ISystemContext context, IFilterTarget e)
        {
            if ((Auditing == false) && (e is AuditEventState))
            {
                // do not report auditing events if server Auditing flag is false
                return;
            }

            m_serverObject?.ReportEvent(context, e);
        }

        /// <summary>
        /// Refreshes the conditions for the specified subscription.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="subscriptionId">The subscription identifier.</param>
        public void ConditionRefresh(OperationContext context, uint subscriptionId)
        {
            m_subscriptionManager.ConditionRefresh(context, subscriptionId);
        }

        /// <summary>
        /// Refreshes the conditions for the specified subscription.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="subscriptionId">The subscription identifier.</param>
        /// <param name="monitoredItemId">The monitored item identifier.</param>
        public void ConditionRefresh2(OperationContext context, uint subscriptionId, uint monitoredItemId)
        {
            m_subscriptionManager.ConditionRefresh2(context, subscriptionId, monitoredItemId);
        }
        #endregion

        #region IAuditReportEvents Members
        /// <inheritdoc/>
        public bool Auditing => m_auditing;

        /// <inheritdoc/>
        public ISystemContext DefaultAuditContext => DefaultSystemContext.Copy();

        /// <inheritdoc/>
        public void ReportAuditEvent(ISystemContext context, AuditEventState e)
        {
            if (Auditing == false)
            {
                // do not report auditing events if server Auditing flag is false
                return;
            }

            ReportEvent(context, e);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Creates the ServerObject and attaches it to the NodeManager.
        /// </summary>
        private void CreateServerObject()
        {
            lock (m_diagnosticsNodeManager.Lock)
            {
                // get the server object.
                ServerObjectState serverObject = m_serverObject = (ServerObjectState)m_diagnosticsNodeManager.FindPredefinedNode(
                    ObjectIds.Server,
                    typeof(ServerObjectState));

                // update server capabilities.
                serverObject.ServiceLevel.Value = 255;
                serverObject.ServerCapabilities.LocaleIdArray.Value = m_resourceManager.GetAvailableLocales();
                serverObject.ServerCapabilities.ServerProfileArray.Value = m_configuration.ServerConfiguration.ServerProfileArray.ToArray();
                serverObject.ServerCapabilities.MinSupportedSampleRate.Value = 0;
                serverObject.ServerCapabilities.MaxBrowseContinuationPoints.Value = (ushort)m_configuration.ServerConfiguration.MaxBrowseContinuationPoints;
                serverObject.ServerCapabilities.MaxQueryContinuationPoints.Value = (ushort)m_configuration.ServerConfiguration.MaxQueryContinuationPoints;
                serverObject.ServerCapabilities.MaxHistoryContinuationPoints.Value = (ushort)m_configuration.ServerConfiguration.MaxHistoryContinuationPoints;
                serverObject.ServerCapabilities.MaxArrayLength.Value = (uint)m_configuration.TransportQuotas.MaxArrayLength;
                serverObject.ServerCapabilities.MaxStringLength.Value = (uint)m_configuration.TransportQuotas.MaxStringLength;
                serverObject.ServerCapabilities.MaxByteStringLength.Value = (uint)m_configuration.TransportQuotas.MaxByteStringLength;

                // Any operational limits Property that is provided shall have a non zero value.
                var operationLimits = serverObject.ServerCapabilities.OperationLimits;
                var configOperationLimits = m_configuration.ServerConfiguration.OperationLimits;
                if (configOperationLimits != null)
                {
                    operationLimits.MaxNodesPerRead = SetPropertyValue(operationLimits.MaxNodesPerRead, configOperationLimits.MaxNodesPerRead);
                    operationLimits.MaxNodesPerHistoryReadData = SetPropertyValue(operationLimits.MaxNodesPerHistoryReadData, configOperationLimits.MaxNodesPerHistoryReadData);
                    operationLimits.MaxNodesPerHistoryReadEvents = SetPropertyValue(operationLimits.MaxNodesPerHistoryReadEvents, configOperationLimits.MaxNodesPerHistoryReadEvents);
                    operationLimits.MaxNodesPerWrite = SetPropertyValue(operationLimits.MaxNodesPerWrite, configOperationLimits.MaxNodesPerWrite);
                    operationLimits.MaxNodesPerHistoryUpdateData = SetPropertyValue(operationLimits.MaxNodesPerHistoryUpdateData, configOperationLimits.MaxNodesPerHistoryUpdateData);
                    operationLimits.MaxNodesPerHistoryUpdateEvents = SetPropertyValue(operationLimits.MaxNodesPerHistoryUpdateEvents, configOperationLimits.MaxNodesPerHistoryUpdateEvents);
                    operationLimits.MaxNodesPerMethodCall = SetPropertyValue(operationLimits.MaxNodesPerMethodCall, configOperationLimits.MaxNodesPerMethodCall);
                    operationLimits.MaxNodesPerBrowse = SetPropertyValue(operationLimits.MaxNodesPerBrowse, configOperationLimits.MaxNodesPerBrowse);
                    operationLimits.MaxNodesPerRegisterNodes = SetPropertyValue(operationLimits.MaxNodesPerRegisterNodes, configOperationLimits.MaxNodesPerRegisterNodes);
                    operationLimits.MaxNodesPerTranslateBrowsePathsToNodeIds = SetPropertyValue(operationLimits.MaxNodesPerTranslateBrowsePathsToNodeIds, configOperationLimits.MaxNodesPerTranslateBrowsePathsToNodeIds);
                    operationLimits.MaxNodesPerNodeManagement = SetPropertyValue(operationLimits.MaxNodesPerNodeManagement, configOperationLimits.MaxNodesPerNodeManagement);
                    operationLimits.MaxMonitoredItemsPerCall = SetPropertyValue(operationLimits.MaxMonitoredItemsPerCall, configOperationLimits.MaxMonitoredItemsPerCall);
                }
                else
                {
                    operationLimits.MaxNodesPerRead =
                    operationLimits.MaxNodesPerHistoryReadData =
                    operationLimits.MaxNodesPerHistoryReadEvents =
                    operationLimits.MaxNodesPerWrite =
                    operationLimits.MaxNodesPerHistoryUpdateData =
                    operationLimits.MaxNodesPerHistoryUpdateEvents =
                    operationLimits.MaxNodesPerMethodCall =
                    operationLimits.MaxNodesPerBrowse =
                    operationLimits.MaxNodesPerRegisterNodes =
                    operationLimits.MaxNodesPerTranslateBrowsePathsToNodeIds =
                    operationLimits.MaxNodesPerNodeManagement =
                    operationLimits.MaxMonitoredItemsPerCall = null;
                }

                // setup PublishSubscribe Status State value
                PubSubState pubSubState = PubSubState.Disabled;

                var default_PubSubState = (BaseVariableState)m_diagnosticsNodeManager.FindPredefinedNode(
                    VariableIds.PublishSubscribe_Status_State,
                    typeof(BaseVariableState));
                default_PubSubState.Value = pubSubState;

                // setup value for SupportedTransportProfiles
                var default_SupportedTransportProfiles = (BaseVariableState)m_diagnosticsNodeManager.FindPredefinedNode(
                   VariableIds.PublishSubscribe_SupportedTransportProfiles,
                   typeof(BaseVariableState));
                default_SupportedTransportProfiles.Value = "uadp";

                // setup callbacks for dynamic values.
                serverObject.NamespaceArray.OnSimpleReadValue = OnReadNamespaceArray;
                serverObject.NamespaceArray.MinimumSamplingInterval = 1000;

                serverObject.ServerArray.OnSimpleReadValue = OnReadServerArray;
                serverObject.ServerArray.MinimumSamplingInterval = 1000;

                // dynamic change of enabledFlag is disabled to pass CTT
                serverObject.ServerDiagnostics.EnabledFlag.AccessLevel = AccessLevels.CurrentRead;
                serverObject.ServerDiagnostics.EnabledFlag.UserAccessLevel = AccessLevels.CurrentRead;
                serverObject.ServerDiagnostics.EnabledFlag.OnSimpleReadValue = OnReadDiagnosticsEnabledFlag;
                serverObject.ServerDiagnostics.EnabledFlag.OnSimpleWriteValue = OnWriteDiagnosticsEnabledFlag;
                serverObject.ServerDiagnostics.EnabledFlag.MinimumSamplingInterval = 1000;

                // initialize status.
                ServerStatusDataType serverStatus = new ServerStatusDataType {
                    StartTime = DateTime.UtcNow,
                    CurrentTime = DateTime.UtcNow,
                    State = ServerState.Shutdown
                };

                var buildInfo = new BuildInfo() {
                    ProductName = m_serverDescription.ProductName,
                    ProductUri = m_serverDescription.ProductUri,
                    ManufacturerName = m_serverDescription.ManufacturerName,
                    SoftwareVersion = m_serverDescription.SoftwareVersion,
                    BuildNumber = m_serverDescription.BuildNumber,
                    BuildDate = m_serverDescription.BuildDate,
                };
                var buildInfoVariableState = (BuildInfoVariableState)m_diagnosticsNodeManager.FindPredefinedNode(VariableIds.Server_ServerStatus_BuildInfo, typeof(BuildInfoVariableState));
                var buildInfoVariable = new BuildInfoVariableValue(buildInfoVariableState, buildInfo, null);
                serverStatus.BuildInfo = buildInfoVariable.Value;

                serverObject.ServerStatus.MinimumSamplingInterval = 1000;
                serverObject.ServerStatus.CurrentTime.MinimumSamplingInterval = 1000;

                m_serverStatus = new ServerStatusValue(
                    serverObject.ServerStatus,
                    serverStatus,
                    m_dataLock);

                m_serverStatus.Timestamp = DateTime.UtcNow;
                m_serverStatus.OnBeforeRead = OnReadServerStatus;

                // initialize diagnostics.
                m_serverDiagnostics = new ServerDiagnosticsSummaryDataType {
                    ServerViewCount = 0,
                    CurrentSessionCount = 0,
                    CumulatedSessionCount = 0,
                    SecurityRejectedSessionCount = 0,
                    RejectedSessionCount = 0,
                    SessionTimeoutCount = 0,
                    SessionAbortCount = 0,
                    PublishingIntervalCount = 0,
                    CurrentSubscriptionCount = 0,
                    CumulatedSubscriptionCount = 0,
                    SecurityRejectedRequestsCount = 0,
                    RejectedRequestsCount = 0
                };

                m_diagnosticsNodeManager.CreateServerDiagnostics(
                    m_defaultSystemContext,
                    m_serverDiagnostics,
                    OnUpdateDiagnostics);

                // set the diagnostics enabled state.
                m_diagnosticsNodeManager.SetDiagnosticsEnabled(
                    m_defaultSystemContext,
                    m_configuration.ServerConfiguration.DiagnosticsEnabled);

                ConfigurationNodeManager configurationNodeManager = m_diagnosticsNodeManager as ConfigurationNodeManager;
                configurationNodeManager?.CreateServerConfiguration(
                    m_defaultSystemContext,
                    m_configuration);

                m_auditing = m_configuration.ServerConfiguration.AuditingEnabled;
                PropertyState<bool> auditing = serverObject.Auditing;
                auditing.OnSimpleWriteValue += OnWriteAuditing;
                auditing.OnSimpleReadValue += OnReadAuditing;
                auditing.Value = m_auditing;
                auditing.RolePermissions = new RolePermissionTypeCollection {
                        new RolePermissionType {
                            RoleId = ObjectIds.WellKnownRole_AuthenticatedUser,
                            Permissions = (uint)(PermissionType.Browse|PermissionType.Read)
                            },
                        new RolePermissionType {
                            RoleId = ObjectIds.WellKnownRole_SecurityAdmin,
                            Permissions = (uint)(PermissionType.Browse|PermissionType.Write|PermissionType.ReadRolePermissions|PermissionType.Read)
                            }};
                auditing.AccessLevel = AccessLevels.CurrentRead;
                auditing.UserAccessLevel = AccessLevels.CurrentReadOrWrite;
                auditing.MinimumSamplingInterval = 1000;
            }
        }

        /// <summary>
        /// Updates the server status before a read.
        /// </summary>
        private void OnReadServerStatus(
            ISystemContext context,
            BaseVariableValue variable,
            NodeState component)
        {
            lock (m_dataLock)
            {
                DateTime now = DateTime.UtcNow;
                m_serverStatus.Timestamp = now;
                m_serverStatus.Value.CurrentTime = now;
                // update other timestamps in NodeState objects which are used to derive the source timestamp
                if (variable is ServerStatusValue serverStatusValue &&
                    serverStatusValue.Variable is ServerStatusState serverStatusState)
                {
                    serverStatusState.Timestamp = now;
                    serverStatusState.CurrentTime.Timestamp = now;
                }
            }
        }

        /// <summary>
        /// Returns a copy of the namespace array.
        /// </summary>
        private ServiceResult OnReadNamespaceArray(
            ISystemContext context,
            NodeState node,
            ref object value)
        {
            value = m_namespaceUris.ToArray();
            return ServiceResult.Good;
        }

        /// <summary>
        /// Returns a copy of the server array.
        /// </summary>
        private ServiceResult OnReadServerArray(
            ISystemContext context,
            NodeState node,
            ref object value)
        {
            value = m_serverUris.ToArray();
            return ServiceResult.Good;
        }

        /// <summary>
        /// Returns Diagnostics.EnabledFlag
        /// </summary>
        private ServiceResult OnReadDiagnosticsEnabledFlag(
            ISystemContext context,
            NodeState node,
            ref object value)
        {
            value = m_diagnosticsNodeManager.DiagnosticsEnabled;
            return ServiceResult.Good;
        }

        /// <summary>
        /// Sets the Diagnostics.EnabledFlag
        /// </summary>
        private ServiceResult OnWriteDiagnosticsEnabledFlag(
            ISystemContext context,
            NodeState node,
            ref object value)
        {
            bool enabled = (bool)value;
            m_diagnosticsNodeManager.SetDiagnosticsEnabled(
                m_defaultSystemContext,
                enabled);

            return ServiceResult.Good;
        }

        /// <summary>
        /// Updates the Server.Auditing flag.
        /// </summary>
        private ServiceResult OnWriteAuditing(
            ISystemContext context,
            NodeState node,
            ref object value)
        {
            m_auditing = Convert.ToBoolean(value, CultureInfo.InvariantCulture);
            return ServiceResult.Good;
        }

        /// <summary>
        /// Reads the Server.Auditing flag.
        /// </summary>
        private ServiceResult OnReadAuditing(
            ISystemContext context,
            NodeState node,
            ref object value)
        {
            value = m_auditing;
            return ServiceResult.Good;
        }

        /// <summary>
        /// Returns a copy of the current diagnostics.
        /// </summary>
        private ServiceResult OnUpdateDiagnostics(
            ISystemContext context,
            NodeState node,
            ref object value)
        {
            lock (m_serverDiagnostics)
            {
                value = Utils.Clone(m_serverDiagnostics);
            }

            return ServiceResult.Good;
        }

        /// <summary>
        /// Set the property to null if the value is zero,
        /// to the value otherwise.
        /// </summary>
        private PropertyState<uint> SetPropertyValue(PropertyState<uint> property, uint value)
        {
            if (value != 0)
            {
                property.Value = value;
            }
            else
            {
                property = null;
            }
            return property;
        }
        #endregion

        #region Private Fields
        private ServerProperties m_serverDescription;
        private ApplicationConfiguration m_configuration;
        private List<Uri> m_endpointAddresses;
        private IServiceMessageContext m_messageContext;
        private ServerSystemContext m_defaultSystemContext;
        private NamespaceTable m_namespaceUris;
        private StringTable m_serverUris;
        private IEncodeableFactory m_factory;
        private TypeTable m_typeTree;
        private ResourceManager m_resourceManager;
        private RequestManager m_requestManager;
        private AggregateManager m_aggregateManager;
        private MasterNodeManager m_nodeManager;
        private CoreNodeManager m_coreNodeManager;
        private DiagnosticsNodeManager m_diagnosticsNodeManager;
        private EventManager m_eventManager;
        private SessionManager m_sessionManager;
        private SubscriptionManager m_subscriptionManager;
        private IMonitoredItemQueueFactory m_monitoredItemQueueFactory;
        private ISubscriptionStore m_subscriptionStore;

        private readonly object m_dataLock = new object();
        private ServerObjectState m_serverObject;
        private ServerStatusValue m_serverStatus;
        private bool m_auditing;
        private ServerDiagnosticsSummaryDataType m_serverDiagnostics;
        #endregion
    }
}
