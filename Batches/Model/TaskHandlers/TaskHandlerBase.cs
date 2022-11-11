﻿using Batches.Model.BatchRequest;
using Notifications.Model.Interfaces;
using Notifications.Services;

namespace Batches.Model.TaskHandlers
{
    public abstract class TaskHandlerBase
    {
        protected delegate void TaskLogicDelegate();
        protected TaskLogicDelegate? TaskLogic;
        protected readonly IBatchRequest batchRequest;
        protected TaskHandlerBase? _nextHandler;

        protected abstract string NotificationMessage { get; }
        protected readonly INotificationService notificationService;
        protected readonly IProgressNotification? progressNotification;
        
        protected readonly int numberOfJobsInTask = 1;
        internal int TaskCount => numberOfJobsInTask + (_nextHandler?.TaskCount ?? 0);

        protected TaskHandlerBase(IBatchRequest batchRequest, INotificationService notificationService, IProgressNotification? progressNotification)
        {
            this.notificationService = notificationService;
            this.batchRequest = batchRequest;
            this.progressNotification = progressNotification;
        }

        internal TaskHandlerBase SetNext(TaskHandlerBase nextBatchHandler)
        {
            _nextHandler = nextBatchHandler;
            return _nextHandler;
        }

        /*
         * TODO: add error handling
         */
        public virtual void Handle()
        {
            if (TaskLogic is null)
                throw new ArgumentNullException("Task logic has not been set.");

            UpdateNotification();

            TaskLogic();

            _nextHandler?.Handle();
        }

        protected virtual void UpdateNotification()
        {
            if (progressNotification != null)
            {
                progressNotification.Message = NotificationMessage;
                notificationService.UpdateNotification(progressNotification);
            }
        }
    }
}