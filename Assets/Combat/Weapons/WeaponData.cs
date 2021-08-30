using Assets.Combat.Powers.Range;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Combat.Weapons
{
	[CreateAssetMenu]
	public	class WeaponData : ScriptableObject
	{
		public int ProficencyBonus;
		public Die WeaponDie;
		public WeaponScaling WeaponScaling;
		public WeaponType WeaponType;

		public Range Range;
		public Handedness Handedness;
		public WeaponProperty[] WeaponProperties;
		public int Price;
		public int Weight;


		public IWeapon GetWeapon()
		{
			return new Weapon(name, Price, WeaponDie, ProficencyBonus, Weight, WeaponType, GetRange(), Handedness, GetWeaponProperties());
		}

		public IRange GetRange()
		{
			return WeaponType switch
			{
				WeaponType.MELEE => new Melee(Range.MaximumRange, Range.MinimumRange, Range.SweetSpot),
				WeaponType.RANGE => new Ranged(Range.MaximumRange, Range.MinimumRange, Range.SweetSpot),
				_ => new NullRange(),
			};
		}
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
	MELEE, RANGE
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
public enum WeaponScaling
{
	STRENGTH, DEXTERITY, CONSTITUTION, INTELLIGENCE, WISDOM, CHARISMA
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

