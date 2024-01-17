
namespace AutomationFramework.Helpers.RedisHelper
{
    public static class RedisReader
    {
        public static string? ReadKeyValue(string searchKey, string fixture)
        {
            var db = Base.Base.KafkaConnection.GetDatabase();

            var keys = Base.Base.KafkaConnection.GetServer(Base.Base.Configuration.RedisDbUrl).Keys()
                .FirstOrDefault(i => i.ToString() == searchKey);
            var byteValue = (byte[])db.StringGet(keys)!;
            var response=RedisHelper.GetJsonFromRedisBytes(searchKey, fixture, byteValue);
            //_byteSerializer.DeserializeFromBytes<T>(value);
            return response;
        }

        public static void AddKeyValues(string key, string value)
        {
            var db = Base.Base.KafkaConnection.GetDatabase();
            db.StringSet(key, value);
        }

        public static async Task DeleteKeyValues(string fixtureKey)
        {
            var db = Base.Base.KafkaConnection.GetDatabase();
            var keys= Base.Base.KafkaConnection.GetServer(Base.Base.Configuration.RedisDbUrl).KeysAsync(pattern: "*" + fixtureKey + "*");

            await foreach (var key in keys)
            {
                await db.KeyDeleteAsync(key);
            }
        }
    }
}
