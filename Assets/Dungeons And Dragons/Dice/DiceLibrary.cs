using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DiceLibrary
{
	public static Die TwentyDie = new Die(1, 1, 20);
	public static int RollDie(Die die)
	{
		int value = 0;
		for(int i = 0; i < die.Count; i++)
		{
			value += Random.Range(die.Maximum, die.Minimum + 1);
		}
		return value;
	}

	public static bool AttackRollSucessful(int modifiers, int targetNumber)
	{
		return RollDie(TwentyDie) + modifiers >= targetNumber;
	}
}
[System.Serializable]
public class Die
{
	public int Count;
	public int Minimum;
	public int Maximum;

	public Die(int count, int minimum, int maximum)
	{
		Count = count;
		Minimum = minimum;
		Maximum = maximum;
	}

	public override string ToString()
	{
		return $"{Minimum * Count} - {Maximum * Count}";
	}
}
