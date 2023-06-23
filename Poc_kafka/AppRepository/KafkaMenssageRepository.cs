using AppModel;
using AppRepository.Interfaces;
using Confluent.Kafka;
using System.Text.Json;

namespace AppRepository
{
  public class KafkaMenssageRepository : IAppMessageRepository
  {
    public void SendMessage(AppMessage message)
    {
      var jsonMessage = JsonSerializer.Serialize(message);
      var config = new ProducerConfig { BootstrapServers = "localhost:9092" };
      using (var producer = new ProducerBuilder<string, string>(config).Build())
      {
        var result = producer.ProduceAsync("queue_kafka",
                        new Message<string, string>
                        {
                          Key = Guid.NewGuid().ToString(),
                          Value = jsonMessage
                        });

        producer.Flush();
      }
    }
  }
}
