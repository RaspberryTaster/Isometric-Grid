using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Camera cam;
    public delegate void hitRaycast(RaycastHit hit);
    public event hitRaycast OnClick;

    public delegate void Highlight(RaycastHit hit);
    public event Highlight OnHiglight;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
	{
		MouseClick();

		MouseHover();

	}

	private void MouseHover()
	{
		Ray ray = cam.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out RaycastHit hit, 900))
		{


			OnHiglight?.Invoke(hit);
		}
	}

	private void MouseClick()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out RaycastHit hit, 900))
			{

				OnClick?.Invoke(hit);
				OnHiglight?.Invoke(hit);
			}


		}
	}
}
