using System;

namespace PodiumRNGRemover.Utils
{
    public static class PodiumTypeHelper
    {
        public enum PodiumType
        {
            Good,
            Medium,
            Bad,
            Custom
        }

        public static bool AreFloatsEqual(float value1, float value2)
        {
            return Math.Abs(value1 - value2) < Constants.FLOAT_COMPARISON_TOLERANCE;
        }

        public static PodiumType GetPodiumType(float deduction)
        {
            if (AreFloatsEqual(deduction, Constants.GOOD_PODIUM_VALUE))
                return PodiumType.Good;
            
            if (AreFloatsEqual(deduction, Constants.MEDIUM_PODIUM_VALUE))
                return PodiumType.Medium;
            
            if (AreFloatsEqual(deduction, Constants.BAD_PODIUM_VALUE))
                return PodiumType.Bad;
            
            return PodiumType.Custom;
        }

        public static string GetPodiumDisplayText(float deduction)
        {
            var podiumType = GetPodiumType(deduction);
            
            switch (podiumType)
            {
                case PodiumType.Good:
                    return Constants.Messages.GOOD_PODIUM_TEXT;
                    
                case PodiumType.Medium:
                    return string.Format(Constants.Messages.MEDIUM_PODIUM_FORMAT, deduction);
                    
                case PodiumType.Bad:
                    return string.Format(Constants.Messages.BAD_PODIUM_FORMAT, deduction);
                    
                case PodiumType.Custom:
                default:
                    return string.Format(Constants.Messages.CUSTOM_PODIUM_FORMAT, deduction);
            }
        }

        public static PodiumType GetClosestPodiumTypeForCounting(float deduction)
        {
            if (Math.Abs(deduction) < Constants.FLOAT_COMPARISON_TOLERANCE)
                return PodiumType.Good;
            
            if (Math.Abs(deduction - Constants.MEDIUM_PODIUM_VALUE) < Math.Abs(deduction - Constants.BAD_PODIUM_VALUE))
                return PodiumType.Medium;
            else
                return PodiumType.Bad;
        }
    }
}