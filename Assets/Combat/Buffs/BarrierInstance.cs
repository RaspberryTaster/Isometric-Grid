using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Combat.Buffs
{
    [System.Serializable]
    public class BarrierInstance
    {
        public int Order;
        public int CurValue;
        public int InitialValue;
        public Unit unit;
        public object source;
        public BarrierInstance(Unit unit, int value, object source)
        {
            this.unit = unit;
            this.source = source;
            Set(value);
        }

        public void Set(int value)
        {
            InitialValue = value;
            CurValue = InitialValue;
            unit.BarrierPoints.IncreaseCurrent(CurValue);
        }

        public int Damage(int value)
        {
            int InitialValue = CurValue;
            CurValue -= value;
            if (CurValue <= 0)
            {
                unit.BarrierPoints.ReduceCurrent(InitialValue);
                Remove();
                return CurValue * -1;
            }
            else
            {
                unit.BarrierPoints.ReduceCurrent(CurValue);
                return 0;
            }
        }

        public void Remove()
        {
            if(CurValue > 0)
			{
                unit.BarrierPoints.ReduceCurrent(CurValue);
			}
            unit.BarrierInstances.Remove(this);
        }
    }
}
