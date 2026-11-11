using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter
{
    public class WashingMachine : Device
    {
        public WashingMachine(string customerName, string customerPhone, double repairCost)
            : base(customerName, customerPhone, repairCost) { }

        public override double CalculateRepairCost()
        {
            // +10% for water pump check
            return RepairCost * 1.10;
        }

        public override string GetDeviceType() => "WashingMachine";
    }
}