namespace Bayes.Categorizer;

internal class NaiveBayeisanCategorizer<TObservation, TCategory>
	where TObservation : IObservation<TCategory>
{
	public NaiveBayeisanCategorizer(IEnumerable<TCategory> categories)
	{
		this.Probabilizers = categories.Select(x => new Probabilizer<TObservation, TCategory>(x)).ToList();
	}

	public delegate bool IsSimilarDelegate(TObservation newObservation, TObservation existingObservation);
	public required IsSimilarDelegate IsSimilar { get; set; }

	public IEnumerable<Probabilizer<TObservation, TCategory>> Probabilizers { get; }

	public void CategorizeObservation(IEnumerable<TObservation> observations, TObservation newObservation)
	{
		var similarObservations = observations.Where(x => this.IsSimilar(newObservation, x));

		var winner = this.Probabilizers
			.Select(x => x.Calculate(similarObservations, observations))
			.MaxBy(x => x.PosteriorProbability)!;

		newObservation.Category = winner.PosteriorProbability == -1 ? default! : winner.Category;
		newObservation.Probability = Math.Abs(winner.PosteriorProbability);
	}

}
