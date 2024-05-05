using Core.Dto.Common;
using Core.Dto.Encryption;

namespace Application.Wrapper.Utility
{
    public interface IUtilityWrapper
    {
        ResponseData<EncryptParamUrlResponse> EncryptParamUrl(EncryptParamUrlRequest request);
        ResponseData<DecryptParamUrlResponse> DecryptParamUrl(DecryptParamUrlRequest request);
    }
}