using UnityEngine;

public class Unit : MonoBehaviour
{
	public RaspberryStat Health;
	[Space]
	public int Attack = 3;
	public int Skill = 2;
	public int Speed = 5;
	public int Luck = 10;
	public int Defence = 4;
	public int Resistance = 0;
	[Space]
	public RaspberryStat StandardAction;
	public RaspberryStat MinorAction;
	public RaspberryStat MovementPoints;
	[Space]
	public int MovementAnimationSpeed = 10;

	public Vector2Int attackRange = new Vector2Int(1,1);
	public void AttackOpponent(Unit target)
	{
		Debug.Log($"{gameObject.name} attacked {target.gameObject.name}!");
		target.Hit(Attack);
	}

	public void Hit(int damage)
	{
		Health.ReduceCurrent(damage);
	}
}
