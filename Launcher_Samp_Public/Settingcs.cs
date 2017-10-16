using System;
using System.IO;
using System.Windows.Forms;

namespace Launcher_Samp_Public
{
    public class Settingcs
    {
        public static readonly string ConfigPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\GTA San Andreas User Files\\SAMP";
        public static readonly string SAMPConfigPath = ConfigPath + "\\sa-mp.cfg";
        
        public void SaveSettings()
        {
            
            try
            {
                using (StreamWriter sw = new StreamWriter(SAMPConfigPath))
                {
                    string[] text = {
                                    "pagesize=" + Api.Pagesize,
                                    "\nfpslimit=" + Api.Fpslimit,
                                    "\ndisableheadmove=" + Api.Disableheadmove,
                                    "\ntimestamp=" + Api.Timestamp,
                                    "\nime=" + Api.Ime,
                                    "\nmulticore=" + Api.Multicore,
                                    "\ndirectmode=" + Api.Directmode,
                                    "\naudiomsgoff=" + Api.Audiomsgoff,
                                    "\nfontweight=" + Api.Fontweight,
                                    "\nnonametagstatus=" + Api.Nonametagstatus,
                                  };
                    foreach (var qq in text)
                        sw.WriteLine(qq);
                    MessageBox.Show("Сохранено!");
                }
            }
            catch(Exception er) { MessageBox.Show(er.ToString()); }
        }
        public void SetDefault()
        {

            try
            { 
                using (StreamWriter sww = new StreamWriter(SAMPConfigPath))
                {
                    string[] text = {
                                "pagesize=10",
                                "fpslimit=90\n",
                                "disableheadmove=1\n",
                                "timestamp=0\n",
                                "ime=0\n",
                                "multicore=1\n",
                                "directmode=0\n",
                                "audiomsgoff=0\n",
                                "nonametagstatus=0\n",
                                "fontweight=1\n",
                                "audioproxyoff=0\n", };
                    foreach (var line in text)
                        sww.WriteLine(line);
                    MessageBox.Show("Успешно!");
                }
                LoadSetting();
            }
            catch(Exception er)
            {
                MessageBox.Show(er.Message + "\n\n" + er.ToString());
            }
        }
        public void LoadSetting()
        {
            
            try
            {
               
                string line;
                using (StreamReader info = new StreamReader(SAMPConfigPath))
                {
                    
                    while (!info.EndOfStream)
                    {
                        line = info.ReadLine();
                        string[] q = line.Split('=');
                        switch (q[0])
                        {
                            case "fpslimit":
                                {
                                    Api.Fpslimit = q[1];
                                    break;
                                }
                            case "pagesize":
                                {
                                    Api.Pagesize = q[1];
                                    break;
                                }
                            case "audiomsgoff":
                                {
                                    Api.Audiomsgoff = q[1];
                                    break;
                                }
                            case "audioproxyoff":
                                {
                                    Api.Audioproxyoff = q[1];
                                    break;
                                }
                            case "directmode":
                                {
                                    Api.Directmode = q[1];
                                    break;
                                }
                            case "disableheadmove":
                                {
                                    Api.Disableheadmove = q[1];
                                    break;
                                }
                            case "fontweight":
                                {
                                    Api.Fontweight = q[1];
                                    break;
                                }
                            case "ime":
                                {
                                    Api.Ime = q[1];
                                    break;
                                }
                            case "multicore":
                                {
                                    Api.Multicore = q[1];
                                    break;
                                }
                            case "nonametagstatus":
                                {
                                    Api.Nonametagstatus = q[1];
                                    break;
                                }
                            case "timestamp":
                                {
                                    Api.Timestamp = q[1];
                                    break;
                                }
                            default:
                                {
                                    if (Api.Fpslimit == null) Api.Fpslimit = "90";
                                    if (Api.Audiomsgoff == null) Api.Audiomsgoff = "0";
                                    if (Api.Audioproxyoff == null) Api.Audioproxyoff = "0";
                                    if (Api.Directmode == null) Api.Directmode = "0";
                                    if (Api.Disableheadmove == null) Api.Disableheadmove = "1";
                                    if (Api.Fontweight == null) Api.Fontweight = "0";
                                    if (Api.Ime == null) Api.Ime = "0";
                                    if (Api.Multicore == null) Api.Multicore = "1";
                                    if (Api.Nonametagstatus == null) Api.Nonametagstatus = "0";
                                    if (Api.Pagesize == null) Api.Pagesize = "10";
                                    if (Api.Timestamp == null) Api.Timestamp = "0";
                                    break;
                                }
                        }
                    }

                }
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
                SetDefault();
            }
        }
    }
}
