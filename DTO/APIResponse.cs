using System;
using System.Text.Json.Serialization;

namespace Forum.DTO
{
    /// <summary>
    /// API返回的基类
    /// </summary>
	public class APIResponse
	{
        /// <summary>
        /// 是否成功
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        /// <summary>
        /// 信息
        /// </summary>
        [JsonPropertyName("msg")]
        public string? Message { get; set; }

    }
}

