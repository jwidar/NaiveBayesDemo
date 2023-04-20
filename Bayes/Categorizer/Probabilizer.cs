namespace Bayes.Categorizer;

public class Probabilizer<TObservation, TCategory>
	where TObservation : IObservation<TCategory>
{
	public Probabilizer(TCategory category)
	{
		this.Category = category;
	}

	public double PosteriorProbability { get; private set; }
	public TCategory Category { get; private set; }

	public Probabilizer<TObservation, TCategory> Calculate(IEnumerable<TObservation> similarObservations
		, IEnumerable<TObservation> dataPoints
		)
	{
		if (!similarObservations.Any() || !dataPoints.Any())
		{
			this.PosteriorProbability = -1;
			return this;
		}

		// see Categorizer.md

		this.PosteriorProbability = 0;

		var observationsInCategory = (double)dataPoints
			.Where(x => object.Equals(x.Category, this.Category))
			.Count();

		var totalObservations = (double)dataPoints
			.Count();

		if (totalObservations == 0 || observationsInCategory == 0)
		{
			return this;
		}

		var priorProbability = observationsInCategory
			/ (double)totalObservations;

		var likelihood = similarObservations
			.Where(x => object.Equals(x.Category, this.Category))
			.Count()
			/ (double)observationsInCategory;

		var marginalLikelihood = similarObservations.Count()
			/ (double)totalObservations;

		this.PosteriorProbability = likelihood * priorProbability / marginalLikelihood;

		return this;
	}
}
