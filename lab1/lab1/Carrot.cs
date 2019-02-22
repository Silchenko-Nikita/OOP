using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace lab1
{
    [Serializable]
    public class Carrot : AgriCulture
    {
        [XmlAttribute]
        public new int CALORIES = 5;
        [XmlAttribute]
        public new int GROWING_TIME = 32;
        [XmlAttribute]
        public new double weight = 10;
    }
}
