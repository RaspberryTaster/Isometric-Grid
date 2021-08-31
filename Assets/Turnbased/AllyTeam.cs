using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyTeam : ITeam
{
	private Unit unit;

	public AllyTeam(Unit unit)
	{
		this.unit = unit;
	}

	public void Enter()
	{
		BattleManager.Instance.AllyUnit.Add(unit);
	}

	public void Exit()
	{
		BattleManager.Instance.AllyUnit.Remove(unit);
	}
}
public class NeutralTeam : ITeam
{
	private Unit unit;

	public NeutralTeam(Unit unit)
	{
		this.unit = unit;
	}

	public void Enter()
	{
		BattleManager.Instance.NeutralUnit.Add(unit);
	}

	public void Exit()
	{
		BattleManager.Instance.NeutralUnit.Remove(unit);
	}
}
public class EnemyTeam : ITeam
{
	private Unit unit;

	public EnemyTeam(Unit unit)
	{
		this.unit = unit;
	}

	public void Enter()
	{
		BattleManager.Instance.EnemyUnit.Add(unit);
	}

	public void Exit()
	{
		BattleManager.Instance.EnemyUnit.Remove(unit);
	}
}
