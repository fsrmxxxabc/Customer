using Spire.Doc;

namespace Customer.Until.Chat
{
    interface IRtfToHtmlUtil
    {
        Document Documents { get; set; }
        string GetHtmlContent { get; set; }
        string RtfToHtml { set; }
        Document SetDocument { set; }
    }
}