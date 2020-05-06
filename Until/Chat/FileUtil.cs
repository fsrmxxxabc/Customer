using System;
using System.Collections.Generic;
using System.Text;

namespace Customer.Until.Chat
{
    class FileUtil
    {
        private enum FileType
        {
            zip,rar,xls,txt,psd,csv,doc
        }

        public FileUtil()
        {

        }

        public string GetIconName(string str)
        {
            return FileIcon(str);
        }

        private static string GetFileType(FileType fileType)
        {
            switch (fileType)
            {
                case FileType.zip:
                    return "&#xe61e;";
                case FileType.rar:
                    return "&#xe61e;";
                case FileType.xls:
                    return "&#xe669;";
                case FileType.csv:
                    return "&#xe669;";
                case FileType.txt:
                    return "&#xe63b;";
                case FileType.psd:
                    return "&#xe61f;";
                case FileType.doc:
                    return "&#xe66d;";

                default:
                    return "&#xe621;";
            }
        }

        private static string FileIcon(string str)
        {
            return GetFileType((FileType)Enum.Parse(typeof(FileType), str));
        }
    }
}
