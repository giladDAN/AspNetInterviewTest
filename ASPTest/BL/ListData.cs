using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPTest.BL
{
    public class ListData
    {
        ListData(){}

        private static readonly object padlock = new object();

        private static ListData instance = null;

        public List<int> Items { get; private set; }
        public Dictionary<int, Dictionary<int, int>> ItemsPositions { get; private set; }
        public Dictionary<int, Dictionary<int, int>> ItemsPositionsInvers { get; private set; }

        public void InitItems(List<int>  items, Dictionary<int, Dictionary<int, int>> itemsPositions, Dictionary<int, Dictionary<int, int>> itemsPositionsInvers)
        {

            if(Items == null)
            {
                Items = items;
                ItemsPositions = itemsPositions;
                ItemsPositionsInvers = itemsPositionsInvers;
            }
        }

        public static ListData Instance
        {

            get

            {

                lock (padlock)

                {

                    if (instance == null)

                    {

                        instance = new ListData();

                    }

                    return instance;

                }

            }

        }
    }
}
