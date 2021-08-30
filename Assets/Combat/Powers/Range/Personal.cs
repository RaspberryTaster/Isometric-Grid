using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Combat.Powers.Range
{
	public class Personal : IRange
	{
		private const int DEFAULTVALUE = 0;
		public int SweetSpot { get => DEFAULTVALUE; }
		public int MinimumRange { get => DEFAULTVALUE; }
		public int MaximumRange { get => DEFAULTVALUE; }

		public InRangeData CheckRange(Unit user)
		{
			List<Node> nodesInRange = new List<Node>() { SquareGrid.Instance.NodeGrid.NodeFromWorldPoint(user.transform.position) };
			List<Unit> units = new List<Unit>() { user };
			return new InRangeData(nodesInRange, units);
		}
	}
}
