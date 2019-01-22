using Microsoft.VisualStudio.TestTools.UnitTesting;
using KazoeciaoOutputAnalyzer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KazoeciaoOutputAnalyzer.KazoeciaoDefault;

namespace KazoeciaoOutputAnalyzer.Tests
{
    [TestClass()]
    public class SourcesDifferenceTests
    {
        SourcesDifference sourcesDiff = null;
        [TestInitialize()]
        public void TestInitialize()
        {
            IFunctionDifference[] funcs =
            {
                new FunctionDifferenceDefault("t1", @"TestProject\TestModule1\TestClassA.cpp", 5, 0, 0, 5, 0),
                new FunctionDifferenceDefault("t2", @"TestProject\TestModule1\TestClassA.cpp", 0, 0, 0, 5, 5),
                new FunctionDifferenceDefault("t3", @"TestProject\TestModule1\TestClassB.cpp", 0, 5, 0, 5, 5),
                new FunctionDifferenceDefault("t4", @"TestProject\TestModule2\TestClassC.cpp", 0, 0, 5, 5, 0),
            };
            sourcesDiff = new SourcesDifference(funcs.ToList());
        }

        [TestMethod()]
        public void StepNum()
        {
            Assert.AreEqual(5, sourcesDiff.ModifiedStepNum);
            Assert.AreEqual(5, sourcesDiff.NewAddedStepNum);
            Assert.AreEqual(5, sourcesDiff.DeletedStepNum);
            //Assert.AreEqual(20, sourcesDiff.OldTotalStepNum);
            Assert.AreEqual(10, sourcesDiff.DiversionStepNum);
            Assert.AreEqual(10, sourcesDiff.MeasuredStepNum());
            Assert.AreEqual(10, sourcesDiff.MeasuredStepNumWithDiversion());
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
            Assert.IsTrue(r.Any(x => x == @"\TestProject\TestModule1"));
            Assert.IsTrue(r.Any(x => x == @"\TestProject\TestModule2"));
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
            var r = sourcesDiff.SelectByDirectoryPath(@"\TestProject\TestModule1");
            Assert.AreEqual(3, r.Functions().Count());
            Assert.IsTrue(r.Functions().Any(x => x.FunctionName == "t1"));
            Assert.IsTrue(r.Functions().Any(x => x.FunctionName == "t2"));
            Assert.IsTrue(r.Functions().Any(x => x.FunctionName == "t3"));
        }

        [TestMethod()]
        public void SelectByDirectoryPathTest_ProjectDir()
        {
            var r = sourcesDiff.SelectByDirectoryPath(@"\TestProject\");
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