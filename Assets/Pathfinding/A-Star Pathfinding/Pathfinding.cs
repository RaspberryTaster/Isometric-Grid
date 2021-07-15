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

	public void StartFindPath(Node startPos, Node targetPos, int stoppingDistance)
	{
		StartCoroutine(FindPath(startPos, targetPos, stoppingDistance));
	}

	IEnumerator FindPath(Node startPos, Node targetPos, int stoppingDistance)
	{

		Node[] waypoints = new Node[0];
		bool pathSuccess = false;
		if(grid == null)
		{
			Debug.LogWarning("Null grid");
		}
		Node startNode = startPos;
		Node targetNode = targetPos;


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

					int newMovementCostToNeighbour = currentNode.GCost + grid.GetDistance(currentNode, neighbour) + neighbour.MovementPenalty;
					if (newMovementCostToNeighbour < neighbour.GCost || !openSet.Contains(neighbour))
					{
						neighbour.GCost = newMovementCostToNeighbour;
						neighbour.HCost = grid.GetDistance(neighbour, targetNode);
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

	Node[] RetracePath(Node startNode, Node endNode, int stoppingDistance)
	{
		List<Node> path = new List<Node>();
		Node currentNode = endNode;
		while (currentNode != startNode)
		{
			path.Add(currentNode);
			currentNode = currentNode.parent;
		}

		Node[] waypoints = PositionPath(path, stoppingDistance);
		Array.Reverse(waypoints);
		return waypoints;
	}

	Node[] PositionPath(List<Node> path, int stoppingDistance)
	{
		List<Node> waypoints = new List<Node>();
		for (int i = stoppingDistance; i < path.Count; i++)
		{
			waypoints.Add(path[i]);
		}
		return waypoints.ToArray();
	}

	[SerializeField] private int diagonalCost = 2;
	[SerializeField] private int horizontalCost = 1;
	public int DiagonalCost
	{
		get
		{
			return diagonalCost;
		}
	}
	public int HorizontalCost
	{
		get
		{
			return horizontalCost;
		}
	}
}
