using System.Text.RegularExpressions;

namespace WpfApp_Project.Validators
{
    public class CPFValidate
    {
        public bool IsValidCpf(string cpf)
        {

            cpf = cpf.Replace(".", "").Replace("-", "");


            if (cpf.Length != 11 || !Regex.IsMatch(cpf, @"^\d{11}$"))
            {
                return false;
            }


            if (new string(cpf[0], 11) == cpf)
            {
                return false;
            }


            int sum1 = 0;
            for (int i = 0; i < 9; i++)
            {
                sum1 += (cpf[i] - '0') * (10 - i);
            }

            int remainder1 = sum1 % 11;
            int firstDigit = remainder1 < 2 ? 0 : 11 - remainder1;


            int sum2 = 0;
            for (int i = 0; i < 10; i++)
            {
                sum2 += (cpf[i] - '0') * (11 - i);
            }

            int remainder2 = sum2 % 11;
            int secondDigit = remainder2 < 2 ? 0 : 11 - remainder2;


            return cpf[9] - '0' == firstDigit && cpf[10] - '0' == secondDigit;
        }
    }
}
