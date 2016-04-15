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
using System.Diagnostics;

namespace EntityFac
{
    public partial class Form1 : Form
    {
        #region 查询语句
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
        #endregion

        public Form1()
        {
            InitializeComponent();
            var cfg = SerializeHelper.Deserialize<ConfigInfo>();
            if (cfg != null)
            {
                if (cfg.ConnectionString != "")
                    this.txtConnection.Text = ConfigurationManager.AppSettings["connectionStr"];
                else
                {
                    string connection = string.Format("Data Source={0};User Id={1};Password={2};"
                           , cfg.ServerAddress
                           , cfg.Account
                           , cfg.Password);
                    if (cfg.DataBase != "")
                        connection += string.Format("Initial Catalog={0};", cfg.DataBase);
                    this.txtConnection.Text = connection;
                }
                this.txtServerAddress.Text = cfg.ServerAddress;
                this.txtAccount.Text = cfg.Account;
                this.txtPassword.Text = cfg.Password;


                this.txtNameSpace.Text = cfg.NameSpace;
                this.txtPrefix.Text = cfg.Prefix;
                this.txtAddress.Text = cfg.FilePath;
            }
        }

        public DataTable ExecuteDataTable(string constr, string cmdText)
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

        private void ResetDBCon()
        {
            string connection = string.Format("Data Source={0};User Id={1};Password={2};"
                                , this.txtServerAddress.Text
                                , this.txtAccount.Text
                                , this.txtPassword.Text);
            if (this.cbxDataBase.DataSource != null)
            {
                connection += string.Format("Initial Catalog={0};", this.cbxDataBase.SelectedValue);
            }
            this.txtConnection.Text = connection;
        }

