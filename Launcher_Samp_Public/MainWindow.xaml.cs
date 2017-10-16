using System.Windows;
using System;
using System.Net.NetworkInformation;
using System.Windows.Media;
using System.Net;
using System.Diagnostics;

namespace Launcher_Samp_Public
{
    public partial class MainWindow : Window
    {
        public string IP = "5.254.105.203";
        public int PORT = 7777;
        public static int GroupID = 139495960; /// ID группы вставлять без - !!!!!!!!!!
        

        public string UserName;
        public static string[] www;
        public static readonly string RegistryKey = "HKEY_CURRENT_USER\\SOFTWARE\\SAMP";
        public static readonly string ConfigPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\GTA San Andreas User Files\\SAMP";
        public static readonly string SAMPConfigPath = ConfigPath + "\\sa-mp.cfg";

        public MainWindow()
        {
            InitializeComponent();
            Load();
            Settingcs set = new Settingcs();
            set.LoadSetting();
        }
        
        
        void Load()
        {
            try
            {
                Api.IP = IP;
                Api.PORT = PORT;
                Api.Query q = new Api.Query(Api.IP, Api.PORT);
                q.Send('i');
                int count = q.Receive();
                string[] info = q.Store(count);
                Api.ServerPassword = info[0];
                Api.Player = info[1];
                Api.MaxPlayer = info[2];
                Api.ServerName = info[3];
                Api.Gamemode = info[4];
                Api.Language = info[5];
                Pass.Content = "";
                if (Api.ServerPassword == "1")
                {
                    Pass.Foreground = Brushes.Red;
                    Pass.Content += "(!)SERVER PASSWORD(!) ";
                }
                NameServer.Foreground = Brushes.White;
                NameServer.Content += Api.ServerName + "\t\tИгроки: " + Api.Player + "/" + Api.MaxPlayer;          
                Ping();
            }
            catch { }
        }
        void Ping()
        {
            try
            { 
                var ping = new Ping();
                IPAddress addr = IPAddress.Parse(Api.IP);
                var pingReply = ping.Send(addr);
                if(pingReply.Status.ToString() != "Success")
                    MessageBox.Show("Возможно у вас проблемы с интернетом или сервер сейчас не доступен! Попробуйте позже!");
                pingg.Content = "Ping: "+pingReply.RoundtripTime+"ms";
            }
            catch (Exception er)
            {
                MessageBox.Show(er.ToString());
            }
        }
        private void Label_Click(object sender, RoutedEventArgs e) => Process.Start("https://vk.com/club" + GroupID);
        private void LoadMain(object sender, RoutedEventArgs e) => frame.NavigationService.Navigate(new Uri("Main.xaml", UriKind.Relative));
        private void LoadPageSettings(object sender, RoutedEventArgs e) => frame.NavigationService.Navigate(new Uri("Settings.xaml", UriKind.Relative));
    }
}
