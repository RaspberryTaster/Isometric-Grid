using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
	PathRequestManager requestManager;
	SquareGrid grid;
	void Awake()
	{
		requestManager = GetComponent<PathRequestManager>();
		grid = GetComponent<SquareGrid>();

	}

	public void StartFindPath(Vector3 startPos, Vector3 targetPos, int stoppingDistance)
	{
		StartCoroutine(FindPath(startPos, targetPos, stoppingDistance));
	}

	IEnumerator FindPath(Vector3 startPos, Vector3 targetPos, int stoppingDistance)
	{

		Vector3[] waypoints = new Vector3[0];
		bool pathSuccess = false;
		if(grid == null)
		{
			Debug.LogWarning("Null grid");
		}
		Node startNode = grid.NodeGrid.NodeFromWorldPoint(startPos);
		Node targetNode = grid.NodeGrid.NodeFromWorldPoint(targetPos);


		if (startNode.Walkable && targetNode.Walkable)
		{
			Heap<Node> openSet = new Heap<Node>(grid.NodeGrid.MaxSize);
			HashSet<Node> closedSet = new HashSet<Node>();
			openSet.Add(startNode);

			while (openSet.Count > 0)
			{
				Node currentNode = openSet.RemoveFirst();
				closedSet.Add(currentNode);

				if (currentNode == targetNode)
				{
					pathSuccess = true;
					break;
				}

				foreach (Node neighbour in grid.NodeGrid.GetNeighbours(currentNode))
				{
					if (!neighbour.Walkable || closedSet.Contains(neighbour))
					{
						continue;
					}

					int newMovementCostToNeighbour = currentNode.GCost + GetDistance(currentNode, neighbour) + neighbour.MovementPenalty;
					if (newMovementCostToNeighbour < neighbour.GCost || !openSet.Contains(neighbour))
					{
						neighbour.GCost = newMovementCostToNeighbour;
						neighbour.HCost = GetDistance(neighbour, targetNode);
						neighbour.parent = currentNode;

						if (!openSet.Contains(neighbour))
						{
							openSet.Add(neighbour);
						}
						else
						{
							openSet.UpdateItem(neighbour);
						}
					}
				}
			}
		}
		yield return null;
		
		if (pathSuccess)
		{
			waypoints = RetracePath(startNode, targetNode, stoppingDistance);
		}
		requestManager.FinishedProcessingPath(waypoints, pathSuccess);

	}

	Vector3[] RetracePath(Node startNode, Node endNode, int stoppingDistance)
	{
		List<Node> path = new List<Node>();
		Node currentNode = endNode;
		while (currentNode != startNode)
		{
			path.Add(currentNode);
			currentNode = currentNode.parent;
		}

		Vector3[] waypoints = PositionPath(path, stoppingDistance);
		Array.Reverse(waypoints);
		return waypoints;
	}

	Vector3[] PositionPath(List<Node> path, int stoppingDistance)
	{
		List<Vector3> waypoints = new List<Vector3>();
		for (int i = stoppingDistance; i < path.Count; i++)
		{
			waypoints.Add(path[i].WorldPosition);
		}
		return waypoints.ToArray();
	}

	[SerializeField] private int diagonalCost = 2;
	[SerializeField] private int horizontalCost = 1;
	public int GetDistance(Node nodeA, Node nodeB)
	{
		if (nodeA == null || nodeB == null) return 0;

		int value;
		int dstX = Mathf.Abs(nodeA.GridPosition.x - nodeB.GridPosition.x);
		int dstY = Mathf.Abs(nodeA.GridPosition.y - nodeB.GridPosition.y);

		if (dstX > dstY)
		{
			value = diagonalCost * dstY + horizontalCost
				* (dstX - dstY);
		}
		else
		{
			value = diagonalCost * dstX + horizontalCost * (dstY - dstX);
		}

		return value;
	}
}
