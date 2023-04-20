using System.Drawing.Drawing2D;
using System.Text;

namespace Bayes;

public class Graph : Control
{
	private bool refreshList;

	public Form1 Form1 { get; set; } = null!;
	public List<DataPoint> DataPoints => this.Form1?.DataPoints ?? new();

	public Keys Modifiers { get; internal set; }
	public Point? PointerLocation { get; private set; }
	public bool CategorizeMode { get; private set; }

	public event EventHandler<DataPoint>? AddNewDataPoint;

	public Graph()
	{
		this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

		this.Font = new Font("Calibri", 14);
	}

	protected override void OnPaintBackground(PaintEventArgs pevent)
	{
		base.OnPaintBackground(pevent);
		pevent.Graphics.FillRectangle(Brushes.White, this.ClientRectangle);
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		base.OnPaint(e);
		e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

		if (this.DesignMode || this.Form1 == null)
		{
			return;
		}

		if (this.refreshList)
		{
			this.Form1.RefreshListPlease();
			this.refreshList = false;
		}

		var dataPointRadius = 5;

		using var highlightBrush = new SolidBrush(Color.FromArgb(100, Color.Red));
		using var cursorBrush = new SolidBrush(Color.FromArgb(20, Color.Blue));
		var categoryBrushes = Enum.GetValues<Category>()
			.ToDictionary(x => x, this.GetBrush);

		void draw(float pointX, float pointY, Pen? pen, Brush? brush, float radius)
		{
			var rect = new RectangleF(
				 pointX + 0.5f - radius
				, this.DisplayRectangle.Height - pointY + 0.5f - radius
				, radius * 2
				, radius * 2);

			if (brush != null)
			{
				e.Graphics.FillEllipse(brush, rect);
			}

			if (pen != null)
			{
				e.Graphics.DrawEllipse(pen, rect);
			}
		}

		foreach (var dataPoint in this.DataPoints)
		{
			var point = this.FromDataPoint(dataPoint);
			draw(point.X
				, point.Y
				, Pens.Black
				, categoryBrushes[dataPoint.Category]
				, dataPointRadius
				);
		}

		if (this.Form1?.SelectedDataPoint != null)
		{
			var point = this.FromDataPoint(this.Form1.SelectedDataPoint);
			draw(point.X
				, point.Y
				, Pens.Black
				, highlightBrush
				, dataPointRadius);
		}

		if (this.PointerLocation.HasValue)
		{
			var point = this.PointerLocation.Value;
			draw(point.X
				, point.Y
				, null
				, cursorBrush
				, this.Form1!.ProximityDistance /** this.ClientRectangle.Width / this.DataPoints.Bounds.Width*/);

			if (this.CategorizeMode)
			{
				draw(point.X
					, point.Y
					, Pens.Black
					, null
					, this.Form1!.ProximityDistance /** this.ClientRectangle.Width / this.DataPoints.Bounds.Width*/);
			}
		}
		var sb = new StringBuilder();
		foreach (var probabilzer in this.Form1!.NaiveBayeisanCategorizer.Probabilizers)
		{
			var subset = this.DataPoints.Where(x => x.Category == probabilzer.Category);
			if (subset.Any())
			{
				_ = sb.AppendLine($"Max {probabilzer.Category} Prob: {subset.Max(x => x.Probability):N2}");
			}
		}

		if (this.PointerLocation.HasValue)
		{
			_ = sb.Append($"PointerLocation: {this.PointerLocation?.X}, {this.PointerLocation?.Y}");
		}
		if (this.CategorizeMode)
		{
			_ = sb.Append($"\r\nCategorize");
		}

		e.Graphics.DrawString(sb.ToString(), this.Font, Brushes.Black, 3, 3);

		foreach (var brush in categoryBrushes.Values)
		{
			brush.Dispose();
		}
	}

	protected override void OnMouseMove(MouseEventArgs e)
	{
		base.OnMouseMove(e);
		this.PointerLocation = new Point(e.Location.X, this.DisplayRectangle.Height - e.Location.Y);

		this.Categorize(e);

		this.Invalidate();
	}

	protected override void OnInvalidated(InvalidateEventArgs e)
	{
		this.CategorizeMode = (this.Modifiers & Keys.Control) != Keys.None;
		base.OnInvalidated(e);
	}

	protected override void OnMouseLeave(EventArgs e)
	{
		base.OnMouseLeave(e);

		this.PointerLocation = null;
		this.CategorizeMode = false;
		this.Modifiers = Keys.None;

		this.Invalidate();
	}

	protected override void OnMouseDown(MouseEventArgs e)
	{
		this.Categorize(e);
		this.AddPoint(e);

		base.OnMouseDown(e);

		this.Invalidate();
	}

	private void AddPoint(MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Left && this.PointerLocation.HasValue && !this.CategorizeMode)
		{
			this.Form1.AddAndCategorizeDataPoint(this.ToDataPoint(this.PointerLocation.Value));
			this.refreshList = true;
		}
	}

	private void Categorize(MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Left && this.PointerLocation.HasValue && this.CategorizeMode)
		{
			this.Form1.CategorizeNear(this.ToDataPoint(this.PointerLocation.Value), Category.Foo);
			this.refreshList = true;
		}
		if (e.Button == MouseButtons.Right && this.PointerLocation.HasValue && this.CategorizeMode)
		{
			this.Form1.CategorizeNear(this.ToDataPoint(this.PointerLocation.Value), Category.Bar);
			this.refreshList = true;
		}
	}

	private Brush GetBrush(Category category)
	{
		return category switch
		{
			Category.None => new SolidBrush(Color.White),
			Category.Foo => new SolidBrush(Color.Red),
			Category.Bar => new SolidBrush(Color.Blue),
			_ => throw new ArgumentOutOfRangeException(nameof(category), category, null),
		};
	}

	public DataPoint ToDataPoint(PointF point)
	{
		return new DataPoint { X = point.X, Y = point.Y };

		//var fractionX = (point.X - this.ClientRectangle.Left) / this.ClientRectangle.Width;
		//var fractionY = (point.Y - this.ClientRectangle.Top) / this.ClientRectangle.Height;
		//var bounds = this.DataPoints.Bounds;

		//var result = new DataPoint
		//{
		//    X = bounds.Width * fractionX + bounds.MinX,
		//    Y = bounds.Height * fractionY + bounds.MinY,
		//};

		//return result;
	}

	public PointF FromDataPoint(DataPoint dataPoint)
	{
		return new PointF { X = dataPoint.X, Y = dataPoint.Y };

		//var bounds = this.DataPoints.Bounds;
		//var fractionX = (dataPoint.X - bounds.MinX) / bounds.Width;
		//var fractionY = (dataPoint.Y - bounds.MinY) / bounds.Height;

		//return new PointF(
		//    this.ClientRectangle.Width * fractionX + this.ClientRectangle.Left,
		//    this.ClientRectangle.Height * fractionY + this.ClientRectangle.Top
		//);
	}
}
