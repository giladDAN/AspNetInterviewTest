using ASPTest.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace ASPTest.Controllers
{
    public class DataController : ApiController
    {
        IData _dataRep = new BL.DataRepository();

        public List<string> Get()
        {
            return ConvertToFormat(_dataRep.GetItemsAndCount());
        }

        /// <summary>
        /// Get events after a given date
        /// </summary>
        /// <param name="Date"></param>
        /// <returns></returns>
        public List<string> Get(DateTime Date)
        {
            return ConvertToFormat(_dataRep.GetItemsAccroding(Date));
        }

        /// <summary>
        /// Convert to the return of the client's format 
        /// </summary>
        /// <param name="EventItems"></param>
        /// <returns></returns>
        private List<string> ConvertToFormat(List<Tuple<int, int>> EventItems)
        {
            List<string> returnEvents = new List<string>();

            if (EventItems != null)
            {
                for (int i = 0; i < EventItems.Count; i++)
                {
                    returnEvents.Add(EventItems[i].Item1 + "," + EventItems[i].Item2);
                }

                return returnEvents;
            }

            throw new Exception("Debug: No items in current context of EventItems");
        }
    }
}
