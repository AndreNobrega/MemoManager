﻿using Batches.Model.Batches;
using Batches.Model.BatchRequest;
using Notifications.Services;
using Serilog;

namespace Batches.Model.BatchBuilders
{
    internal interface IBatchBuilder
    {
        public BatchBase BuildBatch(ILogger logger, IBatchRequest request, INotificationService notificationService);
    }
}