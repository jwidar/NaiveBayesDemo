using Bayes.Categorizer;

namespace Bayes;

public partial class Form1 : Form
{
	private Later? later;
	private readonly System.Windows.Forms.Timer timer = new() { Interval = 150 };

	public List<DataPoint> DataPoints { get; set; } = new List<DataPoint>();
	public DataPoint? SelectedDataPoint { get; private set; }
	public float ProximityDistance { get; internal set; } = 50;
	internal NaiveBayeisanCategorizer<DataPoint, Category> NaiveBayeisanCategorizer { get; private set; }

	public Form1()
	{
		this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

		this.InitializeComponent();

		this.NaiveBayeisanCategorizer = new NaiveBayeisanCategorizer<DataPoint, Category>(Enum.GetValues<Category>())
		{
			IsSimilar = this.IsSimilar
		};

		this.RandomizeALotOfPoints();
		this.graph1.Form1 = this;
		this.timer.Tick += this.Timer_Tick;
	}

	private bool IsSimilar(DataPoint newObservation, DataPoint existingObservation)
	{
		return newObservation.IsProximalTo(existingObservation, this.ProximityDistance);
	}

	private void RandomizeALotOfPoints()
	{
		var rnd = new Random();
		for (var idx = 0; idx < 50; idx++)
		{
			this.AddAndCategorizeDataPoint(this.RandomizePoint(rnd));
		}
	}

	public void RefreshListPlease()
	{
		this.later?.Restart();
		this.later ??= new Later(this.RefreshList, 100);
	}

	private DataPoint RandomizePoint(Random rnd)
	{
		return new DataPoint
		{
			X = rnd.Next(20, this.graph1.Width - 40),
			Y = rnd.Next(20, this.graph1.Height - 40),
			Category = Category.None,
			Probability = 1
		};
	}

	protected override void OnLoad(EventArgs e)
	{
		base.OnLoad(e);
		this.RefreshList();
	}

	private void RefreshList()
	{
		this.later = null;
		this.listView1.BeginUpdate();
		this.listView1.Items.Clear();

		foreach (var dataPoint in this.DataPoints)
		{
			var lvi = new ListViewItem(new[] { dataPoint.X.ToString(), dataPoint.Y.ToString(), dataPoint.Category.ToString() })
			{
				Tag = dataPoint
			};
			_ = this.listView1.Items.Add(lvi);
		}

		this.listView1.EndUpdate();
		this.graph1.Invalidate();
	}


	private void listView1_SelectedIndexChanged(object sender, EventArgs e)
	{
		this.SelectedDataPoint = null;
		if (this.listView1.SelectedItems.Count > 0)
		{
			var selectedItem = this.listView1.SelectedItems[0];
			this.SelectedDataPoint = selectedItem.Tag as DataPoint;
			this.graph1.Invalidate();
		}
	}

	internal void CategorizeNear(DataPoint point, Category category)
	{
		//Debug.WriteLine($"");
		var proximalPoints = this.DataPoints.Where(x => x.IsProximalTo(point, this.ProximityDistance));
		foreach (var dataPoint in proximalPoints)
		{
			//Debug.WriteLine(dataPoint);
			dataPoint.Category = category;
			dataPoint.Probability = 1.0;
		}
	}

	protected override void OnKeyDown(KeyEventArgs e)
	{
		this.graph1.Modifiers = e.Modifiers;
		base.OnKeyDown(e);

		this.graph1.Invalidate();
	}

	protected override void OnKeyUp(KeyEventArgs e)
	{
		this.graph1.Modifiers = e.Modifiers;
		base.OnKeyUp(e);

		this.graph1.Invalidate();
	}

	internal void AddAndCategorizeDataPoint(DataPoint newPoint)
	{
		this.NaiveBayeisanCategorizer.CategorizeObservation(this.DataPoints, newPoint);

		this.DataPoints.Add(newPoint);

		while (this.DataPoints.Count > 2000)
		{
			this.DataPoints.RemoveAt(0);
		}
	}

	private void button1_Click(object sender, EventArgs e)
	{
		this.timer.Enabled = !this.timer.Enabled;
	}
	private void Timer_Tick(object? sender, EventArgs e)
	{
		this.RandomizeALotOfPoints();
		this.graph1.Invalidate();
	}
}
