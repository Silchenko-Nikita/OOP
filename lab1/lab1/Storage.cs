using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    class Storage : IDisposable
    {
        private StorageItemsList<GameItem> storagedItems;
        private int maxItemsNum;
        private static Storage inst;

        static Storage()
        {
            inst = null;
        }

        private Storage()
        {
            storagedItems = new StorageItemsList<GameItem>();
            maxItemsNum = 50;
        }

        public void Apgrade()
        {
            maxItemsNum += 10;
        }

        public static Storage Instance
        {
            get
            {
                if (inst == null)
                {
                    inst = new Storage();
                }
                return inst;
            }
        }

        public void AddItem(GameItem item)
        {
            if (storagedItems.Count <= maxItemsNum)
            {
                storagedItems.Add(item);
            }
            else if (storagedItems.Count - maxItemsNum <= 10)
            {
                throw new StorageOverflowException(StorageOverflowExcArg.LIGHT_OVERFLOW, "Light Overflow");
            }
            else
            {
                throw new StorageOverflowException(StorageOverflowExcArg.LARGE_OVERFLOW, "Large Overflow");
            }
        }

        public void AddItems(List<GameItem> items)
        {
            if (storagedItems.Count + items.Count - maxItemsNum < 10)
            {
                throw new StorageOverflowException(StorageOverflowExcArg.LIGHT_OVERFLOW, "Light Overflow");
            }
            else if (storagedItems.Count + items.Count < maxItemsNum)
            {   
                for (var i = 0; i < items.Count; i++)
                    storagedItems.Add(items[i]);
            }
            else
            {
                throw new StorageOverflowException(StorageOverflowExcArg.LARGE_OVERFLOW, "Large Overflow");
            }
        }

        public void Dispose()
        {
            if (this.storagedItems != null)
            {
                this.storagedItems.Clear();
                this.storagedItems = null;
                GC.SuppressFinalize(this);
            }
        }
    }
}
