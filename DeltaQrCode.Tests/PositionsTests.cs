using System;
using System.Collections.Generic;
using System.Text;

namespace DeltaQrCode.Tests
{
    using DeltaQrCode.Repositories;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PositionsTests
    {
        [TestMethod]
        public void GeneratingPositionsShouldWork()
        {
            var x = HelpersAndExtensions.Helpers.GetAllCombinationsRowsAndPositionsAndIntervals();
            
            Assert.IsNotNull(x);
        }
    }
}
