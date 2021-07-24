using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardAction : IActionType
{
	public void ActionTaken(Unit instigator)
	{
		instigator.StandardAction.ReduceCurrent(1);
	}

	public bool MetRequirement(Unit instigator)
	{
		return instigator.StandardAction.CurrentValue > 0;
	}
}
