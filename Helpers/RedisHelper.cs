using StackExchange.Redis;

namespace Forum.Helpers
{
    /// <summary>
    /// Redis连接服务，将会被按需注入到控制类中。
    /// </summary>
    public class RedisHelper
    {
        private readonly IConnectionMultiplexer _redis;

        public RedisHelper(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        /// <summary>
        /// 获取数据库实例。
        /// 数据库编号-用途对应：
        /// 1 - 用户信息
        /// 2 - 用户未读通知信息
        /// 【无论如何也不要使用默认数据库】
        /// </summary>
        /// <param name="db">数据库编号</param>
        /// <returns></returns>
        public IDatabase GetDatabase(int db = -1)
        {
            return _redis.GetDatabase(db);
        }
    }
}