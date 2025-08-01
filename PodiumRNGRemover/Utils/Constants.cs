using System.Windows.Forms;

namespace PodiumRNGRemover.Utils
{
    public static class Constants
    {
        public const float GOOD_PODIUM_VALUE = 0f;
        public const float MEDIUM_PODIUM_VALUE = 2.3f;
        public const float BAD_PODIUM_VALUE = 3.3f;
        
        public const float FLOAT_COMPARISON_TOLERANCE = 0.01f;
        
        public static class DefaultKeys
        {
            public static readonly Keys KEY1 = Keys.D1;
            public static readonly Keys KEY2 = Keys.D2;
            public static readonly Keys KEY3 = Keys.D3;
            public static readonly Keys KEY4 = Keys.D0;
        }
        
        public static class UI
        {
            public const string COMPONENT_NAME = "Podium RNG Remover";
            public const string COMPONENT_DESCRIPTION = "Manages time deductions for randomized podiums by pressing configured keys";
            public const string COMPONENT_VERSION = "1.0.0";
            
            public const float VERTICAL_HEIGHT = 80f;
            public const float HORIZONTAL_WIDTH = 250f;
            public const float MINIMUM_WIDTH = 200f;
            public const float MINIMUM_HEIGHT = 80f;
            
            public const float PADDING_TOP = 0f;
            public const float PADDING_BOTTOM = 0f;
            public const float PADDING_LEFT = 7f;
            public const float PADDING_RIGHT = 7f;
            
            public const string DEFAULT_FONT_NAME = "Segoe UI";
            public const float DEFAULT_FONT_SIZE = 8f;
        }
        
        public static class Messages
        {
            public const string TITLE_TEXT = "Podium RNG";
            public const string CURRENT_SPLIT_PREFIX = "Current Split: ";
            public const string NO_DEDUCTION_TEXT = "No deduction";
            public const string GOOD_PODIUM_TEXT = "Has a Good Podium";
            public const string MEDIUM_PODIUM_FORMAT = "Has a Medium Podium (-{0}s to substract)";
            public const string BAD_PODIUM_FORMAT = "Has a Hard Podium (-{0}s to substract)";
            public const string CUSTOM_PODIUM_FORMAT = "Has Podium (-{0}s to substract)";
            public const string COUNTERS_FORMAT = "Good: {0} | Medium: {1} | Bad: {2}";
            public const string TOTAL_REDUCTION_FORMAT = "Total reduction: {0:F1}s";
        }
    }
}