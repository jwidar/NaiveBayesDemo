using Bayes.Categorizer;

namespace Bayes
{
	public record DataPoint : IObservation<Category>
	{
		public float X { get; set; }
		public float Y { get; set; }
		public Category Category { get; set; }
		public double Probability { get; internal set; }

		public override string ToString()
		{
			return $"DataPoint: ({this.X},{this.Y}) Probability: {this.Probability:N2}, Category: {this.Category}";
		}
	}
}
