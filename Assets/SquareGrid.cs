using Assets;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(NodeSpawner))]
public class SquareGrid : Singleton<SquareGrid>
{
	public Vector2Int gridSize = new Vector2Int(30, 30);
	public NodeGrid NodeGrid = new NodeGrid(Vector2Int.zero);
	[SerializeField] private NodeParent NodeParent;
	[SerializeField] private NodeObject NodePrefab;
	[SerializeField] private NodeSpawner NodeSpawner;

	public float NodeRadius = 0.5f;
	[ShowNativeProperty] public float NodeDiameter => NodeRadius * 2;

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
#if UNITY_EDITOR
		if (!EditorApplication.isPlaying) return;
#endif
		CreateGrid();
	}

	[Button]
	void CreateGrid()
	{
		NodeSpawner.SetRegions();
		RoundGridSize();
		if (NodeParent == null)
		{
			NodeParent = GetComponentInChildren<NodeParent>();
		}

		if (NodeParent != null)
		{
			DestroyImmediate(NodeParent.gameObject);
		}
		NodeParent = new GameObject("Node Parent").AddComponent<NodeParent>();
		NodeParent.transform.parent = transform;
		NodeParent.transform.position = Vector3.zero;

		NodeGrid = new NodeGrid(new Vector2Int(gridSize.x, gridSize.y));
		Vector3 worldBottomLeft = transform.position - Vector3.right * gridSize.x / 2 - Vector3.forward * gridSize.y / 2;

		for (int x = 0; x < NodeGrid.NodeArray.GetLength(0); x++)
		{
			for (int y = 0; y < NodeGrid.NodeArray.GetLength(1); y++)
			{
				Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * NodeDiameter + NodeRadius) + Vector3.forward * (y * NodeDiameter + NodeRadius);
				NodeSpawner.Spawn(x, y, worldPoint, NodePrefab, NodeParent, NodeGrid);
			}
		}
	}

	public List<Node> GetNodesWithinDistance(Node center, int distance)
	{

		Queue<Node> frontier = new Queue<Node>();
		frontier.Enqueue(center);
		List<Node> reached = new List<Node>() { center };

		while (frontier.Count != 0)
		{
			Node current = frontier.Dequeue();
			List<Node> neighbours = NodeGrid.GetNeighbours(current);
			foreach (Node next in neighbours)
			{
				if (GetDistance(center, next) > distance! & reached.Contains(next)) continue;
				frontier.Enqueue(next);
				reached.Add(next);
			}
		}

		return reached;
	}

	public int GetDistance(Node nodeA, Node nodeB)
	{
		if (nodeA == null || nodeB == null) return 0;

		int value;
		int dstX = Mathf.Abs(nodeA.GridPosition.x - nodeB.GridPosition.x);
		int dstY = Mathf.Abs(nodeA.GridPosition.y - nodeB.GridPosition.y);

		if (dstX > dstY)
		{
			value = Pathfinding.Instance.DiagonalCost * dstY + Pathfinding.Instance.HorizontalCost
				* (dstX - dstY);
		}
		else
		{
			value = Pathfinding.Instance.DiagonalCost * dstX + Pathfinding.Instance.HorizontalCost * (dstY - dstX);
		}

		return value;
	}

}
