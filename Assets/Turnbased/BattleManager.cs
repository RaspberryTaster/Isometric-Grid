using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleManager : Singleton<BattleManager>
{
	public delegate void BattleStarted();
	public BattleStarted OnBattleStarted;

	public delegate void SetCurrentUnit();
	public SetCurrentUnit OnSetCurrentUnit;

	public List<Unit> AllyUnit = new List<Unit>();
	public List<Unit> EnemyUnit = new List<Unit>();
	public List<Unit> NeutralUnit = new List<Unit>();
	[Space]
	public List<Unit> SortedAlly = new List<Unit>();
	public List<Unit> SortedEnemy = new List<Unit>();
	public List<Unit> TurnOrder = new List<Unit>();
    [Space]

    Dictionary<Unit, int> allyUnitDictionary = new Dictionary<Unit, int>();
    Dictionary<Unit, int> enemyUnitDictionary = new Dictionary<Unit, int>();
    public Unit CurrentUnit;
    public Unit FirstUnitInTurnOrder => TurnOrder[0];
    public int UnitIndex = 0;

    // Start is called before the first frame update
    void Awake()
    {
		OnBattleStarted += SetUpTurnOrder;
    }

	public void SetUpTurnOrder()
	{
		OrderUnits();

		TurnOrderBackwards();

		CurrentUnitSet(FirstUnitInTurnOrder);

	}

	public void NextUnitIncrement()
	{
		#region "Check if anything is in the turn order count, return if nothing is there."

		//If nothing is in the turn order, default the index to 0 and do not play the rest of the metod.
		if (TurnOrder.Count == 0)
		{
			UnitIndex = 0;
			return;
		}

		#endregion

		#region "Increment index as long as there are units in the turn order."

		UnitIndex++;

		if (UnitIndex > TurnOrder.Count && TurnOrder.Count != 0)
		{
			UnitIndex = TurnOrder.Count + 1;
		}

		#endregion

		#region "If turn order index equals turn order count, reset the turn order index to 0 and then, invoke next round action."

		//If the index is equal to the turnorder count then its reached the end
		//Rest it back to 0
		if (UnitIndex == TurnOrder.Count)
		{
			UnitIndex = 0;
		}

		#endregion

		#region "If there are units in the turn order, pick the unit with the unit index."

		if (TurnOrder.Count != 0)
		{
			//print(unitIndex)
			CurrentUnitSet(TurnOrder[UnitIndex]);
		}

		#endregion

		//Does the next round event.
		//SetCurrentUnit();
	}

	public void CurrentUnitSet(Unit unit)
	{
		CurrentUnit = unit;
		OnSetCurrentUnit?.Invoke();
	}
	private void OrderUnits()
	{
		OrderAlly();

		OrderEnemy();
	}

	private void OrderEnemy()
	{
		#region "Clear enemy unit dictionary and sorted enemy list."

		//Have to clear before adding new units to the list.
		enemyUnitDictionary.Clear();
		SortedEnemy.Clear();

		#endregion

		foreach (Unit unit in EnemyUnit)
		{
			enemyUnitDictionary.Add(unit, unit.GetInitaitve());
		}

		foreach (KeyValuePair<Unit, int> unit in enemyUnitDictionary.OrderBy(key => key.Value))
		{
			//print($"{unit.Key}, {unit.Value}");
			SortedEnemy.Add(unit.Key);
		}
	}

	private void OrderAlly()
	{
		#region "Clear ally unit dictionary and sorted ally list."

		//Have to clear before adding new units to the lsit.
		allyUnitDictionary.Clear();
		SortedAlly.Clear();

		#endregion

		foreach (Unit unit in AllyUnit)
		{
			allyUnitDictionary.Add(unit, unit.GetInitaitve());
		}

		foreach (KeyValuePair<Unit, int> unit in allyUnitDictionary.OrderBy(key => key.Value))
		{
			//print($"{unit.Key}, {unit.Value}");
			SortedAlly.Add(unit.Key);
		}
	}

	private void TurnOrderBackwards()
	{
		TurnOrder.Clear();
		if (SortedAlly.Count > SortedEnemy.Count)
		{
			int iterationNumber = 0;
			int curIteration = SortedEnemy.Count;
			for (int i = SortedAlly.Count; i-- > 0;)
			{
				iterationNumber++;
				//print(i);
				TurnOrder.Add(SortedAlly[i]);
				if (!(curIteration == 0))
				{
					curIteration = SortedEnemy.Count - iterationNumber;
					TurnOrder.Add(SortedEnemy[curIteration]);
				}
			}
		}
		else
		{
			int iterationNumber = 0;
			int curIteration = SortedAlly.Count;
			for (int i = SortedEnemy.Count; i-- > 0;)
			{
				iterationNumber++;
				//print(i);
				TurnOrder.Add(SortedEnemy[i]);
				if (!(curIteration == 0))
				{
					curIteration = SortedAlly.Count - iterationNumber;
					TurnOrder.Add(SortedAlly[curIteration]);
				}
			}
		}
	}
	public void ChangeTurnOrder(Unit unit, int changeOfOrder)
	{
		if (!(TurnOrder.Contains(unit))) return;
		var unitInQuestion = unit;
		int unitIndex = TurnOrder.IndexOf(unit);
		int newIndex = unitIndex + changeOfOrder;
		TurnOrder.Remove(unit);
		//Big came about when switching position of units, it would remove the unit but never add them back in,
		//possibly due to the turnorder count changing after the unit is removed, placing the new index here to see if that changes it.
		//seems to have worked.
		newIndex = Mathf.Clamp(newIndex, 0, TurnOrder.Count);
		TurnOrder.Insert(newIndex, unitInQuestion);
	}

}
