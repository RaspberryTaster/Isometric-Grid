using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Combat.Buffs
{
	[CreateAssetMenu]
	public class EffectData : ScriptableObject
	{
		public int stackLimit;
		public int duration;
		public bool allowMultiple;
	}
}
