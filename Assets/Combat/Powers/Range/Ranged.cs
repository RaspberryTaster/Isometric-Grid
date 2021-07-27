using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged : IRange
{
	private const int MAXIMUMCLAMP = 99999;


	private int minimumRange;
	private int maximumRange;
	private int sweetSpot;

	public Ranged(int maximumRange, int minimumRange = 1, int sweetSpot = 1)
	{
		this.minimumRange = minimumRange;
		this.maximumRange = maximumRange;
		this.sweetSpot = sweetSpot;
	}

	public int SweetSpot
	{
		get
		{
			return sweetSpot = Mathf.Clamp(sweetSpot, MinimumRange, MaximumRange);
		}
		set
		{
			sweetSpot = Mathf.Clamp(value, MinimumRange, MaximumRange);
		}
	}

	public int MinimumRange
	{
		get
		{
			return minimumRange = Mathf.Clamp(minimumRange, 1, MAXIMUMCLAMP);
		}
		set
		{
			minimumRange = Mathf.Clamp(value, 1, MAXIMUMCLAMP);
		}
	}
	public int MaximumRange
	{
		get
		{
			return maximumRange = Mathf.Clamp(maximumRange, MinimumRange, MAXIMUMCLAMP);
		}
		set
		{
			maximumRange = Mathf.Clamp(value, MinimumRange, MAXIMUMCLAMP);
		}
	}

	public InRangeData CheckRange(Unit user)
	{
		List<Unit> suitableUnits = new List<Unit>();

		List<Node> nodes = SquareGrid.Instance.NodeGrid.GetWithinRange(SquareGrid.Instance.NodeGrid.NodeFromWorldPoint(user.transform.position), MinimumRange, MaximumRange);

		foreach (Node n in nodes)
		{
			foreach (Unit u in n.OccupyingUnits)
			{
				if (suitableUnits.Contains(u)) continue;
				suitableUnits.Add(u);
			}
		}
		return new InRangeData(nodes, suitableUnits);
	}
}
