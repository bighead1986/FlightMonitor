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
    public partial class AddPoint : Form
    {
        private string Flag;
        private string constr = Properties.Settings.Default.constr;
        private OracleHelper ora;
        public AddPoint()
        {
            InitializeComponent();
        }
        public AddPoint(string flag)
        {
            InitializeComponent();
            Flag = flag;
        }
        //private void txtY_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (!(((e.KeyChar >= '0') && (e.KeyChar <= '9')) || e.KeyChar <= 31))
        //    {
        //        if (e.KeyChar == '.')
        //        {
        //            if (((TextBox)sender).Text.Trim().IndexOf('.') > -1)
        //                e.Handled = true;
        //        }
        //        else
        //            e.Handled = true;
        //    }
        //    else
        //    {
        //        if (e.KeyChar <= 31)
        //        {
        //            e.Handled = false;
        //        }
        //        else if (((TextBox)sender).Text.Trim().IndexOf('.') > -1)
        //        {
        //            if (((TextBox)sender).Text.Trim().Substring(((TextBox)sender).Text.Trim().IndexOf('.') + 1).Length >= 4)
        //                e.Handled = true;
        //        }
        //    }
        //}

        //private void txtX_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (!(((e.KeyChar >= '0') && (e.KeyChar <= '9')) || e.KeyChar <= 31))
        //    {
        //        if (e.KeyChar == '.')
        //        {
        //            if (((TextBox)sender).Text.Trim().IndexOf('.') > -1)
        //                e.Handled = true;
        //        }
        //        else
        //            e.Handled = true;
        //    }
        //    else
        //    {
        //        if (e.KeyChar <= 31)
        //        {
        //            e.Handled = false;
        //        }
        //        else if (((TextBox)sender).Text.Trim().IndexOf('.') > -1)
        //        {
        //            if (((TextBox)sender).Text.Trim().Substring(((TextBox)sender).Text.Trim().IndexOf('.') + 1).Length >= 4)
        //                e.Handled = true;
        //        }
        //    }
        //}

      
        private bool isIP(String IpStr)
        {
            bool flag;
            string num = "(25[0-5]|2[0-4]\\d|[0-1]\\d{2}|[1-9]?\\d)";

            flag = Regex.IsMatch(IpStr, ("^" + num + "\\." + num + "\\." + num + "\\." + num + "$"));
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
        public bool CheckXY()
        {
            Regex rx = new Regex(@"^\d{1,4}$");    //匹配输入1-4位数字
            if (rx.IsMatch(txtX.Text) && rx.IsMatch(txtY.Text) && Int32.Parse(txtX.Text) <= 1024 && Int32.Parse(txtY.Text) <= 768)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool Ishave(TextBox txt)
        {
            bool flag = false;
            DataTable dt = ora.GetDt("select t.sip from B_DEVICES t");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["sip"].ToString() == txt.Text)
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
        private void button1_Click(object sender, EventArgs e)
        {
           
           // string path = System.Environment.CurrentDirectory + "//config//" + Flag + ".txt";
            //if(File.Exists(path))
            //{
            //    string[] line = File.ReadAllLines(path, Encoding.UTF8);
                //foreach(string str in line)
                //{
                //    if (str.Contains(txtname.Text + "," + txtipaddr.Text) || str.Contains(txtname.Text) || str.Contains(txtipaddr.Text))
                //    {
                //        MessageBox.Show("此点位已经存在，请重新添加！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        return;
                //    }
                //}
                //StreamWriter sw = new StreamWriter(path, true);
                //sw.WriteLine(txtname.Text+","+txtipaddr.Text+","+txtX.Text+","+txtY.Text);
                //sw.Close();
            if (checkBox1.Checked == false)
            {
                if(CheckTxtbox()==false)
                {
                    return;
                }
                try
                {
                    if (Ishave(txtipaddr) == false)
                    {
                        ora.ExecuteSql("insert into b_devices (id,sip,resource_name,pos_x,pos_y,pos_tc) values (SEQ_B_DEVICES.NEXTVAL,'" + txtipaddr.Text + "','" + txtname.Text + "','" + txtX.Text + "','" + txtY.Text + "','" + comboBox1.Text + "')");
                        Anj_Jk frm = (Anj_Jk)this.Owner;
                        frm.DeleteButton();
                        frm.AddbuttonInfo("images/" + Flag + ".jpg");
                    }
                    else
                    {
                        MessageBox.Show("此点位已经存在，请重新添加！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                if (CheckTxtbox() == false)
                {
                    return;
                }
                if (CheckInput(txttc) == false)
                {
                    txtname.BackColor = Color.Red;
                    return;
                }
                else
                {
                    ora = new OracleHelper(constr);
                    DataTable dt = ora.GetDt("select distinct t.pos_tc  from b_devices t where t.pos_tc is not null");
                    //string path = System.Environment.CurrentDirectory + "//config//Position.txt";      //读取Position文件中显示层
                    if (txtname.Text == "" || txtname.Text == null)
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
                            if (dt.Rows[i]["pos_tc"].ToString() == txtname.Text.ToString().Trim())
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
                        string dbName = "\\images\\" + txttc.Text.ToString().Trim() + "." + ext;
                        string newName = System.Environment.CurrentDirectory + dbName;
                        File.Copy(oldName, newName, true);
                        //File.AppendAllText(path, txtname.Text.ToString().Trim() + "\r\n");
                        ora.ExecuteSql("insert into b_devices (id,sip,resource_name,pos_x,pos_y,pos_tc) values (SEQ_B_DEVICES.NEXTVAL,'" + txtipaddr.Text + "','" + txtname.Text + "','" + txtX.Text + "','" + txtY.Text + "','" + txttc.Text.Trim() + "')");
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
                
        }
        private bool CheckTxtbox()
        {
            bool flag = false;
            if (CheckInput(txtname) == false)
            {
                txtname.BackColor = Color.Red;
                return flag;
            }
            if (isIP(txtipaddr.Text.Trim()) == false)
            {
                txtipaddr.BackColor = Color.Red;
                return flag;
            }
            if (CheckXY() == false)
            {
                txtX.BackColor = Color.Red;
                txtY.BackColor = Color.Red;
                return flag;
            }
            else
            {
                flag = true;
                return flag;
            }
        }

        private void 新增点位_Load(object sender, EventArgs e)
        {
            BindCombox();
        }
        private void BindCombox()
        {
            ora = new OracleHelper(constr);
            DataTable dt = ora.GetDt("select distinct t.pos_tc  from b_devices t where t.pos_tc is not null");
            comboBox1.Items.Clear();
            if (dt.Rows.Count > 0)
            {
                // string str = File.ReadAllText(path);
                //string[] line = File.ReadAllLines(path, Encoding.UTF8);
                comboBox1.DataSource = dt;
                comboBox1.ValueMember = "pos_tc";
                comboBox1.DisplayMember = "pos_tc";
                comboBox1.SelectedIndex = 0;
            }
        }

        private void txtY_TextChanged(object sender, EventArgs e)
        {
            txtname.BackColor = Color.White;
            txtipaddr.BackColor = Color.White;
            txtX.BackColor = Color.White;
            txtY.BackColor = Color.White;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked==true)
            {
                groupBox1.Visible = false;
                groupBox2.Visible = true;
            }
            else
            {
                groupBox2.Visible = false;
                groupBox1.Visible = true;
            }
        }
    }
}
