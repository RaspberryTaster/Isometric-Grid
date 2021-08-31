using Assets.Combat.Powers.Power_Type.Attack_Powers.Attack.Target_Defence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Combat.Powers.Power_Type.Attack_Powers.Attack
{
	public class CharismaAttack : IAttack
	{
		public int Modifier { get; set; }
		private ITargetDefence targetDefence;
		public ITargetDefence TargetDefence => targetDefence;

		public CharismaAttack(ITargetDefence targetDefence, int modifier = 0)
		{
			this.targetDefence = targetDefence;
			Modifier = modifier;

		}

		public bool AttackSuccesful(Unit instigator, Unit target)
		{
			int modifiers = instigator.CharismaMod + instigator.equippedWeapon.ProficencyBonus + instigator.equippedWeapon.EnhancementBonus + Modifier;
			return DiceLibrary.AttackRollSucessful(modifiers, TargetDefence.Defence(target));
		}
	}
}
