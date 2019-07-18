using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPTest.BL
{
    public class DataHandler : IDataHandler
    {
        private const int DEFAULT_VALUE = -1;
        public Dictionary<int, int> PopulateNextItems(List<int> items)
        {
            Dictionary<int, int> retItems = new Dictionary<int, int>();

            foreach (var item in items.OfType<object>().Select((x, i) => new { x, i }))
            {
                foreach (var runningItem in items.Skip(item.i + 1).OfType<object>().Select((x, i) => new { x, i }))
                { // Items.skip - start from item index to the right
                    if (item.x.Equals(runningItem.x))
                    {
                        retItems.Add(item.i, runningItem.i + item.i + 1);
                        break; // Next index found, continue to the next position
                    }
                }
                // If no next value found populate with DEFAULT_VALUE
                if (!retItems.ContainsKey(item.i))
                {
                    retItems.Add(item.i, DEFAULT_VALUE);
                }
            }

            return retItems;
        }
    }

    interface IDataHandler
    {
        /// <summary>
        /// Returns list of next items
        /// </summary>
        /// <param name="Items"></param>
        /// <returns></returns>
        Dictionary<int, int> PopulateNextItems(List<int> Items);
    }

}