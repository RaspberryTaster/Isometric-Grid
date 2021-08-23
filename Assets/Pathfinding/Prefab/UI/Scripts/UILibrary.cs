using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UILibrary
{
	public static Vector3 AdjustForScreenBoundrys(Vector3 newPos, RectTransform rect, Canvas canvas, float padding = 0)
	{
		float rightEdgeToScreenEdgeDistance = Screen.width - (newPos.x + rect.rect.width * canvas.scaleFactor / 2) - padding;
		if (rightEdgeToScreenEdgeDistance < 0)
		{
			newPos.x += rightEdgeToScreenEdgeDistance;
		}
		float leftEdgeToScreenEdgeDistance = 0 - (newPos.x - rect.rect.width * canvas.scaleFactor / 2) + padding;
		if (leftEdgeToScreenEdgeDistance > 0)
		{
			newPos.x += leftEdgeToScreenEdgeDistance;
		}
		float topEdgeToScreenEdgeDistance = Screen.height - (newPos.y + rect.rect.height * canvas.scaleFactor / 2) - padding;
		if (topEdgeToScreenEdgeDistance < 0)
		{
			newPos.y += topEdgeToScreenEdgeDistance;
		}
		float bottomEdgeToScreenEdgeDistance = 0 - (newPos.y - rect.rect.height * canvas.scaleFactor / 2) + padding;
		if (bottomEdgeToScreenEdgeDistance > 0)
		{
			newPos.y += bottomEdgeToScreenEdgeDistance;
		}

		return newPos;
	}
}
