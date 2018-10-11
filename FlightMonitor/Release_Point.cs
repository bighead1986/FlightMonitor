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

    public partial class Release_Point : Form
    {
        private string Hostname;
        private string Ip;
        private string Flag;
        private string constr = Properties.Settings.Default.constr;
        private OracleHelper ora;
        public Release_Point()
        {
            InitializeComponent();
        }
        public Release_Point(string name, string ip, string flag)
        {
            Ip = ip;
            Hostname = name;
            Flag = flag;
            InitializeComponent();
        }
        private void 发布点位消息_Load(object sender, EventArgs e)
        {
            ora = new OracleHelper(constr);
            label4.Text = "点位名称:" + Hostname + "," + "IP地址:" + Ip;
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
        private bool CheckDTP(DateTimePicker dtpks, DateTimePicker dtpjs)
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
            if (label5.Visible == true)
            {
                label5.Text = "";
                label5.Visible = false;
            }
        }
       
        private void btnfb_Click(object sender, EventArgs e)
        {
            if (CheckInput(txtnr) == false)
            {
                label5.Text = "只能输入中文字符，标点符号，英文，数字，下划线。但不能输入@,#,$,%,^,&,*  等特殊字符";
                label5.Visible = true;
                return;
            }
            if (CheckDTP(dtpks, dtpjs) == false)
            {
                MessageBox.Show("结束时间必须大于开始时间，请重新选择时间！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {

                try
                {
                    DataTable dt = ora.GetDt("select t.id from B_DEVICES t where t.sip='" + Ip + "' ");
                    if (dt.Rows.Count > 0)
                    {
                        int d_id = Int32.Parse(dt.Rows[0]["id"].ToString());
                        ora.ExecuteSql("insert into b_devices_msg (id,devices_id,start_time,end_time,description,release_time,ter_id) values (SEQ_B_DEVICES_MSG.NEXTVAL,"+d_id+",to_date('" + dtpks.Text + "','yyyy-mm-dd hh24:mi:ss'),to_date('" + dtpjs.Text + "','yyyy-mm-dd hh24:mi:ss'),'" + txtnr.Text.Trim() + "',sysdate,1)");  //1代表T1航站楼
                        MessageBox.Show("消息发布成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("没有找到IP为"+Ip+"的相关数据，请先检查数据库！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }
    }
}
