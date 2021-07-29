using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class StrengthArmorClass : IAttack
{
	public int Modifier { get; set; }

	public StrengthArmorClass(int modifier = 0)
	{
		Modifier = modifier;
	}

	public bool AttackSuccesful(Unit instigator,Unit target)
	{
		int modifiers = instigator.Strength + (int)math.floor(instigator.Level / 2) + instigator.WeaponProficiency + instigator.WeaponEnhancement + Modifier;
		return DiceLibrary.AttackRollSucessful(modifiers, (int)target.ArmorClass.Value);
	}
}
