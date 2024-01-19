using Foundation;

namespace Nau.Simple.Maui.BindingIssue.Platforms.iOS
{
	public class AsynchronousWorker : NSObject
	{
		#region Static Properties
		private static NSOperationQueue _operationQueue = null;
		#endregion

		#region Private Member Variables
		private AsynchronousOperation _operation = null;
		private bool _isCancelled = false;
		#endregion

		#region Public Accessors

		public bool IsRunning
		{
			get { return _operation.IsExecuting; }
		}

		#endregion

		#region Construction

		public AsynchronousWorker(Action doWorkInBackground, Action workCompleted, bool start)
		{
			_operation = new AsynchronousOperation(doWorkInBackground, workCompleted);
			if (start)
			{
				Start();
			}
		}

		#endregion

		#region Public Methods

		public void Start()
		{
			if (_operationQueue == null)
			{
				_operationQueue = new NSOperationQueue()
				{
					MaxConcurrentOperationCount = 1,
					Name = "AsynchronousWorkerQueue",
				};
			}

			_operationQueue.AddOperation(_operation);
		}

		#endregion
	}

	public class AsynchronousOperation : NSOperation
	{
		#region Private Members
		private Action _doWorkInBackground;
		private Action _workCompleted;
		private Action _workCancelled;
		#endregion

		#region Construction

		public AsynchronousOperation(Action doWorkInBackground, Action workCompleted, Action workCancelled)
		{
			_doWorkInBackground = doWorkInBackground;
			_workCompleted = workCompleted;
			_workCancelled = workCancelled;
		}

		public AsynchronousOperation(Action doWorkInBackground, Action workCompleted)
		{
			_doWorkInBackground = doWorkInBackground;
			_workCompleted = workCompleted;
		}
		#endregion

		#region Overrides

		public override void Main()
		{
			if (IsCancelled)
			{
				InvokeOnMainThread(() =>
				{
					if (_workCancelled != null)
					{
						_workCancelled.Invoke();
					}

					return;
				});
			}

			_doWorkInBackground.Invoke();
			InvokeOnMainThread(() =>
			{
				if (IsCancelled)
				{
					if (_workCancelled != null)
					{
						_workCancelled.Invoke();
					}
				}
				else
				{
					_workCompleted.Invoke();
				}
			});
		}
		#endregion
	}
}
