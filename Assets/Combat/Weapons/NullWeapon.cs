using Assets.Combat.Damage;
using Assets.Combat.Powers.Range;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Combat.Weapons
{
	public class NullWeapon : IWeapon
	{
		public string Name => "None";

		public int Price => 0;

		public Die WeaponDie => new Die(0, 0, 0);

		public int ProficencyBonus => 0;

		public int Weight => 0;

		public WeaponType WeaponType => WeaponType.MELEE;

		public IRange Range => new NullRange();

		public Handedness Handedness => Handedness.ONEHANDED;

		public List<IWeaponProperty> WeaponProperties => new List<IWeaponProperty>();

		public int EnhancementBonus => 0;

		public IAttack Attack => new StrengthArmorClass();

		public IDamage Damage => new NullDamage();
	}
}
