using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace FlightMonitor
{
    public partial class Edit_msg : Form
    {
        private string Hostname;
        private string Ip;
        private string Flag;
        private string constr = Properties.Settings.Default.constr;
        private OracleHelper ora;
        public Edit_msg()
        {
            InitializeComponent();
        }
        public Edit_msg(string name,string ip,string flag)
        {
            Ip = ip;
            Hostname = name;
            Flag = flag;
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            CheckTxtbox();
            try
            {
                if (Ishave(txtipaddr) == false)
                {
                    ora.ExecuteSql("update b_devices t set t.sip='" + txtipaddr.Text + "',t.resource_name='" + txtname.Text + "',t.pos_tc='" + comboBox1.SelectedValue.ToString() + "' where t.sip='" + Ip + "' and t.resource_name='" + Hostname + "'");
                    MessageBox.Show("此点位信息已经更新成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //string path = System.Environment.CurrentDirectory + "//config//" + Flag + ".txt";
                    //string[] lines = System.IO.File.ReadAllLines(path);
                    ////转换
                    //List<string> list = new List<string>();
                    //list.AddRange(lines);
                    //for (int i = 0; i < list.Count; i++)
                    //{
                    //    if (list[i].ToString().Contains(Hostname + "," + Ip))
                    //    {
                    //        list[i] = txtname.Text + "," + txtipaddr.Text + "," + txtX.Text + "," + txtY.Text;
                    //    }
                    //}

                    //lines = list.ToArray();
                    ////保存
                    //File.WriteAllLines(path, lines, Encoding.UTF8);
                    //File.w
                    // MessageBox.Show(lines.ToString());
                    //MessageBox.Show("此点位信息已经更新成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //this.Close();
                    Anj_Jk frm = (Anj_Jk)this.Owner;
                    frm.DeleteButton();
                    frm.AddbuttonInfo("images/" + Flag + ".jpg");
                }
                else
                {
                    MessageBox.Show(txtipaddr.Text + "已经存在，请不要重复更新！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool Ishave(TextBox txt)
        {
            bool flag=false;
            DataTable dt = ora.GetDt("select t.sip from B_DEVICES t");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["sip"].ToString() == txt.Text && txt.Text!=Ip)
                    {
                        flag = true;
                        break;
                    }
                }
                return flag;
            }
            else
            {
                flag = false;
                return flag;
            }

        }
        private void CheckTxtbox()
        {
            if (isIP(txtipaddr.Text.Trim()) == false)
            {
                txtipaddr.BackColor = Color.Red;
                return;
            }
            if (CheckInput(txtname) == false)
            {
                txtname.BackColor = Color.Red;
                return;
            }
            if (txtX.Text == "" || txtX.Text == null)
            {
                txtX.BackColor = Color.Red;
                return;
            }
            if (txtY.Text == "" || txtY.Text == null)
            {
                txtY.BackColor = Color.Red;
                return;
            }
        }

        private void 编辑点位_Load(object sender, EventArgs e)
        {
            //ShowForm();
            BindCombox();
            ShowInfo();
           
        }
        private void BindCombox()
        {
            ora = new OracleHelper(constr);
            DataTable dt = ora.GetDt("select * from B_DEVICES_ADDTYPE t where t.terminal='LGH'");
            comboBox1.Items.Clear();
            if (dt.Rows.Count > 0)
            {
                // string str = File.ReadAllText(path);
                //string[] line = File.ReadAllLines(path, Encoding.UTF8);
                comboBox1.DataSource = dt;
                comboBox1.ValueMember = "ID";
                comboBox1.DisplayMember = "ADD_DISPLAY";
              //  comboBox1.Items.Clear();
                comboBox1.Text = Flag;
            }
        }
       private void ShowInfo()
        {
            //读入
            //string path = System.Environment.CurrentDirectory + "//config//"+Flag+".txt";      //读取各个层配置文件的详细信息
            //string[] lines = System.IO.File.ReadAllLines(path);
            ////转换
            //List<string> list = new List<string>();
            //list.AddRange(lines);
            DataTable dt = ora.GetDt("select t.sip,t.resource_name,t.pos_x,t.pos_y from B_DEVICES t where t.pos_tc='"+Flag+"'");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
               // MessageBox.Show(dt.Rows[i].ToString());
                if (dt.Rows[i]["resource_name"].ToString()==Hostname&&dt.Rows[i]["sip"].ToString()==Ip)
                {
                    //string[] line = list[i].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    txtname.Text = dt.Rows[i]["resource_name"].ToString();
                    txtipaddr.Text = dt.Rows[i]["sip"].ToString();
                    txtX.Text = dt.Rows[i]["pos_x"].ToString();
                    txtY.Text = dt.Rows[i]["pos_y"].ToString();
                    return;
                  
                }
            }

           // lines = list.ToArray();
            //保存
           // File.WriteAllLines(path, lines, Encoding.UTF8);
            // MessageBox.Show("请检查文件！");
          //  MessageBox.Show("所选图层已成功删除！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

       private void checkBox1_CheckedChanged(object sender, EventArgs e)
       {
           if(checkBox1.Checked)
           {
               txtname.ReadOnly = false;
               txtipaddr.ReadOnly = false;
           }
           else
           {
               txtname.ReadOnly = true;
               txtipaddr.ReadOnly = true;
           }
       }
       private bool isIP(String IpStr)
       {
           bool flag;
           string num = "(25[0-5]|2[0-4]\\d|[0-1]\\d{2}|[1-9]?\\d)";

          flag= Regex.IsMatch(IpStr, ("^" + num + "\\." + num + "\\." + num + "\\." + num + "$"));
          return flag;
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

       /**/
       /// <summary>
       /// 控制只能输入整数或小数
       /// (小数位最多位4位，小数位可以自己修改)
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
       private void txtX_KeyPress(object sender, KeyPressEventArgs e)
       {
           if (!(((e.KeyChar >= '0') && (e.KeyChar <= '9')) || e.KeyChar <= 31))
           {
               if (e.KeyChar == '.')
               {
                   if (((TextBox)sender).Text.Trim().IndexOf('.') > -1)
                       e.Handled = true;
               }
               else
                   e.Handled = true;
           }
           else
           {
               if (e.KeyChar <= 31)
               {
                   e.Handled = false;
               }
               else if (((TextBox)sender).Text.Trim().IndexOf('.') > -1)
               {
                   if (((TextBox)sender).Text.Trim().Substring(((TextBox)sender).Text.Trim().IndexOf('.') + 1).Length >= 4)
                       e.Handled = true;
               }
           }
       }

       private void txtY_KeyPress(object sender, KeyPressEventArgs e)
       {
           if (!(((e.KeyChar >= '0') && (e.KeyChar <= '9')) || e.KeyChar <= 31))
           {
               if (e.KeyChar == '.')
               {
                   if (((TextBox)sender).Text.Trim().IndexOf('.') > -1)
                       e.Handled = true;
               }
               else
                   e.Handled = true;
           }
           else
           {
               if (e.KeyChar <= 31)
               {
                   e.Handled = false;
               }
               else if (((TextBox)sender).Text.Trim().IndexOf('.') > -1)
               {
                   if (((TextBox)sender).Text.Trim().Substring(((TextBox)sender).Text.Trim().IndexOf('.') + 1).Length >= 4)
                       e.Handled = true;
               }
           }
       }

       private void txtname_TextChanged(object sender, EventArgs e)
       {
           txtname.BackColor = Color.White;
           txtipaddr.BackColor = Color.White;
       }
    }
}
