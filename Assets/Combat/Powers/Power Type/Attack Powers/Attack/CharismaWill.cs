using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Combat.Powers.Power_Type.Attack_Powers.Attack
{
	public class CharismaWill : IAttack
	{
		public int Modifier { get; set; }

		public CharismaWill(int modifier = 0)
		{
			Modifier = modifier;
		}

		public bool AttackSuccesful(Unit instigator, Unit target)
		{
			int modifiers = instigator.Charisma + instigator.WeaponProficiency + instigator.WeaponEnhancement + Modifier;
			return DiceLibrary.AttackRollSucessful(modifiers, (int)target.Will.Value);
		}
	}
}
