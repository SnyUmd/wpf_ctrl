using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;

namespace wpf_ctrl_lib
{
    public class ClsUmdDebug
    {
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
        public void PerformClick(Button button)
        {
            if (button == null)
                throw new ArgumentNullException("button");

            var provider = new ButtonAutomationPeer(button) as IInvokeProvider;
            provider.Invoke();
        }

        //***************************************************************
        public void PerformClick2(Button self)
        {

            var peer = new ButtonAutomationPeer(self);
            var provider = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;

            provider.Invoke();
        }
    }
}
