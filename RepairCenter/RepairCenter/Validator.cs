using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter
{
    public static class Validator
    {
        public static bool IsValidPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return false;
            if (phone.Length < 7) return false;
            foreach (char c in phone)
            {
                if (!char.IsDigit(c)) return false;
            }
            return true;
        }

        public static bool IsValidName(string name)
        {
            return !string.IsNullOrWhiteSpace(name) && name.Length >= 3;
        }

        public static bool IsValidCost(double cost)
        {
            return cost >= 0;
        }
    }

}
