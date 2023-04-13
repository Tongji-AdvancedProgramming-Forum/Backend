using System;
using System.Text.Json.Serialization;


namespace Forum.DTO.StudentService
{
	public class LoginResponse: APIResponse
	{
		/// <summary>
		/// 令牌
		/// </summary>
		[JsonPropertyName("token")]
		public string? Token { get; set; }

        /// <summary>
        /// 用户数据
        /// </summary>
        [JsonPropertyName("userdata")]
		public Object? UserData { get; set; }
	}
}

