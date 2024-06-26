﻿using Application.Common.Interfaces.Notifications;
using Serilog;

namespace Application.Common.Interfaces.Batches
{
	internal abstract class BatchBase
	{
		protected readonly ILogger _logger;
		protected readonly IBatchRequest _request;
		protected readonly BaseTaskHandler _initialTask;
		protected int TaskCount => _initialTask.TaskCount;

		internal BatchBase(ILogger logger, IBatchRequest request, BaseTaskHandler initialTask)
		{
			_logger = logger;
			_request = request ?? throw new ArgumentNullException(nameof(request));
			_initialTask = initialTask ?? throw new ArgumentNullException(nameof(initialTask));
		}

		internal virtual bool Run()
		{
			try
			{
				_initialTask.Handle();
				return true;
			}
			catch (Exception ex)
			{
				_logger.Error(ex, "Error occurred during batch task execution.");
				return false;
			}
		}

		protected virtual void UpdateNotification(INotification notification)
		{
			throw new NotImplementedException();
		}
	}
}
