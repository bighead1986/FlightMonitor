using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FlightMonitor
{
    public partial class 发布消息 : Form
    {
        private string constr = Properties.Settings.Default.constr;
        private OracleHelper ora;
        public 发布消息()
        {
            InitializeComponent();
        }

        private void 发布消息_Load(object sender, EventArgs e)
        {
            ora = new OracleHelper(constr);
            BindData();
        }
        private void BindData()
        {
          DataTable dt=ora.GetDt("select t.id,t.add_display from B_DEVICES_ADDTYPE t");
          cmbgn.DataSource = dt;
          dt.Rows.Add(-10, "空");
          cmbgn.ValueMember = "id";
          cmbgn.DisplayMember = "add_display";
          cmbgn.SelectedIndex = 0;
          //DataTable dt1 = ora.GetDt("select t.tru_id,t.tru_display from B_TRUNKS t");
          //cmbzl.DataSource = dt1;
          //cmbzl.ValueMember = "tru_id";
          //cmbzl.DisplayMember = "tru_display";
          //cmbzl.SelectedIndex = 0;
        }

        private void btnfb_Click(object sender, EventArgs e)
        {
            if(CheckInput(txtnr)==false)
            {
                label5.Text = "只能输入中文字符，标点符号，英文，数字，下划线。但不能输入@,#,$,%,^,&,*  等特殊字符";
                label5.Visible = true;
                return;
            }
                if(CheckDTP(dtpks,dtpjs)==false)
                {
                    MessageBox.Show("结束时间必须大于开始时间，请重新选择时间！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            else
            {

                try
                {
                    if (cmbgn.Text == "空")
                    {
                        ora.ExecuteSql("insert into b_devices_msg (id,addtype_id,start_time,end_time,description,release_time,ter_id) values (SEQ_B_DEVICES_MSG.NEXTVAL,null,to_date('" + dtpks.Text + "','yyyy-mm-dd hh24:mi:ss'),to_date('" + dtpjs.Text + "','yyyy-mm-dd hh24:mi:ss'),'" + txtnr.Text.Trim() + "',sysdate,1)");  //1代表T1航站楼
                        MessageBox.Show("消息发布成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Reflesh();
                    }
                    else
                    {
                        ora.ExecuteSql("insert into b_devices_msg (id,addtype_id,start_time,end_time,description,release_time,ter_id) values (SEQ_B_DEVICES_MSG.NEXTVAL," + cmbgn.SelectedValue + ",to_date('" + dtpks.Text + "','yyyy-mm-dd hh24:mi:ss'),to_date('" + dtpjs.Text + "','yyyy-mm-dd hh24:mi:ss'),'" + txtnr.Text.Trim() + "',sysdate,1)");  //1代表T1航站楼
                        MessageBox.Show("消息发布成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Reflesh();
                    }
                }
                    catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }
        private bool CheckInput(TextBox txt)
        {
           // Regex rx = new Regex(@"^[^@#$%^&*-=+]+$");
            Regex rx = new Regex(@"^[^@#$%^&*=]+$");    //匹配：中文字符，标点符号，英文，数字，下划线。但不能输入@,#,$,%,^,&,* 等特殊字符.
            if (rx.IsMatch(txt.Text))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Reflesh()
        {
            Manager_Msg glxx = (Manager_Msg)this.Owner;
            glxx.BindData();
            this.Close();
        }
        private bool CheckDTP(DateTimePicker dtpks,DateTimePicker dtpjs)
        {
            DateTime ks = DateTime.Parse(dtpks.Text);
            DateTime js = DateTime.Parse(dtpjs.Text);
            if (DateTime.Compare(js, ks) > 0)
            { 
                //Console.WriteLine("t1   >   t2"); 
                return true;
            } 
            else
            {
                return false;
            }
        }

        private void txtnr_TextChanged(object sender, EventArgs e)
        {
            if(label5.Visible==true)
            {
                label5.Text = "";
                label5.Visible = false;
            }
        }
    }
}
