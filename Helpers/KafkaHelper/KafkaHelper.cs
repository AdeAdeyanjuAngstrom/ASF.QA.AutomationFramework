using Confluent.Kafka;

namespace AutomationFramework.Helpers.KafkaHelper
{
    internal class KafkaHelper
    {
        public static void ReadKafkaMessages(string bootstrapServers,string topic, string consumerGroupId)
        {
            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = bootstrapServers,
                GroupId = consumerGroupId,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using var consumer = new ConsumerBuilder<Ignore, string>(consumerConfig).Build();
            consumer.Subscribe(topic);
            CancellationTokenSource cts = new();
            
            // Handle incoming messages
            Console.CancelKeyPress += (_, e) => { e.Cancel = true; cts.Cancel(); };
            cts.CancelAfter(TimeSpan.FromSeconds(30));
            try
            {
                while (true)
                {
                    try
                    {
                        var consumeResult = consumer.Consume(cts.Token);
                        Console.WriteLine($"Consumed message: {consumeResult.Message.Value}");
                    }
                    catch (ConsumeException e)
                    {
                        Console.WriteLine($"Error consuming message: {e.Error.Reason}");
                    }
                }
            }
            catch
            {
                consumer.Close();
                consumer.Dispose();                
            }
            finally
            {
                consumer.Close();
                consumer.Dispose();
            }
        }


        public static async Task WriteKafkaMessages(string bootstrapServers, string topic, string message)
        {
            var producerConfig = new ProducerConfig { BootstrapServers = bootstrapServers };
            using var producer = new ProducerBuilder<Null, string>(producerConfig).Build();
            
            var value = new Message<Null, string> { Value = message };
            await producer.ProduceAsync(topic, value);       
        }
    }
}
