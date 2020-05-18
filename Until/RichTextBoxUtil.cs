using System;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Customer.Until
{
    class RichTextBoxUtil
    {
        public RichTextBox RichTextBoxs { get; set; }

        private TextRange TextRanges { get; set; }


        public RichTextBoxUtil(RichTextBox richTextBoxs)
        {
            RichTextBoxs = richTextBoxs;
            TextRanges = new TextRange(richTextBoxs.Document.ContentStart, richTextBoxs.Document.ContentEnd);
        }

        /// <summary>
        /// 清空Richbox的内容
        /// </summary>
        public string ClearRichBox
        {
            set
            {
                RichTextBoxs.Document.Blocks.Clear();
            }
        }

        /// <summary>
        /// 获取RichTextBox的文本内容
        /// </summary>
        /// <returns></returns>
        public string GetRichTextBoxCont
        {
            get
            {
                return Regex.Replace(TextRanges.Text,@"\s","");
            }
        }

        /// <summary>
        /// 将RichTextBox中的内容转为string
        /// </summary>
        /// <returns></returns>
        public string GetRichTextBoxString
        {
            get
            {
                using(MemoryStream stream = new MemoryStream())
                {
                    TextRanges.Save(stream,DataFormats.Xaml);
                    return Convert.ToBase64String(stream.ToArray());
                }
            }
        }

        /// <summary>
        /// 将richBox内容转为String
        /// </summary>
        /// <returns></returns>
        public string GetRichTextBoxToString
        {
            get
            {
                return System.Windows.Markup.XamlWriter.Save(RichTextBoxs.Document);
            }
        }

        /// <summary>
        /// 将richTextBox的内容转为二进制
        /// </summary>
        /// <returns></returns>
        public  byte[] GetRichTextBoxToByte
        {
            get
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    System.Windows.Markup.XamlWriter.Save(RichTextBoxs.Document, stream);
                    Byte[] data = new byte[stream.Length];
                    stream.Position = 0;
                    stream.Read(data, 0, data.Length);
                    stream.Close();
                    return data;
                }
            }
        }

        /// <summary>
        /// 将richTextBox内容转为rtf格式
        /// </summary>
        /// <returns></returns>
        public string GetRichTextBoxToRtf
        {
            get
            {
                using(MemoryStream stream = new MemoryStream())
                {
                    TextRanges.Save(stream, DataFormats.Rtf);
                    stream.Seek(0,SeekOrigin.Begin);
                    using StreamReader streamReader = new StreamReader(stream);
                    return streamReader.ReadToEnd();
                }
            }
        }

        public string SaveRichTextBoxContent
        {
            set
            {
                if (File.Exists(value))
                {
                    using (MemoryStream stream = new MemoryStream())
                    {
                        using(FileStream file = new FileStream(value,FileMode.Create,FileAccess.Write))
                        {
                            TextRanges.Save(file, DataFormats.Rtf);
                        }
                    }
                }
            }
        }

        public string GetRichTextBoxToHtml
        {
            get
            {
                using(MemoryStream stream = new MemoryStream())
                {
                    if (!TextRanges.CanSave(DataFormats.Html))
                    {
                        return null;
                    }
                    TextRanges.Save(stream, DataFormats.Html);
                    stream.Seek(0, SeekOrigin.Begin);
                    using StreamReader streamReader = new StreamReader(stream);
                    return streamReader.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// 将取出的整个字符串放入RichTextBox中
        /// </summary>
        /// <param name="str"></param>
        public string AddRichTextBoxCont
        {
            set
            {
                if(value != null)
                {
                    using(StringReader stringReader  = new StringReader(value))
                    {
                        using System.Xml.XmlReader xmlReader = System.Xml.XmlReader.Create(stringReader);
                        RichTextBoxs.Document = (FlowDocument)System.Windows.Markup.XamlReader.Load(xmlReader);
                    }
                }
            }
        }

        public FlowDocument GetFlowDocument
        {
            get
            {
                using (StringReader stringReader = new StringReader(GetRichTextBoxToString))
                {
                    using System.Xml.XmlReader xmlReader = System.Xml.XmlReader.Create(stringReader);
                    return (FlowDocument)System.Windows.Markup.XamlReader.Load(xmlReader);
                }
            }
        }

        public string AddRichTextBoxFromRtf
        {
            set
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    using(StreamWriter streamWriter = new StreamWriter(stream))
                    {
                        if (value != null)
                        {
                            streamWriter.Write(value);
                            streamWriter.Flush();
                            stream.Seek(0, SeekOrigin.Begin);
                            TextRanges.Load(stream,DataFormats.Rtf);
                        }
                    }
                }
            }
        }
    }
}
