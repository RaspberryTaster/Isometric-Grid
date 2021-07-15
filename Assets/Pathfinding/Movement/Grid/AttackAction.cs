using Raspberry.Movement;
using Raspberry.Movement.Actions;

public class AttackAction : IAction
{
	private Unit unitCombatComponent;
	private Unit targetUnit;
	private StateMachine queueComponent;
	private bool hasAttacked = false;
	private IAction[] preActions = new IAction[0];

	public AttackAction(Unit unitCombatComponent, StateMachine queueComponent, Unit targetUnit, params IAction[] preActions)
	{
		this.unitCombatComponent = unitCombatComponent;
		this.queueComponent = queueComponent;
		this.preActions = preActions;
		this.targetUnit = targetUnit;
	}

	public bool IsDone()
	{
		return hasAttacked;
	}

	public void Start()
	{
		bool notReady = false;
		for (int i = 0; i < preActions.Length; i++)
		{
			if (!preActions[i].IsDone())
			{
				queueComponent.Delay_Action_With_Action(this, preActions[i], Action_Types.DELAY_WITH_MOVE);
				notReady = true;
				break;
			}
		}
		if (notReady) return;
		unitCombatComponent.AttackOpponent(targetUnit);
		hasAttacked = true;
	}

	public void Update()
	{
		if (IsDone())
		{
			Exit();
		}
	}
	public void Exit()
	{
		queueComponent.Dequeue(this);
	}

}
