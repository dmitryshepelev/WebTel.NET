using WebTelNET.Models.Models;

namespace WebTelNET.Models.Repository
{
    public interface IZadarmaAccountRepository
    {
        ZadarmaAccount GetAccount(string userId);
    }
}