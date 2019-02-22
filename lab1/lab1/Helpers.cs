using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    public static class Helpers
    {
        public static int CountAgriCultures(this List<GameObject> list)
        {
            int count = 0;
            foreach (var go in list)
            {
                if (go.GetType().Name == "lab1.AgriCulture")
                {
                    count++;
                }
            }
            return count;
        }
    }
}
