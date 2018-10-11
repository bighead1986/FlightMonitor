using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace FlightMonitor
{
    public partial class AddPic : Form
    {
        private OracleHelper ora;
        private string constr = Properties.Settings.Default.constr;
        public AddPic()
        {
            InitializeComponent();
        }
    

        private void button1_Click(object sender, EventArgs e)
        {
            if (CheckInput(txtname) == false)
            {
                MessageBox.Show("只能输入数字，中文或者字母，请不要非法输入！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                ora = new OracleHelper(constr);
                DataTable dt = ora.GetDt("select distinct t.pos_tc  from b_devices t where t.pos_tc is not null");
                //string path = System.Environment.CurrentDirectory + "//config//Position.txt";      //读取Position文件中显示层
                if (txtname.Text == "" || txtname.Text == null )
                {
                    MessageBox.Show("请输入图层名称！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (dt.Rows.Count > 0)
                {
                    //string str = File.ReadAllText(path);
                    // string[] line = File.ReadAllLines(path, Encoding.Default);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["pos_tc"].ToString() == txtname.Text.ToString().Trim() )
                        {
                            MessageBox.Show("图层名称已经存在，请重新输入！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                }
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string oldName = openFileDialog1.FileName;
                    string[] splitName = oldName.Split('.');
                    string ext = splitName[splitName.Length - 1];
                    string dbName = "\\images\\" + txtname.Text.ToString().Trim() + "." + ext;
                    string newName = System.Environment.CurrentDirectory + dbName;
                    File.Copy(oldName, newName, true);
                    //File.AppendAllText(path, txtname.Text.ToString().Trim() + "\r\n");
                    ora.ExecuteSql("insert into b_devices_addtype values (SEQ_B_DEVICES_ADDTYPE.NEXTVAL,'','"+txtname.Text.Trim()+"','LGH','')");
                    string paths = System.Environment.CurrentDirectory + "//config//" + txtname.Text.ToString().Trim() + ".txt";
                    if (File.Exists(paths) == false)
                    {
                        File.CreateText(paths);
                    }
                    MessageBox.Show("图层已经成功添加！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtname.Text = "";
                }
            }
        }
        public bool CheckInput(TextBox txtname)
        {
             Regex rx = new Regex(@"^[\u4E00-\u9FFFA-Za-z0-9]+$");    //匹配输入的是中文，英文或者数字
             if (rx.IsMatch(txtname.Text))
           {
               return true;
            }
           else
           {
               return false;
           }
        }
       
    }

}
