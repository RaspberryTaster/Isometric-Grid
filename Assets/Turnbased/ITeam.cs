using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TeamFealty
{
	public ITeam currentTeam;
	
	public void ChangeState(ITeam team)
	{
		currentTeam?.Exit();
		currentTeam = team;
		currentTeam?.Enter();
	}
}
public interface ITeam 
{
	public void Enter();
	public void Exit();
}
