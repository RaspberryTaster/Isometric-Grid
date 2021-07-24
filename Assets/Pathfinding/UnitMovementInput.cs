using Assets;
using Raspberry.Movement;
using Raspberry.Movement.Actions;
using UnityEngine;

public class UnitMovementInput : MonoBehaviour
{
	public DistanceCheck SurroundingNodes;
	public Node selectedNode;
	public UnitMovement unitMovement;
	public Unit unit;
	public StateMachine stateMachine;
	public bool CanMove;
	PlayerInput playerInput;

	void Awake()
	{
		playerInput = GetComponent<PlayerInput>();
		unit = unitMovement.GetComponent<Unit>();
	}

	private void OnEnable()
	{
		playerInput.OnClick += PlayerInput_OnHitRaycast;
	}

	public void OnDisable()
	{
		playerInput.OnClick -= PlayerInput_OnHitRaycast;
	}

	private void PlayerInput_OnHitRaycast(RaycastHit hit)
	{
		if (unit.currentState != ControlState.MOVEMENT) return;
		unitMovement.SetDistanceNodes();
		
		UnitMovement targetUnit = hit.collider.GetComponent<UnitMovement>();
		
		Node targetUnitNode = null;
		if (targetUnit != null)
		{
			targetUnitNode = SquareGrid.Instance.NodeGrid.NodeFromWorldPoint(targetUnit.transform.position);
		}
		
		Node hitNode = SquareGrid.Instance.NodeGrid.NodeFromWorldPoint(hit.point);
		Unit targtCombatComponent = hit.collider.GetComponent<Unit>();

		int stoppingDistance = 0;
		IAction action;
		ActionTypesDebug actionTypesDebug;
		
		if (targetUnit != null && targetUnitNode != null && unit.powerHandler.rangeData.nodesInRange.Contains(targetUnitNode))
		{
			SurroundingNodes.SetUp(unit.transform, targetUnitNode);
			selectedNode = SurroundingNodes.closestNode;
			MoveAction moveAction = new MoveAction(unitMovement, stateMachine, selectedNode, 0);
			action = new AttackAction(unit, stateMachine, targtCombatComponent, moveAction);
			actionTypesDebug = ActionTypesDebug.ATTACK;
		}
		else
		{
			selectedNode = hitNode;
			action = new MoveAction(unitMovement, stateMachine, selectedNode, stoppingDistance);
			actionTypesDebug = ActionTypesDebug.WALK;
		}
		

		//selectedNode = hitNode;
		//action = new MoveAction(unitMovement, stateMachine, selectedNode, stoppingDistance);
		//actionTypesDebug = ActionTypesDebug.WALK;
		if (selectedNode == null || !selectedNode.Walkable || action == null || !unitMovement.MovementNodes.Contains(selectedNode)) return;
		stateMachine.Dequeue_All_Before_Adding_Action(action, actionTypesDebug);
	}

	private void OnDrawGizmos()
	{
		if (selectedNode != null)
		{
			Gizmos.color = Color.green;
			Gizmos.DrawCube(selectedNode.WorldPosition, Vector3.one);
		}

	}
}
