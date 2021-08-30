using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System;

[Serializable]
public class DepletingStat
{
    [SerializeField] private int current = 1;
    public int CurrentValue
    {
        get => current > Maximum ? Maximum : current;
        set
        {
            if (value > Maximum)
            {
                value = Maximum;
            }

            current = value;
            Debug.Log(current);
            OnValueChange?.Invoke(this);
        }
    }

    public int Maximum = 1;

    public delegate void ValueChange(DepletingStat depletingStat);
    public ValueChange OnValueChange;

    public DepletingStat(int maximum, int current)
    {
        SetCurrent(current);
        SetMaximum(maximum);
    }
    public DepletingStat(int maximum = 1)
    {
        SetMaximum(maximum);
        SetCurrentToMaximum();
    }

    public void SetCurrentToMaximum()
    {
        SetCurrent(Maximum);
    }

    public void ReduceCurrent(int value, int minimum_Clamp)
    {
        CurrentValue -= value;
        Clamp(minimum_Clamp);
    }

    public void IncreaseCurrent(int value, int minimum_Clamp)
    {
        CurrentValue += value;
        Clamp(minimum_Clamp);
    }

    public void SetMaximum(int number)
    {
        Maximum = number;
    }

    public void ReduceCurrent(int value)
    {
        CurrentValue -= value;
    }

    public void IncreaseCurrent(int value)
    {
        CurrentValue += value;
    }

    public void SetCurrent(int number)
    {
        CurrentValue = number;
    }

    private void Clamp(int minimum)
    {
        CurrentValue = Mathf.Clamp(CurrentValue, minimum, Maximum);
    }
}
