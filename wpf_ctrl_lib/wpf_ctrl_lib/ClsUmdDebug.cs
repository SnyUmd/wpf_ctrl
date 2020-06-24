using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;

using System.Windows.Threading;


namespace UmdCtrl
{
    public class ClsUmdDebug
    {

        //DoEvent***************************************************************
        //参照必要 using System.Windows.Threading;
        private void DoEvents()
        {
            DispatcherFrame frame = new DispatcherFrame();
            var callback = new DispatcherOperationCallback(ExitFrames);
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, callback, frame);
            Dispatcher.PushFrame(frame);
        }
        private object ExitFrames(object obj)
        {
            ((DispatcherFrame)obj).Continue = false;
            return null;
        }
        //DoEven***************************************************************


        //***************************************************************
        //WinFormsでいうPerformClickを実行.
        //
        //■参照の追加が必要
        //　PresentationCore
        //　PresentationFramework
        //　UIAutomationProvider
        //　WindowsBase
        // 
        //   System.Windows.Automation.Peers
        //   System.Windows.Automation.Provider
        // 名前空間が必要。
        //
        // IInvokeProviderインターフェースは、UIAutomationProvider.dll
        // の参照設定が追加で必要となる。
        //
        //***************************************************************
        public void PerformClick0(Button button)
        {
            if (button == null)
                throw new ArgumentNullException("button");

            var provider = new ButtonAutomationPeer(button) as IInvokeProvider;
            provider.Invoke();
        }

        //***************************************************************
        public void PerformClick1(Button self)
        {

            var peer = new ButtonAutomationPeer(self);
            var provider = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;

            provider.Invoke();
        }

        //***************************************************************
        public void umdTextSet(TextBox Txb, string str)
        {
            Txb.Text = str;
        }
        //***************************************************************
        public void umdPassSet(PasswordBox Pwb, string str)
        {
            Pwb.Password = str;
        }


        //***************************************************************
        /// <summary>
        /// コントロールのサイズをプラス
        /// </summary>
        /// <param name="Ctrl">サイズを変更するコントロール</param>
        /// <param name="i_add_width">幅の加算値</param>
        /// <param name="i_add_height">高さの加算値</param>
        /// <param name="bl_animation">アニメーション（段階的に）</param>
        /// <param name="i_loop">サイズ加算ステップ</param>
        //***************************************************************
        public void umdSetCtrlSize_Add(System.Windows.Controls.Control Ctrl, int i_add_width, int i_add_height, bool bl_animation, int i_loop)
        {
            int iStepWidth = 0, iStepHeigh = 0;
            int iRemainingWidth = 0, iRemainingHeigh = 0;

            if (!bl_animation)//アニメーション無しの時は単純に足して終わり
            {
                Ctrl.Width += i_add_width;
                Ctrl.Height += i_add_height;
            }
            else//アニメーションが必要な時
            {
                //ステップ値と余りを取得
                iStepWidth = i_add_width / i_loop;
                iRemainingWidth = i_add_width % i_loop;

                iStepHeigh = i_add_height / i_loop;
                iRemainingHeigh = i_add_height % i_loop;

                //最初に余りの値をプラス
                Ctrl.Width += iRemainingWidth;
                Ctrl.Height += iRemainingHeigh;
                DoEvents();


                for (int i = 0; i < i_loop; i++)
                {
                    Ctrl.Width += iStepWidth;
                    Ctrl.Height += iStepHeigh;
                    DoEvents();
                }
            }
        }
        //***************************************************************
        ///
        public void umdSetCtrlSize_Sub(System.Windows.Controls.Control Ctrl, int i_sub_width, int i_sub_height, bool bl_animation, int i_loop)
        {

            ★現在のサイズ以上の長さは拒否

            int iStepWidth = 0, iStepHeigh = 0;
            int iRemainingWidth = 0, iRemainingHeigh = 0;

            if (!bl_animation)//アニメーション無しの時は単純に足して終わり
            {
                Ctrl.Width -= i_sub_width;
                Ctrl.Height -= i_sub_height;
            }
            else//アニメーションが必要な時
            {
                //ステップ値と余りを取得
                iStepWidth = i_sub_width / i_loop;
                iRemainingWidth = i_sub_width % i_loop;

                iStepHeigh = i_sub_height / i_loop;
                iRemainingHeigh = i_sub_height % i_loop;

                //最初に余りの値をプラス
                Ctrl.Width -= iRemainingWidth;
                Ctrl.Height -= iRemainingHeigh;
                DoEvents();


                for (int i = 0; i < i_loop; i++)
                {
                    Ctrl.Width -= iStepWidth;
                    Ctrl.Height -= iStepHeigh;
                    DoEvents();
                }
            }
        }

    }
}
