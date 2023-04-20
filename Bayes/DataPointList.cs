using System.Collections;

namespace Bayes
{
	public class DataPointList : IList<DataPoint>
	{
		private readonly List<DataPoint> dataPoints = new();

		public DataPointList(IEnumerable<DataPoint>? enumerable = null)
		{
			this.dataPoints = enumerable?.ToList() ?? new DataPoint[0].ToList();
			//foreach (var point in enumerable)
			//{
			//    this.Bounds.Extend(point);
			//}
		}

		public DataPoint this[int index] { get => this.dataPoints[index]; set => this.dataPoints[index] = value; }

		public int Count => this.dataPoints.Count;

		public bool IsReadOnly => false;

		//public DataBounds Bounds { get; } = new();

		public void Add(DataPoint item)
		{
			this.dataPoints.Add(item);
			//this.Bounds.Extend(item);
		}

		public void Clear()
		{
			this.dataPoints.Clear();
		}

		public bool Contains(DataPoint item)
		{
			return this.dataPoints.Contains(item);
		}

		public void CopyTo(DataPoint[] array, int arrayIndex)
		{
			this.dataPoints.CopyTo(array, arrayIndex);
		}

		public IEnumerator<DataPoint> GetEnumerator()
		{
			return this.dataPoints.GetEnumerator();
		}

		public int IndexOf(DataPoint item)
		{
			return this.dataPoints.IndexOf(item);
		}

		public void Insert(int index, DataPoint item)
		{
			this.dataPoints.Insert(index, item);
			//this.Bounds.Extend(item);
		}

		public bool Remove(DataPoint item)
		{
			return this.dataPoints.Remove(item);
		}

		public void RemoveAt(int index)
		{
			this.dataPoints.RemoveAt(index);
			// TODO: this.Bounds.Shrink(item);

		}

		public void AddRange(IEnumerable<DataPoint> enumerable)
		{
			this.dataPoints.AddRange(enumerable);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.dataPoints.GetEnumerator();
		}
	}
}
