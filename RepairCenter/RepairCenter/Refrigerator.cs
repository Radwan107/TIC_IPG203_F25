using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter
{
    public class Refrigerator : Device
    {
        public Refrigerator(string customerName, string customerPhone, double repairCost)
            : base(customerName, customerPhone, repairCost) { }

        public override double CalculateRepairCost()
        {
            // +20% for gas and compressor check
            return RepairCost * 1.20;
        }

        public override string GetDeviceType() => "Refrigerator";
    }
}