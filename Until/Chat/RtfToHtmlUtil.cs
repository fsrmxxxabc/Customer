using Spire.Doc;
using System.IO;


namespace Customer.Until.Chat
{
    class RtfToHtmlUtil
    {
        public Document Documents { get; set; }

        public RtfToHtmlUtil(string path)
        {
            GetHtmlContent = path;
        }

        public Document SetDocument
        {
            set
            {
                if(value != null)
                {
                    Documents = value;
                }
            }
        }

        public string RtfToHtml
        {
            set
            {
                if(value != null)
                {
                    SetDocument = new Document();
                    Documents.LoadFromFile(@value);
                    Documents.SaveToFile("../../../Resources/Content/Content.html", FileFormat.Html);
                }
            }
        }

        public string GetHtmlContent
        {
            get
            {
                using (FileStream stream = File.OpenRead("../../../Resources/Content/Content.html"))
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    using StreamReader streamReader = new StreamReader(stream);
                    return streamReader.ReadToEnd();
                }
            }

            set
            {
                if(value != null)
                {
                    RtfToHtml = value;
                }
            }
        }
    }
}
