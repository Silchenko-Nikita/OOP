using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    public enum Condition
    {
        FAVORABLE,
        UNFAVORABLE
    }

    public class Field<T> : GameObject where T: AgriCulture
    {
        Condition condition;
        Func<List<T>> getHarvest;

        public delegate void SetCondition(Condition condition);
        public event SetCondition NewCondition;

        private List<T> growingCultures;

        public Field(Texture texture, int metersWidth, int metersHeight, int metersAltitude) : base(texture, metersWidth, metersHeight, metersAltitude)
        {
            growingCultures = new List<T>();
            NewCondition += (condition) =>
            {
                this.condition = condition;
                if (condition == Condition.FAVORABLE)
                {
                    getHarvest = getFavorableHarvest;
                }
                else if (condition == Condition.UNFAVORABLE)
                {
                    getHarvest = getUnfavorableHarvest;
                }
            };
        }

        private List<T> getFavorableHarvest()
        {
            List<T> harvest = (List<T>) growingCultures.Take(growingCultures.Count / 2);
            growingCultures.Clear();
            return harvest;
        }

        private List<T> getUnfavorableHarvest()
        {
            List<T> harvest = (List<T>)growingCultures.Take(growingCultures.Count);
            growingCultures.Clear();
            return harvest;
        }
    }
}
