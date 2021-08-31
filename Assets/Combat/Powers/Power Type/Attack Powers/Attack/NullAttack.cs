using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Combat.Powers.Power_Type.Attack_Powers.Attack
{
	public class NullAttack : IAttack
	{
		private int modifier;
		public int Modifier 
		{ 
			get => modifier; 
			set { modifier = value; } 
		}

		public bool AttackSuccesful(Unit instigator, Unit target)
		{
			return true;
		}
	}
}
