using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class BarUI : MonoBehaviour
{
	public float MaximumBarUI;
	public float MaxValue;
	public float CurValue;
	[Range(0, 1)]
	public float LowValue;
	public bool Low;
	public bool ShouldReferToOtherBar;
	public BarUI ReferalBar;
	public Image barImage;
	private void OnValidate()
	{
		barImage = GetComponent<Image>();
	}

	public float GUINormalized
	{
		get
		{
			float value = CurValue == 0 || MaximumBarUI == 0 ? 0 : CurValue / MaximumBarUI;
			return Mathf.Clamp01(value);

		}
	}

	public float ValueNormalized
	{
		get
		{
			float value = CurValue == 0 || MaximumBarUI == 0 ? 0 : CurValue / MaxValue;
			return Mathf.Clamp01(value);

		}
	}
	public float FillValue
	{ 
		get
		{	
			if(ShouldReferToOtherBar)
			{
				return Mathf.Clamp01(GUINormalized + ReferalBar.GUINormalized);
			}
			else
			{
				return GUINormalized;
			}
		}
	}

	public void Execute(float MaxValue, float CurValue)
	{
		this.MaximumBarUI = MaxValue;
		this.CurValue = CurValue;
		
		if (ValueNormalized <= LowValue)
		{
			Low = true;
		}
		else
		{
			Low = false;
		}

		if (barImage == null) return;
		barImage.fillAmount = FillValue;

	}
}
