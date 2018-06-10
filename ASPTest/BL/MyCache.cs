using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPTest.BL
{
    public static class MyCache
    {
        public static int[] _LinkedItems = null;

        /// <summary>
        /// Check if the position is in the range of the items
        /// </summary>
        /// <param name="Position"></param>
        /// <returns></returns>
        public static bool Validator(int Position)
        {
            return _LinkedItems != null &&
                   (Position >= 0 && Position < _LinkedItems.Count());
        }

        /// <summary>
        /// The items here should be already indexed 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        internal static int GetItemByIndex(int index)
        {
            if (IsCacheExist())
            {
                if (Validator(index))
                {
                    return _LinkedItems[index];
                }
                else
                {
                    throw new Exception("Index is out of the boundaries");
                }
            }

            throw new Exception("No item to show");
        }

        internal static bool IsCacheExist()
        {
            return _LinkedItems != null;
        }
    }
}
