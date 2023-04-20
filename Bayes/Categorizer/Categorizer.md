
https://medium.com/@omairaasim/machine-learning-project-14-naive-bayes-classifier-step-by-step-a1f4a5e5f834

Probability of "classification" given "features".

X: observations with features similar to an incoming observation 
, which has known features but unknown classification.

P(X|Walks) – Likelihood:
The probability that somebody who walks exhibits features X.
(# of observations in the circle among ppl who walk) / (Total walkers)

P(Walks) – Prior probability:
This is simply the probability that a person walks to work.
(Number of people who walk) / Total observations

P(X) – Marginal likelihood:
The probability that any new data point that we add will fall inside the circle.
(Number of Observations with similar features) / (Total Observations)

P(Walks|X) – Posterior probability:
The probability that a data point in the center of limit we use to 
find similar observations will be someone who walks.


Posterior probability = Likelihood * Prior probability / Marginal likelihood
P(Walks|X) = P(X|Walks) * P(Walks) / P(X)

P(Walks|X) = (P(X|Walks) *  P(Walks)) / P(X)
P(Walks|X) = (3/10) * (10/30) / (4/30)
P(Walks|X) = 0.75 or 75%


Compare posterior probability for different classifications (outcomes)
, and select the classification of highest probability.