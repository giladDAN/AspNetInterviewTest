using ASPTest.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace ASPTest.BL
{
    public class DataRepository : IData
    {
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
            return Filter(Date);
        }

        /// <summary>
        /// Will return each number and the count of times it is in the DB
        /// 2,100 - the number 2 appears 100 times.
        /// </summary>
        /// <returns></returns>
        public List<Tuple<int, int>> GetItemsAndCount()
        {
            return Filter(null);
        }

        private List<Tuple<int, int>> Filter(DateTime? Date)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();

            using (Model context = new Model())
            {
                List<SelectedItems> temp;

                if (Date != null)
                    temp = context.SelectedItems.Where(w => w.ShowDate.Year == Date.Value.Year && w.ShowDate.Month == Date.Value.Month && w.ShowDate.Day == Date.Value.Day).OrderBy(z => z.Number).ToList();
                else
                    temp = context.SelectedItems.OrderBy(z => z.Number).ToList();

                if (temp.Count > 0)
                {
                    int num = temp.First().Number;
                    int count = 0;

                    for (int i = 0; i < 100; i++)
                    {
                        dict.Add(i, 0);
                    }
                    foreach (SelectedItems item in temp)
                    {
                        if (item.Number == num)
                        {
                            count++;
                        }
                        else
                        {
                            dict[num] = count;
                            num = item.Number;
                            count = 1;
                        }
                    }
                }
            }

            List<Tuple<int, int>> lst = new List<Tuple<int, int>>();
            foreach (var item in dict)
            {
                lst.Add(new Tuple<int, int>(item.Key, item.Value));
            }
            return lst;
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
