using Qiniu.Http;
using Qiniu.Storage;
using Qiniu.Util;
using System.Diagnostics;
using System.IO;

namespace Customer.Until.Qiniu
{
    class QiniuUtil
    {
        private static string AccessKey { get { return "ZigZwoagv9VwtTTBfdpIxDE_iwR-lHAWe1DZcx8X"; } }

        private static string SecretKey { get { return "8ePzGl7w6Gd8KVZco3Y2fHcATHhRc0d6mIZwlKms"; } }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("代码质量", "IDE0051:删除未使用的私有成员", Justification = "<挂起>")]
        public static string Domain { get { return "https://video.yestar.com/"; } }

        private static string Bucket { get { return "xingpai"; } }

        private Config QiNiuConfig { get; set; }

        private PutPolicy PutPolicys { get; set; }

        private enum QiniuZones {ZONE_CN_East,ZONE_CN_North,ZONE_CN_South,ZONE_US_North,ZONE_AS_Singapore};

        /// <summary>
        /// 设置必备的参数以及返回的数据格式和文件的生命周期
        /// 文件的生命周期默认是永久的
        /// </summary>
        public PutPolicy PutPolicyUtil
        {
            get { return PutPolicys; }
            set
            {
                PutPolicys = new PutPolicy
                {
                    Scope = Bucket,
                    ReturnBody = "{\"key\":\"$(key)\",\"hash\":\"$(etag)\",\"fsiz\":$(fsize),\"bucket\":\"$(bucket)\",\"name\":\"$(x:name)\"}",
                    DeleteAfterDays = 0
                };
                PutPolicys.SetExpires(3600);
            }
        }

        /// <summary>
        /// token工具类
        /// 此处有区分 如果输入自定义的文件名表示出现同名则覆盖，如果输入null表示出现文件名称不覆盖。
        /// </summary>
        public string TokenUtil
        {
            get
            {
                Mac mac = new Mac(AccessKey,SecretKey);
                return Auth.CreateUploadToken(mac, PutPolicyUtil.ToJsonString());
            }
            set
            {
                PutPolicyUtil = new PutPolicy();
                if(value != null)
                {
                    PutPolicys.Scope = Bucket + ":" + value;
                }
            }
        }

        /// <summary>
        /// 配置对应的机房，以及是否使用cdn，和https
        /// </summary>
        public Config ConfigUtil
        {
            get { return QiNiuConfig; }
            set
            {
                QiNiuConfig = new Config
                {
                    Zone = Zone.ZONE_CN_East,
                    UseHttps = true,
                    //UseCdnDomains = true,
                    ChunkSize = ChunkUnit.U512K
                };
            }
        }

        /// <summary>
        /// form表单上传
        /// </summary>
        /// <param name="key">自定义文件名</param>
        /// <param name="path">文件地址</param>
        /// <param name="type">类别，1表示不覆盖上传，2表示覆盖上传</param>
        /// <returns></returns>
        public string FormUpload(string key,string path,int type)
        {
            ConfigUtil = new Config();
            FormUploader formUploader = new FormUploader(ConfigUtil);
            switch (type)
            {
                case 1:
                    TokenUtil = null;
                    return formUploader.UploadFile(path, key, TokenUtil, null).ToString();
                case 2:
                    TokenUtil = key;
                    return formUploader.UploadFile(path, key, TokenUtil, null).ToString();
                default:
                    return "参数错误";
            }
        }

        /// <summary>
        /// 分片上传（断点续传）
        /// </summary>
        /// <param name="key"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public string ChunkUpload(string key,string path)
        {
            ResumableUploader resumableUploader = new ResumableUploader(ConfigUtil);

            PutExtra putExtra = new PutExtra
            {
                ResumeRecordFile = ResumeHelper.GetDefaultRecordKey(path, key)
            };

            HttpResult httpResult = resumableUploader.UploadFile(path, key, TokenUtil, putExtra);

            return httpResult.Text;
        }



    }
}
