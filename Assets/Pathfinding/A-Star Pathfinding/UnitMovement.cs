using UnityEngine;
using System.Collections;
using NaughtyAttributes;
using System;
using System.Collections.Generic;

[RequireComponent(typeof(Unit))]
public class UnitMovement : MonoBehaviour
{
	[SerializeField] private Pathfinding Pathfinding;
	[SerializeField] private SquareGrid grid;
	[SerializeField] private Unit combatComponent;
	
	private Node[] path;
	private int targetIndex;
	private Action<bool> endOfPath;
	private float yOffset;


	public MovementNodes movementNodes;
	public List<Node> MovementNodes
	{
		get
		{
			return movementNodes.walkable;
		}
	}
	public List<Node> WithinRangeNodes = new List<Node>();

	public bool DrawGizmos;
	private void Awake()
	{
		yOffset = transform.position.y;

		if (grid == null)
		{
			grid = FindObjectOfType<SquareGrid>();
		}
		if (Pathfinding == null)
		{
			Pathfinding = FindObjectOfType<Pathfinding>();
		}
		
		combatComponent = GetComponent<Unit>();


	}

	private void Start()
	{
		SetDistanceNodes();
	}

	public void Move(Node targetPosition, int stoppingDistance, Action<bool> callback)
	{
		endOfPath = callback;
		PathRequestManager.RequestPath(grid.NodeGrid.NodeFromWorldPoint(transform.position), targetPosition, stoppingDistance, OnPathFound);
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
		foreach (Node n in WithinRangeNodes)
		{
			n.SetColor(n.DefaultNodeIndex);
		}
		foreach (Node n in MovementNodes)
		{
			n.SetColor(n.DefaultNodeIndex);
		}

		if(grid.NodeGrid == null)
		{
			Debug.LogWarning("Node grid is null");
		}
		movementNodes = DijkstraFrontier(grid.NodeGrid.NodeFromWorldPoint(transform.position));
		WithinRangeNodes = PredictedRangeNodes(movementNodes.edgeNodes);

		foreach (Node n in WithinRangeNodes)
		{
			n.SetColor((int)TIleMode.ATTACKRANGE);
		}
		foreach (Node n in MovementNodes)
		{
			n.SetColor((int)TIleMode.MOVEMENT);
		}
	}

	public List<Node> PredictedRangeNodes(List<Node> targetNodes)
	{
		List<Node> value = new List<Node>();
		foreach (Node n in targetNodes)
		{
			List<Node> rangeNodes = grid.NodeGrid.GetWithinRange(n, combatComponent.attackRange.x, combatComponent.attackRange.y);
			for (int i = 0; i < rangeNodes.Count; i++)
			{
				if (value.Contains(rangeNodes[i])) continue;
				value.Add(rangeNodes[i]);
			}
		}

		return value;
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
			List<Node> neighbours = grid.NodeGrid.GetNeighbours(current);
			
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
				int newCost = costSoFar[current] + grid.GetDistance(current, next) + next.MovementPenalty;
				if (newCost > combatComponent.MovementPoints.CurrentValue)
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
		Node currentNode = grid.NodeGrid.NodeFromWorldPoint(transform.position);
		Node currentWaypoint = path[0];
		while (true) {
			if (transform.position == currentWaypoint.WorldPosition) {
				currentNode = grid.NodeGrid.NodeFromWorldPoint(transform.position);
				targetIndex ++;
				if (targetIndex >= path.Length) {
					endOfPath?.Invoke(true);
					yield break;
				}
				currentWaypoint = path[targetIndex];
			}

			transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.WorldPosition, combatComponent.MovementAnimationSpeed * currentNode.movementAnimationMultiplier * Time.deltaTime);
			yield return null;

		}
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
			Gizmos.DrawWireCube(n.WorldPosition, Vector3.one * (grid.NodeDiameter - .6f));
		}
		
		foreach (Node n in WithinRangeNodes)
		{
			Gizmos.color = Color.magenta;
			Gizmos.DrawWireCube(n.WorldPosition, Vector3.one * (grid.NodeDiameter - .4f));
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