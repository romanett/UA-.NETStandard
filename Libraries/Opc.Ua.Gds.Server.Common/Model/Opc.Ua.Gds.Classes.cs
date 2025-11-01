/* ========================================================================
 * Copyright (c) 2005-2024 The OPC Foundation, Inc. All rights reserved.
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
using System.Text;
using System.Xml;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Threading;
using Opc.Ua;

#pragma warning disable 1591

namespace Opc.Ua.Gds
{
    #region FindApplicationsMethodState Class
    #if (!OPCUA_EXCLUDE_FindApplicationsMethodState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class FindApplicationsMethodState : MethodState
    {
        #region Constructors
        public FindApplicationsMethodState(NodeState parent) : base(parent)
        {
        }

        public new static NodeState Construct(NodeState parent)
        {
            return new FindApplicationsMethodState(parent);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAACAAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvR0RTL/////8EYYIKBAAAAAEAGgAAAEZp" +
           "bmRBcHBsaWNhdGlvbnNNZXRob2RUeXBlAQECAAAvAQECAAIAAAABAf////8CAAAAF2CpCgIAAAAAAA4A" +
           "AABJbnB1dEFyZ3VtZW50cwEBAwAALgBEAwAAAJYBAAAAAQAqAQEdAAAADgAAAEFwcGxpY2F0aW9uVXJp" +
           "AAz/////AAAAAAABACgBAQAAAAEAAAABAAAAAQH/////AAAAABdgqQoCAAAAAAAPAAAAT3V0cHV0QXJn" +
           "dW1lbnRzAQEEAAAuAEQEAAAAlgEAAAABACoBASEAAAAMAAAAQXBwbGljYXRpb25zAQEBAAEAAAABAAAA" +
           "AAAAAAABACgBAQAAAAEAAAABAAAAAQH/////AAAAAA==";
        #endregion
        #endif
        #endregion

        #region Event Callbacks
        public FindApplicationsMethodStateMethodCallHandler OnCall;

        public FindApplicationsMethodStateMethodAsyncCallHandler OnCallAsync;
        #endregion

        #region Public Properties
        #endregion

        #region Overridden Methods
        protected override ServiceResult Call(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments)
        {
            if (OnCall == null)
            {
                return base.Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            ServiceResult _result = null;

            string applicationUri = (string)_inputArguments[0];

            ApplicationRecordDataType[] applications = (ApplicationRecordDataType[])_outputArguments[0];

            if (OnCall != null)
            {
                _result = OnCall(
                    _context,
                    this,
                    _objectId,
                    applicationUri,
                    ref applications);
            }

            _outputArguments[0] = applications;

            return _result;
        }

        #if (OPCUA_INCLUDE_ASYNC)
        protected override async ValueTask<ServiceResult> CallAsync(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments,
            CancellationToken cancellationToken = default)
        {
            if (OnCall == null && OnCallAsync == null)
            {
                return await base.CallAsync(_context, _objectId, _inputArguments, _outputArguments, cancellationToken).ConfigureAwait(false);
            }

            FindApplicationsMethodStateResult _result = null;

            string applicationUri = (string)_inputArguments[0];

            if (OnCallAsync != null)
            {
                _result = await OnCallAsync(
                    _context,
                    this,
                    _objectId,
                    applicationUri,
                    cancellationToken).ConfigureAwait(false);
            }
            else if (OnCall != null)
            {
                return Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            _outputArguments[0] = _result.Applications;

            return _result.ServiceResult;
        }
        #endif

        #endregion

        #region Private Fields
        #endregion
    }

    /// <exclude />
    public delegate ServiceResult FindApplicationsMethodStateMethodCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        string applicationUri,
        ref ApplicationRecordDataType[] applications);

    /// <exclude />
    public partial class FindApplicationsMethodStateResult
    {
        public ServiceResult ServiceResult { get; set; }
        public ApplicationRecordDataType[] Applications { get; set; }
    }

    /// <exclude />
    public delegate ValueTask<FindApplicationsMethodStateResult> FindApplicationsMethodStateMethodAsyncCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        string applicationUri,
        CancellationToken cancellationToken);
    #endif
    #endregion

    #region RegisterApplicationMethodState Class
    #if (!OPCUA_EXCLUDE_RegisterApplicationMethodState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class RegisterApplicationMethodState : MethodState
    {
        #region Constructors
        public RegisterApplicationMethodState(NodeState parent) : base(parent)
        {
        }

        public new static NodeState Construct(NodeState parent)
        {
            return new RegisterApplicationMethodState(parent);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAACAAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvR0RTL/////8EYYIKBAAAAAEAHQAAAFJl" +
           "Z2lzdGVyQXBwbGljYXRpb25NZXRob2RUeXBlAQEFAAAvAQEFAAUAAAABAf////8CAAAAF2CpCgIAAAAA" +
           "AA4AAABJbnB1dEFyZ3VtZW50cwEBBgAALgBEBgAAAJYBAAAAAQAqAQEcAAAACwAAAEFwcGxpY2F0aW9u" +
           "AQEBAP////8AAAAAAAEAKAEBAAAAAQAAAAEAAAABAf////8AAAAAF2CpCgIAAAAAAA8AAABPdXRwdXRB" +
           "cmd1bWVudHMBAQcAAC4ARAcAAACWAQAAAAEAKgEBHAAAAA0AAABBcHBsaWNhdGlvbklkABH/////AAAA" +
           "AAABACgBAQAAAAEAAAABAAAAAQH/////AAAAAA==";
        #endregion
        #endif
        #endregion

        #region Event Callbacks
        public RegisterApplicationMethodStateMethodCallHandler OnCall;

        public RegisterApplicationMethodStateMethodAsyncCallHandler OnCallAsync;
        #endregion

        #region Public Properties
        #endregion

        #region Overridden Methods
        protected override ServiceResult Call(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments)
        {
            if (OnCall == null)
            {
                return base.Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            ServiceResult _result = null;

            ApplicationRecordDataType application = (ApplicationRecordDataType)ExtensionObject.ToEncodeable((ExtensionObject)_inputArguments[0]);

            NodeId applicationId = (NodeId)_outputArguments[0];

            if (OnCall != null)
            {
                _result = OnCall(
                    _context,
                    this,
                    _objectId,
                    application,
                    ref applicationId);
            }

            _outputArguments[0] = applicationId;

            return _result;
        }

        #if (OPCUA_INCLUDE_ASYNC)
        protected override async ValueTask<ServiceResult> CallAsync(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments,
            CancellationToken cancellationToken = default)
        {
            if (OnCall == null && OnCallAsync == null)
            {
                return await base.CallAsync(_context, _objectId, _inputArguments, _outputArguments, cancellationToken).ConfigureAwait(false);
            }

            RegisterApplicationMethodStateResult _result = null;

            ApplicationRecordDataType application = (ApplicationRecordDataType)ExtensionObject.ToEncodeable((ExtensionObject)_inputArguments[0]);

            if (OnCallAsync != null)
            {
                _result = await OnCallAsync(
                    _context,
                    this,
                    _objectId,
                    application,
                    cancellationToken).ConfigureAwait(false);
            }
            else if (OnCall != null)
            {
                return Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            _outputArguments[0] = _result.ApplicationId;

            return _result.ServiceResult;
        }
        #endif

        #endregion

        #region Private Fields
        #endregion
    }

    /// <exclude />
    public delegate ServiceResult RegisterApplicationMethodStateMethodCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        ApplicationRecordDataType application,
        ref NodeId applicationId);

    /// <exclude />
    public partial class RegisterApplicationMethodStateResult
    {
        public ServiceResult ServiceResult { get; set; }
        public NodeId ApplicationId { get; set; }
    }

    /// <exclude />
    public delegate ValueTask<RegisterApplicationMethodStateResult> RegisterApplicationMethodStateMethodAsyncCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        ApplicationRecordDataType application,
        CancellationToken cancellationToken);
    #endif
    #endregion

    #region UpdateApplicationMethodState Class
    #if (!OPCUA_EXCLUDE_UpdateApplicationMethodState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class UpdateApplicationMethodState : MethodState
    {
        #region Constructors
        public UpdateApplicationMethodState(NodeState parent) : base(parent)
        {
        }

        public new static NodeState Construct(NodeState parent)
        {
            return new UpdateApplicationMethodState(parent);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAACAAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvR0RTL/////8EYYIKBAAAAAEAGwAAAFVw" +
           "ZGF0ZUFwcGxpY2F0aW9uTWV0aG9kVHlwZQEBugAALwEBugC6AAAAAQH/////AQAAABdgqQoCAAAAAAAO" +
           "AAAASW5wdXRBcmd1bWVudHMBAbsAAC4ARLsAAACWAQAAAAEAKgEBHAAAAAsAAABBcHBsaWNhdGlvbgEB" +
           "AQD/////AAAAAAABACgBAQAAAAEAAAABAAAAAQH/////AAAAAA==";
        #endregion
        #endif
        #endregion

        #region Event Callbacks
        public UpdateApplicationMethodStateMethodCallHandler OnCall;

        public UpdateApplicationMethodStateMethodAsyncCallHandler OnCallAsync;
        #endregion

        #region Public Properties
        #endregion

        #region Overridden Methods
        protected override ServiceResult Call(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments)
        {
            if (OnCall == null)
            {
                return base.Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            ServiceResult _result = null;

            ApplicationRecordDataType application = (ApplicationRecordDataType)ExtensionObject.ToEncodeable((ExtensionObject)_inputArguments[0]);

            if (OnCall != null)
            {
                _result = OnCall(
                    _context,
                    this,
                    _objectId,
                    application);
            }

            return _result;
        }

        #if (OPCUA_INCLUDE_ASYNC)
        protected override async ValueTask<ServiceResult> CallAsync(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments,
            CancellationToken cancellationToken = default)
        {
            if (OnCall == null && OnCallAsync == null)
            {
                return await base.CallAsync(_context, _objectId, _inputArguments, _outputArguments, cancellationToken).ConfigureAwait(false);
            }

            UpdateApplicationMethodStateResult _result = null;

            ApplicationRecordDataType application = (ApplicationRecordDataType)ExtensionObject.ToEncodeable((ExtensionObject)_inputArguments[0]);

            if (OnCallAsync != null)
            {
                _result = await OnCallAsync(
                    _context,
                    this,
                    _objectId,
                    application,
                    cancellationToken).ConfigureAwait(false);
            }
            else if (OnCall != null)
            {
                return Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            return _result.ServiceResult;
        }
        #endif

        #endregion

        #region Private Fields
        #endregion
    }

    /// <exclude />
    public delegate ServiceResult UpdateApplicationMethodStateMethodCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        ApplicationRecordDataType application);

    /// <exclude />
    public partial class UpdateApplicationMethodStateResult
    {
        public ServiceResult ServiceResult { get; set; }
    }

    /// <exclude />
    public delegate ValueTask<UpdateApplicationMethodStateResult> UpdateApplicationMethodStateMethodAsyncCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        ApplicationRecordDataType application,
        CancellationToken cancellationToken);
    #endif
    #endregion

    #region UnregisterApplicationMethodState Class
    #if (!OPCUA_EXCLUDE_UnregisterApplicationMethodState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class UnregisterApplicationMethodState : MethodState
    {
        #region Constructors
        public UnregisterApplicationMethodState(NodeState parent) : base(parent)
        {
        }

        public new static NodeState Construct(NodeState parent)
        {
            return new UnregisterApplicationMethodState(parent);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAACAAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvR0RTL/////8EYYIKBAAAAAEAHwAAAFVu" +
           "cmVnaXN0ZXJBcHBsaWNhdGlvbk1ldGhvZFR5cGUBAQgAAC8BAQgACAAAAAEB/////wEAAAAXYKkKAgAA" +
           "AAAADgAAAElucHV0QXJndW1lbnRzAQEJAAAuAEQJAAAAlgEAAAABACoBARwAAAANAAAAQXBwbGljYXRp" +
           "b25JZAAR/////wAAAAAAAQAoAQEAAAABAAAAAQAAAAEB/////wAAAAA=";
        #endregion
        #endif
        #endregion

        #region Event Callbacks
        public UnregisterApplicationMethodStateMethodCallHandler OnCall;

        public UnregisterApplicationMethodStateMethodAsyncCallHandler OnCallAsync;
        #endregion

        #region Public Properties
        #endregion

        #region Overridden Methods
        protected override ServiceResult Call(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments)
        {
            if (OnCall == null)
            {
                return base.Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            ServiceResult _result = null;

            NodeId applicationId = (NodeId)_inputArguments[0];

            if (OnCall != null)
            {
                _result = OnCall(
                    _context,
                    this,
                    _objectId,
                    applicationId);
            }

            return _result;
        }

        #if (OPCUA_INCLUDE_ASYNC)
        protected override async ValueTask<ServiceResult> CallAsync(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments,
            CancellationToken cancellationToken = default)
        {
            if (OnCall == null && OnCallAsync == null)
            {
                return await base.CallAsync(_context, _objectId, _inputArguments, _outputArguments, cancellationToken).ConfigureAwait(false);
            }

            UnregisterApplicationMethodStateResult _result = null;

            NodeId applicationId = (NodeId)_inputArguments[0];

            if (OnCallAsync != null)
            {
                _result = await OnCallAsync(
                    _context,
                    this,
                    _objectId,
                    applicationId,
                    cancellationToken).ConfigureAwait(false);
            }
            else if (OnCall != null)
            {
                return Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            return _result.ServiceResult;
        }
        #endif

        #endregion

        #region Private Fields
        #endregion
    }

    /// <exclude />
    public delegate ServiceResult UnregisterApplicationMethodStateMethodCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        NodeId applicationId);

    /// <exclude />
    public partial class UnregisterApplicationMethodStateResult
    {
        public ServiceResult ServiceResult { get; set; }
    }

    /// <exclude />
    public delegate ValueTask<UnregisterApplicationMethodStateResult> UnregisterApplicationMethodStateMethodAsyncCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        NodeId applicationId,
        CancellationToken cancellationToken);
    #endif
    #endregion

    #region GetApplicationMethodState Class
    #if (!OPCUA_EXCLUDE_GetApplicationMethodState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class GetApplicationMethodState : MethodState
    {
        #region Constructors
        public GetApplicationMethodState(NodeState parent) : base(parent)
        {
        }

        public new static NodeState Construct(NodeState parent)
        {
            return new GetApplicationMethodState(parent);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAACAAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvR0RTL/////8EYYIKBAAAAAEAGAAAAEdl" +
           "dEFwcGxpY2F0aW9uTWV0aG9kVHlwZQEBzwAALwEBzwDPAAAAAQH/////AgAAABdgqQoCAAAAAAAOAAAA" +
           "SW5wdXRBcmd1bWVudHMBAdAAAC4ARNAAAACWAQAAAAEAKgEBHAAAAA0AAABBcHBsaWNhdGlvbklkABH/" +
           "////AAAAAAABACgBAQAAAAEAAAABAAAAAQH/////AAAAABdgqQoCAAAAAAAPAAAAT3V0cHV0QXJndW1l" +
           "bnRzAQHRAAAuAETRAAAAlgEAAAABACoBARwAAAALAAAAQXBwbGljYXRpb24BAQEA/////wAAAAAAAQAo" +
           "AQEAAAABAAAAAQAAAAEB/////wAAAAA=";
        #endregion
        #endif
        #endregion

        #region Event Callbacks
        public GetApplicationMethodStateMethodCallHandler OnCall;

        public GetApplicationMethodStateMethodAsyncCallHandler OnCallAsync;
        #endregion

        #region Public Properties
        #endregion

        #region Overridden Methods
        protected override ServiceResult Call(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments)
        {
            if (OnCall == null)
            {
                return base.Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            ServiceResult _result = null;

            NodeId applicationId = (NodeId)_inputArguments[0];

            ApplicationRecordDataType application = (ApplicationRecordDataType)_outputArguments[0];

            if (OnCall != null)
            {
                _result = OnCall(
                    _context,
                    this,
                    _objectId,
                    applicationId,
                    ref application);
            }

            _outputArguments[0] = application;

            return _result;
        }

        #if (OPCUA_INCLUDE_ASYNC)
        protected override async ValueTask<ServiceResult> CallAsync(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments,
            CancellationToken cancellationToken = default)
        {
            if (OnCall == null && OnCallAsync == null)
            {
                return await base.CallAsync(_context, _objectId, _inputArguments, _outputArguments, cancellationToken).ConfigureAwait(false);
            }

            GetApplicationMethodStateResult _result = null;

            NodeId applicationId = (NodeId)_inputArguments[0];

            if (OnCallAsync != null)
            {
                _result = await OnCallAsync(
                    _context,
                    this,
                    _objectId,
                    applicationId,
                    cancellationToken).ConfigureAwait(false);
            }
            else if (OnCall != null)
            {
                return Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            _outputArguments[0] = _result.Application;

            return _result.ServiceResult;
        }
        #endif

        #endregion

        #region Private Fields
        #endregion
    }

    /// <exclude />
    public delegate ServiceResult GetApplicationMethodStateMethodCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        NodeId applicationId,
        ref ApplicationRecordDataType application);

    /// <exclude />
    public partial class GetApplicationMethodStateResult
    {
        public ServiceResult ServiceResult { get; set; }
        public ApplicationRecordDataType Application { get; set; }
    }

    /// <exclude />
    public delegate ValueTask<GetApplicationMethodStateResult> GetApplicationMethodStateMethodAsyncCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        NodeId applicationId,
        CancellationToken cancellationToken);
    #endif
    #endregion

    #region QueryApplicationsMethodState Class
    #if (!OPCUA_EXCLUDE_QueryApplicationsMethodState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class QueryApplicationsMethodState : MethodState
    {
        #region Constructors
        public QueryApplicationsMethodState(NodeState parent) : base(parent)
        {
        }

        public new static NodeState Construct(NodeState parent)
        {
            return new QueryApplicationsMethodState(parent);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAACAAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvR0RTL/////8EYYIKBAAAAAEAGwAAAFF1" +
           "ZXJ5QXBwbGljYXRpb25zTWV0aG9kVHlwZQEBYQMALwEBYQNhAwAAAQH/////AgAAABdgqQoCAAAAAAAO" +
           "AAAASW5wdXRBcmd1bWVudHMBAWIDAC4ARGIDAACWBwAAAAEAKgEBHwAAABAAAABTdGFydGluZ1JlY29y" +
           "ZElkAAf/////AAAAAAABACoBASEAAAASAAAATWF4UmVjb3Jkc1RvUmV0dXJuAAf/////AAAAAAABACoB" +
           "AR4AAAAPAAAAQXBwbGljYXRpb25OYW1lAAz/////AAAAAAABACoBAR0AAAAOAAAAQXBwbGljYXRpb25V" +
           "cmkADP////8AAAAAAAEAKgEBHgAAAA8AAABBcHBsaWNhdGlvblR5cGUAB/////8AAAAAAAEAKgEBGQAA" +
           "AAoAAABQcm9kdWN0VXJpAAz/////AAAAAAABACoBAR8AAAAMAAAAQ2FwYWJpbGl0aWVzAAwBAAAAAQAA" +
           "AAAAAAAAAQAoAQEAAAABAAAABwAAAAEB/////wAAAAAXYKkKAgAAAAAADwAAAE91dHB1dEFyZ3VtZW50" +
           "cwEBYwMALgBEYwMAAJYDAAAAAQAqAQElAAAAFAAAAExhc3RDb3VudGVyUmVzZXRUaW1lAQAmAf////8A" +
           "AAAAAAEAKgEBGwAAAAwAAABOZXh0UmVjb3JkSWQAB/////8AAAAAAAEAKgEBIQAAAAwAAABBcHBsaWNh" +
           "dGlvbnMBADQBAQAAAAEAAAAAAAAAAAEAKAEBAAAAAQAAAAMAAAABAf////8AAAAA";
        #endregion
        #endif
        #endregion

        #region Event Callbacks
        public QueryApplicationsMethodStateMethodCallHandler OnCall;

        public QueryApplicationsMethodStateMethodAsyncCallHandler OnCallAsync;
        #endregion

        #region Public Properties
        #endregion

        #region Overridden Methods
        protected override ServiceResult Call(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments)
        {
            if (OnCall == null)
            {
                return base.Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            ServiceResult _result = null;

            uint startingRecordId = (uint)_inputArguments[0];
            uint maxRecordsToReturn = (uint)_inputArguments[1];
            string applicationName = (string)_inputArguments[2];
            string applicationUri = (string)_inputArguments[3];
            uint applicationType = (uint)_inputArguments[4];
            string productUri = (string)_inputArguments[5];
            string[] capabilities = (string[])_inputArguments[6];

            DateTime lastCounterResetTime = (DateTime)_outputArguments[0];
            uint nextRecordId = (uint)_outputArguments[1];
            Opc.Ua.ApplicationDescription[] applications = (Opc.Ua.ApplicationDescription[])_outputArguments[2];

            if (OnCall != null)
            {
                _result = OnCall(
                    _context,
                    this,
                    _objectId,
                    startingRecordId,
                    maxRecordsToReturn,
                    applicationName,
                    applicationUri,
                    applicationType,
                    productUri,
                    capabilities,
                    ref lastCounterResetTime,
                    ref nextRecordId,
                    ref applications);
            }

            _outputArguments[0] = lastCounterResetTime;
            _outputArguments[1] = nextRecordId;
            _outputArguments[2] = applications;

            return _result;
        }

        #if (OPCUA_INCLUDE_ASYNC)
        protected override async ValueTask<ServiceResult> CallAsync(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments,
            CancellationToken cancellationToken = default)
        {
            if (OnCall == null && OnCallAsync == null)
            {
                return await base.CallAsync(_context, _objectId, _inputArguments, _outputArguments, cancellationToken).ConfigureAwait(false);
            }

            QueryApplicationsMethodStateResult _result = null;

            uint startingRecordId = (uint)_inputArguments[0];
            uint maxRecordsToReturn = (uint)_inputArguments[1];
            string applicationName = (string)_inputArguments[2];
            string applicationUri = (string)_inputArguments[3];
            uint applicationType = (uint)_inputArguments[4];
            string productUri = (string)_inputArguments[5];
            string[] capabilities = (string[])_inputArguments[6];

            if (OnCallAsync != null)
            {
                _result = await OnCallAsync(
                    _context,
                    this,
                    _objectId,
                    startingRecordId,
                    maxRecordsToReturn,
                    applicationName,
                    applicationUri,
                    applicationType,
                    productUri,
                    capabilities,
                    cancellationToken).ConfigureAwait(false);
            }
            else if (OnCall != null)
            {
                return Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            _outputArguments[0] = _result.LastCounterResetTime;
            _outputArguments[1] = _result.NextRecordId;
            _outputArguments[2] = _result.Applications;

            return _result.ServiceResult;
        }
        #endif

        #endregion

        #region Private Fields
        #endregion
    }

    /// <exclude />
    public delegate ServiceResult QueryApplicationsMethodStateMethodCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        uint startingRecordId,
        uint maxRecordsToReturn,
        string applicationName,
        string applicationUri,
        uint applicationType,
        string productUri,
        string[] capabilities,
        ref DateTime lastCounterResetTime,
        ref uint nextRecordId,
        ref Opc.Ua.ApplicationDescription[] applications);

    /// <exclude />
    public partial class QueryApplicationsMethodStateResult
    {
        public ServiceResult ServiceResult { get; set; }
        public DateTime LastCounterResetTime { get; set; }
        public uint NextRecordId { get; set; }
        public Opc.Ua.ApplicationDescription[] Applications { get; set; }
    }

    /// <exclude />
    public delegate ValueTask<QueryApplicationsMethodStateResult> QueryApplicationsMethodStateMethodAsyncCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        uint startingRecordId,
        uint maxRecordsToReturn,
        string applicationName,
        string applicationUri,
        uint applicationType,
        string productUri,
        string[] capabilities,
        CancellationToken cancellationToken);
    #endif
    #endregion

    #region QueryServersMethodState Class
    #if (!OPCUA_EXCLUDE_QueryServersMethodState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class QueryServersMethodState : MethodState
    {
        #region Constructors
        public QueryServersMethodState(NodeState parent) : base(parent)
        {
        }

        public new static NodeState Construct(NodeState parent)
        {
            return new QueryServersMethodState(parent);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAACAAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvR0RTL/////8EYYIKBAAAAAEAFgAAAFF1" +
           "ZXJ5U2VydmVyc01ldGhvZFR5cGUBAQoAAC8BAQoACgAAAAEB/////wIAAAAXYKkKAgAAAAAADgAAAElu" +
           "cHV0QXJndW1lbnRzAQELAAAuAEQLAAAAlgYAAAABACoBAR8AAAAQAAAAU3RhcnRpbmdSZWNvcmRJZAAH" +
           "/////wAAAAAAAQAqAQEhAAAAEgAAAE1heFJlY29yZHNUb1JldHVybgAH/////wAAAAAAAQAqAQEeAAAA" +
           "DwAAAEFwcGxpY2F0aW9uTmFtZQAM/////wAAAAAAAQAqAQEdAAAADgAAAEFwcGxpY2F0aW9uVXJpAAz/" +
           "////AAAAAAABACoBARkAAAAKAAAAUHJvZHVjdFVyaQAM/////wAAAAAAAQAqAQElAAAAEgAAAFNlcnZl" +
           "ckNhcGFiaWxpdGllcwAMAQAAAAEAAAAAAAAAAAEAKAEBAAAAAQAAAAYAAAABAf////8AAAAAF2CpCgIA" +
           "AAAAAA8AAABPdXRwdXRBcmd1bWVudHMBAQwAAC4ARAwAAACWAgAAAAEAKgEBJQAAABQAAABMYXN0Q291" +
           "bnRlclJlc2V0VGltZQEAJgH/////AAAAAAABACoBARwAAAAHAAAAU2VydmVycwEAnS8BAAAAAQAAAAAA" +
           "AAAAAQAoAQEAAAABAAAAAgAAAAEB/////wAAAAA=";
        #endregion
        #endif
        #endregion

        #region Event Callbacks
        public QueryServersMethodStateMethodCallHandler OnCall;

        public QueryServersMethodStateMethodAsyncCallHandler OnCallAsync;
        #endregion

        #region Public Properties
        #endregion

        #region Overridden Methods
        protected override ServiceResult Call(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments)
        {
            if (OnCall == null)
            {
                return base.Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            ServiceResult _result = null;

            uint startingRecordId = (uint)_inputArguments[0];
            uint maxRecordsToReturn = (uint)_inputArguments[1];
            string applicationName = (string)_inputArguments[2];
            string applicationUri = (string)_inputArguments[3];
            string productUri = (string)_inputArguments[4];
            string[] serverCapabilities = (string[])_inputArguments[5];

            DateTime lastCounterResetTime = (DateTime)_outputArguments[0];
            Opc.Ua.ServerOnNetwork[] servers = (Opc.Ua.ServerOnNetwork[])_outputArguments[1];

            if (OnCall != null)
            {
                _result = OnCall(
                    _context,
                    this,
                    _objectId,
                    startingRecordId,
                    maxRecordsToReturn,
                    applicationName,
                    applicationUri,
                    productUri,
                    serverCapabilities,
                    ref lastCounterResetTime,
                    ref servers);
            }

            _outputArguments[0] = lastCounterResetTime;
            _outputArguments[1] = servers;

            return _result;
        }

        #if (OPCUA_INCLUDE_ASYNC)
        protected override async ValueTask<ServiceResult> CallAsync(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments,
            CancellationToken cancellationToken = default)
        {
            if (OnCall == null && OnCallAsync == null)
            {
                return await base.CallAsync(_context, _objectId, _inputArguments, _outputArguments, cancellationToken).ConfigureAwait(false);
            }

            QueryServersMethodStateResult _result = null;

            uint startingRecordId = (uint)_inputArguments[0];
            uint maxRecordsToReturn = (uint)_inputArguments[1];
            string applicationName = (string)_inputArguments[2];
            string applicationUri = (string)_inputArguments[3];
            string productUri = (string)_inputArguments[4];
            string[] serverCapabilities = (string[])_inputArguments[5];

            if (OnCallAsync != null)
            {
                _result = await OnCallAsync(
                    _context,
                    this,
                    _objectId,
                    startingRecordId,
                    maxRecordsToReturn,
                    applicationName,
                    applicationUri,
                    productUri,
                    serverCapabilities,
                    cancellationToken).ConfigureAwait(false);
            }
            else if (OnCall != null)
            {
                return Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            _outputArguments[0] = _result.LastCounterResetTime;
            _outputArguments[1] = _result.Servers;

            return _result.ServiceResult;
        }
        #endif

        #endregion

        #region Private Fields
        #endregion
    }

    /// <exclude />
    public delegate ServiceResult QueryServersMethodStateMethodCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        uint startingRecordId,
        uint maxRecordsToReturn,
        string applicationName,
        string applicationUri,
        string productUri,
        string[] serverCapabilities,
        ref DateTime lastCounterResetTime,
        ref Opc.Ua.ServerOnNetwork[] servers);

    /// <exclude />
    public partial class QueryServersMethodStateResult
    {
        public ServiceResult ServiceResult { get; set; }
        public DateTime LastCounterResetTime { get; set; }
        public Opc.Ua.ServerOnNetwork[] Servers { get; set; }
    }

    /// <exclude />
    public delegate ValueTask<QueryServersMethodStateResult> QueryServersMethodStateMethodAsyncCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        uint startingRecordId,
        uint maxRecordsToReturn,
        string applicationName,
        string applicationUri,
        string productUri,
        string[] serverCapabilities,
        CancellationToken cancellationToken);
    #endif
    #endregion

    #region DirectoryState Class
    #if (!OPCUA_EXCLUDE_DirectoryState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class DirectoryState : FolderState
    {
        #region Constructors
        public DirectoryState(NodeState parent) : base(parent)
        {
        }

        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(Opc.Ua.Gds.ObjectTypes.DirectoryType, Opc.Ua.Gds.Namespaces.OpcUaGds, namespaceUris);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAACAAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvR0RTL/////8EYIACAQAAAAEAFQAAAERp" +
           "cmVjdG9yeVR5cGVJbnN0YW5jZQEBDQABAQ0ADQAAAP////8IAAAABGCACgEAAAABAAwAAABBcHBsaWNh" +
           "dGlvbnMBAQ4AAC8APQ4AAAD/////AAAAAARhggoEAAAAAQAQAAAARmluZEFwcGxpY2F0aW9ucwEBDwAA" +
           "LwEBDwAPAAAAAQH/////AgAAABdgqQoCAAAAAAAOAAAASW5wdXRBcmd1bWVudHMBARAAAC4ARBAAAACW" +
           "AQAAAAEAKgEBHQAAAA4AAABBcHBsaWNhdGlvblVyaQAM/////wAAAAAAAQAoAQEAAAABAAAAAQAAAAEB" +
           "/////wAAAAAXYKkKAgAAAAAADwAAAE91dHB1dEFyZ3VtZW50cwEBEQAALgBEEQAAAJYBAAAAAQAqAQEh" +
           "AAAADAAAAEFwcGxpY2F0aW9ucwEBAQABAAAAAQAAAAAAAAAAAQAoAQEAAAABAAAAAQAAAAEB/////wAA" +
           "AAAEYYIKBAAAAAEAEwAAAFJlZ2lzdGVyQXBwbGljYXRpb24BARIAAC8BARIAEgAAAAEBAQAAAAApAAEB" +
           "GgACAAAAF2CpCgIAAAAAAA4AAABJbnB1dEFyZ3VtZW50cwEBEwAALgBEEwAAAJYBAAAAAQAqAQEcAAAA" +
           "CwAAAEFwcGxpY2F0aW9uAQEBAP////8AAAAAAAEAKAEBAAAAAQAAAAEAAAABAf////8AAAAAF2CpCgIA" +
           "AAAAAA8AAABPdXRwdXRBcmd1bWVudHMBARQAAC4ARBQAAACWAQAAAAEAKgEBHAAAAA0AAABBcHBsaWNh" +
           "dGlvbklkABH/////AAAAAAABACgBAQAAAAEAAAABAAAAAQH/////AAAAAARhggoEAAAAAQARAAAAVXBk" +
           "YXRlQXBwbGljYXRpb24BAbwAAC8BAbwAvAAAAAEB/////wEAAAAXYKkKAgAAAAAADgAAAElucHV0QXJn" +
           "dW1lbnRzAQG9AAAuAES9AAAAlgEAAAABACoBARwAAAALAAAAQXBwbGljYXRpb24BAQEA/////wAAAAAA" +
           "AQAoAQEAAAABAAAAAQAAAAEB/////wAAAAAEYYIKBAAAAAEAFQAAAFVucmVnaXN0ZXJBcHBsaWNhdGlv" +
           "bgEBFQAALwEBFQAVAAAAAQH/////AQAAABdgqQoCAAAAAAAOAAAASW5wdXRBcmd1bWVudHMBARYAAC4A" +
           "RBYAAACWAQAAAAEAKgEBHAAAAA0AAABBcHBsaWNhdGlvbklkABH/////AAAAAAABACgBAQAAAAEAAAAB" +
           "AAAAAQH/////AAAAAARhggoEAAAAAQAOAAAAR2V0QXBwbGljYXRpb24BAdIAAC8BAdIA0gAAAAEB////" +
           "/wIAAAAXYKkKAgAAAAAADgAAAElucHV0QXJndW1lbnRzAQHTAAAuAETTAAAAlgEAAAABACoBARwAAAAN" +
           "AAAAQXBwbGljYXRpb25JZAAR/////wAAAAAAAQAoAQEAAAABAAAAAQAAAAEB/////wAAAAAXYKkKAgAA" +
           "AAAADwAAAE91dHB1dEFyZ3VtZW50cwEB1AAALgBE1AAAAJYBAAAAAQAqAQEcAAAACwAAAEFwcGxpY2F0" +
           "aW9uAQEBAP////8AAAAAAAEAKAEBAAAAAQAAAAEAAAABAf////8AAAAABGGCCgQAAAABABEAAABRdWVy" +
           "eUFwcGxpY2F0aW9ucwEBZAMALwEBZANkAwAAAQH/////AgAAABdgqQoCAAAAAAAOAAAASW5wdXRBcmd1" +
           "bWVudHMBAWUDAC4ARGUDAACWBwAAAAEAKgEBHwAAABAAAABTdGFydGluZ1JlY29yZElkAAf/////AAAA" +
           "AAABACoBASEAAAASAAAATWF4UmVjb3Jkc1RvUmV0dXJuAAf/////AAAAAAABACoBAR4AAAAPAAAAQXBw" +
           "bGljYXRpb25OYW1lAAz/////AAAAAAABACoBAR0AAAAOAAAAQXBwbGljYXRpb25VcmkADP////8AAAAA" +
           "AAEAKgEBHgAAAA8AAABBcHBsaWNhdGlvblR5cGUAB/////8AAAAAAAEAKgEBGQAAAAoAAABQcm9kdWN0" +
           "VXJpAAz/////AAAAAAABACoBAR8AAAAMAAAAQ2FwYWJpbGl0aWVzAAwBAAAAAQAAAAAAAAAAAQAoAQEA" +
           "AAABAAAABwAAAAEB/////wAAAAAXYKkKAgAAAAAADwAAAE91dHB1dEFyZ3VtZW50cwEBZgMALgBEZgMA" +
           "AJYDAAAAAQAqAQElAAAAFAAAAExhc3RDb3VudGVyUmVzZXRUaW1lAQAmAf////8AAAAAAAEAKgEBGwAA" +
           "AAwAAABOZXh0UmVjb3JkSWQAB/////8AAAAAAAEAKgEBIQAAAAwAAABBcHBsaWNhdGlvbnMBADQBAQAA" +
           "AAEAAAAAAAAAAAEAKAEBAAAAAQAAAAMAAAABAf////8AAAAABGGCCgQAAAABAAwAAABRdWVyeVNlcnZl" +
           "cnMBARcAAC8BARcAFwAAAAEB/////wIAAAAXYKkKAgAAAAAADgAAAElucHV0QXJndW1lbnRzAQEYAAAu" +
           "AEQYAAAAlgYAAAABACoBAR8AAAAQAAAAU3RhcnRpbmdSZWNvcmRJZAAH/////wAAAAAAAQAqAQEhAAAA" +
           "EgAAAE1heFJlY29yZHNUb1JldHVybgAH/////wAAAAAAAQAqAQEeAAAADwAAAEFwcGxpY2F0aW9uTmFt" +
           "ZQAM/////wAAAAAAAQAqAQEdAAAADgAAAEFwcGxpY2F0aW9uVXJpAAz/////AAAAAAABACoBARkAAAAK" +
           "AAAAUHJvZHVjdFVyaQAM/////wAAAAAAAQAqAQElAAAAEgAAAFNlcnZlckNhcGFiaWxpdGllcwAMAQAA" +
           "AAEAAAAAAAAAAAEAKAEBAAAAAQAAAAYAAAABAf////8AAAAAF2CpCgIAAAAAAA8AAABPdXRwdXRBcmd1" +
           "bWVudHMBARkAAC4ARBkAAACWAgAAAAEAKgEBJQAAABQAAABMYXN0Q291bnRlclJlc2V0VGltZQEAJgH/" +
           "////AAAAAAABACoBARwAAAAHAAAAU2VydmVycwEAnS8BAAAAAQAAAAAAAAAAAQAoAQEAAAABAAAAAgAA" +
           "AAEB/////wAAAAA=";
        #endregion
        #endif
        #endregion

        #region Public Properties
        public FolderState Applications
        {
            get => m_applications;

            set
            {
                if (!Object.ReferenceEquals(m_applications, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_applications = value;
            }
        }

        public FindApplicationsMethodState FindApplications
        {
            get => m_findApplicationsMethod;

            set
            {
                if (!Object.ReferenceEquals(m_findApplicationsMethod, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_findApplicationsMethod = value;
            }
        }

        public RegisterApplicationMethodState RegisterApplication
        {
            get => m_registerApplicationMethod;

            set
            {
                if (!Object.ReferenceEquals(m_registerApplicationMethod, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_registerApplicationMethod = value;
            }
        }

        public UpdateApplicationMethodState UpdateApplication
        {
            get => m_updateApplicationMethod;

            set
            {
                if (!Object.ReferenceEquals(m_updateApplicationMethod, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_updateApplicationMethod = value;
            }
        }

        public UnregisterApplicationMethodState UnregisterApplication
        {
            get => m_unregisterApplicationMethod;

            set
            {
                if (!Object.ReferenceEquals(m_unregisterApplicationMethod, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_unregisterApplicationMethod = value;
            }
        }

        public GetApplicationMethodState GetApplication
        {
            get => m_getApplicationMethod;

            set
            {
                if (!Object.ReferenceEquals(m_getApplicationMethod, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_getApplicationMethod = value;
            }
        }

        public QueryApplicationsMethodState QueryApplications
        {
            get => m_queryApplicationsMethod;

            set
            {
                if (!Object.ReferenceEquals(m_queryApplicationsMethod, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_queryApplicationsMethod = value;
            }
        }

        public QueryServersMethodState QueryServers
        {
            get => m_queryServersMethod;

            set
            {
                if (!Object.ReferenceEquals(m_queryServersMethod, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_queryServersMethod = value;
            }
        }
        #endregion

        #region Overridden Methods
        public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
        {
            if (m_applications != null)
            {
                children.Add(m_applications);
            }

            if (m_findApplicationsMethod != null)
            {
                children.Add(m_findApplicationsMethod);
            }

            if (m_registerApplicationMethod != null)
            {
                children.Add(m_registerApplicationMethod);
            }

            if (m_updateApplicationMethod != null)
            {
                children.Add(m_updateApplicationMethod);
            }

            if (m_unregisterApplicationMethod != null)
            {
                children.Add(m_unregisterApplicationMethod);
            }

            if (m_getApplicationMethod != null)
            {
                children.Add(m_getApplicationMethod);
            }

            if (m_queryApplicationsMethod != null)
            {
                children.Add(m_queryApplicationsMethod);
            }

            if (m_queryServersMethod != null)
            {
                children.Add(m_queryServersMethod);
            }

            base.GetChildren(context, children);
        }
            
        protected override void RemoveExplicitlyDefinedChild(BaseInstanceState child)
        {
            if (Object.ReferenceEquals(m_applications, child))
            {
                m_applications = null;
                return;
            }

            if (Object.ReferenceEquals(m_findApplicationsMethod, child))
            {
                m_findApplicationsMethod = null;
                return;
            }

            if (Object.ReferenceEquals(m_registerApplicationMethod, child))
            {
                m_registerApplicationMethod = null;
                return;
            }

            if (Object.ReferenceEquals(m_updateApplicationMethod, child))
            {
                m_updateApplicationMethod = null;
                return;
            }

            if (Object.ReferenceEquals(m_unregisterApplicationMethod, child))
            {
                m_unregisterApplicationMethod = null;
                return;
            }

            if (Object.ReferenceEquals(m_getApplicationMethod, child))
            {
                m_getApplicationMethod = null;
                return;
            }

            if (Object.ReferenceEquals(m_queryApplicationsMethod, child))
            {
                m_queryApplicationsMethod = null;
                return;
            }

            if (Object.ReferenceEquals(m_queryServersMethod, child))
            {
                m_queryServersMethod = null;
                return;
            }

            base.RemoveExplicitlyDefinedChild(child);
        }

        protected override BaseInstanceState FindChild(
            ISystemContext context,
            QualifiedName browseName,
            bool createOrReplace,
            BaseInstanceState replacement)
        {
            if (QualifiedName.IsNull(browseName))
            {
                return null;
            }

            BaseInstanceState instance = null;

            switch (browseName.Name)
            {
                case Opc.Ua.Gds.BrowseNames.Applications:
                {
                    if (createOrReplace)
                    {
                        if (Applications == null)
                        {
                            if (replacement == null)
                            {
                                Applications = new FolderState(this);
                            }
                            else
                            {
                                Applications = (FolderState)replacement;
                            }
                        }
                    }

                    instance = Applications;
                    break;
                }

                case Opc.Ua.Gds.BrowseNames.FindApplications:
                {
                    if (createOrReplace)
                    {
                        if (FindApplications == null)
                        {
                            if (replacement == null)
                            {
                                FindApplications = new FindApplicationsMethodState(this);
                            }
                            else
                            {
                                FindApplications = (FindApplicationsMethodState)replacement;
                            }
                        }
                    }

                    instance = FindApplications;
                    break;
                }

                case Opc.Ua.Gds.BrowseNames.RegisterApplication:
                {
                    if (createOrReplace)
                    {
                        if (RegisterApplication == null)
                        {
                            if (replacement == null)
                            {
                                RegisterApplication = new RegisterApplicationMethodState(this);
                            }
                            else
                            {
                                RegisterApplication = (RegisterApplicationMethodState)replacement;
                            }
                        }
                    }

                    instance = RegisterApplication;
                    break;
                }

                case Opc.Ua.Gds.BrowseNames.UpdateApplication:
                {
                    if (createOrReplace)
                    {
                        if (UpdateApplication == null)
                        {
                            if (replacement == null)
                            {
                                UpdateApplication = new UpdateApplicationMethodState(this);
                            }
                            else
                            {
                                UpdateApplication = (UpdateApplicationMethodState)replacement;
                            }
                        }
                    }

                    instance = UpdateApplication;
                    break;
                }

                case Opc.Ua.Gds.BrowseNames.UnregisterApplication:
                {
                    if (createOrReplace)
                    {
                        if (UnregisterApplication == null)
                        {
                            if (replacement == null)
                            {
                                UnregisterApplication = new UnregisterApplicationMethodState(this);
                            }
                            else
                            {
                                UnregisterApplication = (UnregisterApplicationMethodState)replacement;
                            }
                        }
                    }

                    instance = UnregisterApplication;
                    break;
                }

                case Opc.Ua.Gds.BrowseNames.GetApplication:
                {
                    if (createOrReplace)
                    {
                        if (GetApplication == null)
                        {
                            if (replacement == null)
                            {
                                GetApplication = new GetApplicationMethodState(this);
                            }
                            else
                            {
                                GetApplication = (GetApplicationMethodState)replacement;
                            }
                        }
                    }

                    instance = GetApplication;
                    break;
                }

                case Opc.Ua.Gds.BrowseNames.QueryApplications:
                {
                    if (createOrReplace)
                    {
                        if (QueryApplications == null)
                        {
                            if (replacement == null)
                            {
                                QueryApplications = new QueryApplicationsMethodState(this);
                            }
                            else
                            {
                                QueryApplications = (QueryApplicationsMethodState)replacement;
                            }
                        }
                    }

                    instance = QueryApplications;
                    break;
                }

                case Opc.Ua.Gds.BrowseNames.QueryServers:
                {
                    if (createOrReplace)
                    {
                        if (QueryServers == null)
                        {
                            if (replacement == null)
                            {
                                QueryServers = new QueryServersMethodState(this);
                            }
                            else
                            {
                                QueryServers = (QueryServersMethodState)replacement;
                            }
                        }
                    }

                    instance = QueryServers;
                    break;
                }
            }

            if (instance != null)
            {
                return instance;
            }

            return base.FindChild(context, browseName, createOrReplace, replacement);
        }
        #endregion

        #region Private Fields
        private FolderState m_applications;
        private FindApplicationsMethodState m_findApplicationsMethod;
        private RegisterApplicationMethodState m_registerApplicationMethod;
        private UpdateApplicationMethodState m_updateApplicationMethod;
        private UnregisterApplicationMethodState m_unregisterApplicationMethod;
        private GetApplicationMethodState m_getApplicationMethod;
        private QueryApplicationsMethodState m_queryApplicationsMethod;
        private QueryServersMethodState m_queryServersMethod;
        #endregion
    }
    #endif
    #endregion

    #region ApplicationRegistrationChangedAuditEventState Class
    #if (!OPCUA_EXCLUDE_ApplicationRegistrationChangedAuditEventState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class ApplicationRegistrationChangedAuditEventState : AuditUpdateMethodEventState
    {
        #region Constructors
        public ApplicationRegistrationChangedAuditEventState(NodeState parent) : base(parent)
        {
        }

        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(Opc.Ua.Gds.ObjectTypes.ApplicationRegistrationChangedAuditEventType, Opc.Ua.Gds.Namespaces.OpcUaGds, namespaceUris);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAACAAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvR0RTL/////8EYIACAQAAAAEANAAAAEFw" +
           "cGxpY2F0aW9uUmVnaXN0cmF0aW9uQ2hhbmdlZEF1ZGl0RXZlbnRUeXBlSW5zdGFuY2UBARoAAQEaABoA" +
           "AAD/////DwAAABVgiQoCAAAAAAAHAAAARXZlbnRJZAIBAEFCDwAALgBEQUIPAAAP/////wEB/////wAA" +
           "AAAVYIkKAgAAAAAACQAAAEV2ZW50VHlwZQIBAEJCDwAALgBEQkIPAAAR/////wEB/////wAAAAAVYIkK" +
           "AgAAAAAACgAAAFNvdXJjZU5vZGUCAQBDQg8AAC4ARENCDwAAEf////8BAf////8AAAAAFWCJCgIAAAAA" +
           "AAoAAABTb3VyY2VOYW1lAgEAREIPAAAuAEREQg8AAAz/////AQH/////AAAAABVgiQoCAAAAAAAEAAAA" +
           "VGltZQIBAEVCDwAALgBERUIPAAEAJgH/////AQH/////AAAAABVgiQoCAAAAAAALAAAAUmVjZWl2ZVRp" +
           "bWUCAQBGQg8AAC4AREZCDwABACYB/////wEB/////wAAAAAVYIkKAgAAAAAABwAAAE1lc3NhZ2UCAQBI" +
           "Qg8AAC4AREhCDwAAFf////8BAf////8AAAAAFWCJCgIAAAAAAAgAAABTZXZlcml0eQIBAElCDwAALgBE" +
           "SUIPAAAF/////wEB/////wAAAAAVYIkKAgAAAAAADwAAAEFjdGlvblRpbWVTdGFtcAIBAE5CDwAALgBE" +
           "TkIPAAEAJgH/////AQH/////AAAAABVgiQoCAAAAAAAGAAAAU3RhdHVzAgEAT0IPAAAuAERPQg8AAAH/" +
           "////AQH/////AAAAABVgiQoCAAAAAAAIAAAAU2VydmVySWQCAQBQQg8AAC4ARFBCDwAADP////8BAf//" +
           "//8AAAAAFWCJCgIAAAAAABIAAABDbGllbnRBdWRpdEVudHJ5SWQCAQBRQg8AAC4ARFFCDwAADP////8B" +
           "Af////8AAAAAFWCJCgIAAAAAAAwAAABDbGllbnRVc2VySWQCAQBSQg8AAC4ARFJCDwAADP////8BAf//" +
           "//8AAAAAFWCJCgIAAAAAAAgAAABNZXRob2RJZAIBAFRCDwAALgBEVEIPAAAR/////wEB/////wAAAAAX" +
           "YIkKAgAAAAAADgAAAElucHV0QXJndW1lbnRzAgEAVkIPAAAuAERWQg8AABgBAAAAAQAAAAAAAAABAf//" +
           "//8AAAAA";
        #endregion
        #endif
        #endregion

        #region Public Properties
        #endregion

        #region Overridden Methods
        #endregion

        #region Private Fields
        #endregion
    }
    #endif
    #endregion

    #region StartSigningRequestMethodState Class
    #if (!OPCUA_EXCLUDE_StartSigningRequestMethodState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class StartSigningRequestMethodState : MethodState
    {
        #region Constructors
        public StartSigningRequestMethodState(NodeState parent) : base(parent)
        {
        }

        public new static NodeState Construct(NodeState parent)
        {
            return new StartSigningRequestMethodState(parent);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAACAAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvR0RTL/////8EYYIKBAAAAAEAHQAAAFN0" +
           "YXJ0U2lnbmluZ1JlcXVlc3RNZXRob2RUeXBlAQEzAAAvAQEzADMAAAABAf////8CAAAAF2CpCgIAAAAA" +
           "AA4AAABJbnB1dEFyZ3VtZW50cwEBNAAALgBENAAAAJYEAAAAAQAqAQEcAAAADQAAAEFwcGxpY2F0aW9u" +
           "SWQAEf////8AAAAAAAEAKgEBIQAAABIAAABDZXJ0aWZpY2F0ZUdyb3VwSWQAEf////8AAAAAAAEAKgEB" +
           "IAAAABEAAABDZXJ0aWZpY2F0ZVR5cGVJZAAR/////wAAAAAAAQAqAQEhAAAAEgAAAENlcnRpZmljYXRl" +
           "UmVxdWVzdAAP/////wAAAAAAAQAoAQEAAAABAAAABAAAAAEB/////wAAAAAXYKkKAgAAAAAADwAAAE91" +
           "dHB1dEFyZ3VtZW50cwEBNQAALgBENQAAAJYBAAAAAQAqAQEYAAAACQAAAFJlcXVlc3RJZAAR/////wAA" +
           "AAAAAQAoAQEAAAABAAAAAQAAAAEB/////wAAAAA=";
        #endregion
        #endif
        #endregion

        #region Event Callbacks
        public StartSigningRequestMethodStateMethodCallHandler OnCall;

        public StartSigningRequestMethodStateMethodAsyncCallHandler OnCallAsync;
        #endregion

        #region Public Properties
        #endregion

        #region Overridden Methods
        protected override ServiceResult Call(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments)
        {
            if (OnCall == null)
            {
                return base.Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            ServiceResult _result = null;

            NodeId applicationId = (NodeId)_inputArguments[0];
            NodeId certificateGroupId = (NodeId)_inputArguments[1];
            NodeId certificateTypeId = (NodeId)_inputArguments[2];
            byte[] certificateRequest = (byte[])_inputArguments[3];

            NodeId requestId = (NodeId)_outputArguments[0];

            if (OnCall != null)
            {
                _result = OnCall(
                    _context,
                    this,
                    _objectId,
                    applicationId,
                    certificateGroupId,
                    certificateTypeId,
                    certificateRequest,
                    ref requestId);
            }

            _outputArguments[0] = requestId;

            return _result;
        }

        #if (OPCUA_INCLUDE_ASYNC)
        protected override async ValueTask<ServiceResult> CallAsync(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments,
            CancellationToken cancellationToken = default)
        {
            if (OnCall == null && OnCallAsync == null)
            {
                return await base.CallAsync(_context, _objectId, _inputArguments, _outputArguments, cancellationToken).ConfigureAwait(false);
            }

            StartSigningRequestMethodStateResult _result = null;

            NodeId applicationId = (NodeId)_inputArguments[0];
            NodeId certificateGroupId = (NodeId)_inputArguments[1];
            NodeId certificateTypeId = (NodeId)_inputArguments[2];
            byte[] certificateRequest = (byte[])_inputArguments[3];

            if (OnCallAsync != null)
            {
                _result = await OnCallAsync(
                    _context,
                    this,
                    _objectId,
                    applicationId,
                    certificateGroupId,
                    certificateTypeId,
                    certificateRequest,
                    cancellationToken).ConfigureAwait(false);
            }
            else if (OnCall != null)
            {
                return Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            _outputArguments[0] = _result.RequestId;

            return _result.ServiceResult;
        }
        #endif

        #endregion

        #region Private Fields
        #endregion
    }

    /// <exclude />
    public delegate ServiceResult StartSigningRequestMethodStateMethodCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        NodeId applicationId,
        NodeId certificateGroupId,
        NodeId certificateTypeId,
        byte[] certificateRequest,
        ref NodeId requestId);

    /// <exclude />
    public partial class StartSigningRequestMethodStateResult
    {
        public ServiceResult ServiceResult { get; set; }
        public NodeId RequestId { get; set; }
    }

    /// <exclude />
    public delegate ValueTask<StartSigningRequestMethodStateResult> StartSigningRequestMethodStateMethodAsyncCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        NodeId applicationId,
        NodeId certificateGroupId,
        NodeId certificateTypeId,
        byte[] certificateRequest,
        CancellationToken cancellationToken);
    #endif
    #endregion

    #region StartNewKeyPairRequestMethodState Class
    #if (!OPCUA_EXCLUDE_StartNewKeyPairRequestMethodState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class StartNewKeyPairRequestMethodState : MethodState
    {
        #region Constructors
        public StartNewKeyPairRequestMethodState(NodeState parent) : base(parent)
        {
        }

        public new static NodeState Construct(NodeState parent)
        {
            return new StartNewKeyPairRequestMethodState(parent);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAACAAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvR0RTL/////8EYYIKBAAAAAEAIAAAAFN0" +
           "YXJ0TmV3S2V5UGFpclJlcXVlc3RNZXRob2RUeXBlAQEwAAAvAQEwADAAAAABAf////8CAAAAF2CpCgIA" +
           "AAAAAA4AAABJbnB1dEFyZ3VtZW50cwEBMQAALgBEMQAAAJYHAAAAAQAqAQEcAAAADQAAAEFwcGxpY2F0" +
           "aW9uSWQAEf////8AAAAAAAEAKgEBIQAAABIAAABDZXJ0aWZpY2F0ZUdyb3VwSWQAEf////8AAAAAAAEA" +
           "KgEBIAAAABEAAABDZXJ0aWZpY2F0ZVR5cGVJZAAR/////wAAAAAAAQAqAQEaAAAACwAAAFN1YmplY3RO" +
           "YW1lAAz/////AAAAAAABACoBAR4AAAALAAAARG9tYWluTmFtZXMADAEAAAABAAAAAAAAAAABACoBAR8A" +
           "AAAQAAAAUHJpdmF0ZUtleUZvcm1hdAAM/////wAAAAAAAQAqAQEhAAAAEgAAAFByaXZhdGVLZXlQYXNz" +
           "d29yZAAM/////wAAAAAAAQAoAQEAAAABAAAABwAAAAEB/////wAAAAAXYKkKAgAAAAAADwAAAE91dHB1" +
           "dEFyZ3VtZW50cwEBMgAALgBEMgAAAJYBAAAAAQAqAQEYAAAACQAAAFJlcXVlc3RJZAAR/////wAAAAAA" +
           "AQAoAQEAAAABAAAAAQAAAAEB/////wAAAAA=";
        #endregion
        #endif
        #endregion

        #region Event Callbacks
        public StartNewKeyPairRequestMethodStateMethodCallHandler OnCall;

        public StartNewKeyPairRequestMethodStateMethodAsyncCallHandler OnCallAsync;
        #endregion

        #region Public Properties
        #endregion

        #region Overridden Methods
        protected override ServiceResult Call(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments)
        {
            if (OnCall == null)
            {
                return base.Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            ServiceResult _result = null;

            NodeId applicationId = (NodeId)_inputArguments[0];
            NodeId certificateGroupId = (NodeId)_inputArguments[1];
            NodeId certificateTypeId = (NodeId)_inputArguments[2];
            string subjectName = (string)_inputArguments[3];
            string[] domainNames = (string[])_inputArguments[4];
            string privateKeyFormat = (string)_inputArguments[5];
            string privateKeyPassword = (string)_inputArguments[6];

            NodeId requestId = (NodeId)_outputArguments[0];

            if (OnCall != null)
            {
                _result = OnCall(
                    _context,
                    this,
                    _objectId,
                    applicationId,
                    certificateGroupId,
                    certificateTypeId,
                    subjectName,
                    domainNames,
                    privateKeyFormat,
                    privateKeyPassword,
                    ref requestId);
            }

            _outputArguments[0] = requestId;

            return _result;
        }

        #if (OPCUA_INCLUDE_ASYNC)
        protected override async ValueTask<ServiceResult> CallAsync(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments,
            CancellationToken cancellationToken = default)
        {
            if (OnCall == null && OnCallAsync == null)
            {
                return await base.CallAsync(_context, _objectId, _inputArguments, _outputArguments, cancellationToken).ConfigureAwait(false);
            }

            StartNewKeyPairRequestMethodStateResult _result = null;

            NodeId applicationId = (NodeId)_inputArguments[0];
            NodeId certificateGroupId = (NodeId)_inputArguments[1];
            NodeId certificateTypeId = (NodeId)_inputArguments[2];
            string subjectName = (string)_inputArguments[3];
            string[] domainNames = (string[])_inputArguments[4];
            string privateKeyFormat = (string)_inputArguments[5];
            string privateKeyPassword = (string)_inputArguments[6];

            if (OnCallAsync != null)
            {
                _result = await OnCallAsync(
                    _context,
                    this,
                    _objectId,
                    applicationId,
                    certificateGroupId,
                    certificateTypeId,
                    subjectName,
                    domainNames,
                    privateKeyFormat,
                    privateKeyPassword,
                    cancellationToken).ConfigureAwait(false);
            }
            else if (OnCall != null)
            {
                return Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            _outputArguments[0] = _result.RequestId;

            return _result.ServiceResult;
        }
        #endif

        #endregion

        #region Private Fields
        #endregion
    }

    /// <exclude />
    public delegate ServiceResult StartNewKeyPairRequestMethodStateMethodCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        NodeId applicationId,
        NodeId certificateGroupId,
        NodeId certificateTypeId,
        string subjectName,
        string[] domainNames,
        string privateKeyFormat,
        string privateKeyPassword,
        ref NodeId requestId);

    /// <exclude />
    public partial class StartNewKeyPairRequestMethodStateResult
    {
        public ServiceResult ServiceResult { get; set; }
        public NodeId RequestId { get; set; }
    }

    /// <exclude />
    public delegate ValueTask<StartNewKeyPairRequestMethodStateResult> StartNewKeyPairRequestMethodStateMethodAsyncCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        NodeId applicationId,
        NodeId certificateGroupId,
        NodeId certificateTypeId,
        string subjectName,
        string[] domainNames,
        string privateKeyFormat,
        string privateKeyPassword,
        CancellationToken cancellationToken);
    #endif
    #endregion

    #region FinishRequestMethodState Class
    #if (!OPCUA_EXCLUDE_FinishRequestMethodState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class FinishRequestMethodState : MethodState
    {
        #region Constructors
        public FinishRequestMethodState(NodeState parent) : base(parent)
        {
        }

        public new static NodeState Construct(NodeState parent)
        {
            return new FinishRequestMethodState(parent);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAACAAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvR0RTL/////8EYYIKBAAAAAEAFwAAAEZp" +
           "bmlzaFJlcXVlc3RNZXRob2RUeXBlAQE5AAAvAQE5ADkAAAABAf////8CAAAAF2CpCgIAAAAAAA4AAABJ" +
           "bnB1dEFyZ3VtZW50cwEBOgAALgBEOgAAAJYCAAAAAQAqAQEcAAAADQAAAEFwcGxpY2F0aW9uSWQAEf//" +
           "//8AAAAAAAEAKgEBGAAAAAkAAABSZXF1ZXN0SWQAEf////8AAAAAAAEAKAEBAAAAAQAAAAIAAAABAf//" +
           "//8AAAAAF2CpCgIAAAAAAA8AAABPdXRwdXRBcmd1bWVudHMBATsAAC4ARDsAAACWAwAAAAEAKgEBGgAA" +
           "AAsAAABDZXJ0aWZpY2F0ZQAP/////wAAAAAAAQAqAQEZAAAACgAAAFByaXZhdGVLZXkAD/////8AAAAA" +
           "AAEAKgEBJQAAABIAAABJc3N1ZXJDZXJ0aWZpY2F0ZXMADwEAAAABAAAAAAAAAAABACgBAQAAAAEAAAAD" +
           "AAAAAQH/////AAAAAA==";
        #endregion
        #endif
        #endregion

        #region Event Callbacks
        public FinishRequestMethodStateMethodCallHandler OnCall;

        public FinishRequestMethodStateMethodAsyncCallHandler OnCallAsync;
        #endregion

        #region Public Properties
        #endregion

        #region Overridden Methods
        protected override ServiceResult Call(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments)
        {
            if (OnCall == null)
            {
                return base.Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            ServiceResult _result = null;

            NodeId applicationId = (NodeId)_inputArguments[0];
            NodeId requestId = (NodeId)_inputArguments[1];

            byte[] certificate = (byte[])_outputArguments[0];
            byte[] privateKey = (byte[])_outputArguments[1];
            byte[][] issuerCertificates = (byte[][])_outputArguments[2];

            if (OnCall != null)
            {
                _result = OnCall(
                    _context,
                    this,
                    _objectId,
                    applicationId,
                    requestId,
                    ref certificate,
                    ref privateKey,
                    ref issuerCertificates);
            }

            _outputArguments[0] = certificate;
            _outputArguments[1] = privateKey;
            _outputArguments[2] = issuerCertificates;

            return _result;
        }

        #if (OPCUA_INCLUDE_ASYNC)
        protected override async ValueTask<ServiceResult> CallAsync(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments,
            CancellationToken cancellationToken = default)
        {
            if (OnCall == null && OnCallAsync == null)
            {
                return await base.CallAsync(_context, _objectId, _inputArguments, _outputArguments, cancellationToken).ConfigureAwait(false);
            }

            FinishRequestMethodStateResult _result = null;

            NodeId applicationId = (NodeId)_inputArguments[0];
            NodeId requestId = (NodeId)_inputArguments[1];

            if (OnCallAsync != null)
            {
                _result = await OnCallAsync(
                    _context,
                    this,
                    _objectId,
                    applicationId,
                    requestId,
                    cancellationToken).ConfigureAwait(false);
            }
            else if (OnCall != null)
            {
                return Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            _outputArguments[0] = _result.Certificate;
            _outputArguments[1] = _result.PrivateKey;
            _outputArguments[2] = _result.IssuerCertificates;

            return _result.ServiceResult;
        }
        #endif

        #endregion

        #region Private Fields
        #endregion
    }

    /// <exclude />
    public delegate ServiceResult FinishRequestMethodStateMethodCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        NodeId applicationId,
        NodeId requestId,
        ref byte[] certificate,
        ref byte[] privateKey,
        ref byte[][] issuerCertificates);

    /// <exclude />
    public partial class FinishRequestMethodStateResult
    {
        public ServiceResult ServiceResult { get; set; }
        public byte[] Certificate { get; set; }
        public byte[] PrivateKey { get; set; }
        public byte[][] IssuerCertificates { get; set; }
    }

    /// <exclude />
    public delegate ValueTask<FinishRequestMethodStateResult> FinishRequestMethodStateMethodAsyncCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        NodeId applicationId,
        NodeId requestId,
        CancellationToken cancellationToken);
    #endif
    #endregion

    #region GetCertificateGroupsMethodState Class
    #if (!OPCUA_EXCLUDE_GetCertificateGroupsMethodState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class GetCertificateGroupsMethodState : MethodState
    {
        #region Constructors
        public GetCertificateGroupsMethodState(NodeState parent) : base(parent)
        {
        }

        public new static NodeState Construct(NodeState parent)
        {
            return new GetCertificateGroupsMethodState(parent);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAACAAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvR0RTL/////8EYYIKBAAAAAEAHgAAAEdl" +
           "dENlcnRpZmljYXRlR3JvdXBzTWV0aG9kVHlwZQEB5gAALwEB5gDmAAAAAQH/////AgAAABdgqQoCAAAA" +
           "AAAOAAAASW5wdXRBcmd1bWVudHMBAecAAC4AROcAAACWAQAAAAEAKgEBHAAAAA0AAABBcHBsaWNhdGlv" +
           "bklkABH/////AAAAAAABACgBAQAAAAEAAAABAAAAAQH/////AAAAABdgqQoCAAAAAAAPAAAAT3V0cHV0" +
           "QXJndW1lbnRzAQHoAAAuAEToAAAAlgEAAAABACoBASYAAAATAAAAQ2VydGlmaWNhdGVHcm91cElkcwAR" +
           "AQAAAAEAAAAAAAAAAAEAKAEBAAAAAQAAAAEAAAABAf////8AAAAA";
        #endregion
        #endif
        #endregion

        #region Event Callbacks
        public GetCertificateGroupsMethodStateMethodCallHandler OnCall;

        public GetCertificateGroupsMethodStateMethodAsyncCallHandler OnCallAsync;
        #endregion

        #region Public Properties
        #endregion

        #region Overridden Methods
        protected override ServiceResult Call(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments)
        {
            if (OnCall == null)
            {
                return base.Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            ServiceResult _result = null;

            NodeId applicationId = (NodeId)_inputArguments[0];

            NodeId[] certificateGroupIds = (NodeId[])_outputArguments[0];

            if (OnCall != null)
            {
                _result = OnCall(
                    _context,
                    this,
                    _objectId,
                    applicationId,
                    ref certificateGroupIds);
            }

            _outputArguments[0] = certificateGroupIds;

            return _result;
        }

        #if (OPCUA_INCLUDE_ASYNC)
        protected override async ValueTask<ServiceResult> CallAsync(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments,
            CancellationToken cancellationToken = default)
        {
            if (OnCall == null && OnCallAsync == null)
            {
                return await base.CallAsync(_context, _objectId, _inputArguments, _outputArguments, cancellationToken).ConfigureAwait(false);
            }

            GetCertificateGroupsMethodStateResult _result = null;

            NodeId applicationId = (NodeId)_inputArguments[0];

            if (OnCallAsync != null)
            {
                _result = await OnCallAsync(
                    _context,
                    this,
                    _objectId,
                    applicationId,
                    cancellationToken).ConfigureAwait(false);
            }
            else if (OnCall != null)
            {
                return Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            _outputArguments[0] = _result.CertificateGroupIds;

            return _result.ServiceResult;
        }
        #endif

        #endregion

        #region Private Fields
        #endregion
    }

    /// <exclude />
    public delegate ServiceResult GetCertificateGroupsMethodStateMethodCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        NodeId applicationId,
        ref NodeId[] certificateGroupIds);

    /// <exclude />
    public partial class GetCertificateGroupsMethodStateResult
    {
        public ServiceResult ServiceResult { get; set; }
        public NodeId[] CertificateGroupIds { get; set; }
    }

    /// <exclude />
    public delegate ValueTask<GetCertificateGroupsMethodStateResult> GetCertificateGroupsMethodStateMethodAsyncCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        NodeId applicationId,
        CancellationToken cancellationToken);
    #endif
    #endregion

    #region GetTrustListMethodState Class
    #if (!OPCUA_EXCLUDE_GetTrustListMethodState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class GetTrustListMethodState : MethodState
    {
        #region Constructors
        public GetTrustListMethodState(NodeState parent) : base(parent)
        {
        }

        public new static NodeState Construct(NodeState parent)
        {
            return new GetTrustListMethodState(parent);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAACAAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvR0RTL/////8EYYIKBAAAAAEAFgAAAEdl" +
           "dFRydXN0TGlzdE1ldGhvZFR5cGUBAb4AAC8BAb4AvgAAAAEB/////wIAAAAXYKkKAgAAAAAADgAAAElu" +
           "cHV0QXJndW1lbnRzAQG/AAAuAES/AAAAlgIAAAABACoBARwAAAANAAAAQXBwbGljYXRpb25JZAAR////" +
           "/wAAAAAAAQAqAQEhAAAAEgAAAENlcnRpZmljYXRlR3JvdXBJZAAR/////wAAAAAAAQAoAQEAAAABAAAA" +
           "AgAAAAEB/////wAAAAAXYKkKAgAAAAAADwAAAE91dHB1dEFyZ3VtZW50cwEBwAAALgBEwAAAAJYBAAAA" +
           "AQAqAQEaAAAACwAAAFRydXN0TGlzdElkABH/////AAAAAAABACgBAQAAAAEAAAABAAAAAQH/////AAAA" +
           "AA==";
        #endregion
        #endif
        #endregion

        #region Event Callbacks
        public GetTrustListMethodStateMethodCallHandler OnCall;

        public GetTrustListMethodStateMethodAsyncCallHandler OnCallAsync;
        #endregion

        #region Public Properties
        #endregion

        #region Overridden Methods
        protected override ServiceResult Call(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments)
        {
            if (OnCall == null)
            {
                return base.Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            ServiceResult _result = null;

            NodeId applicationId = (NodeId)_inputArguments[0];
            NodeId certificateGroupId = (NodeId)_inputArguments[1];

            NodeId trustListId = (NodeId)_outputArguments[0];

            if (OnCall != null)
            {
                _result = OnCall(
                    _context,
                    this,
                    _objectId,
                    applicationId,
                    certificateGroupId,
                    ref trustListId);
            }

            _outputArguments[0] = trustListId;

            return _result;
        }

        #if (OPCUA_INCLUDE_ASYNC)
        protected override async ValueTask<ServiceResult> CallAsync(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments,
            CancellationToken cancellationToken = default)
        {
            if (OnCall == null && OnCallAsync == null)
            {
                return await base.CallAsync(_context, _objectId, _inputArguments, _outputArguments, cancellationToken).ConfigureAwait(false);
            }

            GetTrustListMethodStateResult _result = null;

            NodeId applicationId = (NodeId)_inputArguments[0];
            NodeId certificateGroupId = (NodeId)_inputArguments[1];

            if (OnCallAsync != null)
            {
                _result = await OnCallAsync(
                    _context,
                    this,
                    _objectId,
                    applicationId,
                    certificateGroupId,
                    cancellationToken).ConfigureAwait(false);
            }
            else if (OnCall != null)
            {
                return Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            _outputArguments[0] = _result.TrustListId;

            return _result.ServiceResult;
        }
        #endif

        #endregion

        #region Private Fields
        #endregion
    }

    /// <exclude />
    public delegate ServiceResult GetTrustListMethodStateMethodCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        NodeId applicationId,
        NodeId certificateGroupId,
        ref NodeId trustListId);

    /// <exclude />
    public partial class GetTrustListMethodStateResult
    {
        public ServiceResult ServiceResult { get; set; }
        public NodeId TrustListId { get; set; }
    }

    /// <exclude />
    public delegate ValueTask<GetTrustListMethodStateResult> GetTrustListMethodStateMethodAsyncCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        NodeId applicationId,
        NodeId certificateGroupId,
        CancellationToken cancellationToken);
    #endif
    #endregion

    #region RevokeCertificateMethodState Class
    #if (!OPCUA_EXCLUDE_RevokeCertificateMethodState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class RevokeCertificateMethodState : MethodState
    {
        #region Constructors
        public RevokeCertificateMethodState(NodeState parent) : base(parent)
        {
        }

        public new static NodeState Construct(NodeState parent)
        {
            return new RevokeCertificateMethodState(parent);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAACAAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvR0RTL/////8EYYIKBAAAAAEAGwAAAFJl" +
           "dm9rZUNlcnRpZmljYXRlTWV0aG9kVHlwZQEBmToALwEBmTqZOgAAAQH/////AQAAABdgqQoCAAAAAAAO" +
           "AAAASW5wdXRBcmd1bWVudHMBAZo6AC4ARJo6AACWAgAAAAEAKgEBHAAAAA0AAABBcHBsaWNhdGlvbklk" +
           "ABH/////AAAAAAABACoBARoAAAALAAAAQ2VydGlmaWNhdGUAD/////8AAAAAAAEAKAEBAAAAAQAAAAIA" +
           "AAABAf////8AAAAA";
        #endregion
        #endif
        #endregion

        #region Event Callbacks
        public RevokeCertificateMethodStateMethodCallHandler OnCall;

        public RevokeCertificateMethodStateMethodAsyncCallHandler OnCallAsync;
        #endregion

        #region Public Properties
        #endregion

        #region Overridden Methods
        protected override ServiceResult Call(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments)
        {
            if (OnCall == null)
            {
                return base.Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            ServiceResult _result = null;

            NodeId applicationId = (NodeId)_inputArguments[0];
            byte[] certificate = (byte[])_inputArguments[1];

            if (OnCall != null)
            {
                _result = OnCall(
                    _context,
                    this,
                    _objectId,
                    applicationId,
                    certificate);
            }

            return _result;
        }

        #if (OPCUA_INCLUDE_ASYNC)
        protected override async ValueTask<ServiceResult> CallAsync(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments,
            CancellationToken cancellationToken = default)
        {
            if (OnCall == null && OnCallAsync == null)
            {
                return await base.CallAsync(_context, _objectId, _inputArguments, _outputArguments, cancellationToken).ConfigureAwait(false);
            }

            RevokeCertificateMethodStateResult _result = null;

            NodeId applicationId = (NodeId)_inputArguments[0];
            byte[] certificate = (byte[])_inputArguments[1];

            if (OnCallAsync != null)
            {
                _result = await OnCallAsync(
                    _context,
                    this,
                    _objectId,
                    applicationId,
                    certificate,
                    cancellationToken).ConfigureAwait(false);
            }
            else if (OnCall != null)
            {
                return Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            return _result.ServiceResult;
        }
        #endif

        #endregion

        #region Private Fields
        #endregion
    }

    /// <exclude />
    public delegate ServiceResult RevokeCertificateMethodStateMethodCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        NodeId applicationId,
        byte[] certificate);

    /// <exclude />
    public partial class RevokeCertificateMethodStateResult
    {
        public ServiceResult ServiceResult { get; set; }
    }

    /// <exclude />
    public delegate ValueTask<RevokeCertificateMethodStateResult> RevokeCertificateMethodStateMethodAsyncCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        NodeId applicationId,
        byte[] certificate,
        CancellationToken cancellationToken);
    #endif
    #endregion

    #region GetCertificateStatusMethodState Class
    #if (!OPCUA_EXCLUDE_GetCertificateStatusMethodState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class GetCertificateStatusMethodState : MethodState
    {
        #region Constructors
        public GetCertificateStatusMethodState(NodeState parent) : base(parent)
        {
        }

        public new static NodeState Construct(NodeState parent)
        {
            return new GetCertificateStatusMethodState(parent);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAACAAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvR0RTL/////8EYYIKBAAAAAEAHgAAAEdl" +
           "dENlcnRpZmljYXRlU3RhdHVzTWV0aG9kVHlwZQEB2wAALwEB2wDbAAAAAQH/////AgAAABdgqQoCAAAA" +
           "AAAOAAAASW5wdXRBcmd1bWVudHMBAdwAAC4ARNwAAACWAwAAAAEAKgEBHAAAAA0AAABBcHBsaWNhdGlv" +
           "bklkABH/////AAAAAAABACoBASEAAAASAAAAQ2VydGlmaWNhdGVHcm91cElkABH/////AAAAAAABACoB" +
           "ASAAAAARAAAAQ2VydGlmaWNhdGVUeXBlSWQAEf////8AAAAAAAEAKAEBAAAAAQAAAAMAAAABAf////8A" +
           "AAAAF2CpCgIAAAAAAA8AAABPdXRwdXRBcmd1bWVudHMBAd0AAC4ARN0AAACWAQAAAAEAKgEBHQAAAA4A" +
           "AABVcGRhdGVSZXF1aXJlZAAB/////wAAAAAAAQAoAQEAAAABAAAAAQAAAAEB/////wAAAAA=";
        #endregion
        #endif
        #endregion

        #region Event Callbacks
        public GetCertificateStatusMethodStateMethodCallHandler OnCall;

        public GetCertificateStatusMethodStateMethodAsyncCallHandler OnCallAsync;
        #endregion

        #region Public Properties
        #endregion

        #region Overridden Methods
        protected override ServiceResult Call(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments)
        {
            if (OnCall == null)
            {
                return base.Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            ServiceResult _result = null;

            NodeId applicationId = (NodeId)_inputArguments[0];
            NodeId certificateGroupId = (NodeId)_inputArguments[1];
            NodeId certificateTypeId = (NodeId)_inputArguments[2];

            bool updateRequired = (bool)_outputArguments[0];

            if (OnCall != null)
            {
                _result = OnCall(
                    _context,
                    this,
                    _objectId,
                    applicationId,
                    certificateGroupId,
                    certificateTypeId,
                    ref updateRequired);
            }

            _outputArguments[0] = updateRequired;

            return _result;
        }

        #if (OPCUA_INCLUDE_ASYNC)
        protected override async ValueTask<ServiceResult> CallAsync(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments,
            CancellationToken cancellationToken = default)
        {
            if (OnCall == null && OnCallAsync == null)
            {
                return await base.CallAsync(_context, _objectId, _inputArguments, _outputArguments, cancellationToken).ConfigureAwait(false);
            }

            GetCertificateStatusMethodStateResult _result = null;

            NodeId applicationId = (NodeId)_inputArguments[0];
            NodeId certificateGroupId = (NodeId)_inputArguments[1];
            NodeId certificateTypeId = (NodeId)_inputArguments[2];

            if (OnCallAsync != null)
            {
                _result = await OnCallAsync(
                    _context,
                    this,
                    _objectId,
                    applicationId,
                    certificateGroupId,
                    certificateTypeId,
                    cancellationToken).ConfigureAwait(false);
            }
            else if (OnCall != null)
            {
                return Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            _outputArguments[0] = _result.UpdateRequired;

            return _result.ServiceResult;
        }
        #endif

        #endregion

        #region Private Fields
        #endregion
    }

    /// <exclude />
    public delegate ServiceResult GetCertificateStatusMethodStateMethodCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        NodeId applicationId,
        NodeId certificateGroupId,
        NodeId certificateTypeId,
        ref bool updateRequired);

    /// <exclude />
    public partial class GetCertificateStatusMethodStateResult
    {
        public ServiceResult ServiceResult { get; set; }
        public bool UpdateRequired { get; set; }
    }

    /// <exclude />
    public delegate ValueTask<GetCertificateStatusMethodStateResult> GetCertificateStatusMethodStateMethodAsyncCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        NodeId applicationId,
        NodeId certificateGroupId,
        NodeId certificateTypeId,
        CancellationToken cancellationToken);
    #endif
    #endregion

    #region GetCertificatesMethodState Class
    #if (!OPCUA_EXCLUDE_GetCertificatesMethodState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class GetCertificatesMethodState : MethodState
    {
        #region Constructors
        public GetCertificatesMethodState(NodeState parent) : base(parent)
        {
        }

        public new static NodeState Construct(NodeState parent)
        {
            return new GetCertificatesMethodState(parent);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAACAAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvR0RTL/////8EYYIKBAAAAAEAGQAAAEdl" +
           "dENlcnRpZmljYXRlc01ldGhvZFR5cGUBASsAAC8BASsAKwAAAAEB/////wIAAAAXYKkKAgAAAAAADgAA" +
           "AElucHV0QXJndW1lbnRzAQEsAAAuAEQsAAAAlgIAAAABACoBARwAAAANAAAAQXBwbGljYXRpb25JZAAR" +
           "/////wAAAAAAAQAqAQEhAAAAEgAAAENlcnRpZmljYXRlR3JvdXBJZAAR/////wAAAAAAAQAoAQEAAAAB" +
           "AAAAAgAAAAEB/////wAAAAAXYKkKAgAAAAAADwAAAE91dHB1dEFyZ3VtZW50cwEBLQAALgBELQAAAJYC" +
           "AAAAAQAqAQElAAAAEgAAAENlcnRpZmljYXRlVHlwZUlkcwARAQAAAAEAAAAAAAAAAAEAKgEBHwAAAAwA" +
           "AABDZXJ0aWZpY2F0ZXMADwEAAAABAAAAAAAAAAABACgBAQAAAAEAAAACAAAAAQH/////AAAAAA==";
        #endregion
        #endif
        #endregion

        #region Event Callbacks
        public GetCertificatesMethodStateMethodCallHandler OnCall;

        public GetCertificatesMethodStateMethodAsyncCallHandler OnCallAsync;
        #endregion

        #region Public Properties
        #endregion

        #region Overridden Methods
        protected override ServiceResult Call(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments)
        {
            if (OnCall == null)
            {
                return base.Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            ServiceResult _result = null;

            NodeId applicationId = (NodeId)_inputArguments[0];
            NodeId certificateGroupId = (NodeId)_inputArguments[1];

            NodeId[] certificateTypeIds = (NodeId[])_outputArguments[0];
            byte[][] certificates = (byte[][])_outputArguments[1];

            if (OnCall != null)
            {
                _result = OnCall(
                    _context,
                    this,
                    _objectId,
                    applicationId,
                    certificateGroupId,
                    ref certificateTypeIds,
                    ref certificates);
            }

            _outputArguments[0] = certificateTypeIds;
            _outputArguments[1] = certificates;

            return _result;
        }

        #if (OPCUA_INCLUDE_ASYNC)
        protected override async ValueTask<ServiceResult> CallAsync(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments,
            CancellationToken cancellationToken = default)
        {
            if (OnCall == null && OnCallAsync == null)
            {
                return await base.CallAsync(_context, _objectId, _inputArguments, _outputArguments, cancellationToken).ConfigureAwait(false);
            }

            GetCertificatesMethodStateResult _result = null;

            NodeId applicationId = (NodeId)_inputArguments[0];
            NodeId certificateGroupId = (NodeId)_inputArguments[1];

            if (OnCallAsync != null)
            {
                _result = await OnCallAsync(
                    _context,
                    this,
                    _objectId,
                    applicationId,
                    certificateGroupId,
                    cancellationToken).ConfigureAwait(false);
            }
            else if (OnCall != null)
            {
                return Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            _outputArguments[0] = _result.CertificateTypeIds;
            _outputArguments[1] = _result.Certificates;

            return _result.ServiceResult;
        }
        #endif

        #endregion

        #region Private Fields
        #endregion
    }

    /// <exclude />
    public delegate ServiceResult GetCertificatesMethodStateMethodCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        NodeId applicationId,
        NodeId certificateGroupId,
        ref NodeId[] certificateTypeIds,
        ref byte[][] certificates);

    /// <exclude />
    public partial class GetCertificatesMethodStateResult
    {
        public ServiceResult ServiceResult { get; set; }
        public NodeId[] CertificateTypeIds { get; set; }
        public byte[][] Certificates { get; set; }
    }

    /// <exclude />
    public delegate ValueTask<GetCertificatesMethodStateResult> GetCertificatesMethodStateMethodAsyncCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        NodeId applicationId,
        NodeId certificateGroupId,
        CancellationToken cancellationToken);
    #endif
    #endregion

    #region CheckRevocationStatusMethodState Class
    #if (!OPCUA_EXCLUDE_CheckRevocationStatusMethodState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class CheckRevocationStatusMethodState : MethodState
    {
        #region Constructors
        public CheckRevocationStatusMethodState(NodeState parent) : base(parent)
        {
        }

        public new static NodeState Construct(NodeState parent)
        {
            return new CheckRevocationStatusMethodState(parent);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAACAAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvR0RTL/////8EYYIKBAAAAAEAHwAAAENo" +
           "ZWNrUmV2b2NhdGlvblN0YXR1c01ldGhvZFR5cGUBAS4AAC8BAS4ALgAAAAEB/////wIAAAAXYKkKAgAA" +
           "AAAADgAAAElucHV0QXJndW1lbnRzAQEvAAAuAEQvAAAAlgEAAAABACoBARoAAAALAAAAQ2VydGlmaWNh" +
           "dGUAD/////8AAAAAAAEAKAEBAAAAAQAAAAEAAAABAf////8AAAAAF2CpCgIAAAAAAA8AAABPdXRwdXRB" +
           "cmd1bWVudHMBATYAAC4ARDYAAACWAgAAAAEAKgEBIAAAABEAAABDZXJ0aWZpY2F0ZVN0YXR1cwAT////" +
           "/wAAAAAAAQAqAQEdAAAADAAAAFZhbGlkaXR5VGltZQEAJgH/////AAAAAAABACgBAQAAAAEAAAACAAAA" +
           "AQH/////AAAAAA==";
        #endregion
        #endif
        #endregion

        #region Event Callbacks
        public CheckRevocationStatusMethodStateMethodCallHandler OnCall;

        public CheckRevocationStatusMethodStateMethodAsyncCallHandler OnCallAsync;
        #endregion

        #region Public Properties
        #endregion

        #region Overridden Methods
        protected override ServiceResult Call(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments)
        {
            if (OnCall == null)
            {
                return base.Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            ServiceResult _result = null;

            byte[] certificate = (byte[])_inputArguments[0];

            StatusCode certificateStatus = (StatusCode)_outputArguments[0];
            DateTime validityTime = (DateTime)_outputArguments[1];

            if (OnCall != null)
            {
                _result = OnCall(
                    _context,
                    this,
                    _objectId,
                    certificate,
                    ref certificateStatus,
                    ref validityTime);
            }

            _outputArguments[0] = certificateStatus;
            _outputArguments[1] = validityTime;

            return _result;
        }

        #if (OPCUA_INCLUDE_ASYNC)
        protected override async ValueTask<ServiceResult> CallAsync(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments,
            CancellationToken cancellationToken = default)
        {
            if (OnCall == null && OnCallAsync == null)
            {
                return await base.CallAsync(_context, _objectId, _inputArguments, _outputArguments, cancellationToken).ConfigureAwait(false);
            }

            CheckRevocationStatusMethodStateResult _result = null;

            byte[] certificate = (byte[])_inputArguments[0];

            if (OnCallAsync != null)
            {
                _result = await OnCallAsync(
                    _context,
                    this,
                    _objectId,
                    certificate,
                    cancellationToken).ConfigureAwait(false);
            }
            else if (OnCall != null)
            {
                return Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            _outputArguments[0] = _result.CertificateStatus;
            _outputArguments[1] = _result.ValidityTime;

            return _result.ServiceResult;
        }
        #endif

        #endregion

        #region Private Fields
        #endregion
    }

    /// <exclude />
    public delegate ServiceResult CheckRevocationStatusMethodStateMethodCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        byte[] certificate,
        ref StatusCode certificateStatus,
        ref DateTime validityTime);

    /// <exclude />
    public partial class CheckRevocationStatusMethodStateResult
    {
        public ServiceResult ServiceResult { get; set; }
        public StatusCode CertificateStatus { get; set; }
        public DateTime ValidityTime { get; set; }
    }

    /// <exclude />
    public delegate ValueTask<CheckRevocationStatusMethodStateResult> CheckRevocationStatusMethodStateMethodAsyncCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        byte[] certificate,
        CancellationToken cancellationToken);
    #endif
    #endregion

    #region CertificateDirectoryState Class
    #if (!OPCUA_EXCLUDE_CertificateDirectoryState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class CertificateDirectoryState : DirectoryState
    {
        #region Constructors
        public CertificateDirectoryState(NodeState parent) : base(parent)
        {
        }

        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(Opc.Ua.Gds.ObjectTypes.CertificateDirectoryType, Opc.Ua.Gds.Namespaces.OpcUaGds, namespaceUris);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);

            if (RevokeCertificate != null)
            {
                RevokeCertificate.Initialize(context, RevokeCertificate_InitializationString);
            }

            if (GetCertificates != null)
            {
                GetCertificates.Initialize(context, GetCertificates_InitializationString);
            }

            if (CheckRevocationStatus != null)
            {
                CheckRevocationStatus.Initialize(context, CheckRevocationStatus_InitializationString);
            }
        }

        #region Initialization String
        private const string RevokeCertificate_InitializationString =
           "AQAAACAAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvR0RTL/////8EYYIKBAAAAAEAEQAAAFJl" +
           "dm9rZUNlcnRpZmljYXRlAQGbOgAvAQGbOps6AAABAf////8BAAAAF2CpCgIAAAAAAA4AAABJbnB1dEFy" +
           "Z3VtZW50cwEBnDoALgBEnDoAAJYCAAAAAQAqAQEcAAAADQAAAEFwcGxpY2F0aW9uSWQAEf////8AAAAA" +
           "AAEAKgEBGgAAAAsAAABDZXJ0aWZpY2F0ZQAP/////wAAAAAAAQAoAQEAAAABAAAAAgAAAAEB/////wAA" +
           "AAA=";

        private const string GetCertificates_InitializationString =
           "AQAAACAAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvR0RTL/////8EYYIKBAAAAAEADwAAAEdl" +
           "dENlcnRpZmljYXRlcwEBWQAALwEBWQBZAAAAAQH/////AgAAABdgqQoCAAAAAAAOAAAASW5wdXRBcmd1" +
           "bWVudHMBAVoAAC4ARFoAAACWAgAAAAEAKgEBHAAAAA0AAABBcHBsaWNhdGlvbklkABH/////AAAAAAAB" +
           "ACoBASEAAAASAAAAQ2VydGlmaWNhdGVHcm91cElkABH/////AAAAAAABACgBAQAAAAEAAAACAAAAAQH/" +
           "////AAAAABdgqQoCAAAAAAAPAAAAT3V0cHV0QXJndW1lbnRzAQFsAAAuAERsAAAAlgIAAAABACoBASUA" +
           "AAASAAAAQ2VydGlmaWNhdGVUeXBlSWRzABEBAAAAAQAAAAAAAAAAAQAqAQEfAAAADAAAAENlcnRpZmlj" +
           "YXRlcwAPAQAAAAEAAAAAAAAAAAEAKAEBAAAAAQAAAAIAAAABAf////8AAAAA";

        private const string CheckRevocationStatus_InitializationString =
           "AQAAACAAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvR0RTL/////8EYYIKBAAAAAEAFQAAAENo" +
           "ZWNrUmV2b2NhdGlvblN0YXR1cwEBfgAALwEBfgB+AAAAAQH/////AgAAABdgqQoCAAAAAAAOAAAASW5w" +
           "dXRBcmd1bWVudHMBAaAAAC4ARKAAAACWAQAAAAEAKgEBGgAAAAsAAABDZXJ0aWZpY2F0ZQAP/////wAA" +
           "AAAAAQAoAQEAAAABAAAAAQAAAAEB/////wAAAAAXYKkKAgAAAAAADwAAAE91dHB1dEFyZ3VtZW50cwEB" +
           "oQAALgBEoQAAAJYCAAAAAQAqAQEgAAAAEQAAAENlcnRpZmljYXRlU3RhdHVzABP/////AAAAAAABACoB" +
           "AR0AAAAMAAAAVmFsaWRpdHlUaW1lAQAmAf////8AAAAAAAEAKAEBAAAAAQAAAAIAAAABAf////8AAAAA";

        private const string InitializationString =
           "AQAAACAAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvR0RTL/////8EYIACAQAAAAEAIAAAAENl" +
           "cnRpZmljYXRlRGlyZWN0b3J5VHlwZUluc3RhbmNlAQE/AAEBPwA/AAAA/////xIAAAAEYIAKAQAAAAEA" +
           "DAAAAEFwcGxpY2F0aW9ucwIBAFhCDwAALwA9WEIPAP////8AAAAABGGCCgQAAAABABAAAABGaW5kQXBw" +
           "bGljYXRpb25zAgEAWUIPAAAvAQEPAFlCDwABAf////8CAAAAF2CpCgIAAAAAAA4AAABJbnB1dEFyZ3Vt" +
           "ZW50cwIBAFpCDwAALgBEWkIPAJYBAAAAAQAqAQEdAAAADgAAAEFwcGxpY2F0aW9uVXJpAAz/////AAAA" +
           "AAABACgBAQAAAAEAAAABAAAAAQH/////AAAAABdgqQoCAAAAAAAPAAAAT3V0cHV0QXJndW1lbnRzAgEA" +
           "W0IPAAAuAERbQg8AlgEAAAABACoBASEAAAAMAAAAQXBwbGljYXRpb25zAQEBAAEAAAABAAAAAAAAAAAB" +
           "ACgBAQAAAAEAAAABAAAAAQH/////AAAAAARhggoEAAAAAQATAAAAUmVnaXN0ZXJBcHBsaWNhdGlvbgIB" +
           "AFxCDwAALwEBEgBcQg8AAQEBAAAAACkAAQEaAAIAAAAXYKkKAgAAAAAADgAAAElucHV0QXJndW1lbnRz" +
           "AgEAXUIPAAAuAERdQg8AlgEAAAABACoBARwAAAALAAAAQXBwbGljYXRpb24BAQEA/////wAAAAAAAQAo" +
           "AQEAAAABAAAAAQAAAAEB/////wAAAAAXYKkKAgAAAAAADwAAAE91dHB1dEFyZ3VtZW50cwIBAF5CDwAA" +
           "LgBEXkIPAJYBAAAAAQAqAQEcAAAADQAAAEFwcGxpY2F0aW9uSWQAEf////8AAAAAAAEAKAEBAAAAAQAA" +
           "AAEAAAABAf////8AAAAABGGCCgQAAAABABEAAABVcGRhdGVBcHBsaWNhdGlvbgIBAF9CDwAALwEBvABf" +
           "Qg8AAQH/////AQAAABdgqQoCAAAAAAAOAAAASW5wdXRBcmd1bWVudHMCAQBgQg8AAC4ARGBCDwCWAQAA" +
           "AAEAKgEBHAAAAAsAAABBcHBsaWNhdGlvbgEBAQD/////AAAAAAABACgBAQAAAAEAAAABAAAAAQH/////" +
           "AAAAAARhggoEAAAAAQAVAAAAVW5yZWdpc3RlckFwcGxpY2F0aW9uAgEAYUIPAAAvAQEVAGFCDwABAf//" +
           "//8BAAAAF2CpCgIAAAAAAA4AAABJbnB1dEFyZ3VtZW50cwIBAGJCDwAALgBEYkIPAJYBAAAAAQAqAQEc" +
           "AAAADQAAAEFwcGxpY2F0aW9uSWQAEf////8AAAAAAAEAKAEBAAAAAQAAAAEAAAABAf////8AAAAABGGC" +
           "CgQAAAABAA4AAABHZXRBcHBsaWNhdGlvbgIBAGNCDwAALwEB0gBjQg8AAQH/////AgAAABdgqQoCAAAA" +
           "AAAOAAAASW5wdXRBcmd1bWVudHMCAQBkQg8AAC4ARGRCDwCWAQAAAAEAKgEBHAAAAA0AAABBcHBsaWNh" +
           "dGlvbklkABH/////AAAAAAABACgBAQAAAAEAAAABAAAAAQH/////AAAAABdgqQoCAAAAAAAPAAAAT3V0" +
           "cHV0QXJndW1lbnRzAgEAZUIPAAAuAERlQg8AlgEAAAABACoBARwAAAALAAAAQXBwbGljYXRpb24BAQEA" +
           "/////wAAAAAAAQAoAQEAAAABAAAAAQAAAAEB/////wAAAAAEYYIKBAAAAAEAEQAAAFF1ZXJ5QXBwbGlj" +
           "YXRpb25zAgEAZkIPAAAvAQFkA2ZCDwABAf////8CAAAAF2CpCgIAAAAAAA4AAABJbnB1dEFyZ3VtZW50" +
           "cwIBAGdCDwAALgBEZ0IPAJYHAAAAAQAqAQEfAAAAEAAAAFN0YXJ0aW5nUmVjb3JkSWQAB/////8AAAAA" +
           "AAEAKgEBIQAAABIAAABNYXhSZWNvcmRzVG9SZXR1cm4AB/////8AAAAAAAEAKgEBHgAAAA8AAABBcHBs" +
           "aWNhdGlvbk5hbWUADP////8AAAAAAAEAKgEBHQAAAA4AAABBcHBsaWNhdGlvblVyaQAM/////wAAAAAA" +
           "AQAqAQEeAAAADwAAAEFwcGxpY2F0aW9uVHlwZQAH/////wAAAAAAAQAqAQEZAAAACgAAAFByb2R1Y3RV" +
           "cmkADP////8AAAAAAAEAKgEBHwAAAAwAAABDYXBhYmlsaXRpZXMADAEAAAABAAAAAAAAAAABACgBAQAA" +
           "AAEAAAAHAAAAAQH/////AAAAABdgqQoCAAAAAAAPAAAAT3V0cHV0QXJndW1lbnRzAgEAaEIPAAAuAERo" +
           "Qg8AlgMAAAABACoBASUAAAAUAAAATGFzdENvdW50ZXJSZXNldFRpbWUBACYB/////wAAAAAAAQAqAQEb" +
           "AAAADAAAAE5leHRSZWNvcmRJZAAH/////wAAAAAAAQAqAQEhAAAADAAAAEFwcGxpY2F0aW9ucwEANAEB" +
           "AAAAAQAAAAAAAAAAAQAoAQEAAAABAAAAAwAAAAEB/////wAAAAAEYYIKBAAAAAEADAAAAFF1ZXJ5U2Vy" +
           "dmVycwIBAGlCDwAALwEBFwBpQg8AAQH/////AgAAABdgqQoCAAAAAAAOAAAASW5wdXRBcmd1bWVudHMC" +
           "AQBqQg8AAC4ARGpCDwCWBgAAAAEAKgEBHwAAABAAAABTdGFydGluZ1JlY29yZElkAAf/////AAAAAAAB" +
           "ACoBASEAAAASAAAATWF4UmVjb3Jkc1RvUmV0dXJuAAf/////AAAAAAABACoBAR4AAAAPAAAAQXBwbGlj" +
           "YXRpb25OYW1lAAz/////AAAAAAABACoBAR0AAAAOAAAAQXBwbGljYXRpb25VcmkADP////8AAAAAAAEA" +
           "KgEBGQAAAAoAAABQcm9kdWN0VXJpAAz/////AAAAAAABACoBASUAAAASAAAAU2VydmVyQ2FwYWJpbGl0" +
           "aWVzAAwBAAAAAQAAAAAAAAAAAQAoAQEAAAABAAAABgAAAAEB/////wAAAAAXYKkKAgAAAAAADwAAAE91" +
           "dHB1dEFyZ3VtZW50cwIBAGtCDwAALgBEa0IPAJYCAAAAAQAqAQElAAAAFAAAAExhc3RDb3VudGVyUmVz" +
           "ZXRUaW1lAQAmAf////8AAAAAAAEAKgEBHAAAAAcAAABTZXJ2ZXJzAQCdLwEAAAABAAAAAAAAAAABACgB" +
           "AQAAAAEAAAACAAAAAQH/////AAAAAARggAoBAAAAAQARAAAAQ2VydGlmaWNhdGVHcm91cHMBAf8BACMB" +
           "APU1/wEAAP////8BAAAABGCACgEAAAAAABcAAABEZWZhdWx0QXBwbGljYXRpb25Hcm91cAEBAAIALwEA" +
           "CzEAAgAAAQAAAAEALiMAAQCpMwIAAAAEYIAKAQAAAAAACQAAAFRydXN0TGlzdAEBAQIALwEA6jABAgAA" +
           "/////w8AAAAVYIkKAgAAAAAABAAAAFNpemUBAQICAC4ARAICAAAACf////8BAf////8AAAAAFWCJCgIA" +
           "AAAAAAgAAABXcml0YWJsZQEBAwIALgBEAwIAAAAB/////wEB/////wAAAAAVYIkKAgAAAAAADAAAAFVz" +
           "ZXJXcml0YWJsZQEBBAIALgBEBAIAAAAB/////wEB/////wAAAAAVYIkKAgAAAAAACQAAAE9wZW5Db3Vu" +
           "dAEBBQIALgBEBQIAAAAF/////wEB/////wAAAAAEYYIKBAAAAAAABAAAAE9wZW4BAQcCAC8BADwtBwIA" +
           "AAEB/////wIAAAAXYKkKAgAAAAAADgAAAElucHV0QXJndW1lbnRzAQEIAgAuAEQIAgAAlgEAAAABACoB" +
           "ARMAAAAEAAAATW9kZQAD/////wAAAAAAAQAoAQEAAAABAAAAAQAAAAEB/////wAAAAAXYKkKAgAAAAAA" +
           "DwAAAE91dHB1dEFyZ3VtZW50cwEBCQIALgBECQIAAJYBAAAAAQAqAQEZAAAACgAAAEZpbGVIYW5kbGUA" +
           "B/////8AAAAAAAEAKAEBAAAAAQAAAAEAAAABAf////8AAAAABGGCCgQAAAAAAAUAAABDbG9zZQEBCgIA" +
           "LwEAPy0KAgAAAQH/////AQAAABdgqQoCAAAAAAAOAAAASW5wdXRBcmd1bWVudHMBAQsCAC4ARAsCAACW" +
           "AQAAAAEAKgEBGQAAAAoAAABGaWxlSGFuZGxlAAf/////AAAAAAABACgBAQAAAAEAAAABAAAAAQH/////" +
           "AAAAAARhggoEAAAAAAAEAAAAUmVhZAEBDAIALwEAQS0MAgAAAQH/////AgAAABdgqQoCAAAAAAAOAAAA" +
           "SW5wdXRBcmd1bWVudHMBAQ0CAC4ARA0CAACWAgAAAAEAKgEBGQAAAAoAAABGaWxlSGFuZGxlAAf/////" +
           "AAAAAAABACoBARUAAAAGAAAATGVuZ3RoAAb/////AAAAAAABACgBAQAAAAEAAAACAAAAAQH/////AAAA" +
           "ABdgqQoCAAAAAAAPAAAAT3V0cHV0QXJndW1lbnRzAQEOAgAuAEQOAgAAlgEAAAABACoBARMAAAAEAAAA" +
           "RGF0YQAP/////wAAAAAAAQAoAQEAAAABAAAAAQAAAAEB/////wAAAAAEYYIKBAAAAAAABQAAAFdyaXRl" +
           "AQEPAgAvAQBELQ8CAAABAf////8BAAAAF2CpCgIAAAAAAA4AAABJbnB1dEFyZ3VtZW50cwEBEAIALgBE" +
           "EAIAAJYCAAAAAQAqAQEZAAAACgAAAEZpbGVIYW5kbGUAB/////8AAAAAAAEAKgEBEwAAAAQAAABEYXRh" +
           "AA//////AAAAAAABACgBAQAAAAEAAAACAAAAAQH/////AAAAAARhggoEAAAAAAALAAAAR2V0UG9zaXRp" +
           "b24BARECAC8BAEYtEQIAAAEB/////wIAAAAXYKkKAgAAAAAADgAAAElucHV0QXJndW1lbnRzAQESAgAu" +
           "AEQSAgAAlgEAAAABACoBARkAAAAKAAAARmlsZUhhbmRsZQAH/////wAAAAAAAQAoAQEAAAABAAAAAQAA" +
           "AAEB/////wAAAAAXYKkKAgAAAAAADwAAAE91dHB1dEFyZ3VtZW50cwEBEwIALgBEEwIAAJYBAAAAAQAq" +
           "AQEXAAAACAAAAFBvc2l0aW9uAAn/////AAAAAAABACgBAQAAAAEAAAABAAAAAQH/////AAAAAARhggoE" +
           "AAAAAAALAAAAU2V0UG9zaXRpb24BARQCAC8BAEktFAIAAAEB/////wEAAAAXYKkKAgAAAAAADgAAAElu" +
           "cHV0QXJndW1lbnRzAQEVAgAuAEQVAgAAlgIAAAABACoBARkAAAAKAAAARmlsZUhhbmRsZQAH/////wAA" +
           "AAAAAQAqAQEXAAAACAAAAFBvc2l0aW9uAAn/////AAAAAAABACgBAQAAAAEAAAACAAAAAQH/////AAAA" +
           "ABVgiQoCAAAAAAAOAAAATGFzdFVwZGF0ZVRpbWUBARYCAC4ARBYCAAABACYB/////wEB/////wAAAAAE" +
           "YYIKBAAAAAAADQAAAE9wZW5XaXRoTWFza3MBARcCAC8BAP8wFwIAAAEB/////wIAAAAXYKkKAgAAAAAA" +
           "DgAAAElucHV0QXJndW1lbnRzAQEYAgAuAEQYAgAAlgEAAAABACoBARQAAAAFAAAATWFza3MAB/////8A" +
           "AAAAAAEAKAEBAAAAAQAAAAEAAAABAf////8AAAAAF2CpCgIAAAAAAA8AAABPdXRwdXRBcmd1bWVudHMB" +
           "ARkCAC4ARBkCAACWAQAAAAEAKgEBGQAAAAoAAABGaWxlSGFuZGxlAAf/////AAAAAAABACgBAQAAAAEA" +
           "AAABAAAAAQH/////AAAAAARhggoEAAAAAAAOAAAAQ2xvc2VBbmRVcGRhdGUBARoCAC8BAAIxGgIAAAEB" +
           "/////wIAAAAXYKkKAgAAAAAADgAAAElucHV0QXJndW1lbnRzAQEbAgAuAEQbAgAAlgEAAAABACoBARkA" +
           "AAAKAAAARmlsZUhhbmRsZQAH/////wAAAAAAAQAoAQEAAAABAAAAAQAAAAEB/////wAAAAAXYKkKAgAA" +
           "AAAADwAAAE91dHB1dEFyZ3VtZW50cwEBHAIALgBEHAIAAJYBAAAAAQAqAQEjAAAAFAAAAEFwcGx5Q2hh" +
           "bmdlc1JlcXVpcmVkAAH/////AAAAAAABACgBAQAAAAEAAAABAAAAAQH/////AAAAAARhggoEAAAAAAAO" +
           "AAAAQWRkQ2VydGlmaWNhdGUBAR0CAC8BAAQxHQIAAAEB/////wEAAAAXYKkKAgAAAAAADgAAAElucHV0" +
           "QXJndW1lbnRzAQEeAgAuAEQeAgAAlgIAAAABACoBARoAAAALAAAAQ2VydGlmaWNhdGUAD/////8AAAAA" +
           "AAEAKgEBIwAAABQAAABJc1RydXN0ZWRDZXJ0aWZpY2F0ZQAB/////wAAAAAAAQAoAQEAAAABAAAAAgAA" +
           "AAEB/////wAAAAAEYYIKBAAAAAAAEQAAAFJlbW92ZUNlcnRpZmljYXRlAQEfAgAvAQAGMR8CAAABAf//" +
           "//8BAAAAF2CpCgIAAAAAAA4AAABJbnB1dEFyZ3VtZW50cwEBIAIALgBEIAIAAJYCAAAAAQAqAQEZAAAA" +
           "CgAAAFRodW1icHJpbnQADP////8AAAAAAAEAKgEBIwAAABQAAABJc1RydXN0ZWRDZXJ0aWZpY2F0ZQAB" +
           "/////wAAAAAAAQAoAQEAAAABAAAAAgAAAAEB/////wAAAAAXYIkKAgAAAAAAEAAAAENlcnRpZmljYXRl" +
           "VHlwZXMBASECAC4ARCECAAAAEQEAAAABAAAAAAAAAAEB/////wAAAAAEYYIKBAAAAAEAEwAAAFN0YXJ0" +
           "U2lnbmluZ1JlcXVlc3QBAU8AAC8BAU8ATwAAAAEB/////wIAAAAXYKkKAgAAAAAADgAAAElucHV0QXJn" +
           "dW1lbnRzAQFQAAAuAERQAAAAlgQAAAABACoBARwAAAANAAAAQXBwbGljYXRpb25JZAAR/////wAAAAAA" +
           "AQAqAQEhAAAAEgAAAENlcnRpZmljYXRlR3JvdXBJZAAR/////wAAAAAAAQAqAQEgAAAAEQAAAENlcnRp" +
           "ZmljYXRlVHlwZUlkABH/////AAAAAAABACoBASEAAAASAAAAQ2VydGlmaWNhdGVSZXF1ZXN0AA//////" +
           "AAAAAAABACgBAQAAAAEAAAAEAAAAAQH/////AAAAABdgqQoCAAAAAAAPAAAAT3V0cHV0QXJndW1lbnRz" +
           "AQFRAAAuAERRAAAAlgEAAAABACoBARgAAAAJAAAAUmVxdWVzdElkABH/////AAAAAAABACgBAQAAAAEA" +
           "AAABAAAAAQH/////AAAAAARhggoEAAAAAQAWAAAAU3RhcnROZXdLZXlQYWlyUmVxdWVzdAEBTAAALwEB" +
           "TABMAAAAAQH/////AgAAABdgqQoCAAAAAAAOAAAASW5wdXRBcmd1bWVudHMBAU0AAC4ARE0AAACWBwAA" +
           "AAEAKgEBHAAAAA0AAABBcHBsaWNhdGlvbklkABH/////AAAAAAABACoBASEAAAASAAAAQ2VydGlmaWNh" +
           "dGVHcm91cElkABH/////AAAAAAABACoBASAAAAARAAAAQ2VydGlmaWNhdGVUeXBlSWQAEf////8AAAAA" +
           "AAEAKgEBGgAAAAsAAABTdWJqZWN0TmFtZQAM/////wAAAAAAAQAqAQEeAAAACwAAAERvbWFpbk5hbWVz" +
           "AAwBAAAAAQAAAAAAAAAAAQAqAQEfAAAAEAAAAFByaXZhdGVLZXlGb3JtYXQADP////8AAAAAAAEAKgEB" +
           "IQAAABIAAABQcml2YXRlS2V5UGFzc3dvcmQADP////8AAAAAAAEAKAEBAAAAAQAAAAcAAAABAf////8A" +
           "AAAAF2CpCgIAAAAAAA8AAABPdXRwdXRBcmd1bWVudHMBAU4AAC4ARE4AAACWAQAAAAEAKgEBGAAAAAkA" +
           "AABSZXF1ZXN0SWQAEf////8AAAAAAAEAKAEBAAAAAQAAAAEAAAABAf////8AAAAABGGCCgQAAAABAA0A" +
           "AABGaW5pc2hSZXF1ZXN0AQFVAAAvAQFVAFUAAAABAf////8CAAAAF2CpCgIAAAAAAA4AAABJbnB1dEFy" +
           "Z3VtZW50cwEBVgAALgBEVgAAAJYCAAAAAQAqAQEcAAAADQAAAEFwcGxpY2F0aW9uSWQAEf////8AAAAA" +
           "AAEAKgEBGAAAAAkAAABSZXF1ZXN0SWQAEf////8AAAAAAAEAKAEBAAAAAQAAAAIAAAABAf////8AAAAA" +
           "F2CpCgIAAAAAAA8AAABPdXRwdXRBcmd1bWVudHMBAVcAAC4ARFcAAACWAwAAAAEAKgEBGgAAAAsAAABD" +
           "ZXJ0aWZpY2F0ZQAP/////wAAAAAAAQAqAQEZAAAACgAAAFByaXZhdGVLZXkAD/////8AAAAAAAEAKgEB" +
           "JQAAABIAAABJc3N1ZXJDZXJ0aWZpY2F0ZXMADwEAAAABAAAAAAAAAAABACgBAQAAAAEAAAADAAAAAQH/" +
           "////AAAAAARhggoEAAAAAQARAAAAUmV2b2tlQ2VydGlmaWNhdGUBAZs6AC8BAZs6mzoAAAEB/////wEA" +
           "AAAXYKkKAgAAAAAADgAAAElucHV0QXJndW1lbnRzAQGcOgAuAEScOgAAlgIAAAABACoBARwAAAANAAAA" +
           "QXBwbGljYXRpb25JZAAR/////wAAAAAAAQAqAQEaAAAACwAAAENlcnRpZmljYXRlAA//////AAAAAAAB" +
           "ACgBAQAAAAEAAAACAAAAAQH/////AAAAAARhggoEAAAAAQAUAAAAR2V0Q2VydGlmaWNhdGVHcm91cHMB" +
           "AXEBAC8BAXEBcQEAAAEB/////wIAAAAXYKkKAgAAAAAADgAAAElucHV0QXJndW1lbnRzAQFyAQAuAERy" +
           "AQAAlgEAAAABACoBARwAAAANAAAAQXBwbGljYXRpb25JZAAR/////wAAAAAAAQAoAQEAAAABAAAAAQAA" +
           "AAEB/////wAAAAAXYKkKAgAAAAAADwAAAE91dHB1dEFyZ3VtZW50cwEBcwEALgBEcwEAAJYBAAAAAQAq" +
           "AQEmAAAAEwAAAENlcnRpZmljYXRlR3JvdXBJZHMAEQEAAAABAAAAAAAAAAABACgBAQAAAAEAAAABAAAA" +
           "AQH/////AAAAAARhggoEAAAAAQAPAAAAR2V0Q2VydGlmaWNhdGVzAQFZAAAvAQFZAFkAAAABAf////8C" +
           "AAAAF2CpCgIAAAAAAA4AAABJbnB1dEFyZ3VtZW50cwEBWgAALgBEWgAAAJYCAAAAAQAqAQEcAAAADQAA" +
           "AEFwcGxpY2F0aW9uSWQAEf////8AAAAAAAEAKgEBIQAAABIAAABDZXJ0aWZpY2F0ZUdyb3VwSWQAEf//" +
           "//8AAAAAAAEAKAEBAAAAAQAAAAIAAAABAf////8AAAAAF2CpCgIAAAAAAA8AAABPdXRwdXRBcmd1bWVu" +
           "dHMBAWwAAC4ARGwAAACWAgAAAAEAKgEBJQAAABIAAABDZXJ0aWZpY2F0ZVR5cGVJZHMAEQEAAAABAAAA" +
           "AAAAAAABACoBAR8AAAAMAAAAQ2VydGlmaWNhdGVzAA8BAAAAAQAAAAAAAAAAAQAoAQEAAAABAAAAAgAA" +
           "AAEB/////wAAAAAEYYIKBAAAAAEADAAAAEdldFRydXN0TGlzdAEBxQAALwEBxQDFAAAAAQH/////AgAA" +
           "ABdgqQoCAAAAAAAOAAAASW5wdXRBcmd1bWVudHMBAcYAAC4ARMYAAACWAgAAAAEAKgEBHAAAAA0AAABB" +
           "cHBsaWNhdGlvbklkABH/////AAAAAAABACoBASEAAAASAAAAQ2VydGlmaWNhdGVHcm91cElkABH/////" +
           "AAAAAAABACgBAQAAAAEAAAACAAAAAQH/////AAAAABdgqQoCAAAAAAAPAAAAT3V0cHV0QXJndW1lbnRz" +
           "AQHHAAAuAETHAAAAlgEAAAABACoBARoAAAALAAAAVHJ1c3RMaXN0SWQAEf////8AAAAAAAEAKAEBAAAA" +
           "AQAAAAEAAAABAf////8AAAAABGGCCgQAAAABABQAAABHZXRDZXJ0aWZpY2F0ZVN0YXR1cwEB3gAALwEB" +
           "3gDeAAAAAQH/////AgAAABdgqQoCAAAAAAAOAAAASW5wdXRBcmd1bWVudHMBAd8AAC4ARN8AAACWAwAA" +
           "AAEAKgEBHAAAAA0AAABBcHBsaWNhdGlvbklkABH/////AAAAAAABACoBASEAAAASAAAAQ2VydGlmaWNh" +
           "dGVHcm91cElkABH/////AAAAAAABACoBASAAAAARAAAAQ2VydGlmaWNhdGVUeXBlSWQAEf////8AAAAA" +
           "AAEAKAEBAAAAAQAAAAMAAAABAf////8AAAAAF2CpCgIAAAAAAA8AAABPdXRwdXRBcmd1bWVudHMBAeAA" +
           "AC4AROAAAACWAQAAAAEAKgEBHQAAAA4AAABVcGRhdGVSZXF1aXJlZAAB/////wAAAAAAAQAoAQEAAAAB" +
           "AAAAAQAAAAEB/////wAAAAAEYYIKBAAAAAEAFQAAAENoZWNrUmV2b2NhdGlvblN0YXR1cwEBfgAALwEB" +
           "fgB+AAAAAQH/////AgAAABdgqQoCAAAAAAAOAAAASW5wdXRBcmd1bWVudHMBAaAAAC4ARKAAAACWAQAA" +
           "AAEAKgEBGgAAAAsAAABDZXJ0aWZpY2F0ZQAP/////wAAAAAAAQAoAQEAAAABAAAAAQAAAAEB/////wAA" +
           "AAAXYKkKAgAAAAAADwAAAE91dHB1dEFyZ3VtZW50cwEBoQAALgBEoQAAAJYCAAAAAQAqAQEgAAAAEQAA" +
           "AENlcnRpZmljYXRlU3RhdHVzABP/////AAAAAAABACoBAR0AAAAMAAAAVmFsaWRpdHlUaW1lAQAmAf//" +
           "//8AAAAAAAEAKAEBAAAAAQAAAAIAAAABAf////8AAAAA";
        #endregion
        #endif
        #endregion

        #region Public Properties
        public CertificateGroupFolderState CertificateGroups
        {
            get => m_certificateGroups;

            set
            {
                if (!Object.ReferenceEquals(m_certificateGroups, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_certificateGroups = value;
            }
        }

        public StartSigningRequestMethodState StartSigningRequest
        {
            get => m_startSigningRequestMethod;

            set
            {
                if (!Object.ReferenceEquals(m_startSigningRequestMethod, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_startSigningRequestMethod = value;
            }
        }

        public StartNewKeyPairRequestMethodState StartNewKeyPairRequest
        {
            get => m_startNewKeyPairRequestMethod;

            set
            {
                if (!Object.ReferenceEquals(m_startNewKeyPairRequestMethod, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_startNewKeyPairRequestMethod = value;
            }
        }

        public FinishRequestMethodState FinishRequest
        {
            get => m_finishRequestMethod;

            set
            {
                if (!Object.ReferenceEquals(m_finishRequestMethod, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_finishRequestMethod = value;
            }
        }

        public RevokeCertificateMethodState RevokeCertificate
        {
            get => m_revokeCertificateMethod;

            set
            {
                if (!Object.ReferenceEquals(m_revokeCertificateMethod, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_revokeCertificateMethod = value;
            }
        }

        public GetCertificateGroupsMethodState GetCertificateGroups
        {
            get => m_getCertificateGroupsMethod;

            set
            {
                if (!Object.ReferenceEquals(m_getCertificateGroupsMethod, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_getCertificateGroupsMethod = value;
            }
        }

        public GetCertificatesMethodState GetCertificates
        {
            get => m_getCertificatesMethod;

            set
            {
                if (!Object.ReferenceEquals(m_getCertificatesMethod, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_getCertificatesMethod = value;
            }
        }

        public GetTrustListMethodState GetTrustList
        {
            get => m_getTrustListMethod;

            set
            {
                if (!Object.ReferenceEquals(m_getTrustListMethod, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_getTrustListMethod = value;
            }
        }

        public GetCertificateStatusMethodState GetCertificateStatus
        {
            get => m_getCertificateStatusMethod;

            set
            {
                if (!Object.ReferenceEquals(m_getCertificateStatusMethod, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_getCertificateStatusMethod = value;
            }
        }

        public CheckRevocationStatusMethodState CheckRevocationStatus
        {
            get => m_checkRevocationStatusMethod;

            set
            {
                if (!Object.ReferenceEquals(m_checkRevocationStatusMethod, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_checkRevocationStatusMethod = value;
            }
        }
        #endregion

        #region Overridden Methods
        public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
        {
            if (m_certificateGroups != null)
            {
                children.Add(m_certificateGroups);
            }

            if (m_startSigningRequestMethod != null)
            {
                children.Add(m_startSigningRequestMethod);
            }

            if (m_startNewKeyPairRequestMethod != null)
            {
                children.Add(m_startNewKeyPairRequestMethod);
            }

            if (m_finishRequestMethod != null)
            {
                children.Add(m_finishRequestMethod);
            }

            if (m_revokeCertificateMethod != null)
            {
                children.Add(m_revokeCertificateMethod);
            }

            if (m_getCertificateGroupsMethod != null)
            {
                children.Add(m_getCertificateGroupsMethod);
            }

            if (m_getCertificatesMethod != null)
            {
                children.Add(m_getCertificatesMethod);
            }

            if (m_getTrustListMethod != null)
            {
                children.Add(m_getTrustListMethod);
            }

            if (m_getCertificateStatusMethod != null)
            {
                children.Add(m_getCertificateStatusMethod);
            }

            if (m_checkRevocationStatusMethod != null)
            {
                children.Add(m_checkRevocationStatusMethod);
            }

            base.GetChildren(context, children);
        }
            
        protected override void RemoveExplicitlyDefinedChild(BaseInstanceState child)
        {
            if (Object.ReferenceEquals(m_certificateGroups, child))
            {
                m_certificateGroups = null;
                return;
            }

            if (Object.ReferenceEquals(m_startSigningRequestMethod, child))
            {
                m_startSigningRequestMethod = null;
                return;
            }

            if (Object.ReferenceEquals(m_startNewKeyPairRequestMethod, child))
            {
                m_startNewKeyPairRequestMethod = null;
                return;
            }

            if (Object.ReferenceEquals(m_finishRequestMethod, child))
            {
                m_finishRequestMethod = null;
                return;
            }

            if (Object.ReferenceEquals(m_revokeCertificateMethod, child))
            {
                m_revokeCertificateMethod = null;
                return;
            }

            if (Object.ReferenceEquals(m_getCertificateGroupsMethod, child))
            {
                m_getCertificateGroupsMethod = null;
                return;
            }

            if (Object.ReferenceEquals(m_getCertificatesMethod, child))
            {
                m_getCertificatesMethod = null;
                return;
            }

            if (Object.ReferenceEquals(m_getTrustListMethod, child))
            {
                m_getTrustListMethod = null;
                return;
            }

            if (Object.ReferenceEquals(m_getCertificateStatusMethod, child))
            {
                m_getCertificateStatusMethod = null;
                return;
            }

            if (Object.ReferenceEquals(m_checkRevocationStatusMethod, child))
            {
                m_checkRevocationStatusMethod = null;
                return;
            }

            base.RemoveExplicitlyDefinedChild(child);
        }

        protected override BaseInstanceState FindChild(
            ISystemContext context,
            QualifiedName browseName,
            bool createOrReplace,
            BaseInstanceState replacement)
        {
            if (QualifiedName.IsNull(browseName))
            {
                return null;
            }

            BaseInstanceState instance = null;

            switch (browseName.Name)
            {
                case Opc.Ua.Gds.BrowseNames.CertificateGroups:
                {
                    if (createOrReplace)
                    {
                        if (CertificateGroups == null)
                        {
                            if (replacement == null)
                            {
                                CertificateGroups = new CertificateGroupFolderState(this);
                            }
                            else
                            {
                                CertificateGroups = (CertificateGroupFolderState)replacement;
                            }
                        }
                    }

                    instance = CertificateGroups;
                    break;
                }

                case Opc.Ua.Gds.BrowseNames.StartSigningRequest:
                {
                    if (createOrReplace)
                    {
                        if (StartSigningRequest == null)
                        {
                            if (replacement == null)
                            {
                                StartSigningRequest = new StartSigningRequestMethodState(this);
                            }
                            else
                            {
                                StartSigningRequest = (StartSigningRequestMethodState)replacement;
                            }
                        }
                    }

                    instance = StartSigningRequest;
                    break;
                }

                case Opc.Ua.Gds.BrowseNames.StartNewKeyPairRequest:
                {
                    if (createOrReplace)
                    {
                        if (StartNewKeyPairRequest == null)
                        {
                            if (replacement == null)
                            {
                                StartNewKeyPairRequest = new StartNewKeyPairRequestMethodState(this);
                            }
                            else
                            {
                                StartNewKeyPairRequest = (StartNewKeyPairRequestMethodState)replacement;
                            }
                        }
                    }

                    instance = StartNewKeyPairRequest;
                    break;
                }

                case Opc.Ua.Gds.BrowseNames.FinishRequest:
                {
                    if (createOrReplace)
                    {
                        if (FinishRequest == null)
                        {
                            if (replacement == null)
                            {
                                FinishRequest = new FinishRequestMethodState(this);
                            }
                            else
                            {
                                FinishRequest = (FinishRequestMethodState)replacement;
                            }
                        }
                    }

                    instance = FinishRequest;
                    break;
                }

                case Opc.Ua.Gds.BrowseNames.RevokeCertificate:
                {
                    if (createOrReplace)
                    {
                        if (RevokeCertificate == null)
                        {
                            if (replacement == null)
                            {
                                RevokeCertificate = new RevokeCertificateMethodState(this);
                            }
                            else
                            {
                                RevokeCertificate = (RevokeCertificateMethodState)replacement;
                            }
                        }
                    }

                    instance = RevokeCertificate;
                    break;
                }

                case Opc.Ua.Gds.BrowseNames.GetCertificateGroups:
                {
                    if (createOrReplace)
                    {
                        if (GetCertificateGroups == null)
                        {
                            if (replacement == null)
                            {
                                GetCertificateGroups = new GetCertificateGroupsMethodState(this);
                            }
                            else
                            {
                                GetCertificateGroups = (GetCertificateGroupsMethodState)replacement;
                            }
                        }
                    }

                    instance = GetCertificateGroups;
                    break;
                }

                case Opc.Ua.Gds.BrowseNames.GetCertificates:
                {
                    if (createOrReplace)
                    {
                        if (GetCertificates == null)
                        {
                            if (replacement == null)
                            {
                                GetCertificates = new GetCertificatesMethodState(this);
                            }
                            else
                            {
                                GetCertificates = (GetCertificatesMethodState)replacement;
                            }
                        }
                    }

                    instance = GetCertificates;
                    break;
                }

                case Opc.Ua.Gds.BrowseNames.GetTrustList:
                {
                    if (createOrReplace)
                    {
                        if (GetTrustList == null)
                        {
                            if (replacement == null)
                            {
                                GetTrustList = new GetTrustListMethodState(this);
                            }
                            else
                            {
                                GetTrustList = (GetTrustListMethodState)replacement;
                            }
                        }
                    }

                    instance = GetTrustList;
                    break;
                }

                case Opc.Ua.Gds.BrowseNames.GetCertificateStatus:
                {
                    if (createOrReplace)
                    {
                        if (GetCertificateStatus == null)
                        {
                            if (replacement == null)
                            {
                                GetCertificateStatus = new GetCertificateStatusMethodState(this);
                            }
                            else
                            {
                                GetCertificateStatus = (GetCertificateStatusMethodState)replacement;
                            }
                        }
                    }

                    instance = GetCertificateStatus;
                    break;
                }

                case Opc.Ua.Gds.BrowseNames.CheckRevocationStatus:
                {
                    if (createOrReplace)
                    {
                        if (CheckRevocationStatus == null)
                        {
                            if (replacement == null)
                            {
                                CheckRevocationStatus = new CheckRevocationStatusMethodState(this);
                            }
                            else
                            {
                                CheckRevocationStatus = (CheckRevocationStatusMethodState)replacement;
                            }
                        }
                    }

                    instance = CheckRevocationStatus;
                    break;
                }
            }

            if (instance != null)
            {
                return instance;
            }

            return base.FindChild(context, browseName, createOrReplace, replacement);
        }
        #endregion

        #region Private Fields
        private CertificateGroupFolderState m_certificateGroups;
        private StartSigningRequestMethodState m_startSigningRequestMethod;
        private StartNewKeyPairRequestMethodState m_startNewKeyPairRequestMethod;
        private FinishRequestMethodState m_finishRequestMethod;
        private RevokeCertificateMethodState m_revokeCertificateMethod;
        private GetCertificateGroupsMethodState m_getCertificateGroupsMethod;
        private GetCertificatesMethodState m_getCertificatesMethod;
        private GetTrustListMethodState m_getTrustListMethod;
        private GetCertificateStatusMethodState m_getCertificateStatusMethod;
        private CheckRevocationStatusMethodState m_checkRevocationStatusMethod;
        #endregion
    }
    #endif
    #endregion

    #region CertificateRequestedAuditEventState Class
    #if (!OPCUA_EXCLUDE_CertificateRequestedAuditEventState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class CertificateRequestedAuditEventState : AuditUpdateMethodEventState
    {
        #region Constructors
        public CertificateRequestedAuditEventState(NodeState parent) : base(parent)
        {
        }

        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(Opc.Ua.Gds.ObjectTypes.CertificateRequestedAuditEventType, Opc.Ua.Gds.Namespaces.OpcUaGds, namespaceUris);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAACAAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvR0RTL/////8EYIACAQAAAAEAKgAAAENl" +
           "cnRpZmljYXRlUmVxdWVzdGVkQXVkaXRFdmVudFR5cGVJbnN0YW5jZQEBWwABAVsAWwAAAP////8RAAAA" +
           "FWCJCgIAAAAAAAcAAABFdmVudElkAgEAbEIPAAAuAERsQg8AAA//////AQH/////AAAAABVgiQoCAAAA" +
           "AAAJAAAARXZlbnRUeXBlAgEAbUIPAAAuAERtQg8AABH/////AQH/////AAAAABVgiQoCAAAAAAAKAAAA" +
           "U291cmNlTm9kZQIBAG5CDwAALgBEbkIPAAAR/////wEB/////wAAAAAVYIkKAgAAAAAACgAAAFNvdXJj" +
           "ZU5hbWUCAQBvQg8AAC4ARG9CDwAADP////8BAf////8AAAAAFWCJCgIAAAAAAAQAAABUaW1lAgEAcEIP" +
           "AAAuAERwQg8AAQAmAf////8BAf////8AAAAAFWCJCgIAAAAAAAsAAABSZWNlaXZlVGltZQIBAHFCDwAA" +
           "LgBEcUIPAAEAJgH/////AQH/////AAAAABVgiQoCAAAAAAAHAAAATWVzc2FnZQIBAHNCDwAALgBEc0IP" +
           "AAAV/////wEB/////wAAAAAVYIkKAgAAAAAACAAAAFNldmVyaXR5AgEAdEIPAAAuAER0Qg8AAAX/////" +
           "AQH/////AAAAABVgiQoCAAAAAAAPAAAAQWN0aW9uVGltZVN0YW1wAgEAeUIPAAAuAER5Qg8AAQAmAf//" +
           "//8BAf////8AAAAAFWCJCgIAAAAAAAYAAABTdGF0dXMCAQB6Qg8AAC4ARHpCDwAAAf////8BAf////8A" +
           "AAAAFWCJCgIAAAAAAAgAAABTZXJ2ZXJJZAIBAHtCDwAALgBEe0IPAAAM/////wEB/////wAAAAAVYIkK" +
           "AgAAAAAAEgAAAENsaWVudEF1ZGl0RW50cnlJZAIBAHxCDwAALgBEfEIPAAAM/////wEB/////wAAAAAV" +
           "YIkKAgAAAAAADAAAAENsaWVudFVzZXJJZAIBAH1CDwAALgBEfUIPAAAM/////wEB/////wAAAAAVYIkK" +
           "AgAAAAAACAAAAE1ldGhvZElkAgEAf0IPAAAuAER/Qg8AABH/////AQH/////AAAAABdgiQoCAAAAAAAO" +
           "AAAASW5wdXRBcmd1bWVudHMCAQCBQg8AAC4ARIFCDwAAGAEAAAABAAAAAAAAAAEB/////wAAAAAVYIkK" +
           "AgAAAAEAEAAAAENlcnRpZmljYXRlR3JvdXABAc0CAC4ARM0CAAAAEf////8BAf////8AAAAAFWCJCgIA" +
           "AAABAA8AAABDZXJ0aWZpY2F0ZVR5cGUBAc4CAC4ARM4CAAAAEf////8BAf////8AAAAA";
        #endregion
        #endif
        #endregion

        #region Public Properties
        public PropertyState<NodeId> CertificateGroup
        {
            get => m_certificateGroup;

            set
            {
                if (!Object.ReferenceEquals(m_certificateGroup, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_certificateGroup = value;
            }
        }

        public PropertyState<NodeId> CertificateType
        {
            get => m_certificateType;

            set
            {
                if (!Object.ReferenceEquals(m_certificateType, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_certificateType = value;
            }
        }
        #endregion

        #region Overridden Methods
        public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
        {
            if (m_certificateGroup != null)
            {
                children.Add(m_certificateGroup);
            }

            if (m_certificateType != null)
            {
                children.Add(m_certificateType);
            }

            base.GetChildren(context, children);
        }
            
        protected override void RemoveExplicitlyDefinedChild(BaseInstanceState child)
        {
            if (Object.ReferenceEquals(m_certificateGroup, child))
            {
                m_certificateGroup = null;
                return;
            }

            if (Object.ReferenceEquals(m_certificateType, child))
            {
                m_certificateType = null;
                return;
            }

            base.RemoveExplicitlyDefinedChild(child);
        }

        protected override BaseInstanceState FindChild(
            ISystemContext context,
            QualifiedName browseName,
            bool createOrReplace,
            BaseInstanceState replacement)
        {
            if (QualifiedName.IsNull(browseName))
            {
                return null;
            }

            BaseInstanceState instance = null;

            switch (browseName.Name)
            {
                case Opc.Ua.Gds.BrowseNames.CertificateGroup:
                {
                    if (createOrReplace)
                    {
                        if (CertificateGroup == null)
                        {
                            if (replacement == null)
                            {
                                CertificateGroup = new PropertyState<NodeId>(this);
                            }
                            else
                            {
                                CertificateGroup = (PropertyState<NodeId>)replacement;
                            }
                        }
                    }

                    instance = CertificateGroup;
                    break;
                }

                case Opc.Ua.Gds.BrowseNames.CertificateType:
                {
                    if (createOrReplace)
                    {
                        if (CertificateType == null)
                        {
                            if (replacement == null)
                            {
                                CertificateType = new PropertyState<NodeId>(this);
                            }
                            else
                            {
                                CertificateType = (PropertyState<NodeId>)replacement;
                            }
                        }
                    }

                    instance = CertificateType;
                    break;
                }
            }

            if (instance != null)
            {
                return instance;
            }

            return base.FindChild(context, browseName, createOrReplace, replacement);
        }
        #endregion

        #region Private Fields
        private PropertyState<NodeId> m_certificateGroup;
        private PropertyState<NodeId> m_certificateType;
        #endregion
    }
    #endif
    #endregion

    #region CertificateDeliveredAuditEventState Class
    #if (!OPCUA_EXCLUDE_CertificateDeliveredAuditEventState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class CertificateDeliveredAuditEventState : AuditUpdateMethodEventState
    {
        #region Constructors
        public CertificateDeliveredAuditEventState(NodeState parent) : base(parent)
        {
        }

        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(Opc.Ua.Gds.ObjectTypes.CertificateDeliveredAuditEventType, Opc.Ua.Gds.Namespaces.OpcUaGds, namespaceUris);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAACAAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvR0RTL/////8EYIACAQAAAAEAKgAAAENl" +
           "cnRpZmljYXRlRGVsaXZlcmVkQXVkaXRFdmVudFR5cGVJbnN0YW5jZQEBbQABAW0AbQAAAP////8RAAAA" +
           "FWCJCgIAAAAAAAcAAABFdmVudElkAgEAg0IPAAAuAESDQg8AAA//////AQH/////AAAAABVgiQoCAAAA" +
           "AAAJAAAARXZlbnRUeXBlAgEAhEIPAAAuAESEQg8AABH/////AQH/////AAAAABVgiQoCAAAAAAAKAAAA" +
           "U291cmNlTm9kZQIBAIVCDwAALgBEhUIPAAAR/////wEB/////wAAAAAVYIkKAgAAAAAACgAAAFNvdXJj" +
           "ZU5hbWUCAQCGQg8AAC4ARIZCDwAADP////8BAf////8AAAAAFWCJCgIAAAAAAAQAAABUaW1lAgEAh0IP" +
           "AAAuAESHQg8AAQAmAf////8BAf////8AAAAAFWCJCgIAAAAAAAsAAABSZWNlaXZlVGltZQIBAIhCDwAA" +
           "LgBEiEIPAAEAJgH/////AQH/////AAAAABVgiQoCAAAAAAAHAAAATWVzc2FnZQIBAIpCDwAALgBEikIP" +
           "AAAV/////wEB/////wAAAAAVYIkKAgAAAAAACAAAAFNldmVyaXR5AgEAi0IPAAAuAESLQg8AAAX/////" +
           "AQH/////AAAAABVgiQoCAAAAAAAPAAAAQWN0aW9uVGltZVN0YW1wAgEAkEIPAAAuAESQQg8AAQAmAf//" +
           "//8BAf////8AAAAAFWCJCgIAAAAAAAYAAABTdGF0dXMCAQCRQg8AAC4ARJFCDwAAAf////8BAf////8A" +
           "AAAAFWCJCgIAAAAAAAgAAABTZXJ2ZXJJZAIBAJJCDwAALgBEkkIPAAAM/////wEB/////wAAAAAVYIkK" +
           "AgAAAAAAEgAAAENsaWVudEF1ZGl0RW50cnlJZAIBAJNCDwAALgBEk0IPAAAM/////wEB/////wAAAAAV" +
           "YIkKAgAAAAAADAAAAENsaWVudFVzZXJJZAIBAJRCDwAALgBElEIPAAAM/////wEB/////wAAAAAVYIkK" +
           "AgAAAAAACAAAAE1ldGhvZElkAgEAlkIPAAAuAESWQg8AABH/////AQH/////AAAAABdgiQoCAAAAAAAO" +
           "AAAASW5wdXRBcmd1bWVudHMCAQCYQg8AAC4ARJhCDwAAGAEAAAABAAAAAAAAAAEB/////wAAAAAVYIkK" +
           "AgAAAAEAEAAAAENlcnRpZmljYXRlR3JvdXABAc8CAC4ARM8CAAAAEf////8BAf////8AAAAAFWCJCgIA" +
           "AAABAA8AAABDZXJ0aWZpY2F0ZVR5cGUBAdACAC4ARNACAAAAEf////8BAf////8AAAAA";
        #endregion
        #endif
        #endregion

        #region Public Properties
        public PropertyState<NodeId> CertificateGroup
        {
            get => m_certificateGroup;

            set
            {
                if (!Object.ReferenceEquals(m_certificateGroup, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_certificateGroup = value;
            }
        }

        public PropertyState<NodeId> CertificateType
        {
            get => m_certificateType;

            set
            {
                if (!Object.ReferenceEquals(m_certificateType, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_certificateType = value;
            }
        }
        #endregion

        #region Overridden Methods
        public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
        {
            if (m_certificateGroup != null)
            {
                children.Add(m_certificateGroup);
            }

            if (m_certificateType != null)
            {
                children.Add(m_certificateType);
            }

            base.GetChildren(context, children);
        }
            
        protected override void RemoveExplicitlyDefinedChild(BaseInstanceState child)
        {
            if (Object.ReferenceEquals(m_certificateGroup, child))
            {
                m_certificateGroup = null;
                return;
            }

            if (Object.ReferenceEquals(m_certificateType, child))
            {
                m_certificateType = null;
                return;
            }

            base.RemoveExplicitlyDefinedChild(child);
        }

        protected override BaseInstanceState FindChild(
            ISystemContext context,
            QualifiedName browseName,
            bool createOrReplace,
            BaseInstanceState replacement)
        {
            if (QualifiedName.IsNull(browseName))
            {
                return null;
            }

            BaseInstanceState instance = null;

            switch (browseName.Name)
            {
                case Opc.Ua.Gds.BrowseNames.CertificateGroup:
                {
                    if (createOrReplace)
                    {
                        if (CertificateGroup == null)
                        {
                            if (replacement == null)
                            {
                                CertificateGroup = new PropertyState<NodeId>(this);
                            }
                            else
                            {
                                CertificateGroup = (PropertyState<NodeId>)replacement;
                            }
                        }
                    }

                    instance = CertificateGroup;
                    break;
                }

                case Opc.Ua.Gds.BrowseNames.CertificateType:
                {
                    if (createOrReplace)
                    {
                        if (CertificateType == null)
                        {
                            if (replacement == null)
                            {
                                CertificateType = new PropertyState<NodeId>(this);
                            }
                            else
                            {
                                CertificateType = (PropertyState<NodeId>)replacement;
                            }
                        }
                    }

                    instance = CertificateType;
                    break;
                }
            }

            if (instance != null)
            {
                return instance;
            }

            return base.FindChild(context, browseName, createOrReplace, replacement);
        }
        #endregion

        #region Private Fields
        private PropertyState<NodeId> m_certificateGroup;
        private PropertyState<NodeId> m_certificateType;
        #endregion
    }
    #endif
    #endregion

    #region CertificateRevokedAuditEventState Class
    #if (!OPCUA_EXCLUDE_CertificateRevokedAuditEventState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class CertificateRevokedAuditEventState : AuditUpdateMethodEventState
    {
        #region Constructors
        public CertificateRevokedAuditEventState(NodeState parent) : base(parent)
        {
        }

        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(Opc.Ua.Gds.ObjectTypes.CertificateRevokedAuditEventType, Opc.Ua.Gds.Namespaces.OpcUaGds, namespaceUris);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAACAAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvR0RTL/////8EYIACAQAAAAEAKAAAAENl" +
           "cnRpZmljYXRlUmV2b2tlZEF1ZGl0RXZlbnRUeXBlSW5zdGFuY2UBARsAAQEbABsAAAD/////DwAAABVg" +
           "iQoCAAAAAAAHAAAARXZlbnRJZAIBAJpCDwAALgBEmkIPAAAP/////wEB/////wAAAAAVYIkKAgAAAAAA" +
           "CQAAAEV2ZW50VHlwZQIBAJtCDwAALgBEm0IPAAAR/////wEB/////wAAAAAVYIkKAgAAAAAACgAAAFNv" +
           "dXJjZU5vZGUCAQCcQg8AAC4ARJxCDwAAEf////8BAf////8AAAAAFWCJCgIAAAAAAAoAAABTb3VyY2VO" +
           "YW1lAgEAnUIPAAAuAESdQg8AAAz/////AQH/////AAAAABVgiQoCAAAAAAAEAAAAVGltZQIBAJ5CDwAA" +
           "LgBEnkIPAAEAJgH/////AQH/////AAAAABVgiQoCAAAAAAALAAAAUmVjZWl2ZVRpbWUCAQCfQg8AAC4A" +
           "RJ9CDwABACYB/////wEB/////wAAAAAVYIkKAgAAAAAABwAAAE1lc3NhZ2UCAQChQg8AAC4ARKFCDwAA" +
           "Ff////8BAf////8AAAAAFWCJCgIAAAAAAAgAAABTZXZlcml0eQIBAKJCDwAALgBEokIPAAAF/////wEB" +
           "/////wAAAAAVYIkKAgAAAAAADwAAAEFjdGlvblRpbWVTdGFtcAIBAKdCDwAALgBEp0IPAAEAJgH/////" +
           "AQH/////AAAAABVgiQoCAAAAAAAGAAAAU3RhdHVzAgEAqEIPAAAuAESoQg8AAAH/////AQH/////AAAA" +
           "ABVgiQoCAAAAAAAIAAAAU2VydmVySWQCAQCpQg8AAC4ARKlCDwAADP////8BAf////8AAAAAFWCJCgIA" +
           "AAAAABIAAABDbGllbnRBdWRpdEVudHJ5SWQCAQCqQg8AAC4ARKpCDwAADP////8BAf////8AAAAAFWCJ" +
           "CgIAAAAAAAwAAABDbGllbnRVc2VySWQCAQCrQg8AAC4ARKtCDwAADP////8BAf////8AAAAAFWCJCgIA" +
           "AAAAAAgAAABNZXRob2RJZAIBAK1CDwAALgBErUIPAAAR/////wEB/////wAAAAAXYIkKAgAAAAAADgAA" +
           "AElucHV0QXJndW1lbnRzAgEAr0IPAAAuAESvQg8AABgBAAAAAQAAAAAAAAABAf////8AAAAA";
        #endregion
        #endif
        #endregion

        #region Public Properties
        #endregion

        #region Overridden Methods
        #endregion

        #region Private Fields
        #endregion
    }
    #endif
    #endregion

    #region KeyCredentialManagementFolderState Class
    #if (!OPCUA_EXCLUDE_KeyCredentialManagementFolderState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class KeyCredentialManagementFolderState : FolderState
    {
        #region Constructors
        public KeyCredentialManagementFolderState(NodeState parent) : base(parent)
        {
        }

        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(Opc.Ua.Gds.ObjectTypes.KeyCredentialManagementFolderType, Opc.Ua.Gds.Namespaces.OpcUaGds, namespaceUris);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAACAAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvR0RTL/////8EYIACAQAAAAEAKQAAAEtl" +
           "eUNyZWRlbnRpYWxNYW5hZ2VtZW50Rm9sZGVyVHlwZUluc3RhbmNlAQE3AAEBNwA3AAAA/////wEAAAAE" +
           "YMAKAQAAABcAAABTZXJ2aWNlTmFtZV9QbGFjZWhvbGRlcgEADQAAADxTZXJ2aWNlTmFtZT4BAT0AAC8B" +
           "AfwDPQAAAP////8EAAAAFWCJCgIAAAABAAsAAABSZXNvdXJjZVVyaQEBUwAALgBEUwAAAAAM/////wEB" +
           "/////wAAAAAXYIkKAgAAAAEACwAAAFByb2ZpbGVVcmlzAQGiAAAuAESiAAAAAAwBAAAAAQAAAAAAAAAB" +
           "Af////8AAAAABGGCCgQAAAABAAwAAABTdGFydFJlcXVlc3QBAagAAC8BAf8DqAAAAAEB/////wIAAAAX" +
           "YKkKAgAAAAAADgAAAElucHV0QXJndW1lbnRzAQGrAAAuAESrAAAAlgQAAAABACoBAR0AAAAOAAAAQXBw" +
           "bGljYXRpb25VcmkADP////8AAAAAAAEAKgEBGAAAAAkAAABQdWJsaWNLZXkAD/////8AAAAAAAEAKgEB" +
           "IAAAABEAAABTZWN1cml0eVBvbGljeVVyaQAM/////wAAAAAAAQAqAQEhAAAADgAAAFJlcXVlc3RlZFJv" +
           "bGVzABEBAAAAAQAAAAAAAAAAAQAoAQEAAAABAAAABAAAAAEB/////wAAAAAXYKkKAgAAAAAADwAAAE91" +
           "dHB1dEFyZ3VtZW50cwEBwwAALgBEwwAAAJYBAAAAAQAqAQEYAAAACQAAAFJlcXVlc3RJZAAR/////wAA" +
           "AAAAAQAoAQEAAAABAAAAAQAAAAEB/////wAAAAAEYYIKBAAAAAEADQAAAEZpbmlzaFJlcXVlc3QBAcQA" +
           "AC8BAQIExAAAAAEB/////wIAAAAXYKkKAgAAAAAADgAAAElucHV0QXJndW1lbnRzAQHKAAAuAETKAAAA" +
           "lgIAAAABACoBARgAAAAJAAAAUmVxdWVzdElkABH/////AAAAAAABACoBARwAAAANAAAAQ2FuY2VsUmVx" +
           "dWVzdAAB/////wAAAAAAAQAoAQEAAAABAAAAAgAAAAEB/////wAAAAAXYKkKAgAAAAAADwAAAE91dHB1" +
           "dEFyZ3VtZW50cwEBywAALgBEywAAAJYFAAAAAQAqAQEbAAAADAAAAENyZWRlbnRpYWxJZAAM/////wAA" +
           "AAAAAQAqAQEfAAAAEAAAAENyZWRlbnRpYWxTZWNyZXQAD/////8AAAAAAAEAKgEBJAAAABUAAABDZXJ0" +
           "aWZpY2F0ZVRodW1icHJpbnQADP////8AAAAAAAEAKgEBIAAAABEAAABTZWN1cml0eVBvbGljeVVyaQAM" +
           "/////wAAAAAAAQAqAQEfAAAADAAAAEdyYW50ZWRSb2xlcwARAQAAAAEAAAAAAAAAAAEAKAEBAAAAAQAA" +
           "AAUAAAABAf////8AAAAA";
        #endregion
        #endif
        #endregion

        #region Public Properties
        #endregion

        #region Overridden Methods
        #endregion

        #region Private Fields
        #endregion
    }
    #endif
    #endregion

    #region KeyCredentialServiceState Class
    #if (!OPCUA_EXCLUDE_KeyCredentialServiceState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class KeyCredentialServiceState : BaseObjectState
    {
        #region Constructors
        public KeyCredentialServiceState(NodeState parent) : base(parent)
        {
        }

        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(Opc.Ua.Gds.ObjectTypes.KeyCredentialServiceType, Opc.Ua.Gds.Namespaces.OpcUaGds, namespaceUris);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);

            if (SecurityPolicyUris != null)
            {
                SecurityPolicyUris.Initialize(context, SecurityPolicyUris_InitializationString);
            }

            if (Revoke != null)
            {
                Revoke.Initialize(context, Revoke_InitializationString);
            }
        }

        #region Initialization String
        private const string SecurityPolicyUris_InitializationString =
           "AQAAACAAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvR0RTL/////8XYIkKAgAAAAEAEgAAAFNl" +
           "Y3VyaXR5UG9saWN5VXJpcwEB7wEALgBE7wEAAAAMAQAAAAEAAAAAAAAAAQH/////AAAAAA==";

        private const string Revoke_InitializationString =
           "AQAAACAAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvR0RTL/////8EYYIKBAAAAAEABgAAAFJl" +
           "dm9rZQEBBQQALwEBBQQFBAAAAQH/////AQAAABdgqQoCAAAAAAAOAAAASW5wdXRBcmd1bWVudHMBAQYE" +
           "AC4ARAYEAACWAQAAAAEAKgEBGwAAAAwAAABDcmVkZW50aWFsSWQADP////8AAAAAAAEAKAEBAAAAAQAA" +
           "AAEAAAABAf////8AAAAA";

        private const string InitializationString =
           "AQAAACAAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvR0RTL/////8EYIACAQAAAAEAIAAAAEtl" +
           "eUNyZWRlbnRpYWxTZXJ2aWNlVHlwZUluc3RhbmNlAQH8AwEB/AP8AwAA/////wYAAAAVYIkKAgAAAAEA" +
           "CwAAAFJlc291cmNlVXJpAQH9AwAuAET9AwAAAAz/////AQH/////AAAAABdgiQoCAAAAAQALAAAAUHJv" +
           "ZmlsZVVyaXMBAf4DAC4ARP4DAAAADAEAAAABAAAAAAAAAAEB/////wAAAAAXYIkKAgAAAAEAEgAAAFNl" +
           "Y3VyaXR5UG9saWN5VXJpcwEB7wEALgBE7wEAAAAMAQAAAAEAAAAAAAAAAQH/////AAAAAARhggoEAAAA" +
           "AQAMAAAAU3RhcnRSZXF1ZXN0AQH/AwAvAQH/A/8DAAABAf////8CAAAAF2CpCgIAAAAAAA4AAABJbnB1" +
           "dEFyZ3VtZW50cwEBAAQALgBEAAQAAJYEAAAAAQAqAQEdAAAADgAAAEFwcGxpY2F0aW9uVXJpAAz/////" +
           "AAAAAAABACoBARgAAAAJAAAAUHVibGljS2V5AA//////AAAAAAABACoBASAAAAARAAAAU2VjdXJpdHlQ" +
           "b2xpY3lVcmkADP////8AAAAAAAEAKgEBIQAAAA4AAABSZXF1ZXN0ZWRSb2xlcwARAQAAAAEAAAAAAAAA" +
           "AAEAKAEBAAAAAQAAAAQAAAABAf////8AAAAAF2CpCgIAAAAAAA8AAABPdXRwdXRBcmd1bWVudHMBAQEE" +
           "AC4ARAEEAACWAQAAAAEAKgEBGAAAAAkAAABSZXF1ZXN0SWQAEf////8AAAAAAAEAKAEBAAAAAQAAAAEA" +
           "AAABAf////8AAAAABGGCCgQAAAABAA0AAABGaW5pc2hSZXF1ZXN0AQECBAAvAQECBAIEAAABAf////8C" +
           "AAAAF2CpCgIAAAAAAA4AAABJbnB1dEFyZ3VtZW50cwEBAwQALgBEAwQAAJYCAAAAAQAqAQEYAAAACQAA" +
           "AFJlcXVlc3RJZAAR/////wAAAAAAAQAqAQEcAAAADQAAAENhbmNlbFJlcXVlc3QAAf////8AAAAAAAEA" +
           "KAEBAAAAAQAAAAIAAAABAf////8AAAAAF2CpCgIAAAAAAA8AAABPdXRwdXRBcmd1bWVudHMBAQQEAC4A" +
           "RAQEAACWBQAAAAEAKgEBGwAAAAwAAABDcmVkZW50aWFsSWQADP////8AAAAAAAEAKgEBHwAAABAAAABD" +
           "cmVkZW50aWFsU2VjcmV0AA//////AAAAAAABACoBASQAAAAVAAAAQ2VydGlmaWNhdGVUaHVtYnByaW50" +
           "AAz/////AAAAAAABACoBASAAAAARAAAAU2VjdXJpdHlQb2xpY3lVcmkADP////8AAAAAAAEAKgEBHwAA" +
           "AAwAAABHcmFudGVkUm9sZXMAEQEAAAABAAAAAAAAAAABACgBAQAAAAEAAAAFAAAAAQH/////AAAAAARh" +
           "ggoEAAAAAQAGAAAAUmV2b2tlAQEFBAAvAQEFBAUEAAABAf////8BAAAAF2CpCgIAAAAAAA4AAABJbnB1" +
           "dEFyZ3VtZW50cwEBBgQALgBEBgQAAJYBAAAAAQAqAQEbAAAADAAAAENyZWRlbnRpYWxJZAAM/////wAA" +
           "AAAAAQAoAQEAAAABAAAAAQAAAAEB/////wAAAAA=";
        #endregion
        #endif
        #endregion

        #region Public Properties
        public PropertyState<string> ResourceUri
        {
            get => m_resourceUri;

            set
            {
                if (!Object.ReferenceEquals(m_resourceUri, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_resourceUri = value;
            }
        }

        public PropertyState<string[]> ProfileUris
        {
            get => m_profileUris;

            set
            {
                if (!Object.ReferenceEquals(m_profileUris, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_profileUris = value;
            }
        }

        public PropertyState<string[]> SecurityPolicyUris
        {
            get => m_securityPolicyUris;

            set
            {
                if (!Object.ReferenceEquals(m_securityPolicyUris, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_securityPolicyUris = value;
            }
        }

        public KeyCredentialStartRequestMethodState StartRequest
        {
            get => m_startRequestMethod;

            set
            {
                if (!Object.ReferenceEquals(m_startRequestMethod, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_startRequestMethod = value;
            }
        }

        public KeyCredentialFinishRequestMethodState FinishRequest
        {
            get => m_finishRequestMethod;

            set
            {
                if (!Object.ReferenceEquals(m_finishRequestMethod, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_finishRequestMethod = value;
            }
        }

        public KeyCredentialRevokeMethodState Revoke
        {
            get => m_revokeMethod;

            set
            {
                if (!Object.ReferenceEquals(m_revokeMethod, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_revokeMethod = value;
            }
        }
        #endregion

        #region Overridden Methods
        public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
        {
            if (m_resourceUri != null)
            {
                children.Add(m_resourceUri);
            }

            if (m_profileUris != null)
            {
                children.Add(m_profileUris);
            }

            if (m_securityPolicyUris != null)
            {
                children.Add(m_securityPolicyUris);
            }

            if (m_startRequestMethod != null)
            {
                children.Add(m_startRequestMethod);
            }

            if (m_finishRequestMethod != null)
            {
                children.Add(m_finishRequestMethod);
            }

            if (m_revokeMethod != null)
            {
                children.Add(m_revokeMethod);
            }

            base.GetChildren(context, children);
        }
            
        protected override void RemoveExplicitlyDefinedChild(BaseInstanceState child)
        {
            if (Object.ReferenceEquals(m_resourceUri, child))
            {
                m_resourceUri = null;
                return;
            }

            if (Object.ReferenceEquals(m_profileUris, child))
            {
                m_profileUris = null;
                return;
            }

            if (Object.ReferenceEquals(m_securityPolicyUris, child))
            {
                m_securityPolicyUris = null;
                return;
            }

            if (Object.ReferenceEquals(m_startRequestMethod, child))
            {
                m_startRequestMethod = null;
                return;
            }

            if (Object.ReferenceEquals(m_finishRequestMethod, child))
            {
                m_finishRequestMethod = null;
                return;
            }

            if (Object.ReferenceEquals(m_revokeMethod, child))
            {
                m_revokeMethod = null;
                return;
            }

            base.RemoveExplicitlyDefinedChild(child);
        }

        protected override BaseInstanceState FindChild(
            ISystemContext context,
            QualifiedName browseName,
            bool createOrReplace,
            BaseInstanceState replacement)
        {
            if (QualifiedName.IsNull(browseName))
            {
                return null;
            }

            BaseInstanceState instance = null;

            switch (browseName.Name)
            {
                case Opc.Ua.Gds.BrowseNames.ResourceUri:
                {
                    if (createOrReplace)
                    {
                        if (ResourceUri == null)
                        {
                            if (replacement == null)
                            {
                                ResourceUri = new PropertyState<string>(this);
                            }
                            else
                            {
                                ResourceUri = (PropertyState<string>)replacement;
                            }
                        }
                    }

                    instance = ResourceUri;
                    break;
                }

                case Opc.Ua.Gds.BrowseNames.ProfileUris:
                {
                    if (createOrReplace)
                    {
                        if (ProfileUris == null)
                        {
                            if (replacement == null)
                            {
                                ProfileUris = new PropertyState<string[]>(this);
                            }
                            else
                            {
                                ProfileUris = (PropertyState<string[]>)replacement;
                            }
                        }
                    }

                    instance = ProfileUris;
                    break;
                }

                case Opc.Ua.Gds.BrowseNames.SecurityPolicyUris:
                {
                    if (createOrReplace)
                    {
                        if (SecurityPolicyUris == null)
                        {
                            if (replacement == null)
                            {
                                SecurityPolicyUris = new PropertyState<string[]>(this);
                            }
                            else
                            {
                                SecurityPolicyUris = (PropertyState<string[]>)replacement;
                            }
                        }
                    }

                    instance = SecurityPolicyUris;
                    break;
                }

                case Opc.Ua.Gds.BrowseNames.StartRequest:
                {
                    if (createOrReplace)
                    {
                        if (StartRequest == null)
                        {
                            if (replacement == null)
                            {
                                StartRequest = new KeyCredentialStartRequestMethodState(this);
                            }
                            else
                            {
                                StartRequest = (KeyCredentialStartRequestMethodState)replacement;
                            }
                        }
                    }

                    instance = StartRequest;
                    break;
                }

                case Opc.Ua.Gds.BrowseNames.FinishRequest:
                {
                    if (createOrReplace)
                    {
                        if (FinishRequest == null)
                        {
                            if (replacement == null)
                            {
                                FinishRequest = new KeyCredentialFinishRequestMethodState(this);
                            }
                            else
                            {
                                FinishRequest = (KeyCredentialFinishRequestMethodState)replacement;
                            }
                        }
                    }

                    instance = FinishRequest;
                    break;
                }

                case Opc.Ua.Gds.BrowseNames.Revoke:
                {
                    if (createOrReplace)
                    {
                        if (Revoke == null)
                        {
                            if (replacement == null)
                            {
                                Revoke = new KeyCredentialRevokeMethodState(this);
                            }
                            else
                            {
                                Revoke = (KeyCredentialRevokeMethodState)replacement;
                            }
                        }
                    }

                    instance = Revoke;
                    break;
                }
            }

            if (instance != null)
            {
                return instance;
            }

            return base.FindChild(context, browseName, createOrReplace, replacement);
        }
        #endregion

        #region Private Fields
        private PropertyState<string> m_resourceUri;
        private PropertyState<string[]> m_profileUris;
        private PropertyState<string[]> m_securityPolicyUris;
        private KeyCredentialStartRequestMethodState m_startRequestMethod;
        private KeyCredentialFinishRequestMethodState m_finishRequestMethod;
        private KeyCredentialRevokeMethodState m_revokeMethod;
        #endregion
    }
    #endif
    #endregion

    #region KeyCredentialStartRequestMethodState Class
    #if (!OPCUA_EXCLUDE_KeyCredentialStartRequestMethodState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class KeyCredentialStartRequestMethodState : MethodState
    {
        #region Constructors
        public KeyCredentialStartRequestMethodState(NodeState parent) : base(parent)
        {
        }

        public new static NodeState Construct(NodeState parent)
        {
            return new KeyCredentialStartRequestMethodState(parent);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAACAAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvR0RTL/////8EYYIKBAAAAAEAIwAAAEtl" +
           "eUNyZWRlbnRpYWxTdGFydFJlcXVlc3RNZXRob2RUeXBlAQEHBAAvAQEHBAcEAAABAf////8CAAAAF2Cp" +
           "CgIAAAAAAA4AAABJbnB1dEFyZ3VtZW50cwEBCAQALgBECAQAAJYEAAAAAQAqAQEdAAAADgAAAEFwcGxp" +
           "Y2F0aW9uVXJpAAz/////AAAAAAABACoBARgAAAAJAAAAUHVibGljS2V5AA//////AAAAAAABACoBASAA" +
           "AAARAAAAU2VjdXJpdHlQb2xpY3lVcmkADP////8AAAAAAAEAKgEBIQAAAA4AAABSZXF1ZXN0ZWRSb2xl" +
           "cwARAQAAAAEAAAAAAAAAAAEAKAEBAAAAAQAAAAQAAAABAf////8AAAAAF2CpCgIAAAAAAA8AAABPdXRw" +
           "dXRBcmd1bWVudHMBAQkEAC4ARAkEAACWAQAAAAEAKgEBGAAAAAkAAABSZXF1ZXN0SWQAEf////8AAAAA" +
           "AAEAKAEBAAAAAQAAAAEAAAABAf////8AAAAA";
        #endregion
        #endif
        #endregion

        #region Event Callbacks
        public KeyCredentialStartRequestMethodStateMethodCallHandler OnCall;

        public KeyCredentialStartRequestMethodStateMethodAsyncCallHandler OnCallAsync;
        #endregion

        #region Public Properties
        #endregion

        #region Overridden Methods
        protected override ServiceResult Call(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments)
        {
            if (OnCall == null)
            {
                return base.Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            ServiceResult _result = null;

            string applicationUri = (string)_inputArguments[0];
            byte[] publicKey = (byte[])_inputArguments[1];
            string securityPolicyUri = (string)_inputArguments[2];
            NodeId[] requestedRoles = (NodeId[])_inputArguments[3];

            NodeId requestId = (NodeId)_outputArguments[0];

            if (OnCall != null)
            {
                _result = OnCall(
                    _context,
                    this,
                    _objectId,
                    applicationUri,
                    publicKey,
                    securityPolicyUri,
                    requestedRoles,
                    ref requestId);
            }

            _outputArguments[0] = requestId;

            return _result;
        }

        #if (OPCUA_INCLUDE_ASYNC)
        protected override async ValueTask<ServiceResult> CallAsync(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments,
            CancellationToken cancellationToken = default)
        {
            if (OnCall == null && OnCallAsync == null)
            {
                return await base.CallAsync(_context, _objectId, _inputArguments, _outputArguments, cancellationToken).ConfigureAwait(false);
            }

            KeyCredentialStartRequestMethodStateResult _result = null;

            string applicationUri = (string)_inputArguments[0];
            byte[] publicKey = (byte[])_inputArguments[1];
            string securityPolicyUri = (string)_inputArguments[2];
            NodeId[] requestedRoles = (NodeId[])_inputArguments[3];

            if (OnCallAsync != null)
            {
                _result = await OnCallAsync(
                    _context,
                    this,
                    _objectId,
                    applicationUri,
                    publicKey,
                    securityPolicyUri,
                    requestedRoles,
                    cancellationToken).ConfigureAwait(false);
            }
            else if (OnCall != null)
            {
                return Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            _outputArguments[0] = _result.RequestId;

            return _result.ServiceResult;
        }
        #endif

        #endregion

        #region Private Fields
        #endregion
    }

    /// <exclude />
    public delegate ServiceResult KeyCredentialStartRequestMethodStateMethodCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        string applicationUri,
        byte[] publicKey,
        string securityPolicyUri,
        NodeId[] requestedRoles,
        ref NodeId requestId);

    /// <exclude />
    public partial class KeyCredentialStartRequestMethodStateResult
    {
        public ServiceResult ServiceResult { get; set; }
        public NodeId RequestId { get; set; }
    }

    /// <exclude />
    public delegate ValueTask<KeyCredentialStartRequestMethodStateResult> KeyCredentialStartRequestMethodStateMethodAsyncCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        string applicationUri,
        byte[] publicKey,
        string securityPolicyUri,
        NodeId[] requestedRoles,
        CancellationToken cancellationToken);
    #endif
    #endregion

    #region KeyCredentialFinishRequestMethodState Class
    #if (!OPCUA_EXCLUDE_KeyCredentialFinishRequestMethodState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class KeyCredentialFinishRequestMethodState : MethodState
    {
        #region Constructors
        public KeyCredentialFinishRequestMethodState(NodeState parent) : base(parent)
        {
        }

        public new static NodeState Construct(NodeState parent)
        {
            return new KeyCredentialFinishRequestMethodState(parent);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAACAAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvR0RTL/////8EYYIKBAAAAAEAJAAAAEtl" +
           "eUNyZWRlbnRpYWxGaW5pc2hSZXF1ZXN0TWV0aG9kVHlwZQEBCgQALwEBCgQKBAAAAQH/////AgAAABdg" +
           "qQoCAAAAAAAOAAAASW5wdXRBcmd1bWVudHMBAQsEAC4ARAsEAACWAgAAAAEAKgEBGAAAAAkAAABSZXF1" +
           "ZXN0SWQAEf////8AAAAAAAEAKgEBHAAAAA0AAABDYW5jZWxSZXF1ZXN0AAH/////AAAAAAABACgBAQAA" +
           "AAEAAAACAAAAAQH/////AAAAABdgqQoCAAAAAAAPAAAAT3V0cHV0QXJndW1lbnRzAQEMBAAuAEQMBAAA" +
           "lgUAAAABACoBARsAAAAMAAAAQ3JlZGVudGlhbElkAAz/////AAAAAAABACoBAR8AAAAQAAAAQ3JlZGVu" +
           "dGlhbFNlY3JldAAP/////wAAAAAAAQAqAQEkAAAAFQAAAENlcnRpZmljYXRlVGh1bWJwcmludAAM////" +
           "/wAAAAAAAQAqAQEgAAAAEQAAAFNlY3VyaXR5UG9saWN5VXJpAAz/////AAAAAAABACoBAR8AAAAMAAAA" +
           "R3JhbnRlZFJvbGVzABEBAAAAAQAAAAAAAAAAAQAoAQEAAAABAAAABQAAAAEB/////wAAAAA=";
        #endregion
        #endif
        #endregion

        #region Event Callbacks
        public KeyCredentialFinishRequestMethodStateMethodCallHandler OnCall;

        public KeyCredentialFinishRequestMethodStateMethodAsyncCallHandler OnCallAsync;
        #endregion

        #region Public Properties
        #endregion

        #region Overridden Methods
        protected override ServiceResult Call(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments)
        {
            if (OnCall == null)
            {
                return base.Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            ServiceResult _result = null;

            NodeId requestId = (NodeId)_inputArguments[0];
            bool cancelRequest = (bool)_inputArguments[1];

            string credentialId = (string)_outputArguments[0];
            byte[] credentialSecret = (byte[])_outputArguments[1];
            string certificateThumbprint = (string)_outputArguments[2];
            string securityPolicyUri = (string)_outputArguments[3];
            NodeId[] grantedRoles = (NodeId[])_outputArguments[4];

            if (OnCall != null)
            {
                _result = OnCall(
                    _context,
                    this,
                    _objectId,
                    requestId,
                    cancelRequest,
                    ref credentialId,
                    ref credentialSecret,
                    ref certificateThumbprint,
                    ref securityPolicyUri,
                    ref grantedRoles);
            }

            _outputArguments[0] = credentialId;
            _outputArguments[1] = credentialSecret;
            _outputArguments[2] = certificateThumbprint;
            _outputArguments[3] = securityPolicyUri;
            _outputArguments[4] = grantedRoles;

            return _result;
        }

        #if (OPCUA_INCLUDE_ASYNC)
        protected override async ValueTask<ServiceResult> CallAsync(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments,
            CancellationToken cancellationToken = default)
        {
            if (OnCall == null && OnCallAsync == null)
            {
                return await base.CallAsync(_context, _objectId, _inputArguments, _outputArguments, cancellationToken).ConfigureAwait(false);
            }

            KeyCredentialFinishRequestMethodStateResult _result = null;

            NodeId requestId = (NodeId)_inputArguments[0];
            bool cancelRequest = (bool)_inputArguments[1];

            if (OnCallAsync != null)
            {
                _result = await OnCallAsync(
                    _context,
                    this,
                    _objectId,
                    requestId,
                    cancelRequest,
                    cancellationToken).ConfigureAwait(false);
            }
            else if (OnCall != null)
            {
                return Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            _outputArguments[0] = _result.CredentialId;
            _outputArguments[1] = _result.CredentialSecret;
            _outputArguments[2] = _result.CertificateThumbprint;
            _outputArguments[3] = _result.SecurityPolicyUri;
            _outputArguments[4] = _result.GrantedRoles;

            return _result.ServiceResult;
        }
        #endif

        #endregion

        #region Private Fields
        #endregion
    }

    /// <exclude />
    public delegate ServiceResult KeyCredentialFinishRequestMethodStateMethodCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        NodeId requestId,
        bool cancelRequest,
        ref string credentialId,
        ref byte[] credentialSecret,
        ref string certificateThumbprint,
        ref string securityPolicyUri,
        ref NodeId[] grantedRoles);

    /// <exclude />
    public partial class KeyCredentialFinishRequestMethodStateResult
    {
        public ServiceResult ServiceResult { get; set; }
        public string CredentialId { get; set; }
        public byte[] CredentialSecret { get; set; }
        public string CertificateThumbprint { get; set; }
        public string SecurityPolicyUri { get; set; }
        public NodeId[] GrantedRoles { get; set; }
    }

    /// <exclude />
    public delegate ValueTask<KeyCredentialFinishRequestMethodStateResult> KeyCredentialFinishRequestMethodStateMethodAsyncCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        NodeId requestId,
        bool cancelRequest,
        CancellationToken cancellationToken);
    #endif
    #endregion

    #region KeyCredentialRevokeMethodState Class
    #if (!OPCUA_EXCLUDE_KeyCredentialRevokeMethodState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class KeyCredentialRevokeMethodState : MethodState
    {
        #region Constructors
        public KeyCredentialRevokeMethodState(NodeState parent) : base(parent)
        {
        }

        public new static NodeState Construct(NodeState parent)
        {
            return new KeyCredentialRevokeMethodState(parent);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAACAAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvR0RTL/////8EYYIKBAAAAAEAHQAAAEtl" +
           "eUNyZWRlbnRpYWxSZXZva2VNZXRob2RUeXBlAQENBAAvAQENBA0EAAABAf////8BAAAAF2CpCgIAAAAA" +
           "AA4AAABJbnB1dEFyZ3VtZW50cwEBDgQALgBEDgQAAJYBAAAAAQAqAQEbAAAADAAAAENyZWRlbnRpYWxJ" +
           "ZAAM/////wAAAAAAAQAoAQEAAAABAAAAAQAAAAEB/////wAAAAA=";
        #endregion
        #endif
        #endregion

        #region Event Callbacks
        public KeyCredentialRevokeMethodStateMethodCallHandler OnCall;

        public KeyCredentialRevokeMethodStateMethodAsyncCallHandler OnCallAsync;
        #endregion

        #region Public Properties
        #endregion

        #region Overridden Methods
        protected override ServiceResult Call(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments)
        {
            if (OnCall == null)
            {
                return base.Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            ServiceResult _result = null;

            string credentialId = (string)_inputArguments[0];

            if (OnCall != null)
            {
                _result = OnCall(
                    _context,
                    this,
                    _objectId,
                    credentialId);
            }

            return _result;
        }

        #if (OPCUA_INCLUDE_ASYNC)
        protected override async ValueTask<ServiceResult> CallAsync(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments,
            CancellationToken cancellationToken = default)
        {
            if (OnCall == null && OnCallAsync == null)
            {
                return await base.CallAsync(_context, _objectId, _inputArguments, _outputArguments, cancellationToken).ConfigureAwait(false);
            }

            KeyCredentialRevokeMethodStateResult _result = null;

            string credentialId = (string)_inputArguments[0];

            if (OnCallAsync != null)
            {
                _result = await OnCallAsync(
                    _context,
                    this,
                    _objectId,
                    credentialId,
                    cancellationToken).ConfigureAwait(false);
            }
            else if (OnCall != null)
            {
                return Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            return _result.ServiceResult;
        }
        #endif

        #endregion

        #region Private Fields
        #endregion
    }

    /// <exclude />
    public delegate ServiceResult KeyCredentialRevokeMethodStateMethodCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        string credentialId);

    /// <exclude />
    public partial class KeyCredentialRevokeMethodStateResult
    {
        public ServiceResult ServiceResult { get; set; }
    }

    /// <exclude />
    public delegate ValueTask<KeyCredentialRevokeMethodStateResult> KeyCredentialRevokeMethodStateMethodAsyncCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        string credentialId,
        CancellationToken cancellationToken);
    #endif
    #endregion

    #region KeyCredentialRequestedAuditEventState Class
    #if (!OPCUA_EXCLUDE_KeyCredentialRequestedAuditEventState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class KeyCredentialRequestedAuditEventState : KeyCredentialAuditEventState
    {
        #region Constructors
        public KeyCredentialRequestedAuditEventState(NodeState parent) : base(parent)
        {
        }

        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(Opc.Ua.Gds.ObjectTypes.KeyCredentialRequestedAuditEventType, Opc.Ua.Gds.Namespaces.OpcUaGds, namespaceUris);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAACAAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvR0RTL/////8EYIACAQAAAAEALAAAAEtl" +
           "eUNyZWRlbnRpYWxSZXF1ZXN0ZWRBdWRpdEV2ZW50VHlwZUluc3RhbmNlAQEPBAEBDwQPBAAA/////xAA" +
           "AAAVYIkKAgAAAAAABwAAAEV2ZW50SWQCAQC9Qg8AAC4ARL1CDwAAD/////8BAf////8AAAAAFWCJCgIA" +
           "AAAAAAkAAABFdmVudFR5cGUCAQC+Qg8AAC4ARL5CDwAAEf////8BAf////8AAAAAFWCJCgIAAAAAAAoA" +
           "AABTb3VyY2VOb2RlAgEAv0IPAAAuAES/Qg8AABH/////AQH/////AAAAABVgiQoCAAAAAAAKAAAAU291" +
           "cmNlTmFtZQIBAMBCDwAALgBEwEIPAAAM/////wEB/////wAAAAAVYIkKAgAAAAAABAAAAFRpbWUCAQDB" +
           "Qg8AAC4ARMFCDwABACYB/////wEB/////wAAAAAVYIkKAgAAAAAACwAAAFJlY2VpdmVUaW1lAgEAwkIP" +
           "AAAuAETCQg8AAQAmAf////8BAf////8AAAAAFWCJCgIAAAAAAAcAAABNZXNzYWdlAgEAxEIPAAAuAETE" +
           "Qg8AABX/////AQH/////AAAAABVgiQoCAAAAAAAIAAAAU2V2ZXJpdHkCAQDFQg8AAC4ARMVCDwAABf//" +
           "//8BAf////8AAAAAFWCJCgIAAAAAAA8AAABBY3Rpb25UaW1lU3RhbXACAQDKQg8AAC4ARMpCDwABACYB" +
           "/////wEB/////wAAAAAVYIkKAgAAAAAABgAAAFN0YXR1cwIBAMtCDwAALgBEy0IPAAAB/////wEB////" +
           "/wAAAAAVYIkKAgAAAAAACAAAAFNlcnZlcklkAgEAzEIPAAAuAETMQg8AAAz/////AQH/////AAAAABVg" +
           "iQoCAAAAAAASAAAAQ2xpZW50QXVkaXRFbnRyeUlkAgEAzUIPAAAuAETNQg8AAAz/////AQH/////AAAA" +
           "ABVgiQoCAAAAAAAMAAAAQ2xpZW50VXNlcklkAgEAzkIPAAAuAETOQg8AAAz/////AQH/////AAAAABVg" +
           "iQoCAAAAAAAIAAAATWV0aG9kSWQCAQDQQg8AAC4ARNBCDwAAEf////8BAf////8AAAAAF2CJCgIAAAAA" +
           "AA4AAABJbnB1dEFyZ3VtZW50cwIBANJCDwAALgBE0kIPAAAYAQAAAAEAAAAAAAAAAQH/////AAAAABVg" +
           "iQoCAAAAAAALAAAAUmVzb3VyY2VVcmkCAQDUQg8AAC4ARNRCDwAADP////8BAf////8AAAAA";
        #endregion
        #endif
        #endregion

        #region Public Properties
        #endregion

        #region Overridden Methods
        #endregion

        #region Private Fields
        #endregion
    }
    #endif
    #endregion

    #region KeyCredentialDeliveredAuditEventState Class
    #if (!OPCUA_EXCLUDE_KeyCredentialDeliveredAuditEventState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class KeyCredentialDeliveredAuditEventState : KeyCredentialAuditEventState
    {
        #region Constructors
        public KeyCredentialDeliveredAuditEventState(NodeState parent) : base(parent)
        {
        }

        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(Opc.Ua.Gds.ObjectTypes.KeyCredentialDeliveredAuditEventType, Opc.Ua.Gds.Namespaces.OpcUaGds, namespaceUris);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAACAAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvR0RTL/////8EYIACAQAAAAEALAAAAEtl" +
           "eUNyZWRlbnRpYWxEZWxpdmVyZWRBdWRpdEV2ZW50VHlwZUluc3RhbmNlAQEhBAEBIQQhBAAA/////xAA" +
           "AAAVYIkKAgAAAAAABwAAAEV2ZW50SWQCAQDVQg8AAC4ARNVCDwAAD/////8BAf////8AAAAAFWCJCgIA" +
           "AAAAAAkAAABFdmVudFR5cGUCAQDWQg8AAC4ARNZCDwAAEf////8BAf////8AAAAAFWCJCgIAAAAAAAoA" +
           "AABTb3VyY2VOb2RlAgEA10IPAAAuAETXQg8AABH/////AQH/////AAAAABVgiQoCAAAAAAAKAAAAU291" +
           "cmNlTmFtZQIBANhCDwAALgBE2EIPAAAM/////wEB/////wAAAAAVYIkKAgAAAAAABAAAAFRpbWUCAQDZ" +
           "Qg8AAC4ARNlCDwABACYB/////wEB/////wAAAAAVYIkKAgAAAAAACwAAAFJlY2VpdmVUaW1lAgEA2kIP" +
           "AAAuAETaQg8AAQAmAf////8BAf////8AAAAAFWCJCgIAAAAAAAcAAABNZXNzYWdlAgEA3EIPAAAuAETc" +
           "Qg8AABX/////AQH/////AAAAABVgiQoCAAAAAAAIAAAAU2V2ZXJpdHkCAQDdQg8AAC4ARN1CDwAABf//" +
           "//8BAf////8AAAAAFWCJCgIAAAAAAA8AAABBY3Rpb25UaW1lU3RhbXACAQDiQg8AAC4AROJCDwABACYB" +
           "/////wEB/////wAAAAAVYIkKAgAAAAAABgAAAFN0YXR1cwIBAONCDwAALgBE40IPAAAB/////wEB////" +
           "/wAAAAAVYIkKAgAAAAAACAAAAFNlcnZlcklkAgEA5EIPAAAuAETkQg8AAAz/////AQH/////AAAAABVg" +
           "iQoCAAAAAAASAAAAQ2xpZW50QXVkaXRFbnRyeUlkAgEA5UIPAAAuAETlQg8AAAz/////AQH/////AAAA" +
           "ABVgiQoCAAAAAAAMAAAAQ2xpZW50VXNlcklkAgEA5kIPAAAuAETmQg8AAAz/////AQH/////AAAAABVg" +
           "iQoCAAAAAAAIAAAATWV0aG9kSWQCAQDoQg8AAC4AROhCDwAAEf////8BAf////8AAAAAF2CJCgIAAAAA" +
           "AA4AAABJbnB1dEFyZ3VtZW50cwIBAOpCDwAALgBE6kIPAAAYAQAAAAEAAAAAAAAAAQH/////AAAAABVg" +
           "iQoCAAAAAAALAAAAUmVzb3VyY2VVcmkCAQDsQg8AAC4AROxCDwAADP////8BAf////8AAAAA";
        #endregion
        #endif
        #endregion

        #region Public Properties
        #endregion

        #region Overridden Methods
        #endregion

        #region Private Fields
        #endregion
    }
    #endif
    #endregion

    #region KeyCredentialRevokedAuditEventState Class
    #if (!OPCUA_EXCLUDE_KeyCredentialRevokedAuditEventState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class KeyCredentialRevokedAuditEventState : KeyCredentialAuditEventState
    {
        #region Constructors
        public KeyCredentialRevokedAuditEventState(NodeState parent) : base(parent)
        {
        }

        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(Opc.Ua.Gds.ObjectTypes.KeyCredentialRevokedAuditEventType, Opc.Ua.Gds.Namespaces.OpcUaGds, namespaceUris);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAACAAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvR0RTL/////8EYIACAQAAAAEAKgAAAEtl" +
           "eUNyZWRlbnRpYWxSZXZva2VkQXVkaXRFdmVudFR5cGVJbnN0YW5jZQEBMwQBATMEMwQAAP////8QAAAA" +
           "FWCJCgIAAAAAAAcAAABFdmVudElkAgEA7UIPAAAuAETtQg8AAA//////AQH/////AAAAABVgiQoCAAAA" +
           "AAAJAAAARXZlbnRUeXBlAgEA7kIPAAAuAETuQg8AABH/////AQH/////AAAAABVgiQoCAAAAAAAKAAAA" +
           "U291cmNlTm9kZQIBAO9CDwAALgBE70IPAAAR/////wEB/////wAAAAAVYIkKAgAAAAAACgAAAFNvdXJj" +
           "ZU5hbWUCAQDwQg8AAC4ARPBCDwAADP////8BAf////8AAAAAFWCJCgIAAAAAAAQAAABUaW1lAgEA8UIP" +
           "AAAuAETxQg8AAQAmAf////8BAf////8AAAAAFWCJCgIAAAAAAAsAAABSZWNlaXZlVGltZQIBAPJCDwAA" +
           "LgBE8kIPAAEAJgH/////AQH/////AAAAABVgiQoCAAAAAAAHAAAATWVzc2FnZQIBAPRCDwAALgBE9EIP" +
           "AAAV/////wEB/////wAAAAAVYIkKAgAAAAAACAAAAFNldmVyaXR5AgEA9UIPAAAuAET1Qg8AAAX/////" +
           "AQH/////AAAAABVgiQoCAAAAAAAPAAAAQWN0aW9uVGltZVN0YW1wAgEA+kIPAAAuAET6Qg8AAQAmAf//" +
           "//8BAf////8AAAAAFWCJCgIAAAAAAAYAAABTdGF0dXMCAQD7Qg8AAC4ARPtCDwAAAf////8BAf////8A" +
           "AAAAFWCJCgIAAAAAAAgAAABTZXJ2ZXJJZAIBAPxCDwAALgBE/EIPAAAM/////wEB/////wAAAAAVYIkK" +
           "AgAAAAAAEgAAAENsaWVudEF1ZGl0RW50cnlJZAIBAP1CDwAALgBE/UIPAAAM/////wEB/////wAAAAAV" +
           "YIkKAgAAAAAADAAAAENsaWVudFVzZXJJZAIBAP5CDwAALgBE/kIPAAAM/////wEB/////wAAAAAVYIkK" +
           "AgAAAAAACAAAAE1ldGhvZElkAgEAAEMPAAAuAEQAQw8AABH/////AQH/////AAAAABdgiQoCAAAAAAAO" +
           "AAAASW5wdXRBcmd1bWVudHMCAQACQw8AAC4ARAJDDwAAGAEAAAABAAAAAAAAAAEB/////wAAAAAVYIkK" +
           "AgAAAAAACwAAAFJlc291cmNlVXJpAgEABEMPAAAuAEQEQw8AAAz/////AQH/////AAAAAA==";
        #endregion
        #endif
        #endregion

        #region Public Properties
        #endregion

        #region Overridden Methods
        #endregion

        #region Private Fields
        #endregion
    }
    #endif
    #endregion

    #region AuthorizationServicesFolderState Class
    #if (!OPCUA_EXCLUDE_AuthorizationServicesFolderState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class AuthorizationServicesFolderState : FolderState
    {
        #region Constructors
        public AuthorizationServicesFolderState(NodeState parent) : base(parent)
        {
        }

        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(Opc.Ua.Gds.ObjectTypes.AuthorizationServicesFolderType, Opc.Ua.Gds.Namespaces.OpcUaGds, namespaceUris);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAACAAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvR0RTL/////8EYIACAQAAAAEAJwAAAEF1" +
           "dGhvcml6YXRpb25TZXJ2aWNlc0ZvbGRlclR5cGVJbnN0YW5jZQEB6QABAekA6QAAAP////8BAAAABGDA" +
           "CgEAAAAXAAAAU2VydmljZU5hbWVfUGxhY2Vob2xkZXIBAA0AAAA8U2VydmljZU5hbWU+AQHqAAAjAQHG" +
           "A+oAAAD/////AwAAABVgiQoCAAAAAQAKAAAAU2VydmljZVVyaQEB6wAALgBE6wAAAAAM/////wEB////" +
           "/wAAAAAVYIkKAgAAAAEAEgAAAFNlcnZpY2VDZXJ0aWZpY2F0ZQEB7AAALgBE7AAAAAAP/////wEB////" +
           "/wAAAAAEYYIKBAAAAAEAFQAAAEdldFNlcnZpY2VEZXNjcmlwdGlvbgEB7gAALwEB7APuAAAAAQH/////" +
           "AQAAABdgqQoCAAAAAAAPAAAAT3V0cHV0QXJndW1lbnRzAQHvAAAuAETvAAAAlgMAAAABACoBARkAAAAK" +
           "AAAAU2VydmljZVVyaQAM/////wAAAAAAAQAqAQEhAAAAEgAAAFNlcnZpY2VDZXJ0aWZpY2F0ZQAP////" +
           "/wAAAAAAAQAqAQEmAAAAEQAAAFVzZXJUb2tlblBvbGljaWVzAQAwAQEAAAABAAAAAAAAAAABACgBAQAA" +
           "AAEAAAADAAAAAQH/////AAAAAA==";
        #endregion
        #endif
        #endregion

        #region Public Properties
        #endregion

        #region Overridden Methods
        #endregion

        #region Private Fields
        #endregion
    }
    #endif
    #endregion

    #region AuthorizationServiceState Class
    #if (!OPCUA_EXCLUDE_AuthorizationServiceState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class AuthorizationServiceState : BaseObjectState
    {
        #region Constructors
        public AuthorizationServiceState(NodeState parent) : base(parent)
        {
        }

        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(Opc.Ua.Gds.ObjectTypes.AuthorizationServiceType, Opc.Ua.Gds.Namespaces.OpcUaGds, namespaceUris);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);

            if (UserTokenPolicies != null)
            {
                UserTokenPolicies.Initialize(context, UserTokenPolicies_InitializationString);
            }

            if (RequestAccessToken != null)
            {
                RequestAccessToken.Initialize(context, RequestAccessToken_InitializationString);
            }
        }

        #region Initialization String
        private const string UserTokenPolicies_InitializationString =
           "AQAAACAAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvR0RTL/////8XYIkKAgAAAAEAEQAAAFVz" +
           "ZXJUb2tlblBvbGljaWVzAQHHAwAuAETHAwAAAQAwAQEAAAABAAAAAAAAAAEB/////wAAAAA=";

        private const string RequestAccessToken_InitializationString =
           "AQAAACAAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvR0RTL/////8EYYIKBAAAAAEAEgAAAFJl" +
           "cXVlc3RBY2Nlc3NUb2tlbgEByQMALwEByQPJAwAAAQH/////AgAAABdgqQoCAAAAAAAOAAAASW5wdXRB" +
           "cmd1bWVudHMBAcoDAC4ARMoDAACWAgAAAAEAKgEBHgAAAA0AAABJZGVudGl0eVRva2VuAQA8Af////8A" +
           "AAAAAAEAKgEBGQAAAAoAAABSZXNvdXJjZUlkAAz/////AAAAAAABACgBAQAAAAEAAAACAAAAAQH/////" +
           "AAAAABdgqQoCAAAAAAAPAAAAT3V0cHV0QXJndW1lbnRzAQHLAwAuAETLAwAAlgEAAAABACoBARoAAAAL" +
           "AAAAQWNjZXNzVG9rZW4ADP////8AAAAAAAEAKAEBAAAAAQAAAAEAAAABAf////8AAAAA";

        private const string InitializationString =
           "AQAAACAAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvR0RTL/////8EYIACAQAAAAEAIAAAAEF1" +
           "dGhvcml6YXRpb25TZXJ2aWNlVHlwZUluc3RhbmNlAQHGAwEBxgPGAwAA/////wUAAAAVYIkKAgAAAAEA" +
           "CgAAAFNlcnZpY2VVcmkBAesDAC4AROsDAAAADP////8BAf////8AAAAAFWCJCgIAAAABABIAAABTZXJ2" +
           "aWNlQ2VydGlmaWNhdGUBAcgDAC4ARMgDAAAAD/////8BAf////8AAAAAF2CJCgIAAAABABEAAABVc2Vy" +
           "VG9rZW5Qb2xpY2llcwEBxwMALgBExwMAAAEAMAEBAAAAAQAAAAAAAAABAf////8AAAAABGGCCgQAAAAB" +
           "ABUAAABHZXRTZXJ2aWNlRGVzY3JpcHRpb24BAewDAC8BAewD7AMAAAEB/////wEAAAAXYKkKAgAAAAAA" +
           "DwAAAE91dHB1dEFyZ3VtZW50cwEB7QMALgBE7QMAAJYDAAAAAQAqAQEZAAAACgAAAFNlcnZpY2VVcmkA" +
           "DP////8AAAAAAAEAKgEBIQAAABIAAABTZXJ2aWNlQ2VydGlmaWNhdGUAD/////8AAAAAAAEAKgEBJgAA" +
           "ABEAAABVc2VyVG9rZW5Qb2xpY2llcwEAMAEBAAAAAQAAAAAAAAAAAQAoAQEAAAABAAAAAwAAAAEB////" +
           "/wAAAAAEYYIKBAAAAAEAEgAAAFJlcXVlc3RBY2Nlc3NUb2tlbgEByQMALwEByQPJAwAAAQH/////AgAA" +
           "ABdgqQoCAAAAAAAOAAAASW5wdXRBcmd1bWVudHMBAcoDAC4ARMoDAACWAgAAAAEAKgEBHgAAAA0AAABJ" +
           "ZGVudGl0eVRva2VuAQA8Af////8AAAAAAAEAKgEBGQAAAAoAAABSZXNvdXJjZUlkAAz/////AAAAAAAB" +
           "ACgBAQAAAAEAAAACAAAAAQH/////AAAAABdgqQoCAAAAAAAPAAAAT3V0cHV0QXJndW1lbnRzAQHLAwAu" +
           "AETLAwAAlgEAAAABACoBARoAAAALAAAAQWNjZXNzVG9rZW4ADP////8AAAAAAAEAKAEBAAAAAQAAAAEA" +
           "AAABAf////8AAAAA";
        #endregion
        #endif
        #endregion

        #region Public Properties
        public PropertyState<string> ServiceUri
        {
            get => m_serviceUri;

            set
            {
                if (!Object.ReferenceEquals(m_serviceUri, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_serviceUri = value;
            }
        }

        public PropertyState<byte[]> ServiceCertificate
        {
            get => m_serviceCertificate;

            set
            {
                if (!Object.ReferenceEquals(m_serviceCertificate, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_serviceCertificate = value;
            }
        }

        public PropertyState<UserTokenPolicy[]> UserTokenPolicies
        {
            get => m_userTokenPolicies;

            set
            {
                if (!Object.ReferenceEquals(m_userTokenPolicies, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_userTokenPolicies = value;
            }
        }

        public GetServiceDescriptionMethodState GetServiceDescription
        {
            get => m_getServiceDescriptionMethod;

            set
            {
                if (!Object.ReferenceEquals(m_getServiceDescriptionMethod, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_getServiceDescriptionMethod = value;
            }
        }

        public RequestAccessTokenMethodState RequestAccessToken
        {
            get => m_requestAccessTokenMethod;

            set
            {
                if (!Object.ReferenceEquals(m_requestAccessTokenMethod, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_requestAccessTokenMethod = value;
            }
        }
        #endregion

        #region Overridden Methods
        public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
        {
            if (m_serviceUri != null)
            {
                children.Add(m_serviceUri);
            }

            if (m_serviceCertificate != null)
            {
                children.Add(m_serviceCertificate);
            }

            if (m_userTokenPolicies != null)
            {
                children.Add(m_userTokenPolicies);
            }

            if (m_getServiceDescriptionMethod != null)
            {
                children.Add(m_getServiceDescriptionMethod);
            }

            if (m_requestAccessTokenMethod != null)
            {
                children.Add(m_requestAccessTokenMethod);
            }

            base.GetChildren(context, children);
        }
            
        protected override void RemoveExplicitlyDefinedChild(BaseInstanceState child)
        {
            if (Object.ReferenceEquals(m_serviceUri, child))
            {
                m_serviceUri = null;
                return;
            }

            if (Object.ReferenceEquals(m_serviceCertificate, child))
            {
                m_serviceCertificate = null;
                return;
            }

            if (Object.ReferenceEquals(m_userTokenPolicies, child))
            {
                m_userTokenPolicies = null;
                return;
            }

            if (Object.ReferenceEquals(m_getServiceDescriptionMethod, child))
            {
                m_getServiceDescriptionMethod = null;
                return;
            }

            if (Object.ReferenceEquals(m_requestAccessTokenMethod, child))
            {
                m_requestAccessTokenMethod = null;
                return;
            }

            base.RemoveExplicitlyDefinedChild(child);
        }

        protected override BaseInstanceState FindChild(
            ISystemContext context,
            QualifiedName browseName,
            bool createOrReplace,
            BaseInstanceState replacement)
        {
            if (QualifiedName.IsNull(browseName))
            {
                return null;
            }

            BaseInstanceState instance = null;

            switch (browseName.Name)
            {
                case Opc.Ua.Gds.BrowseNames.ServiceUri:
                {
                    if (createOrReplace)
                    {
                        if (ServiceUri == null)
                        {
                            if (replacement == null)
                            {
                                ServiceUri = new PropertyState<string>(this);
                            }
                            else
                            {
                                ServiceUri = (PropertyState<string>)replacement;
                            }
                        }
                    }

                    instance = ServiceUri;
                    break;
                }

                case Opc.Ua.Gds.BrowseNames.ServiceCertificate:
                {
                    if (createOrReplace)
                    {
                        if (ServiceCertificate == null)
                        {
                            if (replacement == null)
                            {
                                ServiceCertificate = new PropertyState<byte[]>(this);
                            }
                            else
                            {
                                ServiceCertificate = (PropertyState<byte[]>)replacement;
                            }
                        }
                    }

                    instance = ServiceCertificate;
                    break;
                }

                case Opc.Ua.Gds.BrowseNames.UserTokenPolicies:
                {
                    if (createOrReplace)
                    {
                        if (UserTokenPolicies == null)
                        {
                            if (replacement == null)
                            {
                                UserTokenPolicies = new PropertyState<UserTokenPolicy[]>(this);
                            }
                            else
                            {
                                UserTokenPolicies = (PropertyState<UserTokenPolicy[]>)replacement;
                            }
                        }
                    }

                    instance = UserTokenPolicies;
                    break;
                }

                case Opc.Ua.Gds.BrowseNames.GetServiceDescription:
                {
                    if (createOrReplace)
                    {
                        if (GetServiceDescription == null)
                        {
                            if (replacement == null)
                            {
                                GetServiceDescription = new GetServiceDescriptionMethodState(this);
                            }
                            else
                            {
                                GetServiceDescription = (GetServiceDescriptionMethodState)replacement;
                            }
                        }
                    }

                    instance = GetServiceDescription;
                    break;
                }

                case Opc.Ua.Gds.BrowseNames.RequestAccessToken:
                {
                    if (createOrReplace)
                    {
                        if (RequestAccessToken == null)
                        {
                            if (replacement == null)
                            {
                                RequestAccessToken = new RequestAccessTokenMethodState(this);
                            }
                            else
                            {
                                RequestAccessToken = (RequestAccessTokenMethodState)replacement;
                            }
                        }
                    }

                    instance = RequestAccessToken;
                    break;
                }
            }

            if (instance != null)
            {
                return instance;
            }

            return base.FindChild(context, browseName, createOrReplace, replacement);
        }
        #endregion

        #region Private Fields
        private PropertyState<string> m_serviceUri;
        private PropertyState<byte[]> m_serviceCertificate;
        private PropertyState<UserTokenPolicy[]> m_userTokenPolicies;
        private GetServiceDescriptionMethodState m_getServiceDescriptionMethod;
        private RequestAccessTokenMethodState m_requestAccessTokenMethod;
        #endregion
    }
    #endif
    #endregion

    #region GetServiceDescriptionMethodState Class
    #if (!OPCUA_EXCLUDE_GetServiceDescriptionMethodState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class GetServiceDescriptionMethodState : MethodState
    {
        #region Constructors
        public GetServiceDescriptionMethodState(NodeState parent) : base(parent)
        {
        }

        public new static NodeState Construct(NodeState parent)
        {
            return new GetServiceDescriptionMethodState(parent);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAACAAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvR0RTL/////8EYYIKBAAAAAEAHwAAAEdl" +
           "dFNlcnZpY2VEZXNjcmlwdGlvbk1ldGhvZFR5cGUBAe4DAC8BAe4D7gMAAAEB/////wEAAAAXYKkKAgAA" +
           "AAAADwAAAE91dHB1dEFyZ3VtZW50cwEB7wMALgBE7wMAAJYDAAAAAQAqAQEZAAAACgAAAFNlcnZpY2VV" +
           "cmkADP////8AAAAAAAEAKgEBIQAAABIAAABTZXJ2aWNlQ2VydGlmaWNhdGUAD/////8AAAAAAAEAKgEB" +
           "JgAAABEAAABVc2VyVG9rZW5Qb2xpY2llcwEAMAEBAAAAAQAAAAAAAAAAAQAoAQEAAAABAAAAAwAAAAEB" +
           "/////wAAAAA=";
        #endregion
        #endif
        #endregion

        #region Event Callbacks
        public GetServiceDescriptionMethodStateMethodCallHandler OnCall;

        public GetServiceDescriptionMethodStateMethodAsyncCallHandler OnCallAsync;
        #endregion

        #region Public Properties
        #endregion

        #region Overridden Methods
        protected override ServiceResult Call(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments)
        {
            if (OnCall == null)
            {
                return base.Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            ServiceResult _result = null;

            string serviceUri = (string)_outputArguments[0];
            byte[] serviceCertificate = (byte[])_outputArguments[1];
            Opc.Ua.UserTokenPolicy[] userTokenPolicies = (Opc.Ua.UserTokenPolicy[])_outputArguments[2];

            if (OnCall != null)
            {
                _result = OnCall(
                    _context,
                    this,
                    _objectId,
                    ref serviceUri,
                    ref serviceCertificate,
                    ref userTokenPolicies);
            }

            _outputArguments[0] = serviceUri;
            _outputArguments[1] = serviceCertificate;
            _outputArguments[2] = userTokenPolicies;

            return _result;
        }

        #if (OPCUA_INCLUDE_ASYNC)
        protected override async ValueTask<ServiceResult> CallAsync(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments,
            CancellationToken cancellationToken = default)
        {
            if (OnCall == null && OnCallAsync == null)
            {
                return await base.CallAsync(_context, _objectId, _inputArguments, _outputArguments, cancellationToken).ConfigureAwait(false);
            }

            GetServiceDescriptionMethodStateResult _result = null;

            if (OnCallAsync != null)
            {
                _result = await OnCallAsync(
                    _context,
                    this,
                    _objectId,
                    cancellationToken).ConfigureAwait(false);
            }
            else if (OnCall != null)
            {
                return Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            _outputArguments[0] = _result.ServiceUri;
            _outputArguments[1] = _result.ServiceCertificate;
            _outputArguments[2] = _result.UserTokenPolicies;

            return _result.ServiceResult;
        }
        #endif

        #endregion

        #region Private Fields
        #endregion
    }

    /// <exclude />
    public delegate ServiceResult GetServiceDescriptionMethodStateMethodCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        ref string serviceUri,
        ref byte[] serviceCertificate,
        ref Opc.Ua.UserTokenPolicy[] userTokenPolicies);

    /// <exclude />
    public partial class GetServiceDescriptionMethodStateResult
    {
        public ServiceResult ServiceResult { get; set; }
        public string ServiceUri { get; set; }
        public byte[] ServiceCertificate { get; set; }
        public Opc.Ua.UserTokenPolicy[] UserTokenPolicies { get; set; }
    }

    /// <exclude />
    public delegate ValueTask<GetServiceDescriptionMethodStateResult> GetServiceDescriptionMethodStateMethodAsyncCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        CancellationToken cancellationToken);
    #endif
    #endregion

    #region RequestAccessTokenMethodState Class
    #if (!OPCUA_EXCLUDE_RequestAccessTokenMethodState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class RequestAccessTokenMethodState : MethodState
    {
        #region Constructors
        public RequestAccessTokenMethodState(NodeState parent) : base(parent)
        {
        }

        public new static NodeState Construct(NodeState parent)
        {
            return new RequestAccessTokenMethodState(parent);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAACAAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvR0RTL/////8EYYIKBAAAAAEAHAAAAFJl" +
           "cXVlc3RBY2Nlc3NUb2tlbk1ldGhvZFR5cGUBAeMDAC8BAeMD4wMAAAEB/////wIAAAAXYKkKAgAAAAAA" +
           "DgAAAElucHV0QXJndW1lbnRzAQHkAwAuAETkAwAAlgIAAAABACoBAR4AAAANAAAASWRlbnRpdHlUb2tl" +
           "bgEAPAH/////AAAAAAABACoBARkAAAAKAAAAUmVzb3VyY2VJZAAM/////wAAAAAAAQAoAQEAAAABAAAA" +
           "AgAAAAEB/////wAAAAAXYKkKAgAAAAAADwAAAE91dHB1dEFyZ3VtZW50cwEB5QMALgBE5QMAAJYBAAAA" +
           "AQAqAQEaAAAACwAAAEFjY2Vzc1Rva2VuAAz/////AAAAAAABACgBAQAAAAEAAAABAAAAAQH/////AAAA" +
           "AA==";
        #endregion
        #endif
        #endregion

        #region Event Callbacks
        public RequestAccessTokenMethodStateMethodCallHandler OnCall;

        public RequestAccessTokenMethodStateMethodAsyncCallHandler OnCallAsync;
        #endregion

        #region Public Properties
        #endregion

        #region Overridden Methods
        protected override ServiceResult Call(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments)
        {
            if (OnCall == null)
            {
                return base.Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            ServiceResult _result = null;

            Opc.Ua.UserIdentityToken identityToken = (Opc.Ua.UserIdentityToken)ExtensionObject.ToEncodeable((ExtensionObject)_inputArguments[0]);
            string resourceId = (string)_inputArguments[1];

            string accessToken = (string)_outputArguments[0];

            if (OnCall != null)
            {
                _result = OnCall(
                    _context,
                    this,
                    _objectId,
                    identityToken,
                    resourceId,
                    ref accessToken);
            }

            _outputArguments[0] = accessToken;

            return _result;
        }

        #if (OPCUA_INCLUDE_ASYNC)
        protected override async ValueTask<ServiceResult> CallAsync(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments,
            CancellationToken cancellationToken = default)
        {
            if (OnCall == null && OnCallAsync == null)
            {
                return await base.CallAsync(_context, _objectId, _inputArguments, _outputArguments, cancellationToken).ConfigureAwait(false);
            }

            RequestAccessTokenMethodStateResult _result = null;

            Opc.Ua.UserIdentityToken identityToken = (Opc.Ua.UserIdentityToken)ExtensionObject.ToEncodeable((ExtensionObject)_inputArguments[0]);
            string resourceId = (string)_inputArguments[1];

            if (OnCallAsync != null)
            {
                _result = await OnCallAsync(
                    _context,
                    this,
                    _objectId,
                    identityToken,
                    resourceId,
                    cancellationToken).ConfigureAwait(false);
            }
            else if (OnCall != null)
            {
                return Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            _outputArguments[0] = _result.AccessToken;

            return _result.ServiceResult;
        }
        #endif

        #endregion

        #region Private Fields
        #endregion
    }

    /// <exclude />
    public delegate ServiceResult RequestAccessTokenMethodStateMethodCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        Opc.Ua.UserIdentityToken identityToken,
        string resourceId,
        ref string accessToken);

    /// <exclude />
    public partial class RequestAccessTokenMethodStateResult
    {
        public ServiceResult ServiceResult { get; set; }
        public string AccessToken { get; set; }
    }

    /// <exclude />
    public delegate ValueTask<RequestAccessTokenMethodStateResult> RequestAccessTokenMethodStateMethodAsyncCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        Opc.Ua.UserIdentityToken identityToken,
        string resourceId,
        CancellationToken cancellationToken);
    #endif
    #endregion

    #region AccessTokenIssuedAuditEventState Class
    #if (!OPCUA_EXCLUDE_AccessTokenIssuedAuditEventState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class AccessTokenIssuedAuditEventState : AuditUpdateMethodEventState
    {
        #region Constructors
        public AccessTokenIssuedAuditEventState(NodeState parent) : base(parent)
        {
        }

        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(Opc.Ua.Gds.ObjectTypes.AccessTokenIssuedAuditEventType, Opc.Ua.Gds.Namespaces.OpcUaGds, namespaceUris);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAACAAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvR0RTL/////8EYIACAQAAAAEAJwAAAEFj" +
           "Y2Vzc1Rva2VuSXNzdWVkQXVkaXRFdmVudFR5cGVJbnN0YW5jZQEBzwMBAc8DzwMAAP////8PAAAAFWCJ" +
           "CgIAAAAAAAcAAABFdmVudElkAgEADkMPAAAuAEQOQw8AAA//////AQH/////AAAAABVgiQoCAAAAAAAJ" +
           "AAAARXZlbnRUeXBlAgEAD0MPAAAuAEQPQw8AABH/////AQH/////AAAAABVgiQoCAAAAAAAKAAAAU291" +
           "cmNlTm9kZQIBABBDDwAALgBEEEMPAAAR/////wEB/////wAAAAAVYIkKAgAAAAAACgAAAFNvdXJjZU5h" +
           "bWUCAQARQw8AAC4ARBFDDwAADP////8BAf////8AAAAAFWCJCgIAAAAAAAQAAABUaW1lAgEAEkMPAAAu" +
           "AEQSQw8AAQAmAf////8BAf////8AAAAAFWCJCgIAAAAAAAsAAABSZWNlaXZlVGltZQIBABNDDwAALgBE" +
           "E0MPAAEAJgH/////AQH/////AAAAABVgiQoCAAAAAAAHAAAATWVzc2FnZQIBABVDDwAALgBEFUMPAAAV" +
           "/////wEB/////wAAAAAVYIkKAgAAAAAACAAAAFNldmVyaXR5AgEAFkMPAAAuAEQWQw8AAAX/////AQH/" +
           "////AAAAABVgiQoCAAAAAAAPAAAAQWN0aW9uVGltZVN0YW1wAgEAG0MPAAAuAEQbQw8AAQAmAf////8B" +
           "Af////8AAAAAFWCJCgIAAAAAAAYAAABTdGF0dXMCAQAcQw8AAC4ARBxDDwAAAf////8BAf////8AAAAA" +
           "FWCJCgIAAAAAAAgAAABTZXJ2ZXJJZAIBAB1DDwAALgBEHUMPAAAM/////wEB/////wAAAAAVYIkKAgAA" +
           "AAAAEgAAAENsaWVudEF1ZGl0RW50cnlJZAIBAB5DDwAALgBEHkMPAAAM/////wEB/////wAAAAAVYIkK" +
           "AgAAAAAADAAAAENsaWVudFVzZXJJZAIBAB9DDwAALgBEH0MPAAAM/////wEB/////wAAAAAVYIkKAgAA" +
           "AAAACAAAAE1ldGhvZElkAgEAIUMPAAAuAEQhQw8AABH/////AQH/////AAAAABdgiQoCAAAAAAAOAAAA" +
           "SW5wdXRBcmd1bWVudHMCAQAjQw8AAC4ARCNDDwAAGAEAAAABAAAAAAAAAAEB/////wAAAAA=";
        #endregion
        #endif
        #endregion

        #region Public Properties
        #endregion

        #region Overridden Methods
        #endregion

        #region Private Fields
        #endregion
    }
    #endif
    #endregion
}
