using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    public class OnClickEventArgs : EventArgs
    {
        public object parcel;

        public OnClickEventArgs(object parcel)
        {
            this.parcel = parcel;
        }

        public OnClickEventArgs() : this(null) { }
    }

    public delegate void OnClickHandle(IClickable obj, OnClickEventArgs arg);

    public interface IClickable: IDisplayable
    {
        event OnClickHandle OnClickEvent;

        void Click();
    }
}
