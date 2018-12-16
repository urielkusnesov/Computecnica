using Model;

namespace Service.Mails
{
    public interface IMailService
    {
        Result Send(Mail mail);
    }
}
