using Assets.Combat.Powers.Power_Type;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : IAttackPower
{
	public Unit unit;
	IRange range;
	IActionType actionType;
	IAttack attackType;
	public IAttack AttackType { get => attackType; }
	IDamage damage;

	public BasicAttack(Unit unit)
	{
		this.unit = unit;
		Update(unit.equippedWeapon.Range, new StandardAction(), new StrengthArmorClass(), new StrengthWeapon());
	}
	public BasicAttack(Unit unit, IRange range, IActionType actionCost, IAttack attack, IDamage damage)
	{
		this.unit = unit;
		Update(range, actionCost, attack, damage);
	}

	public void Update(IRange range, IActionType actionCost, IAttack attack, IDamage damage)
	{
		this.range = range;
		actionType = actionCost;
		attackType = attack;
		this.damage = damage;
	}

	public void Execute()
	{
		if (!CanExecute()) return;
		actionType.ActionTaken(unit);
		foreach (Unit target in unit.powerHandler.TargetUnits)
		{
			if (AttackType.AttackSuccesful(unit, target))
			{
				Hit(target);
			}
			else
			{
				Missed(target);
			}
		}
		unit.powerHandler.Clear();
	}

	public bool CanExecute()
	{
		bool value = unit.powerHandler.TargetUnits.Count > 0;
		Debug.Log(value);
		return value;
	}
	public void SelectPower()
	{
		if(actionType.MetRequirement(unit))
		{
			unit.powerHandler.SelectPower(this, range.CheckRange(unit));
		}
	}

	public void Hit(Unit target)
	{
		unit.TakeDamage(damage.Damage(unit, target));
	}
	public void Missed(Unit target)
	{
		Debug.Log($"{unit} has missed {target}");
	}

	public void Effect(Unit target)
	{

	}
}
