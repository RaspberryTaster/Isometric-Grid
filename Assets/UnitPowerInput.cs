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
        PlayerInput.Instance.OnClick += PlayerInput_OnHitRaycast;
    }

    public void OnDisable()
    {
        PlayerInput.Instance.OnClick -= PlayerInput_OnHitRaycast;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

	private void PlayerInput_OnHitRaycast(RaycastHit hit)
	{
		if (!SelectingUnits) return;
        target = hit.transform.gameObject.GetComponent<Unit>();
        if (target == null || !powerHandler.rangeData.suitableUnits.Contains(target)) return;
        powerHandler.AddTarget(target);
        powerHandler.SelectedPower.Execute();
    }
}
