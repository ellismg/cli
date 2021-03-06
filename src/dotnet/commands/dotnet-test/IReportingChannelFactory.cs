﻿// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.DotNet.Tools.Test
{
    public interface IReportingChannelFactory
    {
        IReportingChannel CreateChannelWithAnyAvailablePort();

        IReportingChannel CreateChannelWithPort(int port);
    }
}
