using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPower
{
	public void SelectPower();
	public void Execute();
	public bool CanExecute();
	public void Update();
	public void Effect(Unit target);
}
