using App.MessageService.Intefaces;
using AppModel;
using AppRepository.Interfaces;

namespace App.MessageService
{
  public class AppMessageService : IAppMessageService
  {
    private readonly IAppMessageRepository _repository;

    public AppMessageService(IAppMessageRepository repository)
    {
      _repository = repository;
    }

    public void SendMessage(AppMessage mensage)
    {
      _repository.SendMessage(mensage);
    }
  }
}
