using ASPTest.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPTest.BL
{
    public class DataRepository : IData
    {
        /// <summary>
        /// Save items to DB
        /// </summary>
        /// <param name="Items"></param>
        public void SaveItems(List<int> Items)
        {
            try
            {
                using (Model context = new Model())
                {
                    var now = DateTime.Now;
                    foreach (var item in Items)
                    {
                        SelectedItems i = new SelectedItems();
                        i.Number = item;
                        i.ShowDate = now;
                        context.SelectedItems.Add(i);
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception exp)
            {
            }
        }

        /// <summary>
        /// Return each  /// Will return each number and the count of times it is in the DB
        /// Gets Date to show from specific date or number
        /// 2,100 - the number 2 appears 100 times. 
        /// </summary>
        /// <param name="Date"></param>
        /// <returns></returns>
        public List<Tuple<int, int>> GetItemsAccroding(DateTime Date)
        {
            try
            {
                using (Model context = new Model())
                {
                    List<SelectedItems> returnItemsByDate = context.SelectedItems.
                                                            Where(i => i.ShowDate > Date).OrderBy(i => i.Number).ToList();

                    return ZeroItemsGapsByRange(returnItemsByDate, CONSTS.START_RANGE, CONSTS.END_RANGE);
                }
            }
            catch (Exception exp)
            {
            }

            return null;
        }



        /// <summary>
        /// Will return each number and the count of times it is in the DB
        /// 2,100 - the number 2 appears 100 times.
        /// </summary>
        /// <returns></returns>
        public List<Tuple<int, int>> GetItemsAndCount()
        {
            try
            {
                using (Model context = new Model())
                {
                    List<SelectedItems> returnItems = context.SelectedItems.ToList();

                    return ZeroItemsGapsByRange(returnItems, CONSTS.START_RANGE, CONSTS.END_RANGE);
                }
            }
            catch (Exception exp)
            {
            }

            return null;
        }

        /// <summary>
        /// Add items to the collection, Only in the specific range (included)
        /// </summary>
        /// <param name="SelectedItems"></param>
        /// <param name="StartRange"></param>
        /// <param name="EndRange"></param>
        /// <returns></returns>
        private List<Tuple<int, int>> ZeroItemsGapsByRange(List<SelectedItems> SelectedItems, int StartRange, int EndRange)
        {
            // Count the times that each number appears in the List
            int NumbersCounter;
            List<Tuple<int, int>> returnItems = new List<Tuple<int, int>>();

            // Run over the range of the new list to return
            for (int i = StartRange; i <= EndRange; i++)
            {
                NumbersCounter = SelectedItems.Where(currItem => currItem.Number == i).Count();

                // If number exist in the list just add it to the return list with the count
                if (NumbersCounter > 0)
                {
                    returnItems.Add(Tuple.Create(i, NumbersCounter));
                }
                else // If number doesn't exist in the list, than add it with cunter to 0
                {
                    returnItems.Add(Tuple.Create(i, 0));
                }
            }

            return returnItems;
        }

    }

    interface IData
    {
        /// <summary>
        /// Saves Item in DB
        /// </summary>
        /// <param name="Items"></param>
        void SaveItems(List<int> Items);

        List<Tuple<int, int>> GetItemsAccroding(DateTime Date);

        List<Tuple<int, int>> GetItemsAndCount();

    }
}
