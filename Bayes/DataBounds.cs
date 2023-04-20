namespace Bayes
{
	public class DataBounds
	{
		public float MinX { get; private set; } = float.MaxValue;
		public float MinY { get; private set; } = float.MaxValue;
		public float MaxX { get; private set; } = float.MinValue;
		public float MaxY { get; private set; } = float.MinValue;

		public float Width => this.MaxX - this.MinX;
		public float Height => this.MaxY - this.MinY;

		internal void Extend(DataPoint item)
		{
			this.MinX = Math.Min(this.MinX, item.X - 50);
			this.MaxX = Math.Max(this.MaxX, item.X + 50);
			this.MinY = Math.Min(this.MinY, item.Y - 50);
			this.MaxY = Math.Max(this.MaxY, item.Y + 50);
		}
	}
}
