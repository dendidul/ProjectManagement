using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.Common
{
    public static class StatusResponse
    {
        #region success
        public static Status SuccessOK = new Status() { Code = "200", Type = "SUCCESS", Message = "Success", MessageInd = "Sukses", Errors = new List<Error>() };
        #endregion

        #region error
        public static Status FailedException = new Status() { Code = "400", Type = "ERROR", Message = "Error from aplication", MessageInd = "Error dari aplikasi" };
        public static Status FailedAuthentication = new Status() { Code = "401", Type = "ERROR", Message = "Authentication failure.", MessageInd = "Gagal autentikasi access token.", Errors = new List<Error>() { new Error() { Message = "Authentication failure.", MessageInd = "Gagal autentikasi access token.", Type = "ERROR" } } };
        public static Status FailedSaveToDB = new Status() { Code = "500", Type = "ERROR", Message = "Failed save data to database.", MessageInd = "Gagal menyimpan data ke database.", Errors = new List<Error>() { new Error() { Message = "Failed save data to database.", MessageInd = "Gagal menyimpan data ke database.", Type = "ERROR" } } };

        public static Status FailedInvalidClientIDSecret = new Status() { Code = "404", Type = "ERROR", Message = "Invalid client id and client secret.", MessageInd = "Client id dan client secret tidak valid.", Errors = new List<Error>() { new Error() { Message = "Invalid client id and client secret.", MessageInd = "Client id dan client secret tidak valid.", Type = "ERROR" } } };
        public static Status FailedClientIDSecretNotFound = new Status() { Code = "404", Type = "ERROR", Message = "Client id and client secret not found.", MessageInd = "Client id dan client secret tidak ditemukan.", Errors = new List<Error>() { new Error() { Message = "Client id and client secret not found.", MessageInd = "Client id dan client secret tidak ditemukan.", Type = "ERROR" } } };
        public static Status FailedClientIDSecretExpired = new Status() { Code = "404", Type = "ERROR", Message = "Client id and client secret expired.", MessageInd = "Client id dan client secret kadaluwarsa.", Errors = new List<Error>() { new Error() { Message = "Client id and client secret expired.", MessageInd = "Client id dan client secret kadaluwarsa.", Type = "ERROR" } } };
        public static Status FailedUsernamePasswordNotFound = new Status() { Code = "404", Type = "ERROR", Message = "Username and password not found.", MessageInd = "Username dan password tidak ditemukan.", Errors = new List<Error>() { new Error() { Message = "Username and password not found.", MessageInd = "Username dan password tidak ditemukan.", Type = "ERROR" } } };
        public static Status FailedNoSalesTransaction = new Status() { Code = "904", Type = "ERROR", Message = "Sales Transaction is null.", MessageInd = "Tidak ada data transaksi penjualan.", Errors = new List<Error>() { new Error() { Message = "Sales Transaction is null.", MessageInd = "Tidak ada data transaksi penjualan.", Type = "ERROR" } } };
        public static Status FailedLogin = new Status() { Code = "404", Type = "ERROR", Message = "Finish with errors.", MessageInd = "Finish dengan errors.", Errors = new List<Error>() { new Error() { Message = "Login failed. Check username/password.", MessageInd = "Login gagal. Cek username/password.", Type = "ERROR" } } };
        public static Status FailedUserAccess = new Status() { Code = "404", Type = "ERROR", Message = "Invalid user access.", MessageInd = "User Access tidak valid.", Errors = new List<Error>() { new Error() { Message = "Invalid user access.", MessageInd = "User Access tidak valid.", Type = "ERROR" } } };
        public static Status FailedTokenRequestTimeExpired = new Status() { Code = "404", Type = "ERROR", Message = "RequestTime expired.", MessageInd = "RequestTime kadaluarsa.", Errors = new List<Error>() { new Error() { Message = "RequestTime expired.", MessageInd = "RequestTime kadaluarsa.", Type = "ERROR" } } };
        public static Status FailedTokenEncryptNotMatch = new Status() { Code = "404", Type = "ERROR", Message = "Client encrypt not match.", MessageInd = "Client encrypt tidak sesuai.", Errors = new List<Error>() { new Error() { Message = "Client encrypt not match.", MessageInd = "Client encrypt tidak sesuai.", Type = "ERROR" } } };
        public static Status FailedAuthenticationNotPermissionMethod = new Status() { Code = "401", Type = "ERROR", Message = "Client doesn't have permission.", MessageInd = "Client tidak mempunyai izin.", Errors = new List<Error>() { new Error() { Message = "Client doesn't have permission.", MessageInd = "Client tidak mempunyai izin." } } };

        #endregion
    }
}
