using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttack
{
	public bool AttackSuccesful(Unit instigator, Unit target);
}
