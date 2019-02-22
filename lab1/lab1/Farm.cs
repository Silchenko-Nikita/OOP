using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    class Farm : IDisposable
    {
        public static readonly int UNIT_WIDTH, UNIT_HEIGHT;
        private static Farm inst;

        public List<GameObject> contentObjs; 
        public Storage storage;

        static Farm()
        {
            inst = null;

            UNIT_WIDTH = 80;
            UNIT_HEIGHT = 80;
        }

        private Farm()
        {
            storage = Storage.Instance;
        }

        public static Farm Instance
        {
            get
            {
                if (inst == null)
                {
                    inst = new Farm();
                }
                return inst;
            }
        }

        public void AddGameObj(GameObject gameObject)
        {
            contentObjs.Add(gameObject);
        }

        public void Dispose()
        {
            if (contentObjs != null)
            {
                contentObjs.Clear();
                contentObjs = null;
            }

            if (storage != null)
            {
                storage.Dispose();
            }

            GC.SuppressFinalize(this);
        }

        ~Farm()
        {
            Console.WriteLine("Destructor");

        }

        public bool PosIsValid(int posX, int posY)
        {
            if (posX < 0 || posX >= UNIT_WIDTH || posY < 0 || posY >= UNIT_HEIGHT) return false;

            foreach(GameObject cntnt in contentObjs)
            {
                if (cntnt.PosX == posX && cntnt.PosY == posY) return false;
            }

            return true;
        }
    }
}
