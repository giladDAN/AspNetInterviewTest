using ASPTest.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ASPTest.Controllers
{
    public class BackOfficeController : ApiController
    {
        IData _DataRepository = new DataRepository();

        /// <summary>
        /// Return each  /// Will return each number and the count of times it is in the DB
        /// Gets Date to show from specific date or number
        /// 2,100 - the number 2 appears 100 times. 
        /// </summary>
        /// <param name="Date"></param>
        /// <returns></returns>
        public List<string> GetItemsAccroding(DateTime Date)
        {
            List<Tuple<int, int>> gia = _DataRepository.GetItemsAccroding(Date);
            return gia.Select(item => $"{item.Item1},{item.Item2}").ToList();
        }

        /// <summary>
        /// Will return each number and the count of times it is in the DB
        /// 2,100 - the number 2 appears 100 times.
        /// </summary>
        /// <returns></returns>
        public List<string> GetItemsAndCount()
        {
            List<Tuple<int, int>> gia = _DataRepository.GetItemsAndCount();
            return gia.Select(item => $"{item.Item1},{item.Item2}").ToList();
        }

    }
}
