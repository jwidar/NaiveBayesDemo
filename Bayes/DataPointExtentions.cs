namespace Bayes;

public static class DataPointExtentions
{
	public static bool IsProximalTo(this DataPoint me, DataPoint other, float proximityDistance)
	{
		var distance = Math.Sqrt(Math.Pow(other.X - me.X, 2.0) + Math.Pow(other.Y - me.Y, 2.0));
		//Debug.WriteLine($"IsProximalTo: {distance} <= {proximityDistance}");
		return distance <= proximityDistance;
	}
}
