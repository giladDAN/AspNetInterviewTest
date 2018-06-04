using ASPTest.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPTest
{
    public class EventsRepository : IEvents
    {
        IData _DataRepository = new DataRepository();
        ListData listData = ListData.Instance;
        Random _rand = new Random();

        public EventsRepository()
        {

            // If list did not initialized yet
            // generate data and save into singleton
            if (listData.Items == null)
            {

                // Used both list and beDirectional dictionary
                // first initial will be slow but its more efficient at run time
                var items = new List<int>();
                var itemsPositions = new Dictionary<int, Dictionary<int, int>>();
                var itemsPositionsInvers = new Dictionary<int, Dictionary<int, int>>();

                for (int i = 0; i < 100; i++)
                {
                    var randomNumber = _rand.Next(0, 100);
                    items.Add(randomNumber);

                    if (!itemsPositions.ContainsKey(randomNumber))
                    {
                        itemsPositions.Add(randomNumber, new Dictionary<int, int>());
                        itemsPositionsInvers.Add(randomNumber, new Dictionary<int, int>());
                    }

                    var currentRepeatNumber = itemsPositions[randomNumber].Count;
                    itemsPositions[randomNumber].Add(i, currentRepeatNumber);
                    itemsPositionsInvers[randomNumber].Add(currentRepeatNumber, i);
                }

                //_DataRepository.SaveItems(items); // Was not needed at first stage
                listData.InitItems(items, itemsPositions, itemsPositionsInvers);
            }
        }

        public int Next(int Postion)
        {
            if (listData.Items.Count > Postion && Postion > 0)
            {
                var number = listData.Items[Postion];

                if (listData.ItemsPositions.ContainsKey(number))
                {
                    var currentRepeatNumber = listData.ItemsPositions[number][Postion];

                    if (listData.ItemsPositionsInvers[number].ContainsKey(currentRepeatNumber + 1))
                    {
                        return listData.ItemsPositionsInvers[number][currentRepeatNumber + 1];
                    }
                }
            }

            // Case there is no number next repeat
            // or Position is not valid
            return -1;
        }

        public List<int> GetAllItems()
        {
            return listData.Items;
        }
    }

    interface IEvents
    {
        int Next(int Postion);
        List<int> GetAllItems();

    }
}
