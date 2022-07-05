﻿// Copyright (c) Microsoft. All Rights Reserved. Licensed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.Collections.Immutable;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using Microsoft.VisualStudio.ProjectSystem.Tools.BuildLogging.Model.RpcContracts;

namespace Microsoft.VisualStudio.ProjectSystem.Tools.BuildLogging.Model.BackEnd
{
    /// <summary>
    /// Implements IBuildLoggerService that interacts with Codespaces
    /// </summary>
    [Export(typeof(IBuildLoggerService))]
    internal sealed class BuildLoggerService : IBuildLoggerService
    {
        private readonly ILoggingDataSource _dataSource;
        private readonly ILoggingController _loggingController;

        [ImportingConstructor]
        public BuildLoggerService(ILoggingDataSource dataSource, ILoggingController loggingController)
        {
            _dataSource = dataSource;
            _loggingController = loggingController;
        }

        public Task<bool> IsLoggingAsync()
        {
            return Task.FromResult(_loggingController.IsLogging);
        }

        public Task<bool> SupportsRoslynLoggingAsync()
        {
            return Task.FromResult(_dataSource.SupportsRoslynLogging);
        }

        public Task StartAsync(Action notifyCallback)
        {
            _dataSource.Start(notifyCallback);
            return Task.CompletedTask;
        }

        public Task StopAsync()
        {
            _dataSource.Stop();
            return Task.CompletedTask;
        }

        public Task ClearAsync()
        {
            _dataSource.Clear();
            return Task.CompletedTask;
        }

        public string? GetLogForBuild(int buildID)
        {
            return _dataSource.GetLogForBuild(buildID);
        }

        public ImmutableArray<BuildSummary> GetAllBuilds()
        {
            return _dataSource.GetAllBuilds();
        }
    }
}
