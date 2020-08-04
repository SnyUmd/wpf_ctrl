using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        //*****************************************************************************************
        //tdcテスト用
        class SystemLoginUser
        {
            static public string NamePolicy_Koen = "試合";
        }

        //*****************************************************************************************
        public MainWindow()
        {
            InitializeComponent();

            //#5839 umd debug---------------------------------------------

            string origin_test =
                "公演図面　興行名：{0}　公演名：{1}"
                ;


            string new_test =
                string.Format("興行名：{0}" + $"{SystemLoginUser.NamePolicy_Koen}名：" + "{1}  {2}の発券一覧表の作成が完了しました。",
                                                            "*****",
                                                            "**********",
                                                            string.Format($"{SystemLoginUser.NamePolicy_Koen}日時：" + "{0}{1} {2}", 100, 1000, 1000)
                                                            )
                ;


            MessageBox.Show(
                            string.Format(origin_test, "test", "test")
                            +
                            "\r\n"
                            +
                            string.Format(new_test, "test", "test")
                            );//#5839 umd debug
            //----------------------------------------------

            this.Close();
        }


        private void ListBox_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            MessageBox.Show("wheel");
            e.Handled = true;
        }
    }
}
