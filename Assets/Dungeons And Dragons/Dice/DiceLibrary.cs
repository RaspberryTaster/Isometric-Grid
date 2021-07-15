using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DiceLibrary
{

	public static int Die(int maximum,int minimum = 1, int diceCount = 1)
	{
		int value = 0;
		for(int i = 0; i < diceCount; i++)
		{
			value += Random.Range(maximum, minimum + 1);
		}
		return value;
	}

	public static bool Hit(int modifiers, int targetRoll)
	{
		return Die(20) + modifiers >= targetRoll;
	}
}
