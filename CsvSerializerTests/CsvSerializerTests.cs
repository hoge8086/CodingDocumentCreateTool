using Microsoft.VisualStudio.TestTools.UnitTesting;
using CsvSerializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvSerializer.Tests
{

    public class Row
    {
        public Row() { }

        public string NoField1 { get; set; }

        [CsvSerializer.Field("名前")]
        public string name { get; set; }

        public string NoField2 { get; set; }

        [CsvSerializer.Field("ID")]
        public int id { get; set; }

        [CsvSerializer.Field("好きな食べ物")]
        public string food { get; set; }

        public string NoField3 { get; set; }

    }
        
    [TestClass()]
    public class CsvSerializerTests
    {
        [TestMethod()]
        public void ReadTest()
        {
            var serializer = new CsvSerializer<Row>(0, 2);
            var list = serializer.Read("test.csv", Encoding.GetEncoding("shift_jis"));
            Assert.AreEqual(2, list.Count);
            Assert.IsTrue(list.Any(r => (
                            r.name == "山田太郎" &&
                            r.id == 523 &&
                            r.food == "コロッケ")));
            Assert.IsTrue(list.Any(r => (
                            r.name == "米田三郎" &&
                            r.id == 3 &&
                            r.food == "キャベツ")));
        }
    }
}