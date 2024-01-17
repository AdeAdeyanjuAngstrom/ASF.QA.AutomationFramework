using Newtonsoft.Json;
using PushTechnology.ClientInterface.Client.Factories;
using PushTechnology.ClientInterface.Data.JSON;

namespace AutomationFramework.Helpers.DiffusionHelper
{
    public static class DiffusionData
    {
        public static async Task<string?> GetTopicLatestData(string topic, DateTime timestamp)
        {
            var diffusionSession = Diffusion.Sessions.Principal(Base.Base.Configuration.DiffusionUsername)
                .Credentials(Diffusion.Credentials.Password(Base.Base.Configuration.DiffusionPassword))
                .Open(Base.Base.Configuration.DiffusionUrl);
            var topics = diffusionSession.Topics;

            var count = 0;

            do
            {
                var data = await topics.FetchRequest.WithValues<IJSON>().FetchAsync(topic);

                if (data.Results.Count > 0)
                {
                    if (data.Results.First().Value != null)
                    {
                        var stringData = data.Results.First().Value.ToJSONString();
                        var time = (DateTime)(JsonConvert.DeserializeObject<dynamic>(stringData)!).transmissionTime;

                        if (time >= timestamp)
                        {
                            Console.WriteLine("Time of message " + time);
                            Console.WriteLine("Time of request " + timestamp);
                            Thread.Sleep(TimeSpan.FromSeconds(3));
                            //diffusionSession.Close();
                            return await GetTopicData(topic);
                        }
                    }
                }
                Thread.Sleep(TimeSpan.FromSeconds(1));
                count++;
            } while (count < 45);
            diffusionSession.Close();
            return null;
        }

        public static async Task<string?> GetTopicData(string topic)
        {
            var diffusionSession = Diffusion.Sessions.Principal(Base.Base.Configuration.DiffusionUsername)
                .Credentials(Diffusion.Credentials.Password(Base.Base.Configuration.DiffusionPassword))
                .Open(Base.Base.Configuration.DiffusionUrl);
            var topics = diffusionSession.Topics;
            var data = await topics.FetchRequest.WithValues<IJSON>().FetchAsync(topic);
            var diffusionDate = data.Results.First().Value.ToJSONString();
            //diffusionSession.Close();
            return JsonConvert.SerializeObject(JsonConvert.DeserializeObject<dynamic>(diffusionDate)!.body);
        }
    }
}