using System;
using System.Text.Json.Serialization;

namespace Forum.DTO.StudentService
{
	public class LoginRequest
	{
		/// <summary>
		/// 用户名
		/// </summary>
		[JsonPropertyName("username")]
		public string UserName { get; set; } = "";

        /// <summary>
        /// 密码
        /// </summary>
        [JsonPropertyName("password")]
        public string PassWord { get; set; } = "";

        /// <summary>
        /// 有效期（单位：分钟）
        /// </summary>
        [JsonPropertyName("validfor")]
        public int ValidityPeriod { get; set; } = 30;
	}
}

