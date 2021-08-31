using UnityEngine;
using System.Collections;
using NaughtyAttributes;
using System;
using System.Collections.Generic;
using Kryz.CharacterStats;

[RequireComponent(typeof(Unit))]
public class UnitMovement : MonoBehaviour
{
	[SerializeField] private Unit unit;
	
	private Node[] path;
	private int targetIndex;
	private Action<bool> endOfPath;
	private float yOffset;


	public MovementNodes movementNodes;

	public List<Node> OccupyingNodes;
	public List<Node> MovementNodes
	{
		get
		{
			return movementNodes.walkable;
		}
	}

	public CharacterStat MovementAnimationSpeed = new CharacterStat(10);

	public bool DrawGizmos;
	private void Awake()
	{
		yOffset = transform.position.y;
		unit = GetComponent<Unit>();


	}

	private void Start()
	{

		UnOccupy();
		Node currentNode = SquareGrid.Instance.NodeGrid.NodeFromWorldPoint(transform.position);
		transform.position = currentNode.WorldPosition;
		Occupy(currentNode);
		SetDistanceNodes();
	}

	public void Move(Node targetPosition, int stoppingDistance, Action<bool> callback)
	{
		endOfPath = callback;
		PathRequestManager.RequestPath(SquareGrid.Instance.NodeGrid.NodeFromWorldPoint(transform.position), targetPosition, stoppingDistance, OnPathFound);
	}
	public void OnPathFound(Node[] newPath, bool pathSuccessful) {
		if (pathSuccessful) {
			path = newPath;
			targetIndex = 0;
			StopCoroutine(nameof(FollowPath));
			StartCoroutine(nameof(FollowPath));
		}
	}

	[Button]
	public void SetDistanceNodes()
	{
		foreach (Node n in MovementNodes)
		{
			n.RemoveColor((int)TIleMode.MOVEMENT);
		}

		if(SquareGrid.Instance.NodeGrid == null)
		{
			Debug.LogWarning("Node grid is null");
		}

		movementNodes = DijkstraFrontier(OccupyingNodes[0]);
		//Debug.Log(MovementNodes.Count);

		for(int i = 0; i < MovementNodes.Count; i++)
		{
			//Debug.Log(i);
			Node n = MovementNodes[i];
			n.SetColor((int)TIleMode.MOVEMENT);
		}
	}

	public MovementNodes DijkstraFrontier(Node center)
	{
		List<Node> edge = new List<Node>();
		List<Node> reached = new List<Node>() { center};
		Queue<Node> frontier = new Queue<Node>();
		frontier.Enqueue(center);
		Dictionary<Node, int> costSoFar = new Dictionary<Node, int>();
		Dictionary<Node, Node> cameFrom = new Dictionary<Node, Node>();
		costSoFar[center] = 0;
		cameFrom[center] = null;

		while (frontier.Count != 0)
		{
			Node current = frontier.Dequeue();
			List<Node> neighbours = SquareGrid.Instance.NodeGrid.GetNeighbours(current);
			
			foreach (Node next in neighbours)
			{
				if (!next.Walkable)
				{
					if (!edge.Contains(current))
					{
						edge.Add(current);
					}
					continue;
				}
				int newCost = costSoFar[current] + SquareGrid.Instance.GetDistance(current, next, Pathfinding.Instance.DiagonalCost, Pathfinding.Instance.HorizontalCost) + next.MovementPenalty;
				if (newCost > unit.MovementPoints.CurrentValue)
				{
					if (!edge.Contains(current))
					{
						edge.Add(current);
					}
					continue;
				}

				if(!costSoFar.ContainsKey(next) || newCost < costSoFar[next])
				{
					costSoFar[next] = newCost;
					frontier.Enqueue(next);
					if(!reached.Contains(next))
					{
						reached.Add(next);
					}

					cameFrom[next] = current;
				}
			}
		}

		return new MovementNodes(reached, edge);
	}

	IEnumerator FollowPath() {
		if (path.Length == 0)
		{
			endOfPath?.Invoke(true);
			yield break;
		}
		Node currentNode = SquareGrid.Instance.NodeGrid.NodeFromWorldPoint(transform.position);
		Node currentWaypoint = path[0];
		while (true) {
			if (transform.position == currentWaypoint.WorldPosition) {
				UnOccupy();
				currentNode = SquareGrid.Instance.NodeGrid.NodeFromWorldPoint(transform.position);
				Occupy(currentNode);
				targetIndex ++;
				if (targetIndex >= path.Length) {
					endOfPath?.Invoke(true);
					yield break;
				}
				currentWaypoint = path[targetIndex];
			}

			transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.WorldPosition, (float)unit.UnitMovement.MovementAnimationSpeed.Value * currentNode.movementAnimationMultiplier * Time.deltaTime);
			yield return null;

		}
	}

	public void Occupy(Node n)
	{
		n.Occupy(unit);
	}

	public void UnOccupy()
	{
		int count = OccupyingNodes.Count;
		for (int i = 0; i < count; i++)
		{
			OccupyingNodes[i].UnOccupy(unit);
		}
		OccupyingNodes.Clear();
	}


	private void DrawPathGizmos()
	{
		for (int i = targetIndex; i < path.Length; i++)
		{
			Gizmos.color = Color.black;
			Gizmos.DrawCube(path[i].WorldPosition, Vector3.one);

			if (i == targetIndex)
			{
				Gizmos.DrawLine(transform.position, path[i].WorldPosition);
			}
			else
			{
				Gizmos.DrawLine(path[i - 1].WorldPosition, path[i].WorldPosition);
			}
		}
	}

	private void MovementGizmos()
	{
		
		foreach (Node n in MovementNodes)
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireCube(n.WorldPosition, Vector3.one * (SquareGrid.Instance.NodeDiameter - .6f));
		}
	}

	public void OnDrawGizmos()
	{
		if (!DrawGizmos) return;
		if (path != null)
		{
			DrawPathGizmos();
		}

		MovementGizmos();
	}
}
[System.Serializable]
public struct MovementNodes
{
	public List<Node> walkable;
	public List<Node> edgeNodes;

	public MovementNodes(List<Node> walkable, List<Node> edgeNodes)
	{
		this.walkable = walkable;
		this.edgeNodes = edgeNodes;
	}
}