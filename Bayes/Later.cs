namespace Bayes;

public class Later
{
	private readonly int timeoutMilliseconds = 1;
	private System.Windows.Forms.Timer? timer = new();
	public Later(Action action)
	{
		this.EnqueueAction(action);
	}

	public Later(Action action, int timeoutMillis)
	{
		this.timeoutMilliseconds = timeoutMillis;
		this.EnqueueAction(action);
	}

	private void EnqueueAction(Action action)
	{
		this.timer!.Tick += (a, b) =>
		{
			action();
			this.StopTimer();
		};

		this.timer.Interval = this.timeoutMilliseconds;
		this.timer.Enabled = true;
	}

	public void Restart()
	{
		if (this.timer?.Enabled ?? false)
		{
			this.timer.Enabled = false;
			this.timer.Enabled = true;
		}
	}

	private void StopTimer()
	{
		if (this.timer != null)
		{
			this.timer.Enabled = false;
			this.timer.Dispose();
			this.timer = null;
		}
	}

	public void Abort()
	{
		this.StopTimer();
	}
}
