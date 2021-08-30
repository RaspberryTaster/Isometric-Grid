using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthWeapon : IDamage
{
	public int Damage(Unit instigator, Unit target)
	{
		return DiceLibrary.RollDie(instigator.equippedWeapon.WeaponDie) + instigator.WeaponEnhancement + instigator.Strength;
	}
}
