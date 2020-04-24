using SpeechLib;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Customer.Until.Speech
{
    class SpRecognition
    {
        private static SpRecognition _Instance = null;
        private ISpeechRecoGrammar isrg;
        private SpSharedRecoContextClass ssrContex = null;

        public delegate void StringEvent(string str);
        public StringEvent SetMessage = null;

        private Paragraph paragraph = new Paragraph();

        public void GetAutoContent(string str)
        {
            App.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.SystemIdle, (ThreadStart)delegate ()
            {
                View.Index.ChatingContMsg.Focus();
                InlineUIContainer inlineUIContainer = new InlineUIContainer() { Child = new TextBlock() { Text =str } };
                paragraph.Inlines.Add(inlineUIContainer);
                View.Index.ChatingContMsg.Document.Blocks.Add(paragraph);
            });
        }

        private SpRecognition()
        {
            ssrContex = new SpSharedRecoContextClass();
            isrg = ssrContex.CreateGrammar(1);
            SpeechLib._ISpeechRecoContextEvents_RecognitionEventHandler recHandle =
               new _ISpeechRecoContextEvents_RecognitionEventHandler(ContexRecognition);
            ssrContex.Recognition += recHandle;
        }
        public void BeginRec() 
        {
            isrg.DictationSetState(SpeechRuleState.SGDSActive);
        }
        public static SpRecognition instance()
        {
            if (_Instance == null)
                _Instance = new SpRecognition();
            return _Instance;
        }
        public void CloseRec()
        {
            isrg.DictationSetState(SpeechRuleState.SGDSInactive);
        }
        private void ContexRecognition(int iIndex, object obj, SpeechLib.SpeechRecognitionType type, SpeechLib.ISpeechRecoResult result)
        {
            SetMessage = new StringEvent(GetAutoContent);
            SetMessage(result.PhraseInfo.GetText(0, -1, true));
        }
    }
}
