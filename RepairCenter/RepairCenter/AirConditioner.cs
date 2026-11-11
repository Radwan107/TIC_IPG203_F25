namespace RepairCenter
{
    public class AirConditioner : Device
    {
        public AirConditioner(string customerName, string customerPhone, double repairCost)
            : base(customerName, customerPhone, repairCost) { }

        public override double CalculateRepairCost()
        {
            // +15% for gas refill and filter cleaning
            return RepairCost * 1.15;
        }

        public override string GetDeviceType() => "AirConditioner";
    }
}