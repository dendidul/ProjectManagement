using Core.Dto.Common;
using Core.Dto.Encryption;
using Infrastructure.Helper.Encryption;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Application.Wrapper.Utility
{
    public class UtilityWrapper: IUtilityWrapper
    {

        private IPasswordEncryption _passwordEncryption;

        public UtilityWrapper(IPasswordEncryption passwordEncryption)
        {
            _passwordEncryption = passwordEncryption;
        }

        public ResponseData<EncryptParamUrlResponse> EncryptParamUrl(EncryptParamUrlRequest request)
        {

            var response = new ResponseData<EncryptParamUrlResponse>()
            {
                Status = new Core.Dto.Common.Status()
                {
                    Code = StatusCodes.Status400BadRequest.ToString(),
                    Type = "ERROR",
                    Message = "Finish with errors",
                    MessageInd = "Finish with errors",
                    Errors = new List<Error>()
                }
            };

            var json  = JsonConvert.SerializeObject(request);

            var jObj = (JObject)JsonConvert.DeserializeObject(json);

            var query = String.Join("&",
                            jObj.Children().Cast<JProperty>()
                            .Select(jp => jp.Name + "=" + HttpUtility.UrlEncode(jp.Value.ToString())));

            //var encrypted =  _passwordEncryption.Encrypt(request.Password);
            var model = new EncryptParamUrlResponse();
            model.paramEncrypted = _passwordEncryption.Encrypt(query);
            response.Data = model;

            response.Status = new Core.Dto.Common.Status()
            {
                Code = StatusCodes.Status200OK.ToString(),
                Type = "SUCCESS",
                Message = "Success",
                MessageInd = "Sukses",
                Errors = new List<Error>()
            };


            return response;

        }

        public ResponseData<DecryptParamUrlResponse> DecryptParamUrl(DecryptParamUrlRequest request)
        {

            var response = new ResponseData<DecryptParamUrlResponse>()
            {
                Status = new Core.Dto.Common.Status()
                {
                    Code = StatusCodes.Status400BadRequest.ToString(),
                    Type = "ERROR",
                    Message = "Finish with errors",
                    MessageInd = "Finish with errors",
                    Errors = new List<Error>()
                }
            };

            var decrypted = _passwordEncryption.Decrypt(request.paramEncrypted);

            var dict = HttpUtility.ParseQueryString(decrypted);
            var jsonreq = JsonConvert.SerializeObject(dict.AllKeys.ToDictionary(y => y, y => dict[y]));

           

            
            var model = new DecryptParamUrlResponse();
            model = JsonConvert.DeserializeObject<DecryptParamUrlResponse>(jsonreq);

            response.Data = model;

            response.Status = new Core.Dto.Common.Status()
            {
                Code = StatusCodes.Status200OK.ToString(),
                Type = "SUCCESS",
                Message = "Success",
                MessageInd = "Sukses",
                Errors = new List<Error>()
            };


            return response;

        }


    }
}
