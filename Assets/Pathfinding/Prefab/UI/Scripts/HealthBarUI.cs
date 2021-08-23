using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class HealthBarUI : MonoBehaviour
{
    public UIState State;
    public float MaxHealth;
    public float CurHealth;
    public float Shield;
    [Range(0,1)]
    public float LowValue;
    public BarUI healthBar;
    public BarUI shieldBar;
	public Image background;
    [SerializeField] private BarColor colorScheme;
    public float MaxValue
	{
        get
		{
			float combinedValue = CurHealth + Shield;
            return combinedValue > MaxHealth ? combinedValue : MaxHealth;
		}
	}

    void OnValidate()
    {
		healthBar.LowValue = LowValue;
		shieldBar.LowValue = LowValue;
        healthBar.Execute(MaxValue, CurHealth);
        shieldBar.Execute(MaxValue, Shield);
		UpdateColor();
    }

	public void SetColor(BarColor barColor)
	{
		colorScheme = barColor;
		UpdateColor();
	}

	private void UpdateColor()
	{
		if (healthBar == null || healthBar.barImage == null || background == null) return;

		background.color = colorScheme.BackgroundPrimary;

		if (healthBar.Low)
		{
			healthBar.barImage.color = colorScheme.HealthSecondary;
		}
		else
		{
			healthBar.barImage.color = colorScheme.HealthPrimary;
		}

		if (shieldBar.Low)
		{
			shieldBar.barImage.color = colorScheme.ShieldSecondary;
		}
		else
		{
			shieldBar.barImage.color = colorScheme.ShieldPrimary;
		}
	}

	public void Execute(float shieldValue, float healthValue)
	{
        CurHealth = healthValue;
        Shield = shieldValue;
        healthBar.Execute(MaxValue, CurHealth);
        shieldBar.Execute(MaxValue, Shield);
    }
}
public enum UIState
{
	friendly, enemy
}
[System.Serializable]
public struct BarColor
{
	public Color HealthPrimary;
	public Color HealthSecondary;
	public Color ShieldPrimary;
	public Color ShieldSecondary;
	public Color BackgroundPrimary;
	public Color BackgroundSecondary;
}