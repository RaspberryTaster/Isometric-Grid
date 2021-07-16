using NaughtyAttributes;
using UnityEngine;

public class Unit : MonoBehaviour
{
	public RaspberryStat HitPoints;

	[ShowNativeProperty] public int Bloodied
	{ 
		get
		{
			return HitPoints.Maximum / 2;
		}
	}

	public int Initative;

	[Space]
	[BoxGroup("Ability Scores")] public int Strength = 10;
	[BoxGroup("Ability Scores")] public int Dexterity = 10;
	[BoxGroup("Ability Scores")] public int Constitution = 10;
	[BoxGroup("Ability Scores")] public int Wisdom = 10;
	[BoxGroup("Ability Scores")] public int Int = 10;
	[BoxGroup("Ability Scores")] public int Charisma = 10;

	[BoxGroup("Defences")] public int ArmorClass = 4;
	[BoxGroup("Defences")] public int Fortitude = 0;
	[BoxGroup("Defences")] public int Reflex = 0;
	[BoxGroup("Defences")] public int Will = 0;

	[Space]
	[BoxGroup("Actions")] public RaspberryStat StandardAction;
	[BoxGroup("Actions")] public RaspberryStat MinorAction;
	[BoxGroup("Actions")] public RaspberryStat MovementPoints;
	[Space]
	public int MovementAnimationSpeed = 10;

	public Vector2Int attackRange = new Vector2Int(1,1);

	public int WeaponDamage = 5;
	public int WeaponEnhancement = 0;
	public int WeaponProficiency = 2;

	public PowerHandler powerHandler;
	private void Awake()
	{
		powerHandler = GetComponent<PowerHandler>();
	}
	public void AttackOpponent(Unit target)
	{
		Debug.Log($"{gameObject.name} attacked {target.gameObject.name}!");
		target.TakeDamage(Strength);
	}

	public void TakeDamage(int damage)
	{
		HitPoints.ReduceCurrent(damage);
	}
}
