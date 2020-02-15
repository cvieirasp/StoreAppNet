using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Store.Business.Validations.Documents
{
    public class CpfValidation
    {
        public const int CPF_LENGTH = 11;

        public static bool Validate(string cpf)
        {
            var cpfDigits = Utils.JustDigits(cpf);
            if (!HasValidLength(cpfDigits))
                return false;
            return !HasRepeatedDigits(cpfDigits) && HasValidDigits(cpfDigits);
        }

        private static bool HasValidLength(string value)
        {
            return value.Length == CPF_LENGTH;
        }

        private static bool HasRepeatedDigits(string value)
        {
            string[] invalidNumbers =
            {
                "00000000000", "11111111111", "22222222222", "33333333333", "44444444444",
                "55555555555", "66666666666", "77777777777", "88888888888", "99999999999"
            };
            return invalidNumbers.Contains(value);
        }

        private static bool HasValidDigits(string value)
        {
            var number = value.Substring(0, CPF_LENGTH - 2);
            var verifyingDigit = new VerifyingDigit(number)
                .WithMultipliersFromTo(2, 11)
                .Replacing("0", 10, 11);
            var firstDigit = verifyingDigit.CalculateDigit();
            verifyingDigit.AddDigit(firstDigit);
            var secondDigit = verifyingDigit.CalculateDigit();

            return string.Concat(firstDigit, secondDigit) == value.Substring(CPF_LENGTH - 2, 2);
        }
    }

    public class CnpjValidation
    {
        public const int CNPJ_LENGTH = 14;

        public static bool Validate(string cnpj)
        {
            var cnpjDigits = Utils.JustDigits(cnpj);
            if (!HasValidLength(cnpjDigits))
                return false;
            return !HasRepeatedDigits(cnpjDigits) && HasValidDigits(cnpjDigits);
        }

        private static bool HasValidLength(string value)
        {
            return value.Length == CNPJ_LENGTH;
        }

        private static bool HasRepeatedDigits(string value)
        {
            string[] invalidNumbers =
            {
                "00000000000000", "11111111111111", "22222222222222", "33333333333333", "44444444444444",
                "55555555555555", "66666666666666", "77777777777777", "88888888888888", "99999999999999"
            };
            return invalidNumbers.Contains(value);
        }

        private static bool HasValidDigits(string value)
        {
            var number = value.Substring(0, CNPJ_LENGTH - 2);

            var verifyingDigit = new VerifyingDigit(number)
                .WithMultipliersFromTo(2, 9)
                .Replacing("0", 10, 11);
            var firstDigit = verifyingDigit.CalculateDigit();
            verifyingDigit.AddDigit(firstDigit);
            var secondDigit = verifyingDigit.CalculateDigit();

            return string.Concat(firstDigit, secondDigit) == value.Substring(CNPJ_LENGTH - 2, 2);
        }
    }

    public class VerifyingDigit
    {
        private string _digit;
        private const int MODULE = 11;
        private readonly List<int> _multipliers = new List<int> { 2, 3, 4, 5, 6, 7, 8, 9 };
        private readonly IDictionary<int, string> _substitutions = new Dictionary<int, string>();
        private bool _moduleComplement = true;

        public VerifyingDigit(string number)
        {
            _digit = number;
        }

        public VerifyingDigit WithMultipliersFromTo(int firstMultipliers, int lastMultipliers)
        {
            _multipliers.Clear();
            for (var i = firstMultipliers; i <= lastMultipliers; i++)
                _multipliers.Add(i);
            return this;
        }

        public VerifyingDigit Replacing(string substitute, params int[] digits)
        {
            foreach (var i in digits)
                _substitutions[i] = substitute;
            return this;
        }

        public void AddDigit(string digit)
        {
            _digit = string.Concat(_digit, digit);
        }

        public string CalculateDigit()
        {
            return !(_digit.Length > 0) ? "" : GetDigitSum();
        }

        private string GetDigitSum()
        {
            var sum = 0;
            for (int i = _digit.Length - 1, m = 0; i >= 0; i--)
            {
                var product = (int)char.GetNumericValue(_digit[i]) * _multipliers[m];
                sum += product;

                if (++m >= _multipliers.Count) 
                    m = 0;
            }

            var mod = (sum % MODULE);
            var result = _moduleComplement ? MODULE - mod : mod;

            return _substitutions.ContainsKey(result) ? _substitutions[result] : result.ToString();
        }
    }

    public class Utils
    {
        public static string JustDigits(string value)
        {
            var onlyNumber = string.Empty;
            foreach (var s in value)
                if (char.IsDigit(s))
                    onlyNumber += s;
            return onlyNumber.Trim();
        }
    }
}
