using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : Singleton<RoundManager>
{

    public delegate void NextTurn();
    public NextTurn OnNextTurn;

    public delegate void TurnStarted();
    public TurnStarted OnTurnStarted;

    public delegate void TurnEnded();
    public TurnEnded OnTurnEnded;
    
    public delegate void NextRound();
    public NextRound OnNextRound;
    

    public int CurrentRound = 0;

	private void Awake()
	{
        BattleManager.Instance.OnBattleStarted += BeginningRound;
        OnNextTurn += IncrementTurn;
        OnNextRound += IncrementRound;
    }


    public void BeginningRound()
    {
        SetCurrentRound(0);
        OnTurnStarted?.Invoke();
    }

    public void IncrementTurn()
    {
        OnTurnEnded?.Invoke();
        //gameStateMachine.ChangeState(new WaitingForPlayer());
        //TurnBasedUnitManager.Instance.NextUnit();
        BattleManager.Instance.NextUnitIncrement();

        if (BattleManager.Instance.UnitIndex == 0)
        {
            Instance.OnNextRound?.Invoke();
        }

        OnTurnStarted?.Invoke();
    }

    public void IncrementRound()
    {
        SetCurrentRound(CurrentRound + 1);
    }

    public void SetCurrentRound(int currentRound)
    {
        CurrentRound = currentRound;
    }

}
