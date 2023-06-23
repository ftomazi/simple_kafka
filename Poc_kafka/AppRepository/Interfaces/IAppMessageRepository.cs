using AppModel;

namespace AppRepository.Interfaces
{
  public interface IAppMessageRepository
  {
    void SendMessage(AppMessage message);
  }
}
