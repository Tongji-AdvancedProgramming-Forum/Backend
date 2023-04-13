using System;
using System.Text;
using System.Security.Cryptography;

namespace Forum.Helpers
{
	public static class MD5Helper
	{
        /// <summary>
        /// 计算一个字符串的MD5
        /// </summary>
        /// <param name="input">待计算的字符串</param>
        /// <returns>结果</returns>
		public static string CalculateMD5Hash(string input)
        {
            using var md5 = MD5.Create();
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            StringBuilder sb = new();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("x2")); // 将每个字节转换为16进制字符串
            }
            return sb.ToString();
        }
    }
}

