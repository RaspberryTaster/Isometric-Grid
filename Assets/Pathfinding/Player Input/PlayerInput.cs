using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInput : Singleton<PlayerInput>
{
	public Unit ControlledUnit;
    private Camera cam;
    public delegate void hitRaycast(RaycastHit hit);
    public event hitRaycast OnLeftClick;
	public event hitRaycast OnRightClick;
	public delegate void Highlight(RaycastHit hit);
    public event Highlight OnHiglight;
	public delegate void SelectUnit();
	public SelectUnit OnSelectUnit;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
	{
		if (EventSystem.current.IsPointerOverGameObject()) return;

		Ray ray = cam.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out RaycastHit hit))
		{
			OnHiglight?.Invoke(hit);

			if (Input.GetMouseButtonDown(0))
			{
				Unit unit = hit.transform.GetComponent<Unit>();
				if (unit != null && unit != ControlledUnit && !UnitPowerInput.Instance.SelectingUnits)
				{
					SetControlledUnit(unit);
				}
				else
				{
					OnLeftClick?.Invoke(hit);
					OnHiglight?.Invoke(hit);
				}

			}

			if (Input.GetMouseButtonDown(1))
			{
				OnRightClick?.Invoke(hit);
			}
		}


	}

	private void SetControlledUnit(Unit unit)
	{
		if(ControlledUnit != null)
		{
			ControlledUnit.powerHandler.Clear();
		}
		ControlledUnit = unit;
		OnSelectUnit();
	}
}
