using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollow : MonoBehaviour
{
    private RectTransform rect;
    public enum TargetType
	{ 
        TRANSFORM, POINTER
    }
    public TargetType targetType;
    public Transform objectToFollow;
    public Canvas targetCanvas;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

	private void Update()
	{
        FollowCursor();
    }

	public bool StayWithinScreen;
	public float padding;
	private void FollowCursor()
	{
		Vector3 newPos = Vector3.zero;

		if (targetType == TargetType.TRANSFORM)
		{
			newPos = Camera.main.WorldToViewportPoint(objectToFollow.position);
		}
		else if (targetType == TargetType.POINTER)
		{
			newPos = Input.mousePosition;
		}

		newPos.z = 0f;

		if(StayWithinScreen)
		{
			newPos = UILibrary.AdjustForScreenBoundrys(newPos, rect, targetCanvas, padding);
		}

		rect.transform.position = newPos;
	}
}
