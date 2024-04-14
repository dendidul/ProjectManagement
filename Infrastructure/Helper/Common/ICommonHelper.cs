using Core.Dto.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Helper.Common
{
    public interface ICommonHelper
    {
        IDictionary<string, string> ToDictionary(object source);
        string ToCamelCase(string source);
        string GetEnumDescription(Enum value);
        string RandomNumber(int length);
        bool IsValidDate(YearMonthDayHourMinute input, out DateTime output);
        bool IsValidDate(YearMonthDay input, out DateTime output);
        DateTime GetDate(YearMonthDayHourMinute input);
        DateTime GetDate(YearMonthDay input);
        bool IsEmail(string uniqueID);
        bool IsPhoneNo(string uniqueID);
        bool IsAlphabet(string dataInput);
        bool IsNumeric(string dataInput);
        bool IsAlphaNumeric(string dataInput);
        bool IsAlphaNumericAndSpecialCharacter(string dataInput);
        bool IsAlphaNumericAndSpecialCharacterMore(string dataInput);
        bool IsPhoneNoIndonesia(string uniqueID);
        List<string> GetAssistDetail(string content);
        string GeneratePhoneNumber(string phoneNumber);
        string GenerateNumberLeadingZero(string phoneNumber);
    }
}
