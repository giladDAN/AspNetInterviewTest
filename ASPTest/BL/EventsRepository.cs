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
                var itemsPositionsDictionary = new Dictionary<int, int>();

                for (int i = 0; i < 100; i++)
                {
                    var randomNumber = _rand.Next(0, 100);
                    items.Add(randomNumber);

                    var freeItemNextPosition = getNumbersExistence(itemsPositionsDictionary, randomNumber);

                    // Combin a unique key 
                    var itemRepeatPositionHash = getCurrentRepeatHash(randomNumber, i);
                    var itemPositionHash = getItemCurrentPositionHash(randomNumber, freeItemNextPosition);

                    // Adding the locations to the dictionary
                    // Enabling be-directional search
                    itemsPositionsDictionary.Add(itemRepeatPositionHash, freeItemNextPosition);
                    itemsPositionsDictionary.Add(itemPositionHash, i);
                }

                //_DataRepository.SaveItems(items); // Was not needed at first stage

                // Set data in to the singleton
                listData.InitItems(items, itemsPositionsDictionary);
            }
        }


        private int getCurrentRepeatHash(int number, int position)
        {
            // Combin unique key
            var StringToHash = number.ToString() + 'R' + position.ToString();
            return StringToHash.GetHashCode();
        }

        private int getItemCurrentPositionHash(int number, int reapeatPosition)
        {
            // Combin unique key
            var StringToHash = number.ToString() + 'P' + reapeatPosition.ToString();
            return StringToHash.GetHashCode();
        }

        /// <summary>
        /// Run through dictionary efficiently and check for free
        /// item repeat position
        /// </summary>
        /// <param name="itemPositions"></param>
        /// <param name="number"></param>
        /// <returns>int free repeat number</returns>
        private int getNumbersExistence(Dictionary<int, int> itemPositions, int number)
        {
            var exist = true;
            var counter = 0;
            while (exist)
            {
                var stringToHash = number.ToString() + 'P' + counter.ToString();
                var hash = stringToHash.GetHashCode();

                if (!itemPositions.ContainsKey(hash)){
                    exist = false;
                    return counter;
                }
                counter++;
            }

            return -1;
        }

        public int Next(int Postion)
        {
            if (listData.Items.Count > Postion && Postion >= 0)
            {
                var number = listData.Items[Postion];

                var currentRepeatHash = getCurrentRepeatHash(number, Postion);

                if (listData.ItemsPositions.ContainsKey(currentRepeatHash))
                {
                    var currentRepeatNumber = listData.ItemsPositions[currentRepeatHash];

                    var NextRepeatPositionHash = getItemCurrentPositionHash(number, currentRepeatNumber + 1);

                    if (listData.ItemsPositions.ContainsKey(NextRepeatPositionHash))
                    {
                        return listData.ItemsPositions[NextRepeatPositionHash];
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
