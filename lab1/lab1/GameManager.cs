using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace lab1
{
    // GameManager -> Game: избегать в названиях классов что-то типа Manager, Processor и тд
    class Game: IDisposable
    {
        private Farm farm;
        // использовать удобопроизносимые имена
        private List<WeakReference> userActionsHistory;

        // общая структура названий для семантически схожих переменных
        private const string carrotXmlFilename = "carrot.xml";
        private const string carrotArrayXmlFilename = "carrots.xml";
        private const string carrotBinFilename = "carrot.bin";
        private const string carrotArrayBinFilename = "carrots.bin";

        private void EmulateUserActions()
        {
            string[] actions = { "Click", "DragStart", "DragStop" };

            Random random = new Random();
            for (int i = 0; i < 1000000; i++)
            {
                int randActionIndex = random.Next(0, actions.Length); // пояснительная переменная вместо actions[random.Next(0, actions.Length)]
                string act = actions[randActionIndex];
                userActionsHistory.Add(new WeakReference(act));
            }
        }

        private int CheckUserActions()
        {
            int num = 0;
            for (int i = 0; i < userActionsHistory.Count; i++)
            {
                if (userActionsHistory[i].IsAlive)
                {
                    num++;
                }
            }

            return num;
        }

        public void Dispose()
        {
            if (userActionsHistory != null)
            {
                userActionsHistory.Clear();
                userActionsHistory = null;
            }

            if (farm != null)
            {
                farm.Dispose();
                farm = null;
            }
            GC.SuppressFinalize(this);

            Console.WriteLine("Memory before collect: {0}", GC.GetTotalMemory(false));
            GC.Collect();
            Console.WriteLine("Memory after collect: {0}", GC.GetTotalMemory(false));
        }

        public Game()
        {
            Console.WriteLine("Memory before init: {0}", GC.GetTotalMemory(false));

            farm = Farm.Instance;
            userActionsHistory = new List<WeakReference>();

            Console.WriteLine("Memory after init: {0}", GC.GetTotalMemory(false));
        }


        // Названия методов должны соответствовать шаблону ГлаголСуществительное и описывать выполняемое действие по отношению к предмету
        // Стараться сводить к минимуму количество аргументов методов: например, создание сериализатора инкапсулировано
        // Избагать аргументов-флагов: например, вместо флага, который указывает сериализовать ли xml или bin использовать два разных метода
        // Действия на едином уровне абстракции
        // Метод выполняет одну операцию
        private void SerializeCarrotXml(Carrot carrot)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Carrot));
            using (StreamWriter streamWriter = new StreamWriter(carrotXmlFilename))
            {
                serializer.Serialize(streamWriter, carrot);
            }
        }

        private void SerializeCarrotArrayXml(Carrot[] carrots)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Carrot[]));
            using (StreamWriter streamWriter = new StreamWriter(carrotArrayXmlFilename))
            {
                serializer.Serialize(streamWriter, carrots);
            }
        }

        private Carrot DeserializeCarrotXml()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Carrot));
            Carrot deserializedCarrot; // не скупиться на длину названия если оно отражает суть данных, на которую ссылается переменная
            using (StreamReader streamReader = new StreamReader(carrotXmlFilename))
            {
                deserializedCarrot = (Carrot)serializer.Deserialize(streamReader);
            }
            return deserializedCarrot;
        }

        // избегать выходных аргументов: массив возвращается как результат метода
        private Carrot[] DeserializeCarrotArrayXml()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Carrot[]));
            Carrot[] deserializedCarrotArray;
            using (StreamReader streamReader = new StreamReader(carrotArrayXmlFilename))
            {
                deserializedCarrotArray = (Carrot[]) serializer.Deserialize(streamReader);
            }
            return deserializedCarrotArray;
        }

        // принцип найменьшего удивления: выполняется аналогично SerializeCarrotXml
        private void SerializeCarrotBin(Carrot carrot)
        {
            IFormatter formatter = new BinaryFormatter();
            using (Stream stream = new FileStream(carrotBinFilename, FileMode.Create))
            {
                formatter.Serialize(stream, carrot);
            }
        }

        private void SerializeCarrotArrayBin(Carrot[] carrots)
        {
            IFormatter formatter = new BinaryFormatter();
            using (Stream stream = new FileStream(carrotArrayBinFilename, FileMode.Create))
            {
                formatter.Serialize(stream, carrots);
            }
        }

        private Carrot DeserializeCarrotBin()
        {
            IFormatter formatter = new BinaryFormatter();
            Carrot deserializedCarrot;
            using (Stream stream = new FileStream(carrotBinFilename, FileMode.Open))
            {
                deserializedCarrot = (Carrot)formatter.Deserialize(stream);
            }
            return deserializedCarrot;
        }

        private Carrot[] DeserializeCarrotArrayBin()
        {
            IFormatter formatter = new BinaryFormatter();
            Carrot[] deserializedCarrotArray;
            using (Stream stream = new FileStream(carrotArrayBinFilename, FileMode.Open))
            {
                deserializedCarrotArray = (Carrot[])formatter.Deserialize(stream);
            }
            return deserializedCarrotArray;
        }

        // минимизировать количество вложений в методах путем вынесения некоторых действий в отдельные методы
        private void FillListWithCarrots(List<Carrot> carrots, int num)
        {
            for (int i = 0; i < num; i++)
            {
                carrots.Add(new Carrot());
            } 
        }

        private void DisplayCarrot(Carrot carrot)
        {
            Console.WriteLine(carrot);
            Console.WriteLine("Calories: " + carrot.CALORIES);
            Console.WriteLine("Growing time: " + carrot.GROWING_TIME);
            Console.WriteLine("Weight: " + carrot.weight);
            Console.WriteLine("");
        }

        private void TestXmlSerialization()
        {
            Carrot[] carrots = new[] { new Carrot(), new Carrot(), new Carrot() };
            Carrot carrot = new Carrot();

            SerializeCarrotXml(carrot);
            SerializeCarrotArrayXml(carrots);
            Carrot deserialesedCarrot = DeserializeCarrotXml();
            Carrot[] deserialesedCarrotArray = DeserializeCarrotArrayXml();

            DisplayCarrot(deserialesedCarrot);

            const int TEST_ARRAY_POSITION = 1; // записывать волшебные числа именованными константами
            DisplayCarrot(deserialesedCarrotArray[TEST_ARRAY_POSITION]);
        }

        private void TestBinSerialization()
        {
            Carrot[] carrots = new[] { new Carrot(), new Carrot(), new Carrot() };
            Carrot carrot = new Carrot();

            SerializeCarrotBin(carrot);
            SerializeCarrotArrayBin(carrots);
            Carrot deserialesedCarrot = DeserializeCarrotBin();
            Carrot[] deserialesedCarrotArray = DeserializeCarrotArrayBin();

            DisplayCarrot(deserialesedCarrot);

            const int TEST_ARRAY_POSITION = 1; // записывать волшебные числа именованными константами
            DisplayCarrot(deserialesedCarrotArray[TEST_ARRAY_POSITION]);
        }

        // декомпозиция функций: писать функции минимального размера
        public void RunGame()
        {

            EmulateUserActions();
            Console.WriteLine("Alive user actions: {0}", CheckUserActions());

            List<Carrot> carrotsTrash = new List<Carrot>();
            FillListWithCarrots(carrotsTrash, 100000);

            Console.WriteLine("Memory after start: {0}", GC.GetTotalMemory(false));

            TestXmlSerialization();
            TestBinSerialization();
            
            Console.WriteLine("Alive user actions: {0}", CheckUserActions());
        }
    }
}
