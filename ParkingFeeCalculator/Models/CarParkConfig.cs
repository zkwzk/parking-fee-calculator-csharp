using ParkingFeeCalculator.Services;
using ParkingFeeCalculator.Services.FeeRuleCalculators;

namespace ParkingFeeCalculator.Models
{
    public static class CarParkConfig
    {
        private static readonly FixedFirstXMinutesRuleCalculator plazaSingapuraCarWeekday0to1759 =
            new FixedFirstXMinutesRuleCalculator(new TimeOnly(0, 0), new TimeOnly(17, 59), 60, 15, 1.95m, 0.55m);
        private static readonly FixedFeePerEntryRuleCalculator plazaSingapuraCarWeekday18to2359 =
            new FixedFeePerEntryRuleCalculator(new TimeOnly(18, 00), new TimeOnly(23, 59), 3.25m);

        private static readonly FixedFeePerXMinutesRuleCalculator plazaSingapuraCarWeekendPH0to0259 =
            new FixedFeePerXMinutesRuleCalculator(new TimeOnly(0, 0), new TimeOnly(2, 59), 15, 0.55m);
        private static readonly FixedFirstXMinutesRuleCalculator plazaSingapuraCarWeekendPH03to1759 =
            new FixedFirstXMinutesRuleCalculator(new TimeOnly(3, 0), new TimeOnly(17, 59), 120, 15, 3.25m, 0.55m);
        private static readonly FixedFeePerEntryRuleCalculator plazaSingapuraCarWeekend18to2359 =
            new FixedFeePerEntryRuleCalculator(new TimeOnly(18, 00), new TimeOnly(23, 59), 3.25m);

        private static readonly FixedFeePerEntryRuleCalculator plazaSingapuraMotorcyclePerEntry =
            new FixedFeePerEntryRuleCalculator(new TimeOnly(0, 0), new TimeOnly(23, 59), 1.3m);

        public static CarPark PlazaSingapuraCarPark = new CarPark(
            "Plaza Singapura Car Park",
            new List<RuleCalculatorBase>
            {
                plazaSingapuraCarWeekday0to1759,
                plazaSingapuraCarWeekday18to2359,
            },
            new List<RuleCalculatorBase>
            {
                plazaSingapuraCarWeekendPH0to0259,
                plazaSingapuraCarWeekendPH03to1759,
                plazaSingapuraCarWeekend18to2359,
            },
            new List<RuleCalculatorBase>
            {
                plazaSingapuraMotorcyclePerEntry
            },
            15
        );

        private static readonly FixedFirstXMinutesRuleCalculator orchardCentralWeekday0to1759 =
            new FixedFirstXMinutesRuleCalculator(new TimeOnly(0, 0), new TimeOnly(17, 59), 60, 15, 2.73m, 0.68m);
        private static readonly FixedFeePerEntryRuleCalculator orchardCentralWeekday18to2359 =
            new FixedFeePerEntryRuleCalculator(new TimeOnly(18, 0), new TimeOnly(23, 59), 4.09m);

        private static readonly FixedFirstXMinutesRuleCalculator orchardCentralWeekendPH0to1759 =
            new FixedFirstXMinutesRuleCalculator(new TimeOnly(0, 0), new TimeOnly(17, 59), 60, 15, 2.94m, 0.74m);
        private static readonly FixedFeePerEntryRuleCalculator orchardCentralWeekendPH18to2359 =
            new FixedFeePerEntryRuleCalculator(new TimeOnly(18, 0), new TimeOnly(23, 59), 4.41m);

        public static CarPark OrchardCentralCarPark = new CarPark(
            "Orchard Central Car Park",
            new List<RuleCalculatorBase>
            {
                orchardCentralWeekday0to1759,
                orchardCentralWeekday18to2359,
            },
            new List<RuleCalculatorBase>
            {
                orchardCentralWeekendPH0to1759,
                orchardCentralWeekendPH18to2359,
            },
            new List<RuleCalculatorBase>(),
            10
        );


        private static readonly FixedFeePerXMinutesRuleCalculator tscWeekday0to1159 =
            new FixedFeePerXMinutesRuleCalculator(new TimeOnly(0, 0), new TimeOnly(11, 59), 30, 1.31m);
        private static readonly FixedFeePerXMinutesRuleCalculator tscWeekday12to1359 =
            new FixedFeePerXMinutesRuleCalculator(new TimeOnly(12, 0), new TimeOnly(13, 59), 30, 1.85m);
        private static readonly FixedFeePerXMinutesRuleCalculator tscWeekday14to1659 =
            new FixedFeePerXMinutesRuleCalculator(new TimeOnly(14, 0), new TimeOnly(16, 59), 30, 1.31m);
        private static readonly FixedFeePerXMinutesRuleCalculator tscWeekday17to1859 =
            new FixedFeePerXMinutesRuleCalculator(new TimeOnly(17, 0), new TimeOnly(18, 59), 30, 1.85m);
        private static readonly FixedFeePerEntryRuleCalculator tscWeekday19to2359 =
            new FixedFeePerEntryRuleCalculator(new TimeOnly(19, 0), new TimeOnly(23, 59), 4.36m);

        private static readonly FixedFirstXMinutesRuleCalculator tscWeekendPH0to1159 =
            new FixedFirstXMinutesRuleCalculator(new TimeOnly(0, 0), new TimeOnly(11, 59), 60, 30, 2.62m, 1.64m);
        private static readonly FixedFirstXMinutesRuleCalculator tscWeekendPH12to1359 =
            new FixedFirstXMinutesRuleCalculator(new TimeOnly(12, 0), new TimeOnly(13, 59), 60, 30, 3.71m, 2.18m);
        private static readonly FixedFirstXMinutesRuleCalculator tscWeekendPH14to1659 =
            new FixedFirstXMinutesRuleCalculator(new TimeOnly(14, 0), new TimeOnly(16, 59), 60, 30, 2.62m, 1.64m);
        private static readonly FixedFirstXMinutesRuleCalculator tscWeekendPH17to1859 =
            new FixedFirstXMinutesRuleCalculator(new TimeOnly(17, 0), new TimeOnly(18, 59), 60, 30, 3.71m, 2.18m);
        private static readonly FixedFeePerEntryRuleCalculator tscWeekendPH19to2359 =
            new FixedFeePerEntryRuleCalculator(new TimeOnly(19, 0), new TimeOnly(23, 59), 4.36m);

        public static CarPark TSCCarPark = new CarPark(
            "Takashimaya Shopping Centre",
            new List<RuleCalculatorBase>
            {
            tscWeekday0to1159,
            tscWeekday12to1359,
            tscWeekday14to1659,
            tscWeekday17to1859,
            tscWeekday19to2359
            },
            new List<RuleCalculatorBase>
            {
            tscWeekendPH0to1159,
            tscWeekendPH12to1359,
            tscWeekendPH14to1659,
            tscWeekendPH17to1859,
            tscWeekendPH19to2359
            },
            new List<RuleCalculatorBase>(),
            10
        );

        public static List<CarPark> CarParks = new List<CarPark>
        {
            PlazaSingapuraCarPark,
            OrchardCentralCarPark,
            TSCCarPark
        };
    }
}