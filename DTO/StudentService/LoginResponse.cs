using System;
using System.Text.Json.Serialization;

namespace Forum.DTO.StudentService
{
	public class LoginResponse
	{
		/// <summary>
		/// 是否成功
		/// </summary>
		[JsonPropertyName("success")]
		public bool Success { get; set; }

		/// <summary>
		/// 令牌
		/// </summary>
		[JsonPropertyName("token")]
		public string? Token { get; set; }

        /// <summary>
        /// 信息
        /// </summary>
        [JsonPropertyName("msg")]
        public string? Message { get; set; }

        /// <summary>
        /// 用户数据
        /// </summary>
        [JsonPropertyName("userdata")]
		public Object? UserData { get; set; }
	}
}

