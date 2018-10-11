using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Collections;
using System.Net.Sockets;
using System.Xml;
using System.Net;
using System.Runtime.InteropServices;
using System.Net.NetworkInformation;
using System.Data.OracleClient;
namespace FlightMonitor
{
    public partial class Anj_Jk : Form
    {
        public Anj_Jk()
        {
            InitializeComponent();
        }
        //inifile log = new inifile();
        //private string NoWeather = Properties.Settings.Default.NoWeather;
        //private string NoMsg = Properties.Settings.Default.NoMsg;
        //private string NoWeatherMsg = Properties.Settings.Default.NoWeatherMsg;
        //private string WeatherMsg = Properties.Settings.Default.WeatherMsg;
       
        private string constr = Properties.Settings.Default.constr;
        private OracleHelper ora;
        private Point mouse;
        private string Pathexe = Properties.Settings.Default.Pathexe;
        private int timer = Properties.Settings.Default.Timer;
        public string Name;           //全局变量，用来判断当前界面是出港大厅还是进港大厅 还是其它等等
        //private string Path_cg= Properties.Settings.Default.Path_cg;
        //private string Path_jg = Properties.Settings.Default.Path_jg;
        //private string Pic_cg = Properties.Settings.Default.Pic_cg;
        //private string Pic_jg = Properties.Settings.Default.Pic_jg;
        private string Pic_index = Properties.Settings.Default.Pic_indxe;
      //  private Control ctrbutton = null;
        //创建一个Thread类
        public Control ctrl=null;//全局变量，保存获得当前右键的控件
        private Thread thread;
        private Thread thread1;
        private Thread thread2;
        //创建一个UdpClient对象，来接收消息
        private UdpClient udpReceive;
        private UdpClient udpSend;
        /// 保存所有客户端会话的哈希表
        /// </summary>
        private Hashtable Info = new Hashtable();
       // XmlDocument xmlDoc = new XmlDocument();
        private DataTable mytab;//= new DataTable("tcpconn"); 
        private IPEndPoint remoteIpEndIPoint = new IPEndPoint(IPAddress.Any, 8002);
       private Rectangle rect = Screen.PrimaryScreen.Bounds;
       private ArrayList list = new ArrayList();
        private void 航显监控_Load(object sender, EventArgs e)
        {
            Connect();
            timer1.Interval = timer * 1000;
           // log.writelog_server(DateTime.Now.ToLongDateString(), "服务端程序成功开启" + "      " + "|时间：" + DateTime.Now.ToString());
            //初始化该线程并指定线程执行时要调用的方法
            CheckForIllegalCrossThreadCalls = false;
            thread1 = new Thread(new ThreadStart(ReceiveMessage));
            thread1.IsBackground = true;
            //启动线程
            thread1.Start();
            //LoadXML();
            //LoadChecklist();
            LoadForm();
            Addmenu();
           // ShowInfo(button1);
           // Setposition_button(button1);
           // AddbuttonInfo(Path_cg,Pic_cg);
            pictureBox1.Image = Image.FromFile(Pic_index);
           // MessageBox.Show(Name);
          
        }
        public void Connect()
        {
           ora = new OracleHelper(constr);
            try
            {
                ora.ExecuteSql("select * from B_DEVICES t");

            }

            catch(Exception e)

          {

              throw new Exception(e.Message);

          }
        }
               
            

        
        public void DeleteButton()
        {
            foreach (Control item in this.pictureBox1.Controls)
            {
                if (item is Button)
                {
                    this.pictureBox1.Controls.Remove(item);
                }
            }
            if (this.pictureBox1.Controls!=null)
            {
                this.pictureBox1.Controls.Clear();
            }
        }
        private void Setposition_button(Button btn,float width,float height)    //动态设置各个航线点（button）的位置
        {
            Point pt = new Point();
            pt.X =Int32.Parse(Math.Round(((rect.Width) * width)).ToString());
            pt.Y = Int32.Parse(Math.Round(((rect.Height) * height)).ToString());
            btn.Location=pt;
        }
      private void CheckOnline()          //检查各个信息点网络情况，在线则将背景色改为绿色，断线则改为红色
        {
            try
            {
                foreach (string str in list)
                {
                    string[] txt = str.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    if (Info[txt[0]] != null)
                    {
                        if (str.Contains(Info[txt[0]].ToString()))
                        {
                            //Button button = (Button)txt[0];
                            //MessageBox.Show(txt[0]);
                            Button btn = (Button)pictureBox1.Controls[txt[0]];
                            btn.Image = Image.FromFile("png/连接.png");
                        }

                    }
                    else
                    {
                        Button btn = (Button)pictureBox1.Controls[txt[0]];
                        if (btn != null)
                        {
                            btn.Image = Image.FromFile("png/断开.png");
                        }
                    }
                }
            }
          catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadForm()           //根据屏幕分辨率自动设置窗体大小
        {
           
            int h = rect.Height; //高（像素）
            int w = rect.Width;  //宽（像素）
            this.Width = w;
            this.Height = h;
        }
        private void Addmenu()     //根据txt文件，动态添加菜单
        {
            //添加菜单一
            ToolStripMenuItem subItem;
            subItem = AddContextMenu("航显位置", menuStrip1.Items, null);
         // string path = System.Environment.CurrentDirectory+"//config//Position.txt";
            ora = new OracleHelper(constr);
           // MessageBox.Show(path);
            DataTable dt = ora.GetDt("select distinct t.pos_tc  from b_devices t where t.pos_tc is not null");
            if (dt.Rows.Count>0)
          {
             // string[] str = File.ReadAllLines(path, Encoding.UTF8);
              //FileStream fs = new FileStream(path, FileMode.Open);
              //StreamReader sr = new StreamReader(fs, Encoding.Default);
              //string line = sr.re
              //sr.Close();
              //fs.Close();
              //string[] str = line.Split('@');
              for(int i=0;i<dt.Rows.Count;i++)
              {
                  AddContextMenu(dt.Rows[i]["pos_tc"].ToString(), subItem.DropDownItems, new EventHandler(MenuClicked));    //添加子菜单
              }
             // MessageBox.Show(line);
             
          }
            else
          {
              MessageBox.Show("数据库无任何数据，请检查！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
          }

          //添加菜单二
          subItem = AddContextMenu("人工操作", menuStrip1.Items, null);
          //添加子菜单
          AddContextMenu("重启全部终端", subItem.DropDownItems, new EventHandler(MenuClicked));
          AddContextMenu("重启全部程序", subItem.DropDownItems, new EventHandler(MenuClicked));
          AddContextMenu("关闭全部终端", subItem.DropDownItems, new EventHandler(MenuClicked));
         // AddContextMenu("发布消息", subItem.DropDownItems, new EventHandler(MenuClicked));
          AddContextMenu("管理消息", subItem.DropDownItems, new EventHandler(MenuClicked));
         // AddContextMenu("远程开机全部终端", subItem.DropDownItems, new EventHandler(MenuClicked));
          //添加菜单三
          subItem = AddContextMenu("配置编辑", menuStrip1.Items, null);
          //添加子菜单
         // AddContextMenu("新增图层", subItem.DropDownItems, new EventHandler(MenuClicked));
          AddContextMenu("变更图层", subItem.DropDownItems, new EventHandler(MenuClicked));
          AddContextMenu("新增点位", subItem.DropDownItems, new EventHandler(MenuClicked));
          AddContextMenu("重启本程序", subItem.DropDownItems, new EventHandler(MenuClicked));
         // AddContextMenu("删除点位", subItem.DropDownItems, new EventHandler(MenuClicked));
         
        }
        public void AddbuttonInfo(string picPath)
        {
            try
            {
                string paths = System.Environment.CurrentDirectory + "//config//Size.txt";      //读取Size文件中button的size值（宽和高）
                FileStream fss = new FileStream(paths, FileMode.Open);
                StreamReader srr = new StreamReader(fss, Encoding.Default);
                string lines = srr.ReadToEnd();
                srr.Close();
                fss.Close();
                string[] size = lines.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                DataTable dt = ora.GetDt("select t.id, t.resource_name,t.sip,t.pos_x,t.pos_y,t.pos_tc from B_DEVICES t");
                //string path = System.Environment.CurrentDirectory + configPath;
                pictureBox1.Image = Image.FromFile(picPath);
                // MessageBox.Show(path);
                if (dt.Rows.Count>0)
                {
                    //FileStream fs = new FileStream(path, FileMode.Open);                    //读取出港大厅配置文件详细信息
                   // StreamReader sr = new StreamReader(fs, Encoding.Default);
                   // string line = sr.ReadToEnd();
                    //sr.Close();
                   // fs.Close();
                   // string[] str = line.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    //Button[] btn = new Button[str.Length];
                  
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if(dt.Rows[i]["pos_tc"].ToString()=="")
                        {
                            ora.ExecuteSql("update b_devices t set t.pos_x='1024',t.pos_y='768',t.pos_tc='"+Name+"' where t.id="+Int32.Parse(dt.Rows[i]["id"].ToString())+"");
                        }
                    }
                }
                dt=ora.GetDt("select t.id, t.resource_name,t.sip,t.pos_x,t.pos_y,t.pos_tc from B_DEVICES t");
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["pos_tc"].ToString() == Name)
                        {
                            if (list.Contains(dt.Rows[i]["resource_name"].ToString() + "," + dt.Rows[i]["sip"].ToString()) == false)
                            {
                                list.Add(dt.Rows[i]["resource_name"].ToString() + "," + dt.Rows[i]["sip"].ToString());
                            }
                            Button btn = new Button();                               //动态添加button按钮
                            btn.Parent = this.pictureBox1;
                            btn.ContextMenuStrip = contextMenuStrip1;

                            //btn.Top = 25 * i;
                            btn.Name = dt.Rows[i]["resource_name"].ToString();
                            btn.Text = dt.Rows[i]["sip"].ToString();
                            // btn.ContextMenuStrip = 
                            btn.Size = new System.Drawing.Size(Int32.Parse(size[0]), Int32.Parse(size[1]));
                            // btn.BackColor = Color.Red;
                            btn.Image = Image.FromFile("png/断开.png");
                            btn.Font = new System.Drawing.Font(btn.Font.FontFamily, 1, btn.Font.Style);
                            //btn.Left = Int32.Parse(Math.Round(((rect.Width) * float.Parse(info[2]))).ToString());
                            //btn.Top = Int32.Parse(Math.Round(((rect.Height) * float.Parse(info[3]))).ToString());
                            btn.Location = new Point(Int32.Parse(dt.Rows[i]["pos_x"].ToString()), Int32.Parse(dt.Rows[i]["pos_y"].ToString()));
                            ShowInfo(btn, dt.Rows[i]["resource_name"].ToString(), dt.Rows[i]["sip"].ToString());
                            // Setposition_button(btn,float.Parse(info[2]),float.)
                            // btn.Click += new EventHandler(btn_Click);
                            btn.MouseMove += new System.Windows.Forms.MouseEventHandler(btn_MouseMove);
                            btn.MouseDown += new System.Windows.Forms.MouseEventHandler(btn_MouseDown);
                            btn.MouseUp += new System.Windows.Forms.MouseEventHandler(btn_MouseUp);
                            this.pictureBox1.Controls.Add(btn);
                            // this.Controls.Add(btn);
                        }
                    }

                    BindMenu();
                }
                //MessageBox.Show(list.Count.ToString());
                // MessageBox.Show(line);


