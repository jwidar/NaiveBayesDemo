namespace Bayes.Categorizer;

public interface IObservation<TCategory>
{
	public TCategory Category { get; set; }
	double Probability { get; set; }
}
