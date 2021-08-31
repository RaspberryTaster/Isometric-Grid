using Assets.Combat.Powers.Power_Type;
using Assets.Combat.Powers.Range;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Combat.Buffs
{
	public class FalseLife : IUtilityPower
	{
		public Unit unit;
		IRange range = new Personal();
		IActionType actionType = new ActionCost(0,1);
		Dictionary<Unit, BarrierInstance> barrierInstances = new Dictionary<Unit, BarrierInstance>();

		public FalseLife(Unit unit)
		{
			this.unit = unit;
		}

		public void Execute()
		{
			if (!CanExecute()) return;
			actionType.ActionTaken(unit);
			foreach (Unit target in unit.powerHandler.TargetUnits)
			{
				Effect(target);
			}
			unit.powerHandler.Clear();
		}

		public void Update()
		{

		}

		public void SelectPower()
		{
			if (actionType.MetRequirement(unit))
			{
				unit.powerHandler.SelectPower(this, range.CheckRange(unit));
			}
		}

		public bool CanExecute()
		{
			return true;
		}

		public void Effect(Unit target)
		{
			int value = (unit.Level / 2) + unit.WisdomMod;
			BarrierInstance barrierInstance = new BarrierInstance(unit, value, this);
			/*
			List<BarrierInstance> barrierInstances = target.InstancesWithSameSource(barrierInstance.source);
			if (barrierInstances.Count > 0)
			{
				BarrierInstance lowest = barrierInstances[0];
				foreach(BarrierInstance barrier in  barrierInstances)
				{
					if(barrier.CurValue < lowest.CurValue)
					{
						lowest = barrier;
					}
				}

				if(lowest.CurValue <= barrierInstance.CurValue)
				{
					lowest.Remove();
					this.barrierInstances.Add(target, barrierInstance);
					target.BarrierInstances.Add(barrierInstance);
				}
			}
			else
			{

			}
			*/
			barrierInstances.Add(target, barrierInstance);
			target.BarrierInstances.Add(barrierInstance);



		}
	}
}
