using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Combat.Powers.Range
{
	public class Melee : IRange
	{
		private const int MAXIMUMCLAMP = 99999;

		private int maximumRange;
		private int minimumRange;
		private int sweetSpot;

		public Melee(int maximumRange, int minimumRange, int sweetSpot)
		{
			this.maximumRange = maximumRange;
			this.minimumRange = minimumRange;
			this.sweetSpot = sweetSpot;
		}

		public int SweetSpot
		{
			get
			{
				return sweetSpot = Mathf.Clamp(sweetSpot, MinimumRange, MaximumRange);
			}
			set
			{
				sweetSpot = Mathf.Clamp(value, MinimumRange, MaximumRange);
			}
		}

		public int MinimumRange
		{
			get
			{
				return minimumRange = Mathf.Clamp(minimumRange, 1, MAXIMUMCLAMP);
			}
			set
			{
				minimumRange = Mathf.Clamp(value, 1, MAXIMUMCLAMP);
			}
		}
		public int MaximumRange
		{
			get
			{
				return maximumRange = Mathf.Clamp(maximumRange, MinimumRange, MAXIMUMCLAMP);
			}
			set
			{
				maximumRange = Mathf.Clamp(value, MinimumRange, MAXIMUMCLAMP);
			}
		}

		public InRangeData CheckRange(Unit user)
		{
			List<Unit> suitableUnits = new List<Unit>();

			List<Node> nodes = SquareGrid.Instance.NodeGrid.GetWithinRange(SquareGrid.Instance.NodeGrid.NodeFromWorldPoint(user.transform.position), MinimumRange, MaximumRange);

			foreach (Node n in nodes)
			{
				foreach (Unit u in n.OccupyingUnits)
				{
					if (suitableUnits.Contains(u)) continue;
					suitableUnits.Add(u);
				}
			}
			return new InRangeData(nodes, suitableUnits);
		}
	}
}
