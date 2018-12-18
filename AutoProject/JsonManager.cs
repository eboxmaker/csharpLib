using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoProject
{
    class JsonManager
    {
       // public JObject jo;

        public static JObject OpenFile(string path)
        {
            JObject obj;
            FileStream fs = File.OpenRead(path);
            if (fs == null)
                obj = null;
            else
            {
                int len = (int)fs.Length;
                byte[] byteArray = new byte[len];
                int ReadedLength = fs.Read(byteArray, 0, len);
                string js = Encoding.Default.GetString(byteArray);
                obj = (JObject)JsonConvert.DeserializeObject(js);
            }
            return obj;
        }
        //public static string OpenFile(string path)
        //{
        //    string jstr;
        //    FileStream fs = File.OpenRead(path);
        //    if (fs == null)
        //        jstr = null;
        //    else
        //    {
        //        int len = (int)fs.Length;
        //        byte[] byteArray = new byte[len];
        //        int ReadedLength = fs.Read(byteArray, 0, len);
        //        jstr = Encoding.Default.GetString(byteArray);
        //    }
        //    return jstr;
        //}
        
        public DataTable JobjectToDataTable(JObject obj)
        {
            DataTable dt = new DataTable();
            return dt;
        }
        public static string Serialize(string str)
        {
            //格式化json字符串
            JsonSerializer serializer = new JsonSerializer();
            TextReader tr = new StringReader(str);
            JsonTextReader jtr = new JsonTextReader(tr);
            object obj = serializer.Deserialize(jtr);
            if (obj != null)
            {
                StringWriter textWriter = new StringWriter();
                JsonTextWriter jsonWriter = new JsonTextWriter(textWriter)
                {
                    Formatting = Formatting.Indented,
                    Indentation = 4,
                    IndentChar = ' '
                };
                serializer.Serialize(jsonWriter, obj);
                return textWriter.ToString();
            }
            else
            {
                return str;
            }
        }
        public static string Serialize(JObject obj)
        {
            //格式化json字符串
            JsonSerializer serializer = new JsonSerializer();

            StringWriter textWriter = new StringWriter();
            JsonTextWriter jsonWriter = new JsonTextWriter(textWriter)
            {
                Formatting = Formatting.Indented,
                Indentation = 4,
                IndentChar = ' '
            };
            serializer.Serialize(jsonWriter, obj);
            return textWriter.ToString();
        }
        private static byte[] GetByte(JObject obj)
        {
            string js = Serialize(obj);
            byte[] bytes = Encoding.Default.GetBytes(js);
            return bytes;
        }
        public static bool Save(JObject obj, string filepath)
        {
            FileStream fs = File.Create(filepath);
            byte[] byteArray = GetByte(obj);
            fs.Write(byteArray, 0, (int)byteArray.Length);
            fs.Flush();
            fs.Close();
            return true;
        }
        public static bool Save(string jstr, string filepath)
        {
            FileStream fs = File.Create(filepath);
            byte[] byteArray = Encoding.Default.GetBytes(jstr);
            fs.Write(byteArray, 0, (int)byteArray.Length);
            fs.Flush();
            fs.Close();
            return true;
        }

        public static DataTable toDataTable(string jstring)
        {
            DataTable dt=new DataTable();
            JArray ay = new JArray();
            JObject json = JObject.Parse(jstring);

            dt.Columns.Add("RPN",typeof(string));
            dt.Columns.Add("refname", typeof(string));
            dt.Columns.Add("package", typeof(string));
            dt.Columns.Add("flash", typeof(string));
            dt.Columns.Add("ram", typeof(string));
            dt.Columns.Add("iONb", typeof(string));
            dt.Columns.Add("frequency", typeof(string));
            ay = JArray.Parse(json["MCUs"].ToString());
            for (int i = 0; i < ay.Count;i++ )
            {
                JArray aay = JArray.Parse(ay[i]["subpackages"].ToString());
                for(int j = 0 ; j < aay.Count;j++)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = ay[i]["RPN"].ToString();
                    dr[1] = aay[j]["refname"].ToString();
                    dr[2] = aay[j]["package"].ToString();

                    dr[3] = ay[i]["flash"].ToString();
                    dr[4] = ay[i]["ram"].ToString();
                    dr[5] = aay[j]["iONb"].ToString();
                    dr[6] = ay[i]["frequency"].ToString();
                    dt.Rows.Add(dr);

                }
            }
            return dt;

        }

        //public DataTable todt(string str)
        //{
        //    JArray listy = JArray.Parse(str);

        //    if (listy != null && listy.Count > 0)
        //    {

        //        foreach (JToken jt in listy)
        //        {

        //            foreach (IJEnumerable<JToken> jp in jt.Values())
        //            {
                        
        //                JsonArrayObjects QW = JObject.Parse(jt.ToString());

        //                for (int j = 0; j < QW.Count; j++)
        //                {

        //                    foreach (var qwqw in QW[j])
        //                    {

        //                        Logger.Info(qwqw.Key + "------" + qwqw.Value);

        //                    }

        //                }

        //            }

        //        }

        //    }
        //}
        /// <summary>
        /// Json 字符串 转换为 DataTable数据集合
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        //public static DataTable ToDataTable(string json)
        //{
        //    DataTable dataTable = new DataTable();  //实例化
        //    DataTable result;
        //    try
            //{
                //JsonSerializer javaScriptSerializer = new JsonSerializer();
                //JsonReader reader = new JsonReader(json);
                //JsonSerializer.MaxJsonLength = Int32.MaxValue; //取得最大数值
               // ArrayList arrayList = JsonSerializer
            //    if (arrayList.Count > 0)
            //    {
            //        foreach (Dictionary<string, object> dictionary in arrayList)
            //        {
            //            if (dictionary.Keys.Count<string>() == 0)
            //            {
            //                result = dataTable;
            //                return result;
            //            }
            //            //Columns
            //            if (dataTable.Columns.Count == 0)
            //            {
            //                foreach (string current in dictionary.Keys)
            //                {
            //                    if (current != "data")
            //                        dataTable.Columns.Add(current, dictionary[current].GetType());
            //                    else
            //                    {
            //                        ArrayList list = dictionary[current] as ArrayList;
            //                        foreach (Dictionary<string, object> dic in list)
            //                        {
            //                            foreach (string key in dic.Keys)
            //                            {
            //                                dataTable.Columns.Add(key, dic[key].GetType());
            //                            }
            //                            break;
            //                        }
            //                    }
            //                }
            //            }
            //            //Rows
            //            string root = "";
            //            foreach (string current in dictionary.Keys)
            //            {
            //                if (current != "data")
            //                    root = current;
            //                else
            //                {
            //                    ArrayList list = dictionary[current] as ArrayList;
            //                    foreach (Dictionary<string, object> dic in list)
            //                    {
            //                        DataRow dataRow = dataTable.NewRow();
            //                        dataRow[root] = dictionary[root];
            //                        foreach (string key in dic.Keys)
            //                        {
            //                            dataRow[key] = dic[key];
            //                        }
            //                        dataTable.Rows.Add(dataRow);
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
            //catch
            //{
            //}
            //result = dataTable;
            //return result;
        //}


        /// <summary> 
        /// Datatable转换为Json 
        /// </summary> 
        /// <param name="table">Datatable对象</param> 
        /// <returns>Json字符串</returns> 
        public static string ToJson(DataTable dt)
        {
            StringBuilder jsonString = new StringBuilder();
            jsonString.Append("[");
            DataRowCollection drc = dt.Rows;
            for (int i = 0; i < drc.Count; i++)
            {
                jsonString.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    string strKey = dt.Columns[j].ColumnName;
                    string strValue = drc[i][j].ToString();
                    Type type = dt.Columns[j].DataType;
                    jsonString.Append("\"" + strKey + "\":");
                  //  strValue = StringFormat(strValue, type);
                    if (j < dt.Columns.Count - 1)
                    {
                        jsonString.Append(strValue + ",");
                    }
                    else
                    {
                        jsonString.Append(strValue);
                    }
                }
                jsonString.Append("},");
            }
            jsonString.Remove(jsonString.Length - 1, 1);
            jsonString.Append("]");
            return jsonString.ToString();
        }

    }
}
