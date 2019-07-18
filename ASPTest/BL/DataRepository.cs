using ASPTest.DAL;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace ASPTest.BL
{
    public class DataRepository : IData
    {
        /// <summary>
        /// Daves the randon generated numbers
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
                Debug.WriteLine(exp.Message);
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
                    return context.GetItemsAccroding(Date);
                }
            }
            catch (Exception exp)
            {
                Debug.WriteLine(exp.Message);
                return new List<Tuple<int, int>>();
            }
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
                    return context.GetItemsAndCount();
                }
            }
            catch (Exception exp)
            {
                Debug.WriteLine(exp.Message);
                return new List<Tuple<int, int>>();
            }
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