using Microsoft.VisualStudio.TestTools.UnitTesting;
using KazoeciaoOutputAnalyzer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KazoeciaoOutputAnalyzer.Tests
{
    [TestClass()]
    public class KazoeciaoOutputReaderTests
    {
        [TestMethod()]
        public void ReadTest()
        {
            var reader = new KazoeciaoOutputReaderDefault();
            var soucesDiff = reader.Read("test.csv");
            Assert.AreEqual(16, soucesDiff.Functions().Count());
            Assert.AreEqual(18, soucesDiff.NewAddedStepNum);
            Assert.AreEqual(32, soucesDiff.OldTotalStepNum);
            Assert.AreEqual(12, soucesDiff.ModifiedStepNum);
            Assert.AreEqual(26, soucesDiff.DiversionStepNum);
            Assert.AreEqual(10, soucesDiff.DeletedStepNum);
            var modules = soucesDiff.DirectoryPaths();
        }
    }
}