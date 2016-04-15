
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFac
{
    public static class StringExtension
    {
        public static string StartWithUpper(this string source)
        {
            if (string.IsNullOrEmpty(source)) return source;
            string first = source.Substring(0, 1).ToUpper();
            return first + source.Substring(1);
        }

        public static string ToDBType(this string type)
        {
            switch (type.ToLower())
            {
                case "binary": return "Binary";
                case "varbinary": return "VarBinary";
                case "image": return "Image";
                case "varchar": return "VarChar";
                case "nvarchar": return "NVarChar";
                case "text": return "Text";
                case "ntext": return "NText";
                case "char": return "Char";
                case "int": return "Int";
                case "nchar": return "NChar";
                case "bigint": return "BigInt";
                case "real": return "Real";
                case "float": return "Float";
                case "bit": return "Bit";
                case "tinyint": return "TinyInt";
                case "smallint": return "SmallInt";
                case "date": return "Date";
                case "timestamp": return "TimeStamp";
                case "datetime": return "DateTime";
                case "money": return "Money";
                case "smallmoney": return "SmallMoney";
                case "numeric": return "Numeric";
                case "decimal": return "Decimal";
                default: return "";
            }
        }
        public static string ToDBType(this string type, string length)
        {
            switch (type.ToLower())
            {
                case "binary": return "Binary";
                case "varbinary": return "VarBinary";
                case "image": return "Image";
                case "varchar": return string.Format("VarChar({0})", length);
                case "nvarchar": return string.Format("NVarChar({0})", length);
                case "text": return "Text";
                case "ntext": return "NText";
                case "char": return string.Format("Char({0})", length);
                case "int": return "Int";
                case "nchar": return string.Format("NChar({0})", length);
                case "bigint": return "BigInt";
                case "decimal": return "Decimal(18,2)";
                case "real": return "Real";
                case "float": return "Float";
                case "bit": return "Bit";
                case "tinyint": return "TinyInt";
                case "smallint": return "SmallInt";
                case "date": return "Date";
                case "timestamp": return "TimeStamp";
                case "datetime": return "DateTime";
                case "money": return "Money";
                case "smallmoney": return "SmallMoney";
                case "numeric": return "Numeric(18,2)";
                default: return "";
            }
        }
        public static string ToDataType(this string DBTypeValue)
        {
            string result = "";
            switch (DBTypeValue.ToLower())
            {
                case "binary":
                case "varbinary":
                case "image":
                case "varchar":
                case "nvarchar":
                case "text":
                case "ntext":
                case "char":
                case "nchar": result = "string"; break;
                case "bigint": result = "long"; break;
                case "real":
                case "float": result = "double"; break;
                case "bit": result = "bool"; break;
                case "tinyint":
                case "smallint": result = "int"; break;
                case "date":
                case "timestamp":
                case "datetime": result = "DateTime"; break;
                case "money":
                case "smallmoney":
                case "numeric": result = "decimal"; break;
                default: result = DBTypeValue; break;

            }
            return result;
        }
    }
}
