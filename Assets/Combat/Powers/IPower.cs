using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPower
{
	public void SelectPower(Unit user);
	public void Execute(Unit instigator, Unit target);
}
