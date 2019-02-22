using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    public enum ItemType
    {
        HEAVIEST,
        LIGHTEST
    }

    public class StorageItemsList<T>: List<T>, IEnumerable<T> where T: GameItem
    {
        public T this[ItemType type]
        {
            get
            {
                if (type == ItemType.LIGHTEST)
                {
                    return lightestItem;
                }
                else if (type == ItemType.HEAVIEST)
                {
                    return heaviestItem;
                }
                return null;
            }
        }

        public T heaviestItem
        {
            get {
                double maxWeight = 0;
                T item = null;
                foreach (T i in this)
                {
                    if (item.weight > maxWeight)
                    {
                        maxWeight = i.weight;
                        item = i;
                    }
                }
                return item;
            }
            private set { }
        }

        public T lightestItem
        {
            get
            {
                double minWeight = Double.PositiveInfinity;
                T item = null;
                foreach (T i in this)
                {
                    if (item.weight < minWeight)
                    {
                        minWeight = i.weight;
                        item = i;
                    }
                }
                return item;
            }
            private set { }
        }

        new IEnumerator<T> GetEnumerator()
        {
            return (IEnumerator<T>) new StorageItemsListEnumerator<T>(this);
        }
    }


    public class StorageItemsListEnumerator<T> : IEnumerator<T> where T : GameItem
    {
        StorageItemsList<T> itemsList;
        int pos;

        public T Current()
        {
            if (pos >= 0 && pos < itemsList.Count)
            {
                return itemsList[pos];
            }
            return null;
        }

        object IEnumerator.Current => Current();

        T IEnumerator<T>.Current => Current();

        public StorageItemsListEnumerator(StorageItemsList<T> itemsList)
        {
            this.itemsList = new StorageItemsList<T>();

            foreach (T item in itemsList)
            {
                this.itemsList.Add(item);
            }
            this.itemsList.Sort();
        }

        public void Dispose()
        {
            itemsList = null;
        }

        public bool MoveNext()
        {
            pos++;
            return (pos < itemsList.Count);
        }

        public void Reset()
        {
            pos = -1;
        }
    }
}
