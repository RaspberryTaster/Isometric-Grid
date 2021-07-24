using Raspberry.Movement;
using Raspberry.Movement.Actions;

public class AttackAction : IAction
{
	private Unit unit;
	private Unit targetUnit;
	private StateMachine stateMachine;
	private bool hasAttacked = false;
	private IAction[] preActions = new IAction[0];

	public AttackAction(Unit unit, StateMachine stateMachine, Unit targetUnit, params IAction[] preActions)
	{
		this.unit = unit;
		this.stateMachine = stateMachine;
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
				stateMachine.Delay_Action_With_Action(this, preActions[i], ActionTypesDebug.DELAY_WITH_MOVE);
				notReady = true;
				break;
			}
		}
		if (notReady) return;
		unit.AttackOpponent(targetUnit);
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
		stateMachine.Dequeue(this);
	}

}
