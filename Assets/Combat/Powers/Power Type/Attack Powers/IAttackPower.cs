using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Combat.Powers.Power_Type
{
	public interface IAttackPower : IPower
	{
		public IAttack AttackType { get;}
		public void Hit(Unit target);
		public void Missed(Unit target);
	}
}
