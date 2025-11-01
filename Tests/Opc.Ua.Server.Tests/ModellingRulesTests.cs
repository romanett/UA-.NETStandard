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
using System.Threading.Tasks;
using NUnit.Framework;
using Quickstarts.ReferenceServer;

namespace Opc.Ua.Server.Tests
{
    /// <summary>
    /// Tests for ModellingRules in ServerCapabilities.
    /// </summary>
    [TestFixture]
    [Category("Server")]
    [SetCulture("en-us")]
    [SetUICulture("en-us")]
    public class ModellingRulesTests
    {
        private ServerFixture<ReferenceServer> m_fixture;
        private ReferenceServer m_server;
        private RequestHeader m_requestHeader;

        /// <summary>
        /// Set up a Server fixture.
        /// </summary>
        [OneTimeSetUp]
        public async Task OneTimeSetUpAsync()
        {
            m_fixture = new ServerFixture<ReferenceServer>
            {
                AllNodeManagers = true,
                OperationLimits = true
            };
            m_server = await m_fixture.StartAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Tear down the server fixture.
        /// </summary>
        [OneTimeTearDown]
        public async Task OneTimeTearDownAsync()
        {
            await m_fixture.StopAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Create a session for a test.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            m_requestHeader = m_server.CreateAndActivateSession(TestContext.CurrentContext.Test.Name);
        }

        /// <summary>
        /// Close the session for a test.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            m_server.CloseSession(m_requestHeader);
        }

        /// <summary>
        /// Test that the ModellingRules folder is populated with the expected modelling rules.
        /// </summary>
        [Test]
        public void TestModellingRulesPopulated()
        {
            // Browse ServerCapabilities->ModellingRules
            var modellingRulesNodeId = ObjectIds.Server_ServerCapabilities_ModellingRules;
            
            var browseRequest = new BrowseDescription
            {
                NodeId = modellingRulesNodeId,
                BrowseDirection = BrowseDirection.Forward,
                ReferenceTypeId = ReferenceTypeIds.Organizes,
                IncludeSubtypes = true,
                NodeClassMask = (uint)NodeClass.Object,
                ResultMask = (uint)BrowseResultMask.All
            };

            var browseDescriptions = new BrowseDescriptionCollection { browseRequest };

            BrowseResultCollection results;
            DiagnosticInfoCollection diagnosticInfos;

            m_server.Browse(
                m_requestHeader,
                null,
                0,
                browseDescriptions,
                out results,
                out diagnosticInfos);

            Assert.That(results, Is.Not.Null);
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].References.Count, Is.GreaterThan(0), "ModellingRules folder should not be empty");

            // Check that expected modelling rules are present
            var expectedRules = new[]
            {
                BrowseNames.ModellingRule_Mandatory,
                BrowseNames.ModellingRule_Optional,
                BrowseNames.ModellingRule_ExposesItsArray,
                BrowseNames.ModellingRule_OptionalPlaceholder,
                BrowseNames.ModellingRule_MandatoryPlaceholder
            };

            foreach (var expectedRule in expectedRules)
            {
                bool found = false;
                foreach (var reference in results[0].References)
                {
                    if (reference.BrowseName.Name == expectedRule)
                    {
                        found = true;
                        // Verify it's of the correct type
                        var expectedTypeDefinition = ExpandedNodeId.ToNodeId(
                            ObjectTypeIds.ModellingRuleType,
                            m_server.CurrentInstance.NamespaceUris);
                        Assert.That(reference.TypeDefinition, Is.EqualTo(expectedTypeDefinition));
                        break;
                    }
                }
                Assert.That(found, Is.True,
                    $"Expected modelling rule '{expectedRule}' not found in ServerCapabilities->ModellingRules");
            }
        }

        /// <summary>
        /// Test that all modelling rules have the correct type definition.
        /// </summary>
        [Test]
        public void TestModellingRulesHaveCorrectType()
        {
            var modellingRulesNodeId = ObjectIds.Server_ServerCapabilities_ModellingRules;
            
            var browseRequest = new BrowseDescription
            {
                NodeId = modellingRulesNodeId,
                BrowseDirection = BrowseDirection.Forward,
                ReferenceTypeId = ReferenceTypeIds.Organizes,
                IncludeSubtypes = true,
                NodeClassMask = (uint)NodeClass.Object,
                ResultMask = (uint)BrowseResultMask.All
            };

            var browseDescriptions = new BrowseDescriptionCollection { browseRequest };

            BrowseResultCollection results;
            DiagnosticInfoCollection diagnosticInfos;

            m_server.Browse(
                m_requestHeader,
                null,
                0,
                browseDescriptions,
                out results,
                out diagnosticInfos);

            Assert.That(results, Is.Not.Null);
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].References.Count, Is.GreaterThan(0));

            // All references should be of type ModellingRuleType
            var expectedTypeDefinition = ExpandedNodeId.ToNodeId(
                ObjectTypeIds.ModellingRuleType,
                m_server.CurrentInstance.NamespaceUris);

            foreach (var reference in results[0].References)
            {
                Assert.That(reference.NodeClass, Is.EqualTo(NodeClass.Object));
                Assert.That(reference.TypeDefinition, Is.EqualTo(expectedTypeDefinition),
                    $"Modelling rule '{reference.BrowseName.Name}' does not have the correct type definition");
            }
        }
    }
}
