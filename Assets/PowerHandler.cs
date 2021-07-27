using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerHandler : MonoBehaviour
{
    public Unit unit;
	public UnitMovement unitMovement;
    public List<IPower> powers = new List<IPower>();
	public List<Unit> TargetUnits = new List<Unit>();
    public IPower SelectedPower;
    public InRangeData rangeData;
	private void Awake()
	{
        unit = GetComponent<Unit>();
		unitMovement = GetComponent<UnitMovement>();
	}
	// Start is called before the first frame update
	void Start()
    {
        powers.Add(new BasicAttack(unit));
    }

	[Button]
	public void Test()
	{
		IPower power = new BasicAttack(unit);
		power.Update();
        power.SelectPower();
	}
    
    public void SelectPower(IPower power, InRangeData inRangeData)
	{
		foreach (Node n in rangeData.nodesInRange)
		{
			n.RemoveColor((int)TIleMode.ATTACKRANGE);
		}

		unitMovement.SetDistanceNodes();
		TargetUnits.Clear();
		unit.currentState = ControlState.ATTACK;
		SelectedPower = power;
        rangeData = inRangeData;

		foreach (Node n in rangeData.nodesInRange)
		{
			n.SetColor((int)TIleMode.ATTACKRANGE);
		}
	}

	public void AddTarget(Unit unit)
	{
		Debug.Log($"Add {unit}");
		TargetUnits.Add(unit);
		Debug.Log(TargetUnits.Count);
	}

	private float gizmoBoundry = .1f;
	private float gizmoNodeHeight = 1;
	public bool DrawGizmos;

	[ShowNativeProperty] Vector3 GizmoNodeSize
	{
		get
		{
			Vector3 gizmoSize = Vector3.one * (SquareGrid.Instance.NodeDiameter - gizmoBoundry);
			gizmoSize.y = gizmoNodeHeight;
			return gizmoSize;
		}
	}
	void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		if (!DrawGizmos) return;
		if (rangeData.nodesInRange == null) return;
		if (SquareGrid.Instance.NodeGrid != null)
		{
			foreach (Node n in rangeData.nodesInRange)
			{
				Gizmos.DrawWireCube(n.WorldPosition, GizmoNodeSize);
			}
		}

	}

	public void Clear()
	{
		unit.currentState = ControlState.MOVEMENT;
		ClearNodes();
		rangeData.suitableUnits.Clear();
		TargetUnits.Clear();
	}

	public void ClearNodes()
	{
		int count = rangeData.nodesInRange.Count;
		for (int i = 0; i < count; i++)
		{
			rangeData.nodesInRange[0].RemoveColor((int)TIleMode.ATTACKRANGE);
			rangeData.nodesInRange.RemoveAt(0);
		}
	}
}
