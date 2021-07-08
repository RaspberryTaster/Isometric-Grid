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


    public void Set(Node n)
	{
        nodeName.text = n.Name;
        gridPosition.text = n.GridPosition.ToString();
        worldPosition.text = n.WorldPosition.ToString();
        MovementPenalty.text = n.MovementPenalty.ToString();
	}
}
