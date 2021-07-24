using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectedNodeUI : MonoBehaviour
{
    public TextMeshProUGUI nodeName;
    public TextMeshProUGUI gridPosition;
    public TextMeshProUGUI worldPosition;
    public TextMeshProUGUI MovementPenalty;
    public TextMeshProUGUI AnimationSpeed;

    public void Set(Node n)
	{
        nodeName.text = "Node name: " + n.Name;
        gridPosition.text = "Grid position: " + n.GridPosition.ToString();
        worldPosition.text = "World position: " + n.WorldPosition.ToString();
        MovementPenalty.text = "Movement penalty: " + n.MovementPenalty.ToString();
        AnimationSpeed.text = "Animation multiplier: " + n.movementAnimationMultiplier.ToString();
	}
}
