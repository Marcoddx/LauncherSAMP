using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Launcher_Samp_Public
{
    /// <summary>
    /// Логика взаимодействия для Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {
        public static readonly string ConfigPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\GTA San Andreas User Files\\SAMP";
        public static readonly string SAMPConfigPath = ConfigPath + "\\sa-mp.cfg";
        
        public Settings()
        {
            InitializeComponent();
            LoadHelpers();
            Settingcs Setting = new Settingcs();
            Setting.LoadSetting();
            Loade();
        }
        void Loade()
        {
            fpslimit.Value = Convert.ToDouble(Api.Fpslimit);
            pagesize.Value = Convert.ToDouble(Api.Pagesize);
            if (Api.Disableheadmove == "1") headmove.IsChecked = true;
            else headmove.IsChecked = false;
            if (Api.Timestamp == "1") timestamp.IsChecked = true;
            else timestamp.IsChecked = false;
            if (Api.Ime == "1") ime.IsChecked = true;
            else ime.IsChecked = false;
            if (Api.Directmode == "1") dmode.IsChecked = true;
            else dmode.IsChecked = false;
            if (Api.Audiomsgoff == "1") audiomsgoff.IsChecked = true;
            else audiomsgoff.IsChecked = false;
            if (Api.Nonametagstatus == "1") nonametagstatus.IsChecked = true;
            else nonametagstatus.IsChecked = false;
            if (Api.Fontweight == "0") fontweight.IsChecked = true;
            else fontweight.IsChecked = false;
        }
        void LoadHelpers()
        {
            string fps = "Это позволяет игрокам устанавливать определенный предел FPS ,\nкогда опция ограничения кадров включена в меню GTA: SA. " +
                            "\nЗначения принимаются от 20 до 90. Значение по умолчанию SA-MP равно 50. " +
                            "\nЭтот параметр можно изменить в игре с помощью команды '/fpslimit'.";
            string hps = "Это позволяет игрокам устанавливать определенный предел FPS , когда опция ограничения кадров включена в меню GTA: SA. " +
                            "\nЗначения принимаются от 20 до 90. Значение по умолчанию SA-MP равно 50. " +
                            "\nЭтот параметр можно изменить в игре с помощью команды client-side / fpslimit.";
            string head = "Этот параметр определяет, двигаются ли головы игрока в том направлении, в котором они смотрят. " +
                            "\n1 отключает движения головы, 0 позволяет. Этот параметр можно переключать в игру с помощью команды  / headmove.";
            string titamp = "Это позволяет игрокам показывать локальную метку времени на стороне сообщений чата. " +
                                "\n1 позволяет использовать метки времени, а 0 отключает их. Это можно переключить в игру с помощью команды /timestamp.";
            string imes = "Это определяет, поддерживает ли вход в окно чата текстовое редактирование метода ввода и переключение языков. " +
                            "\n1 позволяет IME, 0 отключает его.";
            string dmodes = "Это позволяет игрокам с проблемами рисования текста чата использовать более медленный режим прямой трансляции текста. " +
                                "\n0 для отключения, 1 для включения.";
            string audiooff = "Если для этой опции установлено значение 1, сообщения «Audio Stream: [URL]» не будут отображаться в окне чата, когда сервер воспроизводит аудиопоток. " +
                                "\nЭтот параметр можно переключать в игре, используя команду /audiomsg.";
            string noname = "Если для этой опции установлено значение 0, игроки будут видеть значок песочных часов рядом с наметами других игроков, когда они будут приостановлены. " +
                                "\nЭто включено (0) по умолчанию. Этот параметр можно изменить в игре, используя команду /nametagstatus.";
            string font = "Этот параметр переключает жирного шрифта чата или нет.";

            hFps.ToolTip = fps;
            hPagesize.ToolTip = hps;
            hHead.ToolTip = head;
            hTimes.ToolTip = titamp;
            hime.ToolTip = imes;
            hdirectmode.ToolTip = dmodes;
            haudiomsgoff.ToolTip = audiooff;
            hnonametagstatus.ToolTip = noname;
            hFontWeight.ToolTip = font;
        }
        private void FpsLimit_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            
        }
        private void Pagesize_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Api.Pagesize = pagesize.Value.ToString();
                Api.Fpslimit = fpslimit.Value.ToString();

                if (audiomsgoff.IsChecked == true)
                    Api.Audiomsgoff = "1";
                else
                    Api.Audiomsgoff = "0";
                if (headmove.IsChecked == true)
                    Api.Disableheadmove = "1";
                else
                    Api.Disableheadmove = "0";
                if (timestamp.IsChecked == true)
                    Api.Timestamp = "1";
                else
                    Api.Timestamp = "0";
                if (ime.IsChecked == true)
                    Api.Ime = "1";
                else
                    Api.Ime = "0";
                if (dmode.IsChecked == true)
                    Api.Directmode = "1";
                else
                    Api.Directmode = "0";
                if (nonametagstatus.IsChecked == true)
                    Api.Nonametagstatus = "1";
                else
                    Api.Nonametagstatus = "0";
                if (fontweight.IsChecked == true)
                    Api.Fontweight = "0";
                else
                    Api.Audiomsgoff = "1";
                Api.Multicore = "1";
                Settingcs Setting = new Settingcs();
                Setting.SaveSettings();
                Setting.LoadSetting();
            }
            catch (Exception er) { MessageBox.Show(er.ToString()); }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Settingcs Setting = new Settingcs();
            Setting.SetDefault();
            Loade();
        }
    }
}
