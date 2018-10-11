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
    public partial class Manager_Msg : Form
    {
        private string Hostname;
        private string Ip;
        private string Flag;
        private string constr = Properties.Settings.Default.constr;
        private OracleHelper ora;
        public Manager_Msg()
        {
            InitializeComponent();
        }
        public Manager_Msg(string name, string ip, string flag)
        {
            Ip = ip;
            Hostname = name;
            Flag = flag;
            InitializeComponent();
        }
        private void 管理消息_Load(object sender, EventArgs e)
        {
            ora = new OracleHelper(constr);
            BindData();
        }
        public void BindData()
        {
            string sql = "";
            sql = "select t.id ID,a.add_display  位置,d.resource_name 设备,t.start_time 开始,t.end_time 结束,tr.tru_name 指廊,ter.ter_name 航站楼,t.description 消息 from ";
            sql += " B_DEVICES_MSG t,b_devices_addtype a,b_devices_funtype f,b_devices d,b_terminals ter,b_trunks tr where";
            sql += " a.id(+)=t.addtype_id and f.id(+)=t.funtype_id and d.id(+)=t.devices_id and tr.tru_id(+)=t.tru_id and ter.ter_id(+)=t.ter_id";
            sql += " order by t.id desc";
            DataTable dt = ora.GetDt(sql);
            dgv_msg.DataSource = dt;
            for (int i = 0; i < dgv_msg.Columns.Count; i++)
            {
                if (dgv_msg.Columns[i].Name == "消息")
                {
                    dgv_msg.Columns[i].Width = 460;
                    // dgv_msg.Columns[i].DefaultCellStyle = Color.Red;
                    //break;
                }
                if (dgv_msg.Columns[i].Name == "ID" || dgv_msg.Columns[i].Name == "设备" || dgv_msg.Columns[i].Name == "指廊" || dgv_msg.Columns[i].Name == "航站楼")
                {
                    dgv_msg.Columns[i].Width = 65;
                }
            }
            // string sqlcount = "select count(*) from B_DEVICES_MSG t,b_devices_addtype a,b_devices_funtype f,b_devices d,b_terminals ter,b_trunks tr where a.id(+)=t.addtype_id and f.id(+)=t.funtype_id and d.id(+)=t.devices_id and tr.tru_id(+)=t.tru_id and ter.ter_id(+)=t.ter_id";
            label1.Text = "消息总数:" + ora.GetRecordCount(sql);

        }

        private void dgv_msg_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show(dgv_msg.Rows[dgv_msg.CurrentRow.Index].Cells["ID"].Value.ToString());
            ShowInfo();
        }
        private void ShowInfo()
        {
            txt_id.Text = dgv_msg.Rows[dgv_msg.CurrentRow.Index].Cells["ID"].Value.ToString();
            //txb_name.Text = dgv_msg.Rows[dgv_msg.CurrentRow.Index].Cells["设备"].Value.ToString();
            txb_msg.Text = dgv_msg.Rows[dgv_msg.CurrentRow.Index].Cells["消息"].Value.ToString();
            Bindcombox("select t.id,t.add_display from B_DEVICES_ADDTYPE t order by t.id asc", "id", "add_display", cmb_wz, "位置");
            Bindcombox("select t.ter_id,t.ter_name from B_TERMINALS t order by t.ter_id asc", "ter_id", "ter_name", cmb_hzl, "航站楼");
            Bindcombox("select t.tru_id,t.tru_name from B_TRUNKS t order by t.tru_id asc", "tru_id", "tru_name", cmb_zl, "指廊");
            Bindcombox("select t.id,t.resource_name from B_DEVICES t order by t.id asc", "id", "resource_name", cmb_sb, "设备");
            dtp_ks.Text = dgv_msg.Rows[dgv_msg.CurrentRow.Index].Cells["开始"].Value.ToString();
            dtp_js.Text = dgv_msg.Rows[dgv_msg.CurrentRow.Index].Cells["结束"].Value.ToString();
        }
        private void Bindcombox(string sql, string value, string display, ComboBox cmb, string cellname)
        {
            DataTable dt = ora.GetDt(sql);
           // MessageBox.Show(cmb.Name);
            cmb.DataSource = dt;
            if (cmb.Name != "cmb_hzl")
            {
                dt.Rows.Add(-10, "空");
            }
            cmb.DisplayMember = display;
            cmb.ValueMember = value;
           // cmb.Items.Add("空");
            if (dgv_msg.Rows[dgv_msg.CurrentRow.Index].Cells[cellname].Value.ToString() != "")
            {
                cmb.Text = dgv_msg.Rows[dgv_msg.CurrentRow.Index].Cells[cellname].Value.ToString();
            }
            else
            {
                cmb.SelectedIndex = cmb.Items.Count-1;
            }
        }
        private void btn_gx_Click(object sender, EventArgs e)
        {
            string sql = "";
            if (CheckInput(txb_msg) == false)
            {
                label10.Text = "只能输入中文字符，标点符号，英文，数字，下划线。但不能输入@,#,$,%,^,&,*  等特殊字符";
                label10.Visible = true;
                return;
            }
            else
            {
               //cmb_zl.Text != "" ? cmb_zl.SelectedValue : null
                if (txt_id.Text == "")
                {
                    MessageBox.Show("你未选择任何行，请先双击需要更新的行！", "提示！", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                   // MessageBox.Show(cmb_sb.Text == "空" ? null : Int32.Parse(cmb_sb.SelectedValue.ToString()).ToString());
                    sql = "update b_devices_msg t set t.addtype_id='" + Getcmb(cmb_wz) + "',t.devices_id='" + Getcmb(cmb_sb) + "',t.release_time=sysdate,";
                    sql+= " t.start_time=to_date('" + dtp_ks.Text + "','yyyy-mm-dd hh24:mi:ss'),t.end_time=to_date('" + dtp_js.Text + "','yyyy-mm-dd hh24:mi:ss'),";
                    sql += " t.description='" + txb_msg.Text + "',t.tru_id='" + Getcmb(cmb_zl) + "',t.ter_id='" + Getcmb(cmb_hzl) + "' ";
                    sql+= " where t.id=" + Int32.Parse(txt_id.Text) + "";
                    try
                    {
                        ora.ExecuteSql(sql);
                        MessageBox.Show("ID为" + txt_id.Text + "消息已经成功更新！", "提示！", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        BindData();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "错误！", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                
            }
        }
        private string Getcmb(ComboBox cmb)
        {
            if (cmb.Text == "空")
            {
                return "";
            }
            else
            {
                return cmb.SelectedValue.ToString();
            }
        }

        private void Clear()
        {
            txt_id.Text = "";
            txb_msg.Text = "";
            cmb_sb.SelectedIndex = -1;
            cmb_zl.SelectedIndex = -1;
            cmb_wz.SelectedIndex = -1;
            cmb_hzl.SelectedIndex = -1;
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
        private void btn_del_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确认删除？", "此删除不可恢复", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {

                if (txt_id.Text != "")
                {
                    try
                    {
                        ora.ExecuteSql("delete from b_devices_msg t where t.id=" + dgv_msg.Rows[dgv_msg.CurrentRow.Index].Cells["ID"].Value + "");
                        Clear();
                        MessageBox.Show("ID为" + dgv_msg.Rows[dgv_msg.CurrentRow.Index].Cells["ID"].Value.ToString()+"的消息已经成功删除！", "提示！", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        BindData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "错误！", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("你未选择任何行，请先双击需要删除的行！", "警告！", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txb_msg_TextChanged(object sender, EventArgs e)
        {
            if (label10.Visible == true)
            {
                label10.Visible = false;
            }
        }

        private void btn_fb_Click(object sender, EventArgs e)
        {
            发布消息 frm = new 发布消息();
            frm.Owner = this;
            frm.ShowDialog();
        }
    }
}
