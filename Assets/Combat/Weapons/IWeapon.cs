using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Combat.Weapons
{
	public interface IWeapon
	{
		public string Name { get; }
		public int Price { get; }
		public Die WeaponDie { get; }
		public int ProficencyBonus { get; }
		public int EnhancementBonus { get; }
		public int Weight { get; }


		public WeaponType WeaponType { get; }
		public IRange Range { get; }
		public IAttack Attack { get; }
		public IDamage Damage { get; }
		public Handedness Handedness { get; }
		public List<IWeaponProperty> WeaponProperties { get; }
	}
}
