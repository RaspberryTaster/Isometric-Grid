using Assets.Combat.Buffs;
using Assets.Combat.Powers.Range;
using Assets.Combat.Weapons;
using Kryz.CharacterStats;
using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
	public int Level;

	public DepletingStat HitPoints;
	[ShowNativeProperty]
	public int Bloodied
	{
		get
		{
			return HitPoints.Maximum / 2;
		}
	}

	public DepletingStat BarrierPoints = new DepletingStat(1000, 0);
	public List<BarrierInstance> BarrierInstances = new List<BarrierInstance>();

	public CharacterStat Initative;

	[Space]
	[BoxGroup("Ability Scores")] public int Strength = 10;
	[BoxGroup("Ability Scores")] public int Dexterity = 10;
	[BoxGroup("Ability Scores")] public int Constitution = 10;
	[BoxGroup("Ability Scores")] public int Wisdom = 10;
	[BoxGroup("Ability Scores")] public int Intelligence = 10;
	[BoxGroup("Ability Scores")] public int Charisma = 10;


	public int StrengthMod
	{
		get
		{
			return (Strength - 10) / 2; 
		}
	}
	public int DexterityMod
	{
		get
		{
			return (Dexterity - 10) / 2;
		}
	}
	public int ConstitutionMod
	{ 
		get
		{
			return (Constitution - 10) / 2;
		}
	}
	public int IntelligenceMod
	{
		get
		{
			return (Intelligence - 10) / 2;
		}
	}
	public int WisdomMod
	{
		get
		{
			return (Wisdom - 10) / 2;
		}
	}
	public int CharismaMod
	{
		get
		{
			return (Charisma - 10) / 2;
		}
	}

	[BoxGroup("Defences")] public CharacterStat Deflection = new CharacterStat(CharacterSheetLibrary.BASEDEFENCE);
	[BoxGroup("Defences")] public CharacterStat Fortitude = new CharacterStat(CharacterSheetLibrary.BASEDEFENCE);
	[BoxGroup("Defences")] public CharacterStat Reflex = new CharacterStat(CharacterSheetLibrary.BASEDEFENCE);
	[BoxGroup("Defences")] public CharacterStat Will = new CharacterStat(CharacterSheetLibrary.BASEDEFENCE);

	[Space]
	[BoxGroup("Actions")] public DepletingStat StandardAction;
	[BoxGroup("Actions")] public DepletingStat MinorAction;
	[BoxGroup("Actions")] public DepletingStat MovementPoints;
	public ControlState currentState;

	public WeaponData MeleeWeapon;
	public WeaponData RangedWeapon;
	public IWeapon meleeWeapon;
	public IWeapon rangedWeapon;
	public IWeapon equippedWeapon;

	public PowerHandler powerHandler;
	public UnitMovement UnitMovement;
	private void Awake()
	{
		UnitMovement = GetComponent<UnitMovement>();
		Deflection.AddModifier(new StatModifier(DexterityMod, StatModType.Flat, this));
		powerHandler = GetComponent<PowerHandler>();
		Initative = new CharacterStat(DexterityMod);
		meleeWeapon = MeleeWeapon.GetWeapon();
		rangedWeapon = RangedWeapon.GetWeapon();
		EquipMelee();
	}
	private void Start()
	{
		HealthBarManager.Instance.Subscribe(this);
	}
	public void AttackOpponent(Unit target)
	{
		Debug.Log($"{gameObject.name} attacked {target.gameObject.name}!");
		target.TakeDamage(StrengthMod);
	}

	public void TakeDamage(int damage)
	{
		int count = BarrierInstances.Count;
		for (int i = 0; i < count; i++)
		{
			if (damage == 0) break;
			damage = BarrierInstances[i].Damage(damage);
		}

		if(damage > 0)
		{
			HitPoints.ReduceCurrent(damage);
			Debug.Log($"{this} took {damage} damage.");
		}
		else if(damage < 0)
		{
			HitPoints.IncreaseCurrent(damage * -1);
			Debug.Log($"{this} healed for {damage} {HitPoints}.");
		}
		
	}

	public List<BarrierInstance> InstancesWithSameSource(object source)
	{
		List<BarrierInstance> value = new List<BarrierInstance>();
		foreach(BarrierInstance barrier in BarrierInstances)
		{
			if(barrier.source.GetType() == source.GetType())
			{
				value.Add(barrier);
			}
		}

		return value;
	}

	public void EquipMelee()
	{
		equippedWeapon = meleeWeapon;
	}
	public void EquipRanged()
	{
		equippedWeapon = rangedWeapon;
	}

	public int GetInitaitve()
	{
		return (int)Initative.Value + DiceLibrary.RollDie(new Die(1, 1, 20)); 
	}
}
public enum ControlState
{
	ATTACK, MOVEMENT
}
