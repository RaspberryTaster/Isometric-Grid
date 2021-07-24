using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Combat.Powers.Range
{
	public class NullRange : IRange
	{
		public InRangeData inRangeData;


		private const int DEFAULTVALUE = 0;

		public NullRange()
		{
			inRangeData = new InRangeData(new List<Node>(), new List<Unit>());
		}

		public int SweetSpot { get => DEFAULTVALUE;}
		public int MinimumRange { get => DEFAULTVALUE;}
		public int MaximumRange { get => DEFAULTVALUE;}

		public InRangeData CheckRange(Unit user)
		{
			return inRangeData;
		}
	}
}
