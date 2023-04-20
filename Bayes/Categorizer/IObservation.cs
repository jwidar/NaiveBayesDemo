namespace Bayes.Categorizer
{
	public interface IObservation<TCategory>
	{
		public TCategory Category { get; set; }
	}
}