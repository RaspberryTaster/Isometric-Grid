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
	public FollowWorld followWorld;
    [SerializeField] private BarColor colorScheme;
    public float MaxValue
	{
        get
		{
			float combinedValue = CurHealth + Shield;
            return combinedValue > MaxHealth ? combinedValue : MaxHealth;
		}
	}
	private void Awake()
	{
		followWorld = GetComponent<FollowWorld>();
	}
	void OnValidate()
    {
		healthBar.LowValue = LowValue;
		shieldBar.LowValue = LowValue;
		ValueChanged();
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

	public void ValueChanged()
	{
        healthBar.Execute(MaxValue, CurHealth);
		shieldBar.Execute(MaxValue, Shield);

		UpdateColor();
    }
	public void HitPointSetUp(DepletingStat depletingStat)
	{
		MaxHealth = depletingStat.Maximum;
		CurHealth = depletingStat.CurrentValue;

		ValueChanged();
	}

	public void BarrierPointSetUp(DepletingStat depletingStat)
	{
		Shield = depletingStat.CurrentValue;

		ValueChanged();
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