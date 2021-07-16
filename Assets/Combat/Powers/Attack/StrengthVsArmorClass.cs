using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthVsArmorClass : IAttack
{
	public bool AttackSuccesful(Unit instigator,Unit target)
	{
		return DiceLibrary.Hit(instigator.Strength + instigator.WeaponProficiency + instigator.WeaponEnhancement, target.ArmorClass);
	}
}
