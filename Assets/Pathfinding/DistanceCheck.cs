using Assets;
using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class DistanceCheck : MonoBehaviour
{
	private Pathfinding pathfinding;
	private SquareGrid grid;
	public List<Node> withinRangeNodes = new List<Node>();
	public bool gizmoGrid;

	public delegate void AddNeighbour(Node node);
	public AddNeighbour OnAddNeighbour;
	public Node closestNode;
	private void Awake()
	{
		if (grid == null)
		{
			grid = FindObjectOfType<SquareGrid>();
		}
		if (pathfinding == null)
		{
			pathfinding = FindObjectOfType<Pathfinding>();
		}
	}
	public void SetUp(Transform unit, Node targetNode,int minimumRange, int maximumRange)
	{
		withinRangeNodes.Clear();
		withinRangeNodes = grid.NodeGrid.GetWithinRange(targetNode, minimumRange, maximumRange);
		closestNode = ClosestNode(withinRangeNodes, grid.NodeGrid.NodeFromWorldPoint(unit.position), targetNode);
	}

	public Node ClosestNode(List<Node> withinRangeNodes,Node seeker, Node target)
	{
		Node closest = null;
		int closestDistance = 0;
		for (int i = 0; i < withinRangeNodes.Count; i++)
		{
			if(closest == null)
			{
				SetClosest(withinRangeNodes[i], pathfinding.GetDistance(withinRangeNodes[i], seeker));
			}
			else
			{
				int distanceToNode = pathfinding.GetDistance(withinRangeNodes[i], seeker);
				if (closestDistance > distanceToNode)
				{
					SetClosest(withinRangeNodes[i], distanceToNode);
				}
			}
		}

		return closest;
		void SetClosest(Node node, int distance)
		{
			if(closestDistance == distance)
			{
				int distanceFromClosest = pathfinding.GetDistance(closest, target);
				int distanceFromNewest = pathfinding.GetDistance(node, target);
				if (distanceFromClosest > distanceFromNewest) return;
			}
			closest = node;
			closestDistance = distance;
		}
	}

	void OnDrawGizmos()
	{
		if (gizmoGrid == false) return;

		if (withinRangeNodes != null)
		{
			foreach (Node n in withinRangeNodes)
			{
				Gizmos.color = n == closestNode ? Color.blue : Color.red;
				Gizmos.DrawWireCube(n.WorldPosition, Vector3.one * (grid.NodeDiameter - .3f));
			}

		}
	}
}
