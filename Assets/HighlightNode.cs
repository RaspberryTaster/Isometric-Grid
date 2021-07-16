using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightNode : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private SelectedNodeUI selectedNodeUI;

    private void OnEnable()
	{
		playerInput.OnHiglight += PlayerInput_OnHiglight;
	}

	private void OnDisable()
	{
        playerInput.OnHiglight -= PlayerInput_OnHiglight;

    }
	private void PlayerInput_OnHiglight(RaycastHit hit)
	{
        Node n = SquareGrid.Instance.NodeGrid.NodeFromWorldPoint(hit.point);
        selectedNodeUI.Set(n);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
