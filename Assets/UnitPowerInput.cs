using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPowerInput : Singleton<UnitPowerInput>
{
    public PowerHandler powerHandler;
    public bool SelectingUnits;
    public Unit target;

	private void OnEnable()
    {
        PlayerInput.Instance.OnSelectUnit += UpdateUnitComponents;
        PlayerInput.Instance.OnLeftClick += PlayerInput_OnLeftClickRaycast;
		PlayerInput.Instance.OnRightClick += Instance_OnRightClick;
    }

	public void OnDisable()
    {
        PlayerInput.Instance.OnSelectUnit -= UpdateUnitComponents;
        PlayerInput.Instance.OnLeftClick -= PlayerInput_OnLeftClickRaycast;
        PlayerInput.Instance.OnRightClick -= Instance_OnRightClick;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

	private void PlayerInput_OnLeftClickRaycast(RaycastHit hit)
	{
		if (powerHandler == null || !SelectingUnits) return;
        target = hit.transform.gameObject.GetComponent<Unit>();
        if (target == null || !powerHandler.rangeData.suitableUnits.Contains(target)) return;
        powerHandler.AddTarget(target);
        powerHandler.SelectedPower.Execute();
    }

    public void UpdateUnitComponents()
	{
        powerHandler = PlayerInput.Instance.ControlledUnit.GetComponent<PowerHandler>();
	}
    private void Instance_OnRightClick(RaycastHit hit)
    {
        if (powerHandler == null) return;
        SelectingUnits = false;
        powerHandler.Clear();
    }

}
