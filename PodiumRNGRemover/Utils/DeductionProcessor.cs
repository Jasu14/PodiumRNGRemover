using System;
using LiveSplit.Model;

namespace PodiumRNGRemover.Utils
{
    public static class DeductionProcessor
    {
        public static void ApplyDeductionToGameTime(LiveSplitState state, float deduction)
        {
            var gameTime = state.CurrentTime.GameTime;
            if (gameTime != null)
            {
                var adjustedGameTime = gameTime.Value - TimeSpan.FromSeconds(deduction);
                state.SetGameTime(adjustedGameTime);
            }
        }

        public static void RestoreDeductionToGameTime(LiveSplitState state, float deduction)
        {
            var gameTime = state.CurrentTime.GameTime;
            if (gameTime != null)
            {
                var restoredGameTime = gameTime.Value + TimeSpan.FromSeconds(deduction);
                state.SetGameTime(restoredGameTime);
            }
        }

        public static void ProcessSplitDeduction(LiveSplitState state, PodiumRNGState rngState, int splitIndex)
        {
            if (splitIndex >= 0 && rngState.HasPendingDeduction(splitIndex))
            {
                float deduction = rngState.GetPendingDeduction(splitIndex);
                ApplyDeductionToGameTime(state, deduction);
                rngState.ApplyDeduction(splitIndex);
            }
        }
    }
}