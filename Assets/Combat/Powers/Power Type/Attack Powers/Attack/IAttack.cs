using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttack
{
	public int Modifier { get; set; }
	public bool AttackSuccesful(Unit instigator, Unit target);
}