using Assets;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(NodeSpawner))]
public class SquareGrid : MonoBehaviour
{
	public Vector2Int gridSize = new Vector2Int(30, 30);
	public float NodeRadius = 0.5f;

	public float NodeDiameter => NodeRadius * 2;
	public NodeGrid NodeGrid;

	[SerializeField] private GameObject GridNodes;
	[SerializeField] private NodeObject NodePrefab;
	[SerializeField] private NodeSpawner NodeSpawner;
	public bool DrawGizmos;




	private void OnValidate()
	{
		RoundGridSize();
	}

	private void RoundGridSize()
	{
		gridSize.x = Mathf.RoundToInt(gridSize.x / NodeDiameter);
		gridSize.y = Mathf.RoundToInt(gridSize.y / NodeDiameter);
	}

	void Awake()
	{
		NodeSpawner = GetComponent<NodeSpawner>();

		CreateGrid();
	}


	private void Start()
	{

	}

	[Button]
	void CreateGrid()
	{
		NodeSpawner.SetRegions();
		RoundGridSize();

		if (GridNodes != null)
		{
			DestroyImmediate(GridNodes);
		}

		GridNodes = new GameObject("Grid Nodes");
		GridNodes.transform.parent = transform;
		GridNodes.transform.position = Vector3.zero;

		NodeGrid = new NodeGrid(new Vector2Int(gridSize.x, gridSize.y));
		Vector3 worldBottomLeft = transform.position - Vector3.right * gridSize.x/ 2 - Vector3.forward * gridSize.y / 2;

		for (int x = 0; x < NodeGrid.NodeArray.GetLength(0); x++)
		{
			for (int y = 0; y < NodeGrid.NodeArray.GetLength(1); y++)
			{
				Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * NodeDiameter + NodeRadius) + Vector3.forward * (y * NodeDiameter + NodeRadius);
				NodeSpawner.Spawn(x, y, worldPoint, NodePrefab, GridNodes, NodeGrid);
			}
		}
	}

	public List<Node> GetNodesWithinDistance(Node center, int distance)
	{

		Queue<Node> frontier = new Queue<Node>();
		frontier.Enqueue(center);
		List<Node> reached = new List<Node>() { center };

		while(frontier.Count != 0)
		{
			Node current = frontier.Dequeue();
			List<Node> neighbours = NodeGrid.GetNeighbours(current);
			foreach (Node next in neighbours)
			{
				if (GetDistance(center, next) > distance) continue;

			}
		}
	}

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

	private float gizmoBoundry = .1f;
	private float gizmoNodeHeight = 1;

	Vector3 GizmoNodeSize
	{
		get
		{
			Vector3 gizmoSize = Vector3.one * (NodeDiameter - gizmoBoundry);
			gizmoSize.y = gizmoNodeHeight;
			return gizmoSize;
		}
	}
	void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(transform.position, new Vector3(gridSize.x, gizmoNodeHeight,gridSize.y));

		if (!DrawGizmos) return;
		if (NodeGrid != null)
		{
			foreach (Node n in NodeGrid.NodeArray)
			{
				Gizmos.DrawWireCube(n.WorldPosition, GizmoNodeSize);
			}
		}
	}
}
