using Customer.until;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Customer.Until.Chat
{
    class LabelShake
    {
        private Label Labels { get; set; }

        /// <summary>
        /// 初始化抖屏功能
        /// </summary>
        public LabelShake()
        {
            SetLabels = FormattableString.Invariant($"{DateTime.Now}") + " " + ConfigUntil.GetSettingString("userName") + "发送了一个抖屏";
        }

        /// <summary>
        /// 设置抖屏的细节
        /// </summary>
        public String SetLabels
        {
            set
            {
                if (value != null)
                {
                    Labels = new Label()
                    {
                        Content = value,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        Foreground = CommonUtil.GetColorBrush(255, 128, 85),
                    };
                }
            }
        }

        public Label GetLabels
        {
            get { return Labels; }
        }
    }
}
