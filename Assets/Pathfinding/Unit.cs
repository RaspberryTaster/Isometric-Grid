using Assets.Combat.Powers.Range;
using Assets.Combat.Weapons;
using Kryz.CharacterStats;
using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
	public int Level;

	public RaspberryStat HitPoints;

	[ShowNativeProperty] public int Bloodied
	{ 
		get
		{
			return HitPoints.Maximum / 2;
		}
	}

	public CharacterStat Initative;

	[Space]
	[BoxGroup("Ability Scores")] public int Strength = 10;
	[BoxGroup("Ability Scores")] public int Dexterity = 10;
	[BoxGroup("Ability Scores")] public int Constitution = 10;
	[BoxGroup("Ability Scores")] public int Wisdom = 10;
	[BoxGroup("Ability Scores")] public int Int = 10;
	[BoxGroup("Ability Scores")] public int Charisma = 10;

	[BoxGroup("Defences")] public CharacterStat ArmorClass = new CharacterStat(CharacterSheetLibrary.BASEDEFENCE);
	[BoxGroup("Defences")] public CharacterStat Fortitude = new CharacterStat(CharacterSheetLibrary.BASEDEFENCE);
	[BoxGroup("Defences")] public CharacterStat Reflex = new CharacterStat(CharacterSheetLibrary.BASEDEFENCE);
	[BoxGroup("Defences")] public CharacterStat Will = new CharacterStat(CharacterSheetLibrary.BASEDEFENCE);

	[Space]
	[BoxGroup("Actions")] public RaspberryStat StandardAction;
	[BoxGroup("Actions")] public RaspberryStat MinorAction;
	[BoxGroup("Actions")] public RaspberryStat MovementPoints;
	[Space]
	public CharacterStat MovementAnimationSpeed = new CharacterStat(10);

	public int WeaponDamage = 5;
	public int WeaponEnhancement = 0;
	public int WeaponProficiency = 2;


	public List<Node> OccupyingNodes;

	public ControlState currentState;

	public WeaponData MeleeWeapon;
	public WeaponData RangedWeapon;
	public IWeapon meleeWeapon;
	public IWeapon rangedWeapon;
	public IWeapon equippedWeapon;

	public PowerHandler powerHandler;
	private void Awake()
	{
		powerHandler = GetComponent<PowerHandler>();
		Initative = new CharacterStat(Dexterity / 2);
		meleeWeapon = MeleeWeapon.GetWeapon();
		rangedWeapon = RangedWeapon.GetWeapon();
	}
	public void AttackOpponent(Unit target)
	{
		Debug.Log($"{gameObject.name} attacked {target.gameObject.name}!");
		target.TakeDamage(Strength);
	}

	public void TakeDamage(int damage)
	{
		HitPoints.ReduceCurrent(damage);
		Debug.Log($"{this} took {damage} damage.");
	}

	public void UnOccupy()
	{
		int count = OccupyingNodes.Count;
		for (int i = 0; i < count; i++)
		{
			OccupyingNodes[i].UnOccupy(this);
		}
		OccupyingNodes.Clear();
	}

	public void EquipMelee()
	{
		equippedWeapon = meleeWeapon;
	}
	public void EquipRanged()
	{
		equippedWeapon = rangedWeapon;
	}
	public void Occupy(Node n)
	{
		n.Occupy(this);
	}
	public void GetInitaitve()
	{

	}
}
public enum ControlState
{
	ATTACK, MOVEMENT
}
