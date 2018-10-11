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

namespace FlightMonitor
{
    public partial class DelPic : Form
    {
        private string constr = Properties.Settings.Default.constr;
        private OracleHelper ora;
        public DelPic()
        {
            InitializeComponent();
        }
       // private string path = System.Environment.CurrentDirectory + "//config//Position.txt";      //读取Position文件中显示层
        private void 删除图层_Load(object sender, EventArgs e)
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

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    if (MessageBox.Show("确认删除？", "此操作将删除点位数据！", MessageBoxButtons.YesNo) == DialogResult.Yes) 
        //    {
        //       // string str = comboBox1.Text;
        //        //string pathtxt = System.Environment.CurrentDirectory + "//config//"+str+".txt";
        //        //string pathpic = System.Environment.CurrentDirectory + "//images//Position.txt"; 
        //        //if(File.Exists(pathtxt))
        //        //{
        //        //    File.Delete(pathtxt);
        //        //}
        //       // string path = "123.txt";
        //        //读入
        //        //string[] lines = System.IO.File.ReadAllLines(path);
        //        //转换
        //       // List<string> list = new List<string>();
        //        //list.AddRange(lines);
        //        //for(int i=0;i<list.Count;i++)
        //        //{
        //        //    if(list[i].ToString()==str)
        //        //    list.RemoveAt(i);
        //        //}
                
        //        //lines = list.ToArray();
        //        ////保存
        //        //File.WriteAllLines(path,lines,Encoding.UTF8);
        //       // MessageBox.Show("请检查文件！");
        //        if (ora.GetDt("select * from B_DEVICES t where t.pos_tc='"+comboBox1.SelectedValue.ToString()+"'").Rows.Count>0)
        //        {
        //            MessageBox.Show("所选图层含有点位信息，请先删除点位信息再删除图层！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            return;
        //        }
        //        ora.ExecuteSql("delete from B_DEVICES_ADDTYPE t where t.ID=" + comboBox1.SelectedValue + " ");
        //        MessageBox.Show("所选图层已成功删除！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        BindCombox();
        //    }  
        //}

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string oldName = openFileDialog1.FileName;
                string[] splitName = oldName.Split('.');
                string ext = splitName[splitName.Length - 1];
                string dbName = "\\images\\" + comboBox1.Text.ToString().Trim() + "." + ext;
                string newName = System.Environment.CurrentDirectory + dbName;
                File.Copy(oldName, newName, true);
                //File.AppendAllText(path, txtname.Text.ToString().Trim() + "\r\n");
               // ora.ExecuteSql("insert into b_devices_addtype values (SEQ_B_DEVICES_ADDTYPE.NEXTVAL,'" + textBox1.Text.Trim() + "','" + txtname.Text.Trim() + "','LGH','')");
               // string paths = System.Environment.CurrentDirectory + "//config//" + txtname.Text.ToString().Trim() + ".txt";
                //if (File.Exists(paths) == false)
                //{
                //    File.CreateText(paths);
                //}
                MessageBox.Show("图层已经变更！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //txtname.Text = "";
            }
        }
    }
}
