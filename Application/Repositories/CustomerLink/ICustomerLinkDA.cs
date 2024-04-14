using Core.Dto.CustomerLink;

namespace Application.Repositories.CustomerLink
{
    public interface ICustomerLinkDA
    {
        List<CustomerLinkResponse> GetUnsentLink();
        void UpdateSentLink(string fprsId, string shortUrl, string notes);
    }
}