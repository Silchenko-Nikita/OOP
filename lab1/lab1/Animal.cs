using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    public enum AnimalBehaivorType
    {
        SOUND,
        ACTION
    }

    public abstract class Animal : GameObject, IDraggable
    {
        public Action<AnimalBehaivorType> behave;

        public Animal(Texture texture, int metersWidth, int metersHeight, int metersAltitude) : base(texture, metersWidth, metersHeight, metersAltitude)
        {
            OnDragStartEvent = delegate (IDraggable sender, EventArgs args)
            {
                // this.animateLevitation();
            };

            OnDragStopEvent = delegate (IDraggable sender, EventArgs args)
            {
                // this.animateLanding();
            };

            behave = Behave;

            OnClickEvent += (obj, args) => {
                Array behaivorTypeVals = AnimalBehaivorType.GetValues(typeof(AnimalBehaivorType));
                Random random = new Random();
                AnimalBehaivorType randomBehaivor = (AnimalBehaivorType) behaivorTypeVals.GetValue(random.Next(behaivorTypeVals.Length));
                behave(randomBehaivor);
            };
        }

        public void Behave(AnimalBehaivorType type)
        {
            if (type == AnimalBehaivorType.SOUND)
            {
                Sound();
            } else if (type == AnimalBehaivorType.ACTION)
            {
                Act();
            }
        }
        public abstract void Sound();
        public abstract void Act();

        public event OnDragStartHandle OnDragStartEvent;
        public event OnDragStopHandle OnDragStopEvent;
    }
}
