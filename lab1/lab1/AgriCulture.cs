using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace lab1
{
    [Serializable]
    public abstract class AgriCulture : GameItem
    {
        [XmlAttribute]
        public int CALORIES = 0;
        [XmlAttribute]
        public int GROWING_TIME = 0;
    }
}
