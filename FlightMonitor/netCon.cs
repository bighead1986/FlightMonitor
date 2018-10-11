using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Management;
using Microsoft.Win32;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using ADOX;
using ADOMD;

namespace KBXSServer
{
    public class netCon
    {
        [DllImport("Iphlpapi.dll")]
        private static extern int SendARP(Int32 dest, Int32 host, ref Int64 mac, ref Int32 length);

        [DllImport("Ws2_32.dll")]
        private static extern Int32 inet_addr(string ip);

        private string _myHostName; //本机名
        private string _myHostIP; //本机IP
        private string _myHostMAC; //本机MAC
        public List<string> HostIP = new List<string>(); //远程主机IP地址
        public List<string> netCard = new List<string>(); //本机网卡列表 

        public string myHostName
        {
            get { return _myHostName; }
        }

        public string myHostIP
        {
            get { return _myHostIP; }
        }

        public string myHostMAC
        {
            get { return _myHostMAC; }
        }

        public netCon()
        {
            ManagementObjectSearcher query = new
                ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapterConfiguration WHERE IPEnabled = 'TRUE'");
            ManagementObjectCollection queryCollection = query.Get();
            foreach (ManagementObject mo in queryCollection)
            {
                netCard.Add(mo["Description"].ToString());
            }
            if (netCard.Count > 0)
            {
                getNetInfo(netCard[0]);
            }
        }

        public void getNetInfo(string nCard)
        {
            ManagementObjectSearcher query = new
                ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapterConfiguration WHERE IPEnabled = 'TRUE'");
            ManagementObjectCollection queryCollection = query.Get();
            string[] tmp;
            foreach (ManagementObject mo in queryCollection)
            {
                if (mo["Description"].ToString() == nCard)
                {
                    tmp = (string[]) mo["IPAddress"];
                    _myHostMAC = mo["MACAddress"].ToString();
                    _myHostName = mo["Description"].ToString();
                    _myHostIP = tmp[0];
                }
            }
        }
        public void Creat()
        {
            string app_path;
            app_path = Application.StartupPath + "\\Manager.mdb";
            ADOX.Catalog catalog = new ADOX.Catalog();
            catalog.Create("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + app_path + ";Jet OLEDB:Engine Type=5");
        }
        public string scanHost(string remoteIP)
        {
            Int32 ldest = inet_addr(remoteIP); //目的ip 
            Int32 lhost = inet_addr(_myHostIP); //本地ip 
            string tmp = "";
            int j = 12;
            try
            {
                Int64 macinfo = new Int64();
                Int32 len = 6;
                int res = SendARP(ldest, 0, ref macinfo, ref len);
                for (int i = 0; i < 6; i++)
                {
                    j -= 2;
                    tmp += Convert.ToString(macinfo, 16).Substring(j, 2).ToUpper();
                    if (i != 5)
                        tmp += "-";
                }
                return tmp;
            }
            catch
            {
                //Console.WriteLine("Error:{0}", err.Message);
                return 0.ToString();
            }
        }

        public void WakeFunction(string MAC_ADDRESS)
        {
            WOLClass client = new WOLClass();
            client.Connect(new
               IPAddress(0xffffffff),  //255.255.255.255  i.e broadcast
               0x2fff); // port=12287 let's use this one 
            client.SetClientToBrodcastMode();
            //set sending bites
            string[] tmp = MAC_ADDRESS.Split('-');
            int counter = 0;
            //buffer to be send
            byte[] bytes = new byte[1024];   // more than enough :-)
            //first 6 bytes should be 0xFF
            for (int y = 0; y < 6; y++)
                bytes[counter++] = 0xFF;
            //now repeate MAC 16 times
            for (int y = 0; y < 16; y++)
            {
                int i = 0;
                for (int z = 0; z < 6; z++)
                {
                    bytes[counter++] =
                        byte.Parse(tmp[z],
                        NumberStyles.HexNumber);
                    i += 2;
                }
            }
            //now send wake up packet
            int reterned_value = client.Send(bytes, 1024);
        }
    }

    public class WOLClass : UdpClient
    {
        public WOLClass()
            : base()
        { }
        //this is needed to send broadcast packet
        public void SetClientToBrodcastMode()
        {
            if (this.Active)
                this.Client.SetSocketOption(SocketOptionLevel.Socket,
                                          SocketOptionName.Broadcast, 0);
        }
    }
  
}
