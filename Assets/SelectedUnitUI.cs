using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectedUnitUI : Singleton<SelectedNodeUI>
{
    public TextMeshProUGUI Name;
    public TextMeshProUGUI GridPosition;
    public TextMeshProUGUI WorldPosition;

	public void OnEnable()
	{
		PlayerInput.Instance.OnHiglight += Instance_OnHiglight;
	}

	public void OnDisable()
	{
		PlayerInput.Instance.OnHiglight -= Instance_OnHiglight;
	}
	private void Instance_OnHiglight(RaycastHit hit)
	{
		Unit unit = hit.transform.gameObject.GetComponent<Unit>();
		if (unit == null) return;
		Set(unit);
	}


	public void Set(Unit unit)
	{
        Name.text = unit.gameObject.name;
        GridPosition.text = unit.OccupyingNodes[0].GridPosition.ToString();
        WorldPosition.text = unit.transform.position.ToString();
	}
}
