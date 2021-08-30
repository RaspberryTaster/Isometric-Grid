using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class HealthBarManager : Singleton<HealthBarManager>
{
    public RectTransform HealthBarParent;
    public HealthBarUI prefab;
    public Dictionary<Unit, HealthBarUI> HealthBars = new Dictionary<Unit, HealthBarUI>();
	private void Awake()
	{
        if (HealthBarParent != null) return;
        HealthBarParent = new GameObject(typeof(HealthBarUI).Name + " Parent", typeof(RectTransform)).GetComponent<RectTransform>();
        HealthBarParent.transform.SetParent(transform);
    }
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Subscribe(Unit unit)
	{
        if (HealthBars.ContainsKey(unit)) return;
        HealthBarUI healthBarUI = Instantiate(prefab);
        if(HealthBarParent != null)
		{
            healthBarUI.transform.parent = HealthBarParent;
        }
        unit.HitPoints.OnValueChange += healthBarUI.HitPointSetUp;
        unit.BarrierPoints.OnValueChange += healthBarUI.BarrierPointSetUp;
        healthBarUI.HitPointSetUp(unit.HitPoints);
        healthBarUI.BarrierPointSetUp(unit.BarrierPoints);
        healthBarUI.followWorld.lookAt = unit.transform;
        HealthBars.Add(unit, healthBarUI);
	}
}
