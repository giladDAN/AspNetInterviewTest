using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ASPTest.Controllers
{
    public class CalcController : ApiController
    {
        IEvents _eventRep = new EventsRepository();

        /// <summary>
        /// Return Next Postion
        /// </summary>
        /// <param name="Postion"></param>
        /// <returns></returns>
        public int Get(int Postion)
        {
            return _eventRep.Next(Postion);
        }

        public List<int> Get()
        {
            return _eventRep.GetAllItems();
        }
    }
}
