using Kryz.CharacterStats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Combat.Powers.Power_Type.Attack_Powers.Attack.Target_Defence
{
	public class TargetDefenceNULL : ITargetDefence
	{
		public int Defence(Unit target)
		{
			return 0;
		}
	}
}
