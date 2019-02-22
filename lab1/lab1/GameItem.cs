using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace lab1
{   
    [Serializable]
    public abstract class GameItem: IComparable
    {
        [XmlAttribute]
        public double weight;

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            GameItem gi = obj as GameItem;
            if (gi != null)
            {
                return Convert.ToInt32(this.weight > gi.weight);
            }
            return 0;
        }
    }
}
