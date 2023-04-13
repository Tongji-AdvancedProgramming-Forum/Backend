using COSXML;
using COSXML.Auth;
using COSXML.Transfer;
using COSXML.Model.Object;
using COSXML.Model.Bucket;
using COSXML.CosException;

namespace Forum.Helpers
{
    public class QCosHelper
    {
        private readonly QCloudCredentialProvider _cosCredentialProvider;
        private readonly CosXmlConfig _cosXmlConfig;
        private readonly string _bucket;
        private readonly string _region;
        private readonly string _prefix;

        /// <summary>
        /// 初始化cos Helper
        /// </summary>
        /// <param name="s_id">SECRET_ID</param>
        /// <param name="s_key">SECRET_KEY</param>
        /// <param name="bucketName">存储桶名</param>
        /// <param name="region">地区</param>
        /// <param name="prefix">网址前缀</param>
        public QCosHelper(string s_id, string s_key, string bucketName, string region, string prefix)
        {
            _cosCredentialProvider = new DefaultQCloudCredentialProvider(s_id, s_key, 600);
            _bucket = bucketName;
            _region = region;
            _prefix = prefix;
            _cosXmlConfig = new CosXmlConfig.Builder()
                                    .IsHttps(true)
                                    .SetRegion(region)
                                    .SetDebugLog(false)
                                    .Build();
        }

        /// <summary>
        /// 上传文件到存储桶
        /// </summary>
        /// <param name="cosPath">存储桶内位置</param>
        /// <param name="srcPath">原文件位置</param>
        /// <returns>Bool - 是否成功</returns>
        public async Task<string?> Upload(string cosPath, string srcPath)
        {
            var cosXml = new CosXmlServer(_cosXmlConfig, _cosCredentialProvider);
            var transferConfig = new TransferConfig();
            var transferManager = new TransferManager(cosXml, transferConfig);

            var uploadTask = new COSXMLUploadTask(_bucket, cosPath);
            uploadTask.SetSrcPath(srcPath);

            try
            {
                var result = await transferManager.UploadAsync(uploadTask);
                return _prefix + cosPath;
            }
            catch (Exception e)
            {
                Console.WriteLine("CosException: " + e);
                return null;
            }
        }

    }
}