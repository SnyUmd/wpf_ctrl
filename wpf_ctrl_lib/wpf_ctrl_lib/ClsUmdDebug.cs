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
        //FormのPerformClickと同じものを実行.
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

        //↑perfomeClick***************************************************************




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
        /// <param name="bl_upper">上へ追加</param>
        //***************************************************************
        public void umdSetCtrlSize_Add(System.Windows.Controls.Control Ctrl, int i_add_width, int i_add_height, bool bl_animation, int i_loop, bool bl_upper)
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
                    if (bl_upper)
                        ;
                    Ctrl.Height += iStepHeigh;
                    if (bl_upper)
                        ;
                    DoEvents();
                }
            }
        }
        //***************************************************************
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Ctrl"></param>
        /// <param name="i_sub_width"></param>
        /// <param name="i_sub_height"></param>
        /// <param name="bl_animation"></param>
        /// <param name="i_loop"></param>
        /// <param name="bl_upper"></param>
        //***************************************************************
        public void umdSetCtrlSize_Sub(System.Windows.Controls.Control Ctrl, int i_sub_width, int i_sub_height, bool bl_animation, int i_loop, bool bl_upper)
        {
            int iStepWidth = 0, iStepHeigh = 0;
            int iRemainingWidth = 0, iRemainingHeigh = 0;

            bool blWidthOver = false, blHeightOver = false;

            if (Ctrl.Width - 10 <= i_sub_width)
                blWidthOver = true;
            if (Ctrl.Height - 10 <= i_sub_height)
                blHeightOver = true;


            if (!bl_animation)//アニメーション無しの時は単純に足して終わり
            {
                if (!blWidthOver)
                    Ctrl.Width -= i_sub_width;
                if (!blHeightOver)
                    Ctrl.Height -= i_sub_height;
            }
            else//アニメーションが必要な時
            {
                //幅--------------------------------
                if (!blWidthOver)
                {
                    //ステップ値と余りを取得
                    iStepWidth = i_sub_width / i_loop;
                    iRemainingWidth = i_sub_width % i_loop;
                    //最初に余りの値をプラス
                    Ctrl.Width -= iRemainingWidth;
                }
                //高さ------------------------------------
                if (!blHeightOver)
                {
                    //ステップ値と余りを取得
                    iStepHeigh = i_sub_height / i_loop;
                    iRemainingHeigh = i_sub_height % i_loop;
                    //最初に余りの値をプラス
                    Ctrl.Height -= iRemainingHeigh;
                }
                DoEvents();

                //アニメーションループ
                for (int i = 0; i < i_loop; i++)
                {
                    if (!blWidthOver)
                        Ctrl.Width -= iStepWidth;
                    if (!blHeightOver)
                        Ctrl.Height -= iStepHeigh;
                    DoEvents();
                }
            }
        }








        //============================================================================================================================
        //============================================================================================================================
        //============================================================================================================================
        public void umdDebugLogin(System.Windows.Controls.TextBox txb_promoter, System.Windows.Controls.TextBox txb_id, System.Windows.Controls.PasswordBox pwb_pass, System.Windows.Controls.Button btn_rogin)
        /*
#if UMD_DEBUG
using UmdCtrl;
#endif
 
#if UMD_DEBUG
            ClsUmdDebug cls_umd_deb = new ClsUmdDebug();
            cls_umd_deb.umdDebugLogin(textboxPromoterId , textboxLoginId, passwordBoxLoginPassword, buttonSystemUserLogin);
#else//UMD_DEBUG
#endif//UMD_DEBUG
        */
        {
            txb_promoter.Text = "tdc";
            txb_id.Text = "umd";
            pwb_pass.Password = "umd";
            PerformClick1(btn_rogin);
        }
    }
}
