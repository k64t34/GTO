using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.IO;

/*
 using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Diagnostics;
using IWshRuntimeLibrary;
using Microsoft.Win32;
 */


namespace GTO
{
    public partial class mainForm : Form
    {
        public static string DBconnectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Workers.mdb;";
        public System.Data.OleDb.OleDbConnection Conn = new System.Data.OleDb.OleDbConnection();
        public OleDbDataAdapter adapter;
        public string PathDB = Path.GetDirectoryName(Application.ExecutablePath);

        public mainForm()
        {
            InitializeComponent();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            Cfg.loadConfig(this);

            //https://www.bestprog.net/ru/2015/12/22/002-%d0%b2%d1%8b%d0%b2%d0%be%d0%b4-%d1%82%d0%b0%d0%b1%d0%bb%d0%b8%d1%86%d1%8b-%d0%b1%d0%b0%d0%b7%d1%8b-%d0%b4%d0%b0%d0%bd%d0%bd%d1%8b%d1%85-microsoft-access-%d0%b2-%d0%ba%d0%be%d0%bc%d0%bf%d0%be/
            /*Conn.ConnectionString = @"Provider = Microsoft.Jet.OLEDB.4.0;" +
                @"Data source = " + PathDB + ";" +
                @"Extended Properties = ""text;HDR=YES;FMT=Delimited"";";
            try
                //https://www.cyberforum.ru/ado-net/thread315558.html
            {
                Conn.Open();
                String strSql;
                strSql = @"select id,title from cam.csv";

                adapter = new OleDbDataAdapter(strSql, Conn);
                adapter.Fill(dataSet1);
                int iTitle = dataSet1.Tables[0].Columns.IndexOf("title");
                //Conver codepage UTF8 to ANSI
                foreach (DataRow row in dataSet1.Tables[0].Rows)
                {
                    var bytes = Encoding.Convert(Encoding.Unicode, Encoding.GetEncoding(1251), Encoding.Unicode.GetBytes(row[iTitle].ToString()));
                    row[iTitle] = Encoding.UTF8.GetString(bytes);
                }
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.BackgroundColor = this.BackColor;
                dataGridView1.RowHeadersVisible = false;
                dataGridView1.ColumnHeadersVisible = false;
                dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
                dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                dataGridView1.AllowUserToResizeRows = false;
                dataGridView1.BorderStyle = BorderStyle.None;
                dataGridView1.DataSource = dataSet1.Tables[0];
                dataGridView1.Columns[1].DataPropertyName = "title";
                dataGridView1.Columns[0].DataPropertyName = "id";
                //dataGridView1.CurrentCellChanged += new System.EventHandler(DataGridView1CurrentCellChanged);
                //DataGridView1CurrentCellChanged(dataGridView1, null);         

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка подключения к базе данных\n" + ex.Message + "\n  " + ex.Source + "\n" + Conn.ConnectionString, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
            finally { }
            */
        }
        

        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cfg.saveConfig(this);
        }

        private void createDoc_Click(object sender, EventArgs e)
        {
          /*  AccessDataBase db = new AccessDataBase(
                new AccessTable("Images",
                    new TableColumn[]
                    {
                        new TableColumn(MDB_Id, ADOX.DataTypeEnum.adInteger, 0,
                            new ColumnProperty("Autoincrement", true)),
                        new TableColumn(MDB_IdOrig, ADOX.DataTypeEnum.adInteger, 0),
                        new TableColumn(MDB_FilePath, ADOX.DataTypeEnum.adVarWChar, 0),
                        new TableColumn(MDB_FileName, ADOX.DataTypeEnum.adVarWChar, 0),
                        new TableColumn(MDB_FileDate, ADOX.DataTypeEnum.adVarWChar, 20),
                        new TableColumn(MDB_FileTime, ADOX.DataTypeEnum.adVarWChar, 20),
                        new TableColumn(MDB_FileSize, ADOX.DataTypeEnum.adInteger, 0),
                        new TableColumn(MDB_Res, ADOX.DataTypeEnum.adInteger, 0)
                    },
                    new TableIndex[]
                    {
                        new TableIndex("id_index", true, true, MDB_Id),
                        new TableIndex("fullpath_index", false, true, MDB_FilePath, MDB_FileName)
                    }
                )
            );
          */

        }

        private void openDoc_Click(object sender, EventArgs e)
        {
        //https://vscode.ru/prog-lessons/ms-access-i-c-sharp-rabotaem-s-bd.html
        }
    }
}
