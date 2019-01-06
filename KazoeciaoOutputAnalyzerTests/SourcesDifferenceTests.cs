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
    public class SourcesDifferenceTests
    {
        SourcesDifference sourcesDiff = null;
        [TestInitialize()]
        public void TestInitialize()
        {
            FunctionDifference[] funcs =
            {
                new FunctionDifference("t1", @"c:\TestProject\TestModule1\TestClassA.cpp", 5, 0, 0, 5, 0),
                new FunctionDifference("t2", @"c:\TestProject\TestModule1\TestClassA.cpp", 0, 0, 0, 5, 5),
                new FunctionDifference("t3", @"c:\TestProject\TestModule1\TestClassB.cpp", 0, 5, 0, 5, 5),
                new FunctionDifference("t4", @"c:\TestProject\TestModule2\TestClassC.cpp", 0, 0, 5, 5, 0),
            };
            sourcesDiff = new SourcesDifference(funcs.ToList());
        }

        [TestMethod()]
        public void StepNum()
        {
            Assert.AreEqual(5, sourcesDiff.ModifiedStepNum);
            Assert.AreEqual(5, sourcesDiff.NewAddedStepNum);
            Assert.AreEqual(5, sourcesDiff.DeletedStepNum);
            Assert.AreEqual(20, sourcesDiff.OldTotalStepNum);
            Assert.AreEqual(10, sourcesDiff.DiversionStepNum);
            Assert.AreEqual(15, sourcesDiff.MeasuredStepNum());
            Assert.AreEqual(15, sourcesDiff.MeasuredStepNumWithDiversion());
        }

        [TestMethod()]
        public void SelectModefiedTest()
        {
            var r = sourcesDiff.SelectModefied();
            Assert.AreEqual(3, r.Functions().Count());
            Assert.IsTrue(r.Functions().Any(x => x.FunctionName == "t1"));
            Assert.IsTrue(r.Functions().Any(x => x.FunctionName == "t3"));
            Assert.IsTrue(r.Functions().Any(x => x.FunctionName == "t4"));
        }

        [TestMethod()]
        public void DirectoryPathsTest()
        {
            var r = sourcesDiff.DirectoryPaths();
            Assert.AreEqual(2, r.Count());
            Assert.IsTrue(r.Any(x => x == @"c:\TestProject\TestModule1"));
            Assert.IsTrue(r.Any(x => x == @"c:\TestProject\TestModule2"));
        }

        [TestMethod()]
        public void FileNamesTest()
        {
            var r = sourcesDiff.FileNames();
            Assert.AreEqual(3, r.Count());
            Assert.IsTrue(r.Any(x => x == "TestClassA"));
            Assert.IsTrue(r.Any(x => x == "TestClassB"));
            Assert.IsTrue(r.Any(x => x == "TestClassC"));
        }

        [TestMethod()]
        public void SelectByDirectoryPathTest_Module1()
        {
            var r = sourcesDiff.SelectByDirectoryPath(@"c:\TestProject\TestModule1");
            Assert.AreEqual(3, r.Functions().Count());
            Assert.IsTrue(r.Functions().Any(x => x.FunctionName == "t1"));
            Assert.IsTrue(r.Functions().Any(x => x.FunctionName == "t2"));
            Assert.IsTrue(r.Functions().Any(x => x.FunctionName == "t3"));
        }

        [TestMethod()]
        public void SelectByDirectoryPathTest_ProjectDir()
        {
            var r = sourcesDiff.SelectByDirectoryPath(@"c:\TestProject\");
            Assert.AreEqual(4, r.Functions().Count());
            Assert.IsTrue(r.Functions().Any(x => x.FunctionName == "t1"));
            Assert.IsTrue(r.Functions().Any(x => x.FunctionName == "t2"));
            Assert.IsTrue(r.Functions().Any(x => x.FunctionName == "t3"));
            Assert.IsTrue(r.Functions().Any(x => x.FunctionName == "t4"));
        }
        [TestMethod()]
        public void SelectByDirectoryPathTest_None()
        {
            var r = sourcesDiff.SelectByDirectoryPath(@"c:\TestProject\None");
            Assert.AreEqual(0, r.Functions().Count());
        }
    }
}