        private void cbxDataBase_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ResetDBCon();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnConnectSQL_Click(object sender, EventArgs e)
        {
            try
            {
                string connection = string.Format("Data Source={0};User Id={1};Password={2};"
                                   , this.txtServerAddress.Text
                                   , this.txtAccount.Text
                                   , this.txtPassword.Text);
                DataTable dt = ExecuteDataTable(connection, queryDataBase);
                this.cbxDataBase.DataSource = dt;
                this.cbxDataBase.ValueMember = "name";
                this.cbxDataBase.DisplayMember = "name";
                this.cbxDataBase.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnQueryTable_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = ExecuteDataTable(this.txtConnection.Text, queryTables);
                this.cbxTables.DataSource = dt;
                this.cbxTables.ValueMember = "name";
                this.cbxTables.DisplayMember = "name";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < cbxTables.Items.Count; i++)
                {
                    cbxTables.SetItemCheckState(i, CheckState.Checked);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCreateFile_Click(object sender, EventArgs e)
        {
            if (this.txtAddress.Text.Trim() == "")
            {
                MessageBox.Show("文件保存路径不能为空");
                return;
            }
            if (this.txtNameSpace.Text.Trim() == "")
            {
                MessageBox.Show("NameSpace不能为空");
                return;
            }
            try
            {
                var file0 = "";
                for (int i = 0; i < cbxTables.CheckedItems.Count; i++)
                {
                    DataRowView dv = ((DataRowView)cbxTables.CheckedItems[i]);
                    if (dv == null) continue;
                    string tableName = dv["name"].ToString();
                    var dt = ExecuteDataTable(this.txtConnection.Text, string.Format(queryColumns, tableName));
                    if (this.txtPrefix.Text != "" && dv["name"].ToString().StartsWith(this.txtPrefix.Text.Trim()))
                    {
                        tableName = dv["name"].ToString().Substring(this.txtPrefix.Text.Length);
                    }
                    tableName = tableName.StartWithUpper();
                    if (i == 0) file0 = this.txtAddress.Text.Trim() + "\\Entity\\" + tableName + ".cs";

                    CreateEntity(tableName, dt);
                    CreateDal(tableName, dt);
                }
                OpenFileDir(file0);
                MessageBox.Show("文件生成成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void CreateDal(string tableName, DataTable dt)
        {
            string fileName = this.txtAddress.Text.Trim() + "\\Dal\\" + tableName + "Dal.cs";
            FileInfo file = new FileInfo(fileName);
            if (!file.Directory.Exists)
                file.Directory.Create();
            using (StreamWriter sw = new StreamWriter(fileName))
            {
                sw.WriteLine("using System;");
                sw.WriteLine("using " + this.txtNameSpace.Text + ".Entity");
                sw.WriteLine("namespace " + this.txtNameSpace.Text + ".Dal");
                sw.WriteLine("{");
                sw.WriteLine("\tpublic partial class " + tableName + "Dal");
                sw.WriteLine("\t{");
                sw.WriteLine("\t\tpublic void Insert(" + tableName + "Dal model)");
                sw.WriteLine("\t\t{");
                StringBuilder sbSql = new StringBuilder();
                StringBuilder sbInsert = new StringBuilder();
                StringBuilder sbParms = new StringBuilder();
                foreach (DataRow dr in dt.Rows)
                {
                    var columnName = dr["ColumnName"].ToString().StartWithUpper();
                    sbSql.AppendLine(string.Format(",[{0}]=@{0}", columnName));
                    sw.WriteLine("\t\t\tnew SqlParameter(\"@" + columnName + "\",SqlDbType." + GetDBType(dr["ColumnType"].ToString()) + ","
                        + dr["MaxLength"].ToString() + ") { Value=model." + columnName + " };");
                    sbInsert.AppendFormat(",[{0}]", columnName);
                    sbParms.AppendFormat(",@{0}", columnName);
                }
                sw.WriteLine("\t\t string sqlUpdate=\n@\"" + sbSql.ToString() + "\";");
                sw.WriteLine("\t\t string sqlInsert=@\"(" + sbInsert.ToString().TrimStart(',') + ") values (" + sbParms.ToString().TrimStart(',') + ")\";");
                sw.WriteLine("\t\t}");
                sw.WriteLine("\t}");
                sw.WriteLine("}");

            }
        }

        private void CreateEntity(string tableName, DataTable dt)
        {
            string fileName = this.txtAddress.Text.Trim() + "\\Entity\\" + tableName + ".cs";
            FileInfo file = new FileInfo(fileName);
            if (!file.Directory.Exists)
                file.Directory.Create();
            using (StreamWriter sw = new StreamWriter(fileName))
            {
                sw.WriteLine("using System;");
                sw.WriteLine("namespace " + this.txtNameSpace.Text + ".Entity");
                sw.WriteLine("{");
                sw.WriteLine("\tpublic partial class " + tableName);
                sw.WriteLine("\t{");
                foreach (DataRow dr in dt.Rows)
                {
                    sw.WriteLine("\t\t/// <summary>");
                    sw.WriteLine("\t\t/// " + dr["Comment"].ToString());
                    sw.WriteLine("\t\t/// <summary>");
                    sw.WriteLine("\t\tpublic " + GetDataType(dr["ColumnType"].ToString()) + " " + dr["ColumnName"].ToString().StartWithUpper() + " { get; set; }");
                }
                sw.WriteLine("\t}");
                sw.WriteLine("}");
            }
        }

        private void OpenFileDir(string filePath)
        {
            Process open = new Process();
            open.StartInfo.FileName = "explorer";
            open.StartInfo.Arguments = @"/select," + filePath;
            open.Start();
        }

        private void btnSelectPath_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog path = new FolderBrowserDialog();
                path.ShowDialog();
                this.txtAddress.Text = path.SelectedPath;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateConfig(string key, string value)
        {
            Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            cfa.AppSettings.Settings[key].Value = "value";
            cfa.Save();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConfigInfo cfg = new ConfigInfo();
            cfg.Account = this.txtAccount.Text;
            cfg.ConnectionString = this.txtConnection.Text;
            if (this.cbxDataBase.DataSource != null)
                cfg.DataBase = this.cbxDataBase.SelectedValue.ToString();
            cfg.FilePath = this.txtAddress.Text;
            cfg.NameSpace = this.txtNameSpace.Text;
            cfg.Password = this.txtPassword.Text;
            cfg.Prefix = this.txtPrefix.Text;
            cfg.ServerAddress = this.txtServerAddress.Text;
            SerializeHelper.Serialize<ConfigInfo>(cfg);
        }

        private string GetDBType(string type)
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
                default: return "";
            }
        }
        private string GetDataType(string DBTypeValue)
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
