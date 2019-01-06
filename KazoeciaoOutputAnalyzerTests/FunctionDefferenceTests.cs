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
    public class FunctionDefferenceTests
    {
        private FunctionDifference CreateFunctionDefferenceSample(
            int modifiedStepNum,
            int newAddedStepNum,
            int deletedStepNum,
            int oldTotalStepNum,
            int diversionStepNum)
        {
            return new FunctionDifference("test()", @"C:\TestProject\TestModule\TestClass.cpp",
                        modifiedStepNum,
                        newAddedStepNum,
                        deletedStepNum,
                        oldTotalStepNum,
                        diversionStepNum);
        }
        [TestMethod()]
        public void PropertyTest()
        {
            var func = CreateFunctionDefferenceSample(0, 5, 0, 0, 0);
            Assert.AreEqual("test()", func.FunctionName);
            Assert.AreEqual(@"C:\TestProject\TestModule", func.DirectoryPath);
            Assert.AreEqual(@"TestClass", func.FileName);
        }
        [TestMethod()]
        public void IsNewAddedTest_True()
        {
            var func = CreateFunctionDefferenceSample(0, 5, 0, 0, 0);
            Assert.IsTrue(func.IsNewAdded());
        }
        [TestMethod()]
        public void IsNewAddedTest_False()
        {
            var func = CreateFunctionDefferenceSample(0, 5, 0, 5, 5);
            Assert.IsFalse(func.IsNewAdded());
        }

        [TestMethod()]
        public void IsModifiedTest_Modified()
        {
            var func = CreateFunctionDefferenceSample(1, 0, 0, 5, 4);
            Assert.IsTrue(func.IsModified());
        }
        [TestMethod()]
        public void IsModifiedTest_Deleted()
        {
            var func = CreateFunctionDefferenceSample(0, 2, 0, 5, 5);
            Assert.IsTrue(func.IsModified());
        }
        [TestMethod()]
        public void IsModifiedTest_NewAdded()
        {
            var func = CreateFunctionDefferenceSample(0, 3, 0, 5, 5);
            Assert.IsTrue(func.IsModified());
        }
        [TestMethod()]
        public void IsModifiedTest_Not()
        {
            var func = CreateFunctionDefferenceSample(0, 0, 0, 5, 5);
            Assert.IsFalse(func.IsModified());
        }

        [TestMethod()]
        public void IsDeletedTest_True()
        {
            var func = CreateFunctionDefferenceSample(0, 0, 5, 5, 0);
            Assert.IsTrue(func.IsDeleted());
        }

        [TestMethod()]
        public void IsDeletedTest_False()
        {
            var func = CreateFunctionDefferenceSample(0, 0, 4, 5, 1);
            Assert.IsFalse(func.IsDeleted());
        }
    }
}