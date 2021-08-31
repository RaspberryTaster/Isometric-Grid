using Assets.Combat.Powers.Power_Type.Attack_Powers.Attack.Target_Defence;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class StrengthAttack : IAttack
{
	public int Modifier { get; set; }
	private ITargetDefence targetDefence;
	public ITargetDefence TargetDefence => targetDefence;

	public StrengthAttack(ITargetDefence targetDefence, int modifier = 0)
	{
		this.targetDefence = targetDefence;
		Modifier = modifier;
	}

	public bool AttackSuccesful(Unit instigator,Unit target)
	{
		int modifiers = instigator.StrengthMod + (int)math.floor(instigator.Level / 2) + instigator.equippedWeapon.ProficencyBonus + instigator.equippedWeapon.EnhancementBonus + Modifier;
		return DiceLibrary.AttackRollSucessful(modifiers, targetDefence.Defence(target));
	}
}
