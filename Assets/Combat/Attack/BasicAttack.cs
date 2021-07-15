using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack
{
	IAttack attack = new StrengthVsArmorClass();
	IDamage damage = new StrengthWeapon();

	public void Attack(Unit instigator, Unit target)
	{
		if(attack.AttackSuccesful(instigator, target))
		{
			instigator.TakeDamage(damage.Damage(instigator, target));
		}
	}
}
