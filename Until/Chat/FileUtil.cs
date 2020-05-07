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

        public System.Drawing.Bitmap GetIconName(string str)
        {
            return FileIcon(str);
        }

        private static System.Drawing.Bitmap GetFileType(FileType fileType)
        {
            switch (fileType)
            {
                case FileType.zip:
                    return Resources.ResourceAll.zip;
                case FileType.rar:
                    return Resources.ResourceAll.RAR;
                case FileType.xls:
                    return Resources.ResourceAll.XLSX;
                case FileType.csv:
                    return Resources.ResourceAll.XLSX;
                case FileType.txt:
                    return Resources.ResourceAll.TXT;
                case FileType.psd:
                    return Resources.ResourceAll.PSD;
                case FileType.doc:
                    return Resources.ResourceAll.DOC;

                default:
                    return Resources.ResourceAll.weizhiwenjian;
            }
        }

        private static System.Drawing.Bitmap FileIcon(string str)
        {
            return GetFileType((FileType)Enum.Parse(typeof(FileType), str));
        }
    }
}
