using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionCost : IActionType
{
	public int StandardActionCost;
	public int MinorActionCost;

	public ActionCost(int standardActionCost = 0, int minorActionCost = 0)
	{
		StandardActionCost = standardActionCost;
		MinorActionCost = minorActionCost;
	}

	public void ActionTaken(Unit instigator)
	{
		instigator.StandardAction.ReduceCurrent(StandardActionCost);
		instigator.MinorAction.ReduceCurrent(MinorActionCost);
	}

	public bool MetRequirement(Unit instigator)
	{
		return instigator.StandardAction.CurrentValue >= StandardActionCost && instigator.MinorAction.CurrentValue >= MinorActionCost;
	}
}
