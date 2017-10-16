using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Windows.Media;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using static Launcher_Samp_Public.MainWindow;
using System.Windows.Media.Imaging;
using System;
using Microsoft.Win32;
using System.Diagnostics;

namespace Launcher_Samp_Public
{
    public partial class Main : Page
    {
        public static readonly string RegistryKey = "HKEY_CURRENT_USER\\SOFTWARE\\SAMP";
        public Main()
        {
            InitializeComponent();
            News();
            Playername.Text = Registry.GetValue(RegistryKey, "PlayerName", "").ToString(); ;
        }
        private void Play(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Playername.Text.Length >= 5 && Playername.Text.Length <= 24)
                {
                    Registry.SetValue(RegistryKey, "PlayerName", Playername.Text);
                    Process.Start("samp://" + Api.IP + ":" + Api.PORT);
                }
                else
                    MessageBox.Show("Ваш Nickname не соответствует требованиями! \nNickname должен состоять от 5 и до 24 символов!");
            }
            catch (Exception err)
            {
                MessageBox.Show("Возможно у Вас не установлен SAMP! Установите его и попробуйте заного. \n\n\n\n Error:\n" + err.Message);
            }
         }

        void News()
        {
            try
            {
                var t = $"eb3e0103eb3e0103eb3e0103d6eb632590eeb3eeb3e0103b26074fca7c9c8eb262fc0a0";
                IEnumerable<JToken> walls(int id)
                {
                    var url = "https://api.vk.com/method/wall.get";
                    var param = $"?owner_id=-{id}&count=10&extended=0&v=5.68&offset=0&access_token=" + t;
                    var client = new WebClient();
                    client.Encoding = System.Text.Encoding.UTF8;
                    var str = client.DownloadString(url + param);
                    return JObject.Parse(str)["response"]["items"];
                }
                IEnumerable<JToken> NameGroup(int id)
                {
                    var url = "https://api.vk.com/method/groups.getById";
                    var param = $"?group_id={id}&offset=0&access_token=" + t + "&v=5.68";
                    var client = new WebClient();
                    client.Encoding = System.Text.Encoding.UTF8;
                    var str = client.DownloadString(url + param);
                    return JObject.Parse(str)["response"];
                }
                string GName = "";
                string photo = "";
                foreach (var items in NameGroup(GroupID))
                {
                    GName = items["name"].ToString();
                    photo = items["photo_50"].ToString();
                }
                foreach (var items in walls(GroupID))
                {
                    Image ava = new Image
                    {
                        Width = 50,
                        Height = 50,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment = VerticalAlignment.Center,
                        SnapsToDevicePixels = true,
                        UseLayoutRounding = false,
                        Margin = new Thickness(20, 2, 0, 0),
                        Stretch = Stretch.UniformToFill,
                        Source = new BitmapImage(new Uri(photo))
                    };
                    StackPanel stack = new StackPanel
                    {
                        Background = Brushes.White,
                        Width = 455,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment = VerticalAlignment.Center,

                    };
                    TextBlock textBlock = new TextBlock
                    {
                        Margin = new Thickness(80, -20, 21, 0),
                        TextWrapping = TextWrapping.WrapWithOverflow,
                        Padding = new Thickness(0, 0, 0, 10),
                        Text = items["text"].ToString() + "\n\n♥" +
                                        items["likes"]["count"] + "\t\t\t\t\t☺" + items["views"]["count"]
                    };
                    Label name = new Label
                    {
                        FontWeight = FontWeights.Bold,
                        Content = GName,
                        Margin = new Thickness(80, -45, 0, 0),
                        Padding = new Thickness(0),
                        HorizontalAlignment = HorizontalAlignment.Left,
                        FontSize = 16
                    };
                    stack.Children.Add(ava);
                    stack.Children.Add(name);
                    stack.Children.Add(textBlock);
                    ListViewItem item = new ListViewItem
                    {
                        Background = Brushes.Aqua,
                        Width = 430
                    };
                    users.Items.Add(stack);
                    users.Background = Brushes.Aqua;
                }
            
            }
            catch { }
        }
        
    }
}
