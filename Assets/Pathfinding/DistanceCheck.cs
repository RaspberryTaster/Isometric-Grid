using Assets;
using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class DistanceCheck : MonoBehaviour
{
	private SquareGrid grid;
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
	}
	public void SetUp(Transform unitTransform, Node targetNode)
	{
		//closestNode = ClosestNode(withinRangeNodes, grid.NodeGrid.NodeFromWorldPoint(unitTransform.position), targetNode);
	}

	public Node ClosestNode(List<Node> withinRangeNodes,Node seeker, Node target)
	{
		Node closest = null;
		int closestDistance = 0;
		for (int i = 0; i < withinRangeNodes.Count; i++)
		{
			if(closest == null)
			{
				SetClosest(withinRangeNodes[i], grid.GetDistance(withinRangeNodes[i], seeker, Pathfinding.Instance.DiagonalCost, Pathfinding.Instance.HorizontalCost));
			}
			else
			{
				int distanceToNode = grid.GetDistance(withinRangeNodes[i], seeker, Pathfinding.Instance.DiagonalCost, Pathfinding.Instance.HorizontalCost);
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
				int distanceFromClosest = grid.GetDistance(closest, target, Pathfinding.Instance.DiagonalCost, Pathfinding.Instance.HorizontalCost);
				int distanceFromNewest = grid.GetDistance(node, target, Pathfinding.Instance.DiagonalCost, Pathfinding.Instance.HorizontalCost);
				if (distanceFromClosest > distanceFromNewest) return;
			}
			closest = node;
			closestDistance = distance;
		}
	}

	void OnDrawGizmos()
	{
		if (gizmoGrid == false) return;

	}
}
