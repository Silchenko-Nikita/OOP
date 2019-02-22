using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{

    public delegate void OnDragStartHandle(IDraggable sender, EventArgs args);
    public delegate void OnDragStopHandle(IDraggable sender, EventArgs args);

    public interface IDraggable: IDisplayable
    {
        event OnDragStartHandle OnDragStartEvent;
        event OnDragStopHandle OnDragStopEvent;

        void DragStart();
        void DragStop();
    }
}
