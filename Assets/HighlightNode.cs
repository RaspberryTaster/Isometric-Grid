using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightNode : MonoBehaviour
{

    private void OnEnable()
	{
        PlayerInput.Instance.OnHiglight += PlayerInput_OnHiglight;
	}

	private void OnDisable()
	{
        PlayerInput.Instance.OnHiglight -= PlayerInput_OnHiglight;

    }
	private void PlayerInput_OnHiglight(RaycastHit hit)
	{
        Node n = SquareGrid.Instance.NodeGrid.NodeFromWorldPoint(hit.point);
        SelectedNodeUI.Instance.Set(n);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
