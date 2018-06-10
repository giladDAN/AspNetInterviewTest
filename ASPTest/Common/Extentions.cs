using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ASPTest.BL
{
    public static class Extensions
    {
        /// <summary>
        /// Initialize the array with a given T parameter
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="defaultVaue"></param>
        public static void Init<T>(this T[] array, T defaultVaue)
        {
            if (array == null)
                return;
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = defaultVaue;
            }
        }
    }
}
