using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Team
{
    FRIENDLY, NEUTRAL, ENEMY
}
[RequireComponent(typeof(Unit))]
public class AssignToTeam : MonoBehaviour
{
    Unit unit;
    public Team team;
    public TeamFealty TeamFealty = new TeamFealty();

	private void Awake()
	{
        unit = GetComponent<Unit>();

        switch (team)
        {
            case Team.FRIENDLY:
                TeamFealty.ChangeState(new AllyTeam(unit));
                break;
            case Team.NEUTRAL:
                TeamFealty.ChangeState(new NeutralTeam(unit));
                break;
            case Team.ENEMY:
                TeamFealty.ChangeState(new EnemyTeam(unit));
                break;
        }
    }
	// Start is called before the first frame update
	void Start()
    {

    }
}
