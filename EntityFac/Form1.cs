using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Configuration;

namespace EntityFac
{
    public partial class Form1 : Form
    {

        private static string queryDataBase = "select [name] from [sysdatabases] order by [name]";
        private static string queryTables = "SELECT  name FROM  sysobjects WHERE   xtype = 'U' ORDER BY name; ";
        private static string queryColumns =
@"SELECT  
            CONVERT(VARCHAR, T1.name) AS ColumnName ,--字段名
            CONVERT(VARCHAR, T2.name) AS ColumnType ,--字段类型
            T1.prec AS MaxLength ,--最大长度
            CONVERT(VARCHAR, T5.COLUMN_DEFAULT) AS DefaultValue ,--默认值
            CONVERT(VARCHAR, T4.value) AS Comment ,--描述
            T1.isnullable AS NullAble--是否为空
    FROM    syscolumns T1
            LEFT JOIN systypes T2 ON T1.xusertype = T2.xusertype
            INNER JOIN sysobjects T3 ON T1.id = T3.id
                                        AND T3.xtype = 'U '
                                        AND T3.name <> 'dtproperties '
            LEFT JOIN sys.extended_properties T4 ON T1.id = T4.major_id
                                                    AND T1.colid = T4.minor_id
            JOIN INFORMATION_SCHEMA.COLUMNS T5 ON T1.name = T5.COLUMN_NAME
                                                    AND T5.TABLE_NAME = '{0}'
    WHERE   T3.name = '{0}';";

        public Form1()
        {
            InitializeComponent();
            string connection = string.Format("Data Source={0};User Id={1};Password={2};"
                , ConfigurationManager.AppSettings["serverAddress"]
                , ConfigurationManager.AppSettings["account"]
                , ConfigurationManager.AppSettings["pwd"]);

            if (ConfigurationManager.AppSettings["database"] != "")
                connection += string.Format("Initial Catalog={0};", ConfigurationManager.AppSettings["database"]);
            this.txtConnection.Text = connection;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dt = ExecuteDataTable(this.txtConnection.Text, queryTables);
            this.cbxTables.DataSource = dt;
            this.cbxTables.ValueMember = "name";
            this.cbxTables.DisplayMember = "name";
        }



        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < cbxTables.CheckedItems.Count; i++)
            {
                DataRowView dv = ((DataRowView)cbxTables.CheckedItems[i]);
                if (dv == null) continue;
                string id = dv["TableName"].ToString();
                var temp = ExecuteDataTable(this.txtConnection.Text, string.Format(queryColumns, id));
                using (var temdp = File.Create("F:/Tmmp/" + id + ".cs"))
                {
                }
                using (StreamWriter sw = new StreamWriter("F:/Tmmp/" + id + ".cs"))
                {
                    sw.WriteLine("using System;");
                    sw.WriteLine("namespace " + "空间");
                    sw.WriteLine("{");
                    sw.WriteLine("\tpublic class " + id);
                    sw.WriteLine("\t{");
                    foreach (DataRow dr in temp.Rows)
                    {
                        sw.WriteLine("\t\t/// <summary>");
                        sw.WriteLine("\t\t/// " + dr["Comment"].ToString());
                        sw.WriteLine("\t\t/// <summary>");
                        sw.WriteLine("\t\tpublic " + dr["ColumnType"] + " " + dr["ColumnName"] + " { get; set; }");
                        sw.WriteLine("");
                    }
                    sw.WriteLine("\t}");
                    sw.WriteLine("}");
                }
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="constr"></param>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        public DataTable ExecuteDataTable(string constr, string cmdText)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(constr))
                {
                    cnn.Open();
                    DataSet ds = new DataSet();
                    try
                    {
                        SqlDataAdapter command = new SqlDataAdapter(cmdText, cnn);
                        command.Fill(ds, "ds");
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    if (ds != null && ds.Tables.Count > 0)
                        return ds.Tables[0];
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < cbxTables.Items.Count; i++)
            {
                cbxTables.SetItemCheckState(i, CheckState.Checked);
            }
        }
    }
}
