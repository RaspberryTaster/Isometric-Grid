using Assets.Combat.Powers.Power_Type.Attack_Powers.Attack.Target_Defence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Mathematics;

namespace Assets.Combat.Powers.Power_Type.Attack_Powers.Attack
{
	public class ConstitutionAttack : IAttack
	{
		public int Modifier { get; set; }
		private ITargetDefence targetDefence;
		public ITargetDefence TargetDefence => targetDefence;

		public ConstitutionAttack(ITargetDefence targetDefence, int modifier = 0)
		{
			this.targetDefence = targetDefence;
			Modifier = modifier;
		}

		public bool AttackSuccesful(Unit instigator, Unit target)
		{
			int modifiers = instigator.ConstitutionMod + (int)math.floor(instigator.Level / 2) + instigator.equippedWeapon.ProficencyBonus + instigator.equippedWeapon.EnhancementBonus + Modifier;
			return DiceLibrary.AttackRollSucessful(modifiers, targetDefence.Defence(target));
		}
	}
}
