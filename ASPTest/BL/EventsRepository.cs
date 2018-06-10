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
        List<int> Items = new List<int>();
        Random _rand = new Random();

        public EventsRepository()
        {
            // load data to the memory if not yet exist (in production probably change to another memory)
            if (!MyCache.IsCacheExist())
            {
                for (int i = 0; i < CONSTS.END_RANGE; i++)
                {
                    Items.Add(_rand.Next(CONSTS.START_RANGE, 100));
                }

                MyCache._LinkedItems = BuildEventsSeries(Items);
            }

            // TODO: Seems to not be needed for now
            //_DataRepository.SaveItems(Items);
        }

        /// <summary>
        /// Go to the next event 
        /// </summary>
        /// <param name="Position"></param>
        /// <returns></returns>
        public int Next(int Position)
        {
            if (MyCache.IsCacheExist())
            {
                return MyCache.GetItemByIndex(Position);
            }

            throw new Exception("Debug: Cache is not availible");
        }

        /// <summary>
        /// Build from scratch the events from the given data
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        private int[] BuildEventsSeries(List<int> items)
        {
            int[] returnLinkedItems = null;
            int EventsLength = items.Count;

            // Skip if items are not exist 
            if (items != null)
            {
                returnLinkedItems = new int[EventsLength];
                returnLinkedItems.Init(-1);

                // Run on the current events on the item list
                for (int currentIndex = 0; currentIndex < EventsLength - 1; currentIndex++)
                {
                    // Run until finding the same events on item list as the currentIndexer
                    for (int runIndex = currentIndex + 1; runIndex < EventsLength; runIndex++)
                    {
                        // Check for the next same event and save it in the returnLinkedItems 
                        if (items[currentIndex] == items[runIndex])
                        {
                            returnLinkedItems[currentIndex] = runIndex;

                            break;
                        }
                    }
                }
            }

            return returnLinkedItems;
        }

        public List<int> GetAllItems()
        {
            return Items;
        }
    }

    interface IEvents
    {
        int Next(int Postion);
        List<int> GetAllItems();

    }
}
