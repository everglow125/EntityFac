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
            List<string> codeTypes = new List<string> { "SqlParameter", "Insert", "Update", "Declare" };
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

                this.cbxCodeType.DataSource = codeTypes;
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
                MessageBox.Show(this, ex.Message, "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(this, ex.Message, "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnQueryTable_Click(object sender, EventArgs e)
        {
            try
            {
                this.chkTables.DataSource = null;
                this.cbxTable.DataSource = null;

                DataTable dt = ExecuteDataTable(this.txtConnection.Text, queryTables);
                this.chkTables.DataSource = dt;
                this.chkTables.ValueMember = "name";
                this.chkTables.DisplayMember = "name";

                this.cbxTable.DataSource = dt;
                this.cbxTable.ValueMember = "name";
                this.cbxTable.DisplayMember = "name";
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < chkTables.Items.Count; i++)
                {
                    chkTables.SetItemCheckState(i, CheckState.Checked);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCreateFile_Click(object sender, EventArgs e)
        {
            if (this.txtAddress.Text.Trim() == "")
            {
                MessageBox.Show(this, "文件保存路径不能为空", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (this.txtNameSpace.Text.Trim() == "")
            {
                MessageBox.Show(this, "NameSpace不能为空", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                var file0 = "";
                int selectCount = 0;
                for (int i = 0; i < chkTables.CheckedItems.Count; i++)
                {
                    DataRowView dv = ((DataRowView)chkTables.CheckedItems[i]);
                    if (dv == null) continue;
                    selectCount++;
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
                if (selectCount == 0)
                    MessageBox.Show(this, "『未选中任何表格』", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    OpenFileDir(file0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                StringBuilder sbDeclare = new StringBuilder();
                foreach (DataRow dr in dt.Rows)
                {
                    var columnName = dr["ColumnName"].ToString().StartWithUpper();
                    sbSql.AppendLine(string.Format(",[{0}]=@{0}", columnName));
                    sw.WriteLine("\t\t\tnew SqlParameter(\"@" + columnName + "\",SqlDbType." + dr["ColumnType"].ToString().ToDBType() + ","
                        + dr["MaxLength"].ToString() + ") { Value=model." + columnName + " };");
                    sbInsert.AppendFormat(",[{0}]", columnName);
                    sbParms.AppendFormat(",@{0}", columnName);
                    sbDeclare.AppendFormat("\nDECLARE @{0} {1};", columnName, dr["columnType"].ToString().ToDBType(dr["MaxLength"].ToString()));
                }
                sw.WriteLine("\t\t string sqlUpdate=\n@\"" + sbSql.ToString() + "\";");
                sw.WriteLine("\t\t string sqlInsert=@\"(" + sbInsert.ToString().TrimStart(',') + ") values (" + sbParms.ToString().TrimStart(',') + ")\";");
                sw.WriteLine("\t\t string declare=\n@\"" + sbDeclare.ToString() + "\";");
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
                    sw.WriteLine("\t\tpublic " + dr["ColumnType"].ToString().ToDataType() + " " + dr["ColumnName"].ToString().StartWithUpper() + " { get; set; }");
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
            MessageBox.Show(this, "保存配置成功", "异常", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btn_CreateCode_Click(object sender, EventArgs e)
        {
            string codeType = this.cbxCodeType.SelectedValue.ToString();
            string tableName = this.cbxTable.SelectedValue.ToString();
            var dt = ExecuteDataTable(this.txtConnection.Text, string.Format(queryColumns, tableName));


            StringBuilder sbSql = new StringBuilder();
            StringBuilder sbParm = new StringBuilder();

            foreach (DataRow dr in dt.Rows)
            {
                var columnName = dr["ColumnName"].ToString().StartWithUpper();
                if (codeType == "Update")
                    sbSql.AppendLine(string.Format(",[{0}]=@{0}", columnName));
                else if (codeType == "Insert")
                {
                    sbSql.AppendFormat(",[{0}]", columnName);
                    sbParm.AppendFormat(",@{0}", columnName);
                }
                else if (codeType == "Declare")
                    sbSql.AppendFormat("\r\nDECLARE @{0} {1};", columnName, dr["columnType"].ToString().ToDBType(dr["MaxLength"].ToString()));
                else
                    sbSql.Append("\r\nnew SqlParameter(\"@" + columnName + "\",SqlDbType." + dr["ColumnType"].ToString().ToDBType() + ","
                        + dr["MaxLength"].ToString() + ") { Value=model." + columnName + " };");

            }
            if (codeType == "Insert") this.rtxtCode.Text = string.Format("INSERT INTO {2}\r\n({0})\r\nVALUES\r\n({1})"
                , sbSql.ToString().TrimStart(','), sbParm.ToString().TrimStart(','), tableName);
            else if (codeType == "Update")
                this.rtxtCode.Text = string.Format("UPDATE {1} WITH(ROWLOCK) \r\nSET {0})", sbSql.ToString().TrimStart(','), tableName);
            else
                this.rtxtCode.Text = sbSql.ToString().TrimStart(',');
            if (this.chxCopy.Checked)
                CopyText(this.rtxtCode.Text);

        }


        /// <summary>
        /// 复制或剪切文件至剪贴板(方法）
        /// </summary>
        /// <param name="files">需要添加到剪切板的文件路径数组</param>
        /// <param name="cut">是否剪切true为剪切，false为复制</param>
        public static void CopyToClipboard(string[] files, bool cut)
        {
            if (files == null) return;
            IDataObject data = new DataObject(DataFormats.FileDrop, files);
            MemoryStream memo = new MemoryStream(4);
            byte[] bytes = new byte[] { (byte)(cut ? 2 : 5), 0, 0, 0 };
            memo.Write(bytes, 0, bytes.Length);
            data.SetData("Preferred DropEffect", memo);
            Clipboard.SetDataObject(data);
        }
        /// <summary>
        /// 获取剪贴板中的文件列表（方法）
        /// </summary>
        /// <returns>System.Collections.List<string>返回剪切板中文件路径集合</returns>
        public static List<string> GetClipboardList()
        {
            List<string> clipboardList = new List<string>();
            System.Collections.Specialized.StringCollection sc = Clipboard.GetFileDropList();
            for (int i = 0; i < sc.Count; i++)
            {
                string listfileName = sc[i];
                clipboardList.Add(listfileName);
            }
            return clipboardList;
        }


        //复制： 
        private void CopyText(string source)
        {
            Clipboard.SetDataObject(source);
        }

        //粘贴： 
        private void PasteText(object sender, System.EventArgs e)
        {
            IDataObject iData = Clipboard.GetDataObject();
            if (iData.GetDataPresent(DataFormats.Text))
            {
                var temp = (String)iData.GetData(DataFormats.Text);
            }
        }



    }
}
