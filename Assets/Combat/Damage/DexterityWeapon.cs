using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Combat.Damage
{
	public class DexterityWeapon : IDamage
	{
		public int Damage(Unit instigator, Unit target)
		{
			return DiceLibrary.RollDie(instigator.equippedWeapon.WeaponDie) + instigator.equippedWeapon.EnhancementBonus + instigator.DexterityMod;
		}
	}
}
