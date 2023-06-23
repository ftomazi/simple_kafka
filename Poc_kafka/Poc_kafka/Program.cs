using App.MessageService;
using App.MessageService.Intefaces;
using AppModel;
using AppRepository;
using AppRepository.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IAppMessageService, AppMessageService>();
builder.Services.AddTransient<IAppMessageRepository, KafkaMenssageRepository>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

var message = new AppMessageService(app.Services.GetRequiredService<IAppMessageRepository>());
var msg = new AppMessage();

while (true)
{
  Console.Write("Digite a Mensagem:");
  var messageText = Console.ReadLine();
  msg.Message = messageText;
  Console.WriteLine("");
  message.SendMessage(msg);
}

//msg.Message = "Loren ipsen 2";
//message.SendMessage(msg);

app.Run();
;
