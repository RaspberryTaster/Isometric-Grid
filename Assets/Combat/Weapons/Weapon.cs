using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Combat.Weapons
{
	[Serializable]
	public class Weapon : IWeapon
	{
		private string name;
		private int price;
		private Die weaponDie;
		private int proficencyBonus;
		private int weight;


		private WeaponType weaponType;
		private IRange range;
		private Handedness handedness;
		private List<IWeaponProperty> weaponProperties;

		public string Name => name;

		public int Price => price;

		public Die WeaponDie => weaponDie;

		public int ProficencyBonus => proficencyBonus;

		public int Weight => weight;

		public WeaponType WeaponType => weaponType;

		public IRange Range => range;

		public Handedness Handedness => handedness;

		public List<IWeaponProperty> WeaponProperties => weaponProperties;

		public Weapon(string name, int price,
		Die weaponDie, int proficencyBonus,
		int weight, WeaponType weaponType,
		IRange range, Handedness handedness,
		List<IWeaponProperty> weaponProperties)
		{
			this.name = name;
			this.price = price;
			this.weaponDie = weaponDie;
			this.proficencyBonus = proficencyBonus;
			this.weight = weight;
			this.weaponType = weaponType;
			this.range = range;
			this.handedness = handedness;
			this.weaponProperties = weaponProperties;
		}

	}
}
