using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : IPower
{
	IRange range = new Ranged();
	IActionCost actionCost = new StandardAction();
	IAttack attack = new StrengthVsArmorClass();
	IDamage damage = new StrengthWeapon();

	public void Attack(Unit instigator, Unit target)
	{
		if(attack.AttackSuccesful(instigator, target))
		{
			instigator.TakeDamage(damage.Damage(instigator, target));
		}
	}

	public void Execute(Unit instigator, Unit target)
	{
		actionCost.ActionTaken(instigator);
		Attack(instigator, target);
	}

	public void SelectPower(Unit user)
	{
		if(actionCost.MetRequirement(user))
		{
			user.powerHandler.SelectPower(this, range.CheckRange(user));
		}
	}
}
