using System;
using System.Collections.Generic;
using PodiumRNGRemover.Utils;

namespace PodiumRNGRemover
{
    public class PodiumRNGState
    {
        private Dictionary<int, float> pendingDeductions;
        private Dictionary<int, float> appliedDeductions;
        private int countNoDeduction;
        private int count23Deduction;
        private int count33Deduction;

        public PodiumRNGState()
        {
            pendingDeductions = new Dictionary<int, float>();
            appliedDeductions = new Dictionary<int, float>();
            Reset();
        }

        public void Reset()
        {
            pendingDeductions.Clear();
            appliedDeductions.Clear();
            countNoDeduction = 0;
            count23Deduction = 0;
            count33Deduction = 0;
        }

        public void SetPendingDeduction(int splitIndex, float deduction)
        {
            pendingDeductions[splitIndex] = deduction;
        }

        public void ClearPendingDeduction(int splitIndex)
        {
            if (pendingDeductions.ContainsKey(splitIndex))
            {
                pendingDeductions.Remove(splitIndex);
            }
        }

        public bool HasPendingDeduction(int splitIndex)
        {
            return pendingDeductions.ContainsKey(splitIndex);
        }

        public float GetPendingDeduction(int splitIndex)
        {
            return pendingDeductions.ContainsKey(splitIndex) ? pendingDeductions[splitIndex] : 0f;
        }

        public void ApplyDeduction(int splitIndex)
        {
            if (pendingDeductions.ContainsKey(splitIndex))
            {
                float deduction = pendingDeductions[splitIndex];
                appliedDeductions[splitIndex] = deduction;
                pendingDeductions.Remove(splitIndex);
                
                var podiumType = PodiumTypeHelper.GetPodiumType(deduction);
                switch (podiumType)
                {
                    case PodiumTypeHelper.PodiumType.Good:
                        countNoDeduction++;
                        break;
                    case PodiumTypeHelper.PodiumType.Medium:
                        count23Deduction++;
                        break;
                    case PodiumTypeHelper.PodiumType.Bad:
                        count33Deduction++;
                        break;
                    case PodiumTypeHelper.PodiumType.Custom:
                        var closestType = PodiumTypeHelper.GetClosestPodiumTypeForCounting(deduction);
                        switch (closestType)
                        {
                            case PodiumTypeHelper.PodiumType.Good:
                                countNoDeduction++;
                                break;
                            case PodiumTypeHelper.PodiumType.Medium:
                                count23Deduction++;
                                break;
                            case PodiumTypeHelper.PodiumType.Bad:
                                count33Deduction++;
                                break;
                        }
                        break;
                }
            }
            else
            {
                countNoDeduction++;
            }
        }
        
        public void CountSplitWithoutDeduction()
        {
            countNoDeduction++;
        }

        public void UnapplyDeduction(int splitIndex)
        {
            if (appliedDeductions.ContainsKey(splitIndex))
            {
                float deduction = appliedDeductions[splitIndex];
                appliedDeductions.Remove(splitIndex);
                
                var podiumType = PodiumTypeHelper.GetPodiumType(deduction);
                switch (podiumType)
                {
                    case PodiumTypeHelper.PodiumType.Good:
                        countNoDeduction = Math.Max(0, countNoDeduction - 1);
                        break;
                    case PodiumTypeHelper.PodiumType.Medium:
                        count23Deduction = Math.Max(0, count23Deduction - 1);
                        break;
                    case PodiumTypeHelper.PodiumType.Bad:
                        count33Deduction = Math.Max(0, count33Deduction - 1);
                        break;
                    case PodiumTypeHelper.PodiumType.Custom:
                        var closestType = PodiumTypeHelper.GetClosestPodiumTypeForCounting(deduction);
                        switch (closestType)
                        {
                            case PodiumTypeHelper.PodiumType.Good:
                                countNoDeduction = Math.Max(0, countNoDeduction - 1);
                                break;
                            case PodiumTypeHelper.PodiumType.Medium:
                                count23Deduction = Math.Max(0, count23Deduction - 1);
                                break;
                            case PodiumTypeHelper.PodiumType.Bad:
                                count33Deduction = Math.Max(0, count33Deduction - 1);
                                break;
                        }
                        break;
                }
            }
        }

        public List<KeyValuePair<int, float>> UnapplyAllDeductionsAfter(int splitIndex)
        {
            var toRemove = new List<KeyValuePair<int, float>>();
            
            foreach (var kvp in appliedDeductions)
            {
                if (kvp.Key > splitIndex)
                {
                    toRemove.Add(kvp);
                }
            }
            
            foreach (var kvp in toRemove)
            {
                UnapplyDeduction(kvp.Key);
            }
            
            return toRemove;
        }

        public List<KeyValuePair<int, float>> UnapplyAllDeductionsFromAndAfter(int splitIndex)
        {
            var toRemove = new List<KeyValuePair<int, float>>();
            
            foreach (var kvp in appliedDeductions)
            {
                if (kvp.Key >= splitIndex)
                {
                    toRemove.Add(kvp);
                }
            }
            
            foreach (var kvp in toRemove)
            {
                UnapplyDeduction(kvp.Key);
            }
            
            return toRemove;
        }

        public Tuple<int, int, int> GetCounters()
        {
            return new Tuple<int, int, int>(countNoDeduction, count23Deduction, count33Deduction);
        }

        public Dictionary<int, float> GetAllPendingDeductions()
        {
            return new Dictionary<int, float>(pendingDeductions);
        }

        public Dictionary<int, float> GetAllAppliedDeductions()
        {
            return new Dictionary<int, float>(appliedDeductions);
        }

        public float GetTotalReduction()
        {
            float total = 0f;
            foreach (var deduction in appliedDeductions.Values)
            {
                total += deduction;
            }
            return total;
        }
    }
}