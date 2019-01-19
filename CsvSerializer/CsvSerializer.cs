using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;

namespace CsvSerializer
{
    /// <summary>
    /// CSVフィールド属性
    /// </summary>
    public class FieldAttribute : System.Attribute  
    {  
        public FieldAttribute(string name, bool allowEmpty = false)  
        {  
            this.Name = name;
            this.AllowEmpty = allowEmpty;
        }  
      
        /// <summary>
        /// ヘッダ名
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 空文字を許可するか
        /// </summary>
        public bool AllowEmpty { get; }
    }  

    /// <summary>
    /// CSVシリアライザ
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CsvSerializer<T> where T : new()
    {
        private int headerRowIndex;
        private int startRowIndex;
        private Dictionary<string, int> map = new Dictionary<string, int>();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="headerRowIndex">ヘッダ行番号</param>
        /// <param name="startRowIndex">アイテム行の読み取り開始行番号</param>
        public CsvSerializer(int headerRowIndex = 0, int startRowIndex = 1)
        {
            if(headerRowIndex < 0)
                throw new ArgumentException("ヘッダ行の行番号が負の数です.");

            if(startRowIndex < 0)
                throw new ArgumentException("開始行の行番号が負の数です.");
                
            if (startRowIndex < headerRowIndex)
                throw new ArgumentException("ヘッダ行がよ開始行より後ろに設定されています.");

            this.headerRowIndex = headerRowIndex;
            this.startRowIndex = startRowIndex;
        }

        /// <summary>
        /// Tクラスのプロパティ名に対応するCSVの列番号のマップを作成する
        /// </summary>
        /// <param name="header"></param>
        private void CreateMap(List<string> header)
        {
            var properties = typeof(T).GetProperties();
            foreach(var property in properties)
            {
                var attrib =  (FieldAttribute)System.Attribute.GetCustomAttribute(property, typeof(FieldAttribute));
                if(attrib != null)
                {
                    int index = header.IndexOf(attrib.Name);
                    if (index < 0)
                        throw new InvalidOperationException("CSV内に[" + attrib.Name + "]ヘッダが見つかりませんでした.");
                    map.Add(property.Name, index);
                }
            }
        }

        /// <summary>
        /// CSVの一行からTクラスのインスタンスを生成する
        /// </summary>
        /// <param name="fields"></param>
        /// <returns></returns>
        private T CreateItem(string[] fields)
        {
            T item = new T();
            var properties = item.GetType().GetProperties();
            foreach(var property in properties)
            {
                if (map.ContainsKey(property.Name))
                {
                    // これを毎回実行するので遅いかも（mapに持たせた方が良いかも)
                    var attrib = (FieldAttribute)System.Attribute.GetCustomAttribute(property, typeof(FieldAttribute));

                    var value = fields[map[property.Name]];
                    if(!attrib.AllowEmpty && string.IsNullOrEmpty(value))
                        throw new FormatException("CSV内に空の文字列がある.");

                    property.SetValue(item, Convert.ChangeType(value, property.PropertyType));
                }
            }
            return item;
        }

        /// <summary>
        /// CSVファイルからTクラスの一覧を取得する
        /// </summary>
        /// <param name="csv_path"></param>
        /// <param name="enc"></param>
        /// <returns></returns>
        public List<T> Read(string csv_path, Encoding enc)
        {
            List<T> list = new List<T>();

            using(var parser = new TextFieldParser(csv_path, enc)) {
                parser.Delimiters = new string[] { "," };
                for(int i=0; !parser.EndOfData; i++) {
                    var fields = parser.ReadFields();
                    if(i == headerRowIndex)
                    {
                        CreateMap(fields.ToList());
                    }
                    if(i >= startRowIndex)
                    {
                        try
                        {
                            list.Add(CreateItem(fields));
                        }
                        catch {
                            // ヘッダがあっているので、行が一致しない場合は、スルーする
                        }
                    }
                }
            }

            return list;
        }
    }
}
