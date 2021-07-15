using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamage
{
	public int Damage(Unit instigator, Unit target);
}
