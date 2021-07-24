using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRange 
{
	public int SweetSpot { get; set; }
	public int MinimumRange { get; set; }
	public int MaximumRange { get; set; }
	public InRangeData CheckRange(Unit user);
}
[System.Serializable]
public struct InRangeData
{
	public List<Node> nodesInRange;
	public List<Unit> suitableUnits;

	public InRangeData(List<Node> nodesInRange, List<Unit> suitableUnits)
	{
		this.nodesInRange = nodesInRange;
		this.suitableUnits = suitableUnits;
	}
}
