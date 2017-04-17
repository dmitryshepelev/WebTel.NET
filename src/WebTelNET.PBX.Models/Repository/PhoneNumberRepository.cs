using WebTelNET.CommonNET.Libs.Repository;
using WebTelNET.PBX.Models.Models;

namespace WebTelNET.PBX.Models.Repository
{
    public interface IPhoneNumberRepository : IRepository<PhoneNumber>
    {
        PhoneNumber GetOrCreate(PhoneNumber phoneNumber);
    }

    public class PhoneNumberRepository : RepositoryBase<PhoneNumber>, IPhoneNumberRepository
    {
        public PhoneNumberRepository(PBXDbContext context) : base(context)
        {
        }

        public PhoneNumber GetOrCreate(PhoneNumber phoneNumber)
        {
            var existingPhoneNumber =
                GetSingle(x => x.Number.Equals(phoneNumber.Number) && x.ZadarmaAccountId == phoneNumber.ZadarmaAccountId);

            return existingPhoneNumber ?? Create(phoneNumber);
        }
    }
}