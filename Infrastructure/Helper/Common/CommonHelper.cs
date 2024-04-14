using Core.Dto.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Infrastructure.Helper.Common
{
    public class CommonHelper : ICommonHelper
    {


        public string GetEnumDescription(Enum value)
        {
            return
                value
                    .GetType()
                    .GetMember(value.ToString())
                    .FirstOrDefault()
                    ?.GetCustomAttribute<DescriptionAttribute>()
                    ?.Description;
        }

        public string ToCamelCase(string source)
        {
            if (source == null || source.Length < 2)
                return source;

            // Split the string into words.
            string[] words = source.Split(
                new char[] { },
                StringSplitOptions.RemoveEmptyEntries);

            // Combine the words.
            string result = words[0].ToLower();
            for (int i = 1; i < words.Length; i++)
            {
                result +=
                    words[i].Substring(0, 1).ToUpper() +
                    words[i].Substring(1);
            }

            return result;
        }

        public IDictionary<string, string> ToDictionary(object source)
        {
            return ToDictionary<object>(source);
        }

        public IDictionary<string, string> ToDictionary<T>(object source)
        {
            if (source == null)
                ThrowExceptionWhenSourceArgumentIsNull();

            var dictionary = new Dictionary<string, string>();
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(source))
                AddPropertyToDictionary<T>(property, source, dictionary);
            return dictionary;
        }

        public string RandomNumber(int length)
        {
            var random = new Random();
            string s = string.Empty;
            for (int i = 0; i < length; i++)
                s = String.Concat(s, random.Next(10).ToString());
            return s;
        }

        public bool IsValidDate(YearMonthDayHourMinute input, out DateTime output)
        {
            output = DateTime.MinValue;
            var date = DateTime.MinValue;
            try
            {
                date = new DateTime(input.Year, input.Month, input.Day, input.Hour, input.Minute, 0);
            }
            catch (Exception)
            {
                return false;
            }

            if (date.Year != input.Year || date.Month != input.Month || date.Day != input.Day || date.Hour != input.Hour || date.Minute != input.Minute)
                return false;

            output = date;
            return true;
        }

        public bool IsValidDate(YearMonthDay input, out DateTime output)
        {
            output = DateTime.MinValue;
            var date = DateTime.MinValue;
            try
            {
                date = new DateTime(input.Year, input.Month, input.Day);
            }
            catch (Exception)
            {
                return false;
            }

            if (date.Year != input.Year || date.Month != input.Month || date.Day != input.Day)
                return false;

            output = date;
            return true;
        }

        public DateTime GetDate(YearMonthDayHourMinute input)
        {
            var date = DateTime.MinValue;
            try
            {
                date = new DateTime(input.Year, input.Month, input.Day, input.Hour, input.Minute, 0);
            }
            catch (Exception)
            {
                return DateTime.MinValue;
            }

            return date;
        }

        public DateTime GetDate(YearMonthDay input)
        {
            var date = DateTime.MinValue;
            try
            {
                date = new DateTime(input.Year, input.Month, input.Day);
            }
            catch (Exception)
            {
                return DateTime.MinValue;
            }

            return date;
        }

        public bool IsEmail(string uniqueID)
        {
            return Regex.IsMatch(uniqueID, @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }

        public bool IsPhoneNo(string uniqueID)
        {
            return Regex.IsMatch(uniqueID, @"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }

        public bool IsPhoneNoIndonesia(string uniqueID)
        {
            //return Regex.IsMatch(uniqueID, @"(\+62 ((\d{3}([ -]\d{3,})([- ]\d{4,})?)|(\d+)))|(\(\d+\) \d+)|\d{3}( \d+)+|(\d+[ -]\d+)|\d+", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            return Regex.IsMatch(uniqueID, @"^(\+62|62)?[\s-]?0?8[1-9]{1}\d{1}[\s-]?\d{3,9}$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }

        public bool IsAlphabet(string dataInput)
        {
            return Regex.IsMatch(dataInput, @"^[a-zA-Z\s,]*$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }

        public bool IsNumeric(string dataInput)
        {
            return Regex.IsMatch(dataInput, @"^[0-9\s,]*$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }

        public bool IsAlphaNumeric(string dataInput)
        {
            return Regex.IsMatch(dataInput, @"^[a-zA-Z0-9\s,]*$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }

        public bool IsAlphaNumericAndSpecialCharacter(string dataInput)
        {
            return Regex.IsMatch(dataInput, @"^[a-zA-Z0-9.,//\-\s,]*$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }

        public bool IsAlphaNumericAndSpecialCharacterMore(string dataInput)
        {
            return Regex.IsMatch(dataInput, @"^[a-zA-Z0-9.(),//\-\s,]*$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }

        public List<string> GetAssistDetail(string content)
        {
            List<string> fileName = content.Split(new string[] { "{{" }, StringSplitOptions.None).Where(x => x.Contains("}}")).ToList();
            List<string> assistDetails = new List<string>();
            foreach (var item in fileName)
            {
                assistDetails.Add(item.Substring(0, item.IndexOf("}")));

            }
            return assistDetails;
        }

        public string GeneratePhoneNumber(string phoneNumber)
        {
            var result = phoneNumber.TrimStart('+');

            var anomaliLeadingList = new List<AnomaliLeadingPhoneNumber>()
            {
                new AnomaliLeadingPhoneNumber{ AnomaliLeading="8",FixLeading="628"},
                new AnomaliLeadingPhoneNumber{ AnomaliLeading="0",FixLeading="62"},
                new AnomaliLeadingPhoneNumber{ AnomaliLeading="6208",FixLeading="628"},
                new AnomaliLeadingPhoneNumber{ AnomaliLeading="62808",FixLeading="628"},
                new AnomaliLeadingPhoneNumber{ AnomaliLeading="6262",FixLeading="62"},
                new AnomaliLeadingPhoneNumber{ AnomaliLeading="008",FixLeading="628"},
            };

            int i = 0;
            var resultPhoneIndo = result;
            foreach (var item in anomaliLeadingList)
            {
                var itemLength = item.AnomaliLeading.Length;
                var leading = resultPhoneIndo.Substring(0, itemLength);
                if (leading == item.AnomaliLeading)
                {

                    var number = resultPhoneIndo.Substring(itemLength, resultPhoneIndo.Length - itemLength);
                    resultPhoneIndo = item.FixLeading + number;

                }
                i++;
            }
            result = resultPhoneIndo;
            //var leadingResult = resultPhoneIndo.Substring(0, 4).Replace("62","0");
            //var numberResult = resultPhoneIndo.Substring(4, resultPhoneIndo.Length - 4);
            //result = leadingResult + numberResult;
            return result;
        }

        public string GenerateNumberLeadingZero(string phoneNumber)
        {
            var result = phoneNumber;
            var leadingResult = phoneNumber.Substring(0, 4).Replace("62", "0");
            var numberResult = phoneNumber.Substring(4, phoneNumber.Length - 4);
            result = leadingResult + numberResult;
            return result;
        }

        #region private
        private void AddPropertyToDictionary<T>(PropertyDescriptor property, object source, Dictionary<string, string> dictionary)
        {
            var jsonProperty = property.Attributes.OfType<JsonPropertyAttribute>();
            object value = property.GetValue(source);

            if (IsOfType<T>(value))
            {
                string valueData = string.Empty;
                if (property.PropertyType == typeof(bool) || property.PropertyType == typeof(Boolean))
                    valueData = value.ToString().ToLower();
                else
                    valueData = value.ToString();

                if (jsonProperty.Any())
                    dictionary.Add(jsonProperty.First().PropertyName, valueData);
                else
                    dictionary.Add(property.Name, valueData);
            }
        }

        private bool IsOfType<T>(object value)
        {
            return value is T;
        }

        private void ThrowExceptionWhenSourceArgumentIsNull()
        {
            throw new ArgumentNullException("source", "Unable to convert object to a dictionary. The source object is null.");
        }
        #endregion
    }
}
