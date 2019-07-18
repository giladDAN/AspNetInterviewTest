using Microsoft.VisualStudio.TestTools.UnitTesting;
using ASPTest.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPTest.BL.Tests
{
    [TestClass()]
    public class DataHandlerTests
    {
        [TestMethod()]
        public void PopulateNextItemsTest()
        {
            // Arrange
            List<int> testingItems = new List<int>() { 1, 2, 8, 1, 2, 3, 5, 6, 7, 2, 2, 9, 9, 9, 3, 2, 1, 2, 3 };
            DataHandler dh = new DataHandler();
            // Act
            Dictionary<int, int>  resultDic = dh.PopulateNextItems(testingItems);
            // Assert
            Assert.IsTrue(resultDic[0].Equals(3));
            Assert.IsTrue(resultDic[3].Equals(16));
            Assert.IsTrue(resultDic[1].Equals(4));
            Assert.IsTrue(resultDic[2].Equals(-1));
        }
    }
}