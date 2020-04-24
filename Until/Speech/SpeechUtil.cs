using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Windows;

namespace Customer.Until.Speech
{
    class SpeechUtil
    {
        private string Txt { set; get; }

       /* private SpeechRecognitionEngine Speech { get; set; }

        private DictationGrammar Dictation { get; set; }

        /*public void SRecognition(string[] fg) //创建关键词语列表  
        {
            CultureInfo myCIintl = new CultureInfo("zh-CN");
            foreach (RecognizerInfo config in SpeechRecognitionEngine.InstalledRecognizers())//获取所有语音引擎  
            {
                Console.WriteLine(config.Culture.EnglishName);
                if (config.Culture.Equals(myCIintl))
                {
                    Speech = new SpeechRecognitionEngine(config);
                    break;
                }//选择识别引擎
            }
            if (Speech != null)
            {
                InitializeSpeechRecognitionEngine(fg);//初始化语音识别引擎  
                Dictation = new DictationGrammar();
            }
            else
            {
                MessageBox.Show("创建语音识别失败");
            }
        }*/

        /// <summary>
        /// 初始化识别引擎
        /// </summary>
        /// <param name="fg"></param>
       /* public void SRecognition(string[] fg)
        {
            SetConfig = fg;
        }

        /// <summary>
        /// 配置识别编码为中文
        /// </summary>
        public string[] SetConfig
        {
            set
            {
                if(value != null)
                {
                    CultureInfo cultureInfo = new CultureInfo("zh_CN");
                    SetRecognizers = cultureInfo;
                    SetDictation = value;
                }
            }
        }

        /// <summary>
        /// 选取识别引擎
        /// </summary>
        public CultureInfo SetRecognizers
        {
            set
            {
                if(value != null)
                {
                    foreach(RecognizerInfo config in SpeechRecognitionEngine.InstalledRecognizers())
                    {
                        if (config.Culture.Equals(value))
                        {
                            Speech = new SpeechRecognitionEngine(config);
                            break;
                        }
                    }//选择识别引擎
                }
            }
        }

        /// <summary>
        /// 初始化语音类库
        /// </summary>
        public string[] SetDictation
        {
            set
            {
                if(value != null)
                {
                    if(Speech != null)
                    {
                        InitializeSpeechRecognitionEngine(value);
                        Dictation = new DictationGrammar();
                        return;
                    }

                    MessageBox.Show("创建语音识别失败");
                    
                }
            }
        }

        /// <summary>
        /// 语音识别类库初始化
        /// </summary>
        /// <param name="s"></param>
        public void InitializeSpeechRecognitionEngine(string[] s)
        {
            // Create and load a dictation grammar.
            //recognizer.LoadGrammar(new DictationGrammar());

            // Configure input to the speech recognizer.
            Speech.SetInputToDefaultAudioDevice();

            // Modify the initial silence time-out value.
            Speech.InitialSilenceTimeout = TimeSpan.FromSeconds(5);

            GrammarBuilder GB = new GrammarBuilder();
            GB.Append("选择");
            GB.Append(new Choices(new string[] { "红色", "绿色" }));
            Grammar G = new Grammar(GB);
            Console.WriteLine(G.RuleName);
            G.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(G_SpeechRecognized);
            Speech.LoadGrammar(G);
        }

        /// <summary>
        /// 语音识别结果的异步回调方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void G_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            Txt = e.Result.Text;
            switch (e.Result.Text)
            {
                case "选择红色":
                    break;
                case "选择绿色":
                    break;
            }
        }*/
    }
}
