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

	public bool DrawGizmos;

	private NodeSpawner NodeSpawner;


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
		NodeSpawner.SetRegions();
		RoundGridSize();

		CreateGrid();
	}


	private void Start()
	{

	}

	[Button]
	void CreateGrid()
	{
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
