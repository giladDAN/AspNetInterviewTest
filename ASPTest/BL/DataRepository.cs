using ASPTest.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

            List<Tuple<int, int>> lst = new List<Tuple<int, int>>();

            try
            {
                using (Model context = new Model())
                {
                    List<SelectedItems> temp = context.SelectedItems.Where(w => w.ShowDate.Year == Date.Year && w.ShowDate.Month == Date.Month && w.ShowDate.Day == Date.Day).OrderBy(z => z.Number).ToList();
                    try
                    {
                        int num = temp.First().Number;
                        int count = 0;

                        for (int i = 0; i < 100; i++)
                        {
                            lst.Add(new Tuple<int, int>(i, 0));
                        }
                        foreach (SelectedItems item in temp)
                        {
                            if (item.Number == num)
                            {
                                count++;
                            }
                            else
                            {
                                lst.Remove(new Tuple<int, int>(num, 0));
                                lst.Insert(num, new Tuple<int, int>(num, count));
                                num = item.Number;
                                count = 1;
                            }
                        }
                    }
                    catch
                    {
                        for (int i = 0; i < 100; i++)
                        {
                            lst.Add(new Tuple<int, int>(i, 0));
                        }
                    }
                }
            }
            catch (Exception exp)
            {
            }

            return lst;
        }

        /// <summary>
        /// Will return each number and the count of times it is in the DB
        /// 2,100 - the number 2 appears 100 times.
        /// </summary>
        /// <returns></returns>
        public List<Tuple<int, int>> GetItemsAndCount()
        {
            List<Tuple<int, int>> lst = new List<Tuple<int, int>>();

            try
            {
                using (Model context = new Model())
                {
                    List<SelectedItems> temp = context.SelectedItems.OrderBy(w => w.Number).ToList();

                    int num = temp.First().Number;
                    int count = 0;

                    for (int i = 0; i < 100; i++)
                    {
                        lst.Add(new Tuple<int, int>(i, 0));
                    }
                    foreach (SelectedItems item in temp)
                    {
                        if (item.Number == num)
                        {
                            count++;
                        }
                        else
                        {
                            lst.Remove(new Tuple<int, int>(num, 0));
                            lst.Insert(num, new Tuple<int, int>(num, count));
                            num = item.Number;
                            count = 1;
                        }
                    }
                }
            }
            catch (Exception exp)
            {
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