                else
                {
                    MessageBox.Show("数据库无数据，请检查！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void BindMenu()
        {
            string str = "重启终端,重启程序,关闭终端,远程开机,VNC查看,发布消息,编辑点位,删除点位";
            string[] line = str.Split(',');
            contextMenuStrip1.Items.Clear();
            for (int i = 0; i < line.Length;i++ )

            {
                ToolStripItem item = new ToolStripMenuItem();
                item.Name = line[i];
                item.Text = line[i].ToString();
                item.Click += new EventHandler(contextMenuStrip1_ItemClick);
                contextMenuStrip1.Items.Add(item);
                //contextMenuStrip1.Items.Add(row[1].ToString(), null, contextMenuStrip1_ItemClick);
            }
        }
      
        private void contextMenuStrip1_ItemClick(object sender, EventArgs e)
        {
            switch(sender.ToString())
            {
                case "重启终端": 
                    //string[] ip = checkedListBox3.Text.Split(',');
                    sendata("RESTPC",ctrl.Text);
                    MessageBox.Show("重启终端操作成功！","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);

                    break;
                case "重启程序":
                    sendata("RESTEXE", ctrl.Text);
                    MessageBox.Show("重启程序操作成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case "关闭终端":
                    sendata("CLOSEPC", ctrl.Text);
                    MessageBox.Show("关闭终端操作成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   break;
                case "远程开机": OpenPC();  break;
                case "VNC查看":
                    try
                    {
                        System.Diagnostics.Process.Start(Pathexe, ctrl.Text);
                    }
                    catch
                    {
                        MessageBox.Show("程序路径为空或路径错误！","警告",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    }
                    break;
                case "编辑点位":
                    // Button button = (Button)sender;
                    Edit_msg Frmbj = new Edit_msg(ctrl.Name, ctrl.Text, Name);
                    Frmbj.Owner = this;
                    Frmbj.ShowDialog();
                    break;
                case "发布消息":
                    Release_Point frm = new Release_Point(ctrl.Name, ctrl.Text, Name);
                     frm.Owner = this;
                    frm.ShowDialog();
                    break;
                
                case "删除点位":
                   // int index;
                    for(int i=0;i<list.Count;i++)
                    {
                        if(list[i].ToString().Contains(ctrl.Name+","+ctrl.Text))
                        {
                            list.RemoveAt(i);
                        }
                    }
                    try
                    {
                        ora.ExecuteSql("delete from b_devices t where t.sip='" + ctrl.Text + "'");


                        // string path = System.Environment.CurrentDirectory + "//config//"+Name+".txt";
                        // string[] lines = System.IO.File.ReadAllLines(path);
                        ////转换
                        //List<string> liststr = new List<string>();
                        //liststr.AddRange(lines);
                        //for(int i=0;i<liststr.Count;i++)
                        //{
                        //    if(liststr[i].ToString().Contains(ctrl.Name+","+ctrl.Text))
                        //    {
                        //        liststr.RemoveAt(i);
                        //    }

                        //}

                        //lines = liststr.ToArray();
                        ////保存
                        //File.WriteAllLines(path,lines,Encoding.UTF8);
                        DeleteButton();
                        Info.Remove(ctrl.Name);
                        AddbuttonInfo("images/" + Name + ".jpg");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
               // MessageBox.Show("请检查文件！");
               // MessageBox.Show("所选点位已成功删除！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
            }
        }
        private void OpenPC()
        {
           // string[] ip = checkedListBox3.Text.Split(',');
            try
            {
               // netCon nct = new netCon();
               // nct.WakeFunction("hh");
                MessageBox.Show("远程开机操作成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("MAC地址为空或格式错误！","警告",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        //private  void btn_Click(object sender, EventArgs e)                   //button点击事件
        //{
        //     Button button = (Button)sender;
        //     编辑点位 Frmbj = new 编辑点位(button.Name, button.Text, Name);
        //     Frmbj.Owner = this;
        //     Frmbj.ShowDialog();
        //    //MessageBox.Show(Name);
        //    //MessageBox.Show(button.Text);
        //}
        private void btn_MouseDown(object sender, MouseEventArgs e)        //button鼠标右键点击事件
        {
            if (e.Button == MouseButtons.Right)//鼠标右键
            {
                this.ctrl = (Control)sender;//这里就是获取的控件
            }
            mouse = new Point(-e.X, -e.Y);
        }
        private void btn_MouseMove(object sender, MouseEventArgs e)        //button鼠标右键点击事件
        {
            ((Control)sender).Cursor = Cursors.Arrow;//设置拖动时鼠标箭头
            if (e.Button == MouseButtons.Left)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouse.X, mouse.Y);//设置偏移
                ((Control)sender).Location = ((Control)sender).Parent.PointToClient(mousePos);

            }
        }
        private void btn_MouseUp(object sender, MouseEventArgs e)
        {
             
            if (e.Button == MouseButtons.Left)
            {
                Button button = (Button)sender;
                string sip = button.Text;
                string display = button.Name;
                string pos_x = button.Left.ToString();
                string pos_y = button.Top.ToString();
                ora.ExecuteSql("update b_devices t set t.pos_x='"+pos_x+"', t.pos_y='"+pos_y+"' where t.sip='"+sip+"'");
               // string path = System.Environment.CurrentDirectory + "//config//" + Name + ".txt";
               // string[] lines = System.IO.File.ReadAllLines(path);
                //转换
                //List<string> list = new List<string>();
                //list.AddRange(lines);
                //for (int i = 0; i < list.Count; i++)
                //{
                //    if (list[i].ToString().Contains(button.Name + "," + button.Text))
                //    {
                //        list[i] = button.Name + "," + button.Text + "," + button.Left + "," + button.Top;
                //    }
                //}

                //lines = list.ToArray();
                ////保存
                //File.WriteAllLines(path, lines, Encoding.UTF8);
                //File.w
                // MessageBox.Show(lines.ToString());
                //MessageBox.Show("此点位信息已经更新成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //this.Close();
                // 航显监控 frm = (航显监控)this.Owner;
                //frm.DeleteButton();
                // AddbuttonInfo("//config//" + Name + ".txt", "images/" + Name + ".jpg");
                // MessageBox.Show((e.X).ToString() + "," + (e.Y).ToString());
            }
        }
        /// <summary>
        /// 添加子菜单
        /// </summary>
        /// <param name="text">要显示的文字，如果为 - 则显示为分割线</param>
        /// <param name="cms">要添加到的子菜单集合</param>
        /// <param name="callback">点击时触发的事件</param>
        /// <returns>生成的子菜单，如果为分隔条则返回null</returns>

       private  ToolStripMenuItem AddContextMenu(string text, ToolStripItemCollection cms, EventHandler callback)
        {
            if (text == "-")
            {
                ToolStripSeparator tsp = new ToolStripSeparator();
                cms.Add(tsp);
                return null;
            }
            else if (!string.IsNullOrEmpty(text))
            {
                ToolStripMenuItem tsmi = new ToolStripMenuItem(text);
                tsmi.Tag = text + "TAG";
                if (callback != null) tsmi.Click += callback;
                cms.Add(tsmi);

                return tsmi;
            }

            return null;
        }
       private void MenuClicked(object sender, EventArgs e)
       {
           DataTable dt = ora.GetDt("select distinct t.pos_tc  from b_devices t where t.pos_tc is not null");
          // string path = System.Environment.CurrentDirectory + "//config//Position.txt";      //读取Position文件中显示层
           if (dt.Rows.Count>0)
           {
               //FileStream fs = new FileStream(path, FileMode.Open);
               //StreamReader sr = new StreamReader(fs, Encoding.Default);
              // string line = sr.ReadLine();
               //sr.Close();
               //fs.Close();
              // string[] str = File.ReadAllLines(path,Encoding.UTF8);
               for (int i = 0; i < dt.Rows.Count; i++)
               {
                   if (sender.ToString() == dt.Rows[i]["pos_tc"].ToString())
                   {
                       Name = dt.Rows[i]["pos_tc"].ToString();
                       groupBox1.Text = "泸沽湖机场" + dt.Rows[i]["pos_tc"].ToString() + "航显监控";
                      
                       DeleteButton();
                       AddbuttonInfo("images/" + dt.Rows[i]["pos_tc"].ToString() + ".jpg");
                       return;
                   }
                  
               }
               // MessageBox.Show(line);

           }
           else
           {
               MessageBox.Show("菜单配置文件不存在，请检查！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
               return;
           }
          
           switch(sender.ToString())
           {
               //case "新增图层":
               //    新增图层 Frmxz = new 新增图层();
               //    Frmxz.ShowDialog();
               //    break;
               case "变更图层":
                   //MessageBox.Show(Name);
                   if (Name == "" || Name == "航显监控")
                   {
                       //MessageBox.Show("请进入点位图层再进行此操作！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                       DelPic Frmsc = new DelPic();
                       Frmsc.ShowDialog();
                   }
                  else
                   {
                       MessageBox.Show("变更图层只能在主界面操作，请退回主界面！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   }
                   break;
               case "新增点位":
                   if (Name == "" || Name == "航显监控")
                   {
                       MessageBox.Show("请进入点位图层再进行此操作！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   }
                   else
                   {
                       AddPoint Frmxzdw = new AddPoint(Name);
                       Frmxzdw.Owner = this;
                       Frmxzdw.ShowDialog();
                   }
                   break;
               case "重启本程序":
                   //DeleteButton();
                   Application.Restart();
                   break;
               case "重启全部终端":
                   //string[] ip = checkedListBox3.Text.Split(',');
                   foreach (string str in list)
                   {
                       string[] line = str.Split(',');
                       sendata("RESTPC", line[1]);
                   } 
                   MessageBox.Show("重启全部终端操作成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                   break;
               case "重启全部程序":
                   foreach (string str in list)
                   {
                       string[] line = str.Split(',');
                       sendata("RESTEXE", line[1]);
                   }
                   MessageBox.Show("重启全部程序操作成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   break;
               case "关闭全部终端":
                   foreach (string str in list)
                   {
                       string[] line = str.Split(',');
                       sendata("CLOSEPC", line[1]);
                   } 
                  // sendata("CLOSEPC", ctrl.Text);
                   MessageBox.Show("关闭终端操作成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   break;
               //case "发布消息":
                   
               //    break;
               case "管理消息":
                   Manager_Msg mfrm = new Manager_Msg();
                   mfrm.Owner = this;
                   mfrm.ShowDialog();
                   break;
              
           }
       }
       private void ShowInfo(Button btnname,string name,string ipaddr)       //显示各个航显点（button）的提示信息
       {
           ToolTip ttpSettings = new ToolTip();
           ttpSettings.InitialDelay = 200;
           ttpSettings.AutoPopDelay = 10 * 1000;
           ttpSettings.ReshowDelay = 200;
           ttpSettings.ShowAlways = true;
           ttpSettings.IsBalloon = true;
           string tipOverwrite = "显示屏:"+name+","+"IP地址:"+ipaddr;
           ttpSettings.SetToolTip(btnname, tipOverwrite); // ckbOverwrite is a checkbox
       }

       [DllImport("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize")]
       public static extern int SetProcessWorkingSetSize(IntPtr process, int minSize, int maxSize);
       /// <summary>
       /// 释放内存
       /// </summary>
       public static void ClearMemory()
       {
           GC.Collect();
           GC.WaitForPendingFinalizers();
           if (Environment.OSVersion.Platform == PlatformID.Win32NT)
           {
               SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
           }
       }
       private void ReceiveMessage()
       {
           //在本机指定的端口接收
           //remoteIpEndIPoint = new IPEndPoint(IPAddress.Any, 8002);
           udpReceive = new UdpClient(remoteIpEndIPoint);
           IPEndPoint iep = new IPEndPoint(IPAddress.Any, 0);
           mytab = creat_tab("heartCheck");
           //接收从远程主机发送过来的信息
           try
           {
               while (true)
               {
                   //ref表示引用类型的 IPPoint实例接收消息
                   byte[] receiveBytes = udpReceive.Receive(ref iep);
                   string returnData = Encoding.UTF8.GetString(receiveBytes);
                   string[] token = returnData.Split(new Char[] { '@' });

                   if (token[0] == "start")
                   {

                       if (Info.Contains(token[1]))
                       {

                       }
                       else
                       {
                           foreach (string str in list)
                           {

                               if (str.Contains(iep.Address.ToString()))
                               {
                                   if (Info.ContainsKey(token[1]) == false)
                                   {
                                       Info.Add(token[1], token[1] + "," + iep.Address.ToString());
                                   }
                                  // MessageBox.Show(Info[token[1]].ToString());
                                   //addclient(token[1] + "," + iep.Address.ToString());
                                   //log.writelog(DateTime.Now.ToLongDateString(), token[1] + "," + iep.Address.ToString() + "成功连接！" + "|时间：" + DateTime.Now.ToString());
                               }
                               else
                               {

                                   // sendata();
                               }
                           }
                       }

                   }
                   if (token[0] == "test")
                   {
                       if (Info.ContainsKey(token[1]))
                       {
                           find_row(mytab, token[2], token[1], DateTime.Now.ToString());
                       }
                       else
                       {
                           Info.Add(token[1], token[1] + "," + iep.Address.ToString());
                       }
                       //MessageBox.Show(Info[token[1]].ToString());
                   }
                   if (token[0] == "iisdie")
                   {
                       MessageBox.Show("IIS服务已关闭或出现故障，请检查！故障时间:" + token[2], "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                      
                   }
                   if (token[0] == "exit")
                   {
                       //addclient(token[1] + "," + token[2] + "断开了连接......");
                       Info.Remove(token[1]);
                       //Removeclient(token[1] + "," + iep.Address.ToString());
                       deleteRow(mytab, iep.Address.ToString(), token[1]);
                     //  log.writelog(DateTime.Now.ToLongDateString(), token[1] + "," + iep.Address.ToString() + "正常退出." + "||时间：" + DateTime.Now.ToString());
                   }

               }
           }
           catch (Exception ex)
           {
               MessageBox.Show(ex.ToString());
           }
       }

       private void sendata(string address, string ip)
       {
           //string address = Properties.Settings.Default.url;
           //string boardadr = Properties.Settings.Default.boardadr;
           // Uri myurl = new Uri(address);

           //初始化UdpClient
           udpSend = new UdpClient();
           //允许发送和接收广播数据报
           udpSend.EnableBroadcast = true;
           //udpSend.JoinMulticastGroup(IPAddress.Parse("224.100.0.10"), 50);
           //必须使用组播地址范围内的地址
           IPEndPoint iep = new IPEndPoint(IPAddress.Parse(ip), 8001);
           //将发送内容转换为字节数组
           byte[] bytes = System.Text.Encoding.UTF8.GetBytes(address);
           //设置重传次数 
           int retry = 0;
           while (true)
           {
               try
               {
                   //发送组播信息
                   udpSend.Send(bytes, bytes.Length, iep);
                   break;
               }
               catch
               {
                   if (retry < 3)
                   {
                       retry++; continue;
                   }
                   else
                   {
                       DialogResult result = MessageBox.Show("发送失败！");
                       break;
                   }
               }
           }
       }
       //private void addclient(string str)
       //{
       //    //if (listBox2.Items.Contains(str + "已经成功连接!"))
       //    //{
       //    //    return;
       //    //}
       //    //else
       //    //{
       //        //if (this.listBox2.Items.Count > 50)
       //        //{
       //        //    this.listBox2.Items.Clear();
       //        //}
       //    foreach (string st in list)
       //        {
       //            if (st.Contains(str))
       //            {
       //                checkedListBox3.SetItemChecked(i, true);
       //            }
       //        }

       //        this.listBox2.Items.Add(str + "已经成功连接!" + DateTime.Now);
       //        this.tbSocketClientsNum.Text = Info.Count.ToString();
          
       //}

       //private void Removeclient(string str)
       //{
       //    foreach (string st in list)
       //    {
       //        if (st.Contains(str))
       //        {
       //            list.Remove(st);
       //        }
       //    }
       //   // this.listBox2.Items.Add(str + "不幸地离开了我们.........." + DateTime.Now.ToString());
       //   // this.tbSocketClientsNum.Text = Info.Count.ToString();
       //}
       private DataTable creat_tab(string tabname)
       {
           DataTable tabtemp = new DataTable(tabname);

           //DataColumn cl1 = new DataColumn("State", System.Type.GetType("System.String"));
           DataColumn cl2 = new DataColumn("Ip", System.Type.GetType("System.String"));
           DataColumn cl3 = new DataColumn("Name", System.Type.GetType("System.String"));
           DataColumn cl4 = new DataColumn("DateTime", System.Type.GetType("System.String"));
           //DataColumn cl5 = new DataColumn("Ping", System.Type.GetType("System.String"));

           //tabtemp.Columns.Add(cl1);
           tabtemp.Columns.Add(cl2);
           tabtemp.Columns.Add(cl3);
           tabtemp.Columns.Add(cl4);
           // tabtemp.Columns.Add(cl5);

           return tabtemp;
       }

       private void deleteRow(DataTable dt, string ipstr, string clientname)
       {
           DataRow[] delRows = dt.Select("Ip ='" + ipstr + "'  and  Name='" + clientname + "'");
           if (delRows.Length > 0)
           {
               dt.Rows.Remove(delRows[0]);
               dt.AcceptChanges();
           }
       }

       private DataRow find_row(DataTable dt, string ipstr, string clientname, string time)
       {
           DataRow[] foundRows;
           DataRow row = dt.NewRow();
           foundRows = dt.Select("Ip ='" + ipstr + "'  and  Name='" + clientname + "'");

           if (foundRows.Length > 0)
           {
               //row["DateTime"] = DateTime.Now.ToString();
               //dt.Rows.Add(row);
               //foundRows[0].Table.Columns[2].
               //dt.AcceptChanges();
               //row.AcceptChanges();
               //dt.AcceptChanges();
               //foundRows[0].Table.Columns[2]=DateTime.Now.ToString();
               foundRows[0].BeginEdit();
               foundRows[0]["DateTime"] = time;
               foundRows[0].EndEdit();
               foundRows[0].AcceptChanges();
               return foundRows[0];
           }
           else
           {

               row["Ip"] = ipstr;
               row["Name"] = clientname;
               row["DateTime"] = time;
               dt.Rows.Add(row);
               dt.AcceptChanges();
               foundRows = dt.Select("Ip ='" + ipstr + "'  and  Name='" + clientname + "'");
               if (foundRows.Length > 0)
               {
                   return foundRows[0];
               }
               else
                   return null;

           }
       }


       public void GetTcpConnections()
       {
           IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
           TcpConnectionInformation[] connections = properties.GetActiveTcpConnections();

           //DataRow rowt;
           DataRow rowt1;
           if (mytab == null)
           {
               return;
           }
           else
           {

               for (int i = 0; i < mytab.Rows.Count; i++)
               {
                   foreach (string str in list)
                   {
                       if (str.Contains(mytab.Rows[i]["Name"] + "," + mytab.Rows[i]["IP"]))
                       {
                           if (Info.Contains(mytab.Rows[i]["Name"]))
                           {
                               //checkedListBox3.SetItemChecked(j, true);
                           }
                           else
                           {
                               Info.Add(mytab.Rows[i]["Name"], mytab.Rows[i]["Name"] + "," + mytab.Rows[i]["IP"]);
                              // addclient(mytab.Rows[i]["Name"] + "," + mytab.Rows[i]["IP"]);
                               //log.writelog(DateTime.Now.ToLongDateString(), mytab.Rows[i]["Name"] + "," + mytab.Rows[i]["IP"] + "成功连接！" + "|时间：" + DateTime.Now.ToString());
                           }
                           // checkedListBox3.SetItemChecked(j, true);
                       }

                       else
                       {
                           //checkedListBox3.SetItemChecked(i, false);
                           //if (Info.Contains(mytab.Rows[i]["Name"]))
                           //{
                           //   // checkedListBox3.SetItemChecked(j, true);
                           //}

                           //else
                           //{
                           //    Info.Add(mytab.Rows[i]["Name"], mytab.Rows[i]["Name"] + "," + mytab.Rows[i]["IP"]);
                           //    addclient(mytab.Rows[i]["Name"] + "," + mytab.Rows[i]["IP"]);
                           //    log.writelog(DateTime.Now.ToLongDateString(), mytab.Rows[i]["Name"] + "," + mytab.Rows[i]["IP"] + "成功连接！" + "|时间：" + DateTime.Now.ToString());
                           //}

                       }
                   }

                   rowt1 = mytab.Rows[i];
                   TimeSpan kk = DateTime.Now - DateTime.Parse(rowt1["DateTime"].ToString());
                   if (kk.TotalMinutes > 1)
                   {
                      // log.writelog(DateTime.Now.ToLongDateString(), mytab.Rows[i]["Name"] + "," + mytab.Rows[i]["IP"] + "异常断开，可能是网络故障或者客户端死机！" + "|||时间：" + DateTime.Now.ToString());
                       Info.Remove(mytab.Rows[i]["Name"]);
                      // Removeclient(mytab.Rows[i]["Name"] + "," + mytab.Rows[i]["IP"]);
                       mytab.Rows.Remove(rowt1);
                       mytab.AcceptChanges();
                   }


               }


           }
       }

       private void timer1_Tick(object sender, EventArgs e)
       {
           GetTcpConnections();
           //MessageBox.Show("haha");
           CheckOnline(); 
           ClearMemory();
       }

       private void 航显监控_FormClosing(object sender, FormClosingEventArgs e)
       {
           try
           {
              // log.writelog_server(DateTime.Now.ToLongDateString(), "服务端程序正常关闭" + "     " + "||时间：" + DateTime.Now.ToString());
               //关闭UdpClient连接
               //udpReceive.Close();
               // udpSend.Close();
               // thread1.Abort();
               // udpSend.Close();
               mytab.Clear();
               Info.Clear();
               //终止线程
               Application.ExitThread();
           }
           catch (Exception ex)
           {
               //log.writelog_server(DateTime.Now.ToLongDateString(), ex.ToString() + "     " + "||时间：" + DateTime.Now.ToString());
           }
       }

       private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
       {
           DeleteButton();
       }

     
    }

}
