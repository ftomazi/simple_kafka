// See https://aka.ms/new-console-template for more information
using AppModel;
using Confluent.Kafka;
using System.Text.Json;

Console.WriteLine("Inicio consumo Fila Kafka");

CancellationTokenSource cts = new CancellationTokenSource();
Console.CancelKeyPress += (_, e) =>
{
  e.Cancel = true; // prevent the process from terminating.
  cts.Cancel();
};

var config = new ConsumerConfig
{
  BootstrapServers = "localhost:9092",
  GroupId = $"queue_kafka-group-0",
  AutoOffsetReset = AutoOffsetReset.Earliest
};

using (var consumer = new ConsumerBuilder<string, string>(config).Build())
{
  consumer.Subscribe("queue_kafka");
  while (!cts.IsCancellationRequested)
  {
    try
    {
      var cr = consumer.Consume(cts.Token);
      var json = cr.Message.Value;
      AppMessage mensagem = JsonSerializer.Deserialize<AppMessage>(json);
      System.Threading.Thread.Sleep(1000);
      Console.WriteLine($"Titulo: {mensagem.Message}; Texto=; Id=");
    }
    catch (OperationCanceledException oce)
    {
      continue;
    }
  }
}
