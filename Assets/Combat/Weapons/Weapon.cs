using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Combat.Weapons
{
	public	class Weapon : ScriptableObject
	{
		public int ProficencyBonus;
		public Die WeaponDie;
		public WeaponType WeaponType;
		public Range Range;
		public Handedness Handedness;
		public WeaponProperty[] WeaponProperties;
		public int Price;
		public int Weight;


		List<IWeaponProperty> GetWeaponProperties()
		{
			List<IWeaponProperty> value = new List<IWeaponProperty>();
			foreach(WeaponProperty weaponProperty in WeaponProperties)
			{
				value.Add(weaponProperty.GetWeaponProperty());
			}
			return value;
		}
	}
}
public enum WeaponType
{ 
	melee,range
}

[Serializable]
public struct Range
{
	public int MinimumRange;
	public int MaximumRange;
	public int SweetSpot;

	public Range(int maximumRange, int minimumRange = 1, int sweetSpot = 1)
	{
		MinimumRange = minimumRange;
		MaximumRange = maximumRange;
		SweetSpot = sweetSpot;
	}
}
public enum Handedness
{ 
	TWOHANDED,ONEHANDED
}
public enum ProficiencyCategory
{
	SIMPLE, MILITARY, SUPERIOR
}
public class WeaponProperty : ScriptableObject
{
	public WeaponPropertyType WeaponPropertyType;
	public IWeaponProperty GetWeaponProperty()
	{
		IWeaponProperty weaponProperty = new NullWeaponPropery();
		switch (WeaponPropertyType)
		{
			case WeaponPropertyType.BRUTAL:
				weaponProperty = new BrutalWeaponProperty();
				break;
			case WeaponPropertyType.BRUTAL2:
				weaponProperty = new BrutalWeaponProperty(2);
				break;
			case WeaponPropertyType.BRUTAL3:
				weaponProperty = new BrutalWeaponProperty(3);
				break;
		}

		//Have a eneum for high crit etc
		//http://hastur.net/wiki/Weapon_properties_(4E)
		return weaponProperty;
	}

}
public enum WeaponPropertyType
{ 
	HIGH_CRIT,BRUTAL, BRUTAL2, BRUTAL3
}

public interface IWeaponProperty
{
	//needs a reference to the user and the actual weapon.
}
public class NullWeaponPropery : IWeaponProperty
{

}
public class BrutalWeaponProperty : IWeaponProperty
{
	int brutalValue;
	int minimumRoll
	{ 
		get
		{
			return brutalValue + 1;
		}
	}
	

	public BrutalWeaponProperty(int brutalValue = 1)
	{
		this.brutalValue = brutalValue;
	}
}

