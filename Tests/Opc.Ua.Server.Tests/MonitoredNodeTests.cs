using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using NUnit.Framework;
using Opc.Ua.Sample;

namespace Opc.Ua.Server.Tests
{
    /// <summary>
    /// Test <see cref="MonitoredNode2"/>
    /// </summary>
    [TestFixture, Category("MonitoredNode")]
    [SetCulture("en-us"), SetUICulture("en-us")]
    [Parallelizable]
    public class MonitoredNodeTests
    {
        private ServerFixture<StandardServer> m_fixture;
        private StandardServer m_server;
        private TestableCustomNodeManger2 m_nodeManager;
        private NodeId m_nodeId;
        private DataItemState m_baseObject;
        private MonitoredNode2 m_monitoredNode;
        private ServerSystemContext m_context;
        private List<MonitoredItem> m_monitoredItems;

        /// <summary>
        /// Set up some variables for benchmarks.
        /// </summary>
        [OneTimeSetUp]
        [GlobalSetup]
        public async Task GlobalSetup()
        {
            m_fixture = new ServerFixture<StandardServer>();
            const string ns = "http://test.org/UA/Data/";
            m_server = await m_fixture.StartAsync(TestContext.Out).ConfigureAwait(false);
            m_nodeManager = new TestableCustomNodeManger2(m_server.CurrentInstance, ns);
            var index = m_server.CurrentInstance.NamespaceUris.GetIndex(ns);
            m_baseObject = new DataItemState(null);
            m_nodeId = new NodeId((string)CommonTestWorkers.NodeIdTestSetStatic.First().Identifier, (ushort)index);
            m_baseObject.NodeId = m_nodeId;
            m_nodeManager.AddPredefinedNode(m_nodeManager.SystemContext, m_baseObject);
            NodeState nodeState = m_nodeManager.Find(m_nodeId);
            m_monitoredNode = new MonitoredNode2(m_nodeManager, nodeState);
            m_context = new ServerSystemContext(m_server.CurrentInstance);
            m_monitoredItems = new List<MonitoredItem>();

            // Arrange
            var createRequest = new MonitoredItemCreateRequest {
                ItemToMonitor = new ReadValueId {
                    NodeId = m_nodeId,
                    AttributeId = Attributes.Value
                },
                MonitoringMode = MonitoringMode.Reporting,
                RequestedParameters = new MonitoringParameters {
                    ClientHandle = 1,
                    QueueSize = 2,
                    DiscardOldest = true,
                    SamplingInterval = 5000,
                    Filter = null
                }
            };

            //var createRequestMi = CreateEventMonitoredItem(m_nodeId);

            var globalCounter = 1L;

            for (int i = 0; i < 50000; i++)
            {
                ServiceResult serviceResult = m_nodeManager.CreateMonitoredItem(m_context, m_nodeManager.GetManagerHandle(m_nodeId) as NodeHandle, 1, 5000, DiagnosticsMasks.All, TimestampsToReturn.Server, createRequest, ref globalCounter, out var _, out var mi);
                Assert.That(ServiceResult.IsGood(serviceResult), Is.True);

                m_monitoredNode.Add(mi as MonitoredItem);
                m_monitoredItems.Add(mi as MonitoredItem);


                //ServiceResult serviceResult2 = m_nodeManager.CreateMonitoredItem(m_context, m_nodeManager.GetManagerHandle(m_nodeId) as NodeHandle, 1, 5000, DiagnosticsMasks.All, TimestampsToReturn.Server, createRequestMi, ref globalCounter, out var _, out var eventMi);
                //Assert.That(ServiceResult.IsGood(serviceResult2), Is.True);

                //m_monitoredNode.Add(eventMi as IEventMonitoredItem);
                //m_monitoredItems.Add(eventMi as MonitoredItem);
            }
        }

        [GlobalCleanup]
        [OneTimeTearDown]
        public async Task GlobalCleanup()
        {
            await m_fixture.StopAsync().ConfigureAwait(false);
        }

        [Benchmark]
        public void SingleDataChange()
        {
            m_baseObject.Value = new DataValue(new Variant(1));
            m_baseObject.ClearChangeMasks(m_context, false);
        }
        //[Test]
        //[Benchmark]
        //public void RaiseEvent()
        //{
        //    var e = new BaseEventState(m_baseObject);
        //    m_monitoredNode.OnReportEvent(m_context, m_baseObject, e);
        //}


        //private static MonitoredItemCreateRequest CreateEventMonitoredItem(NodeId nodeId)
        //{
        //    var whereClause = new ContentFilter();

        //    whereClause.Push(FilterOperator.Equals, new FilterOperand[] {
        //        new SimpleAttributeOperand() {
        //            AttributeId = Attributes.Value,
        //            TypeDefinitionId = ObjectTypeIds.BaseEventType,
        //            BrowsePath = new QualifiedNameCollection(new QualifiedName[] { "EventType" })
        //        },
        //        new LiteralOperand {
        //            Value = new Variant(new NodeId(ObjectTypeIds.BaseEventType))
        //        }
        //    });

        //    var mi = new MonitoredItemCreateRequest() {
        //        ItemToMonitor = new ReadValueId() {
        //            AttributeId = Attributes.EventNotifier,
        //            NodeId = ObjectIds.Server
        //        },
        //        MonitoringMode = MonitoringMode.Reporting,
        //        RequestedParameters = new MonitoringParameters() {
        //            ClientHandle = 1,
        //            SamplingInterval = -1,
        //            Filter = new ExtensionObject(
        //                new EventFilter {
        //                    SelectClauses = new SimpleAttributeOperandCollection(
        //                    new SimpleAttributeOperand[] {
        //                        new SimpleAttributeOperand{
        //                            AttributeId = Attributes.Value,
        //                            TypeDefinitionId = ObjectTypeIds.BaseEventType,
        //                            BrowsePath = new QualifiedNameCollection(new QualifiedName[] { BrowseNames.Message})
        //                        }
        //                    }),
        //                    WhereClause = whereClause,
        //                }),
        //            DiscardOldest = true,
        //            QueueSize = 2
        //        }
        //    };
        //    return mi;
        //}
    }
}
