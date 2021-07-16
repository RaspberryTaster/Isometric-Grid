using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActionCost
{
	public bool MetRequirement(Unit instigator);
	public void ActionTaken(Unit instigator);
}
