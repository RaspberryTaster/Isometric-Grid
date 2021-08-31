using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartBattle()
	{
        BattleManager.Instance.OnBattleStarted?.Invoke();
	}

    public void NextTurn()
	{
        RoundManager.Instance.OnNextTurn?.Invoke();
	}
}
