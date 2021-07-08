using UnityEngine;
using System.Collections;

public enum TIleMode
{
	DEFAULT = 0, UNREACHABLE = 1, ATTACKRANGE = 2, MOVEMENT = 3
}

[System.Serializable]
public class Node : IHeapItem<Node>
{
	public string Name = "Node";
	public int DefaultNodeIndex;

	public bool Selected;
	public bool Walkable;
	public Vector3 WorldPosition;
	public Vector2Int GridPosition;

	public int GCost;
	public int HCost;
	public int MovementPenalty;
	public Node parent;
	public NodeObject nodeObject;
	int heapIndex;
	public Node(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY,int  DefaultNodeIndex, string name, int penalty, NodeObject nodeObject = null)
	{
		Walkable = _walkable;
		WorldPosition = _worldPos;
		GridPosition = new Vector2Int(_gridX, _gridY);
		this.nodeObject = nodeObject;
		this.DefaultNodeIndex = DefaultNodeIndex;
		this.nodeObject.transform.position = WorldPosition;
		SetColor(DefaultNodeIndex);
		Name = name;
		MovementPenalty = penalty;
	}

	public int fCost
	{
		get
		{
			return GCost + HCost;
		}
	}

	public int HeapIndex
	{
		get
		{
			return heapIndex;
		}
		set
		{
			heapIndex = value;
		}
	}

	public int CompareTo(Node nodeToCompare)
	{
		int compare = fCost.CompareTo(nodeToCompare.fCost);
		if (compare == 0)
		{
			compare = HCost.CompareTo(nodeToCompare.HCost);
		}
		return -compare;
	}

	public string Designation()
	{
		return $"Grid position: ({GridPosition.x}, {GridPosition.y}), World position: {WorldPosition}";
	}

	public void SetColor(int index)
	{
		if (nodeObject == null) return;
		nodeObject.ApplyColor(index);
	}
}