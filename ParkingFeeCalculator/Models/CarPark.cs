using ParkingFeeCalculator.Services;

namespace ParkingFeeCalculator.Models
{
    public class CarPark
    {
        public String Name { get; private set; }
        public List<RuleCalculatorBase> CarWeekdayFeeRuleCalculators { get; private set; }
        public List<RuleCalculatorBase> CarWeekendFeeRuleCalculators { get; private set; }
        public int GracePeriodInMinutes { get; private set; }

        public List<RuleCalculatorBase> MotorCycleFeeRuleCalculators { get; private set; }


        public CarPark(String name, List<RuleCalculatorBase> carWeekdayFeeRuleCalculators, List<RuleCalculatorBase> carWeekendFeeRuleCalculators, List<RuleCalculatorBase> motorCycleFeeRuleCalculators, int gracePeriodInMinutes)
        {
            this.Name = name;
            this.GracePeriodInMinutes = gracePeriodInMinutes;
            this.CarWeekdayFeeRuleCalculators = carWeekdayFeeRuleCalculators;
            this.CarWeekendFeeRuleCalculators = carWeekendFeeRuleCalculators;
            this.MotorCycleFeeRuleCalculators = motorCycleFeeRuleCalculators;
        }
    }
}