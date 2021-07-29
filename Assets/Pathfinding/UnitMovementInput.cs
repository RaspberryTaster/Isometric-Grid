using Assets;
using Raspberry.Movement;
using Raspberry.Movement.Actions;
using UnityEngine;

public class UnitMovementInput : MonoBehaviour
{
	public DistanceCheck SurroundingNodes;
	public UnitMovement unitMovement;
	public StateMachine stateMachine;
	public Node selectedNode;

	public bool CanMove;

	private void OnEnable()
	{
		PlayerInput.Instance.OnSelectUnit += UpdateUnitComponents;
		PlayerInput.Instance.OnLeftClick += PlayerInput_OnHitRaycast;

	}

	public void OnDisable()
	{
		PlayerInput.Instance.OnLeftClick -= PlayerInput_OnHitRaycast;
		PlayerInput.Instance.OnSelectUnit -= UpdateUnitComponents;


	}

	private void PlayerInput_OnHitRaycast(RaycastHit hit)
	{
		if (stateMachine == null ||PlayerInput.Instance.ControlledUnit == null ||PlayerInput.Instance.ControlledUnit.currentState != ControlState.MOVEMENT || unitMovement == null) return;
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
		
		if (targetUnit != null && targetUnitNode != null && UnitPowerInput.Instance.powerHandler.rangeData.nodesInRange.Contains(targetUnitNode))
		{
			SurroundingNodes.SetUp(PlayerInput.Instance.ControlledUnit.transform, targetUnitNode);
			selectedNode = SurroundingNodes.closestNode;
			MoveAction moveAction = new MoveAction(unitMovement, stateMachine, selectedNode, 0);
			action = new AttackAction(PlayerInput.Instance.ControlledUnit, stateMachine, targtCombatComponent, moveAction);
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

	public void UpdateUnitComponents()
	{
		unitMovement = PlayerInput.Instance.ControlledUnit.GetComponent<UnitMovement>();
		stateMachine = PlayerInput.Instance.ControlledUnit.GetComponent<StateMachine>();
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
