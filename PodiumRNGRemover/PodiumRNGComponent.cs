using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using LiveSplit.Model;
using LiveSplit.UI;
using LiveSplit.UI.Components;
using PodiumRNGRemover.Utils;

namespace PodiumRNGRemover
{
    public class PodiumRNGComponent : IComponent
    {
        private LiveSplitState state;
        private PodiumRNGState rngState;
        private KeyboardHook keyboardHook;
        private PodiumRNGSettings settings;
        private Font textFont;
        private Brush textBrush;
        private int previouseSplitIndex = -1;
        
        public string ComponentName => Constants.UI.COMPONENT_NAME;

        public float VerticalHeight => Constants.UI.VERTICAL_HEIGHT;

        public float MinimumWidth => Constants.UI.MINIMUM_WIDTH;

        public float HorizontalWidth => Constants.UI.HORIZONTAL_WIDTH;

        public float MinimumHeight => Constants.UI.MINIMUM_HEIGHT;

        public float PaddingTop => Constants.UI.PADDING_TOP;

        public float PaddingBottom => Constants.UI.PADDING_BOTTOM;

        public float PaddingLeft => Constants.UI.PADDING_LEFT;

        public float PaddingRight => Constants.UI.PADDING_RIGHT;

        public IDictionary<string, Action> ContextMenuControls => new Dictionary<string, Action>();

        public PodiumRNGComponent(LiveSplitState state)
        {
            this.state = state;
            this.rngState = new PodiumRNGState();
            
            settings = new PodiumRNGSettings();
            settings.SettingsChanged += Settings_SettingsChanged;
            
            textFont = null;
            textBrush = null;
            
            state.OnSplit += State_OnSplit;
            state.OnReset += State_OnReset;
            state.OnStart += State_OnStart;
            state.OnSkipSplit += State_OnSkipSplit;
            
            InitializeKeyboardHook();
        }

        private void InitializeKeyboardHook()
        {
            try
            {
                keyboardHook = new KeyboardHook();
                keyboardHook.KeyPressed += OnKeyPressed;
                
                try { keyboardHook.RegisterHotKey(settings.Key1); } catch { }
                try { keyboardHook.RegisterHotKey(settings.Key2); } catch { }
                try { keyboardHook.RegisterHotKey(settings.Key3); } catch { }
                try { keyboardHook.RegisterHotKey(settings.Key4); } catch { }
            }
            catch
            {
                keyboardHook?.Dispose();
                keyboardHook = null;
            }
        }

        private void OnKeyPressed(object sender, KeyPressedEventArgs e)
        {
            if (state.CurrentPhase != TimerPhase.Running)
                return;

            int currentSplitIndex = state.CurrentSplitIndex;
            if (currentSplitIndex < 0 || currentSplitIndex >= state.Run.Count)
                return;

            if (e.Key == settings.Key1)
            {
                rngState.SetPendingDeduction(currentSplitIndex, settings.Value1);
            }
            else if (e.Key == settings.Key2)
            {
                rngState.SetPendingDeduction(currentSplitIndex, settings.Value2);
            }
            else if (e.Key == settings.Key3)
            {
                rngState.SetPendingDeduction(currentSplitIndex, settings.Value3);
            }
            else if (e.Key == settings.Key4)
            {
                rngState.ClearPendingDeduction(currentSplitIndex);
            }
        }

        private void State_OnSplit(object sender, EventArgs e)
        {
            int previousSplitIndex = state.CurrentSplitIndex - 1;
            DeductionProcessor.ProcessSplitDeduction(state, rngState, previousSplitIndex);
        }

        private void State_OnReset(object sender, TimerPhase value)
        {
            rngState.Reset();
        }

        private void State_OnStart(object sender, EventArgs e)
        {
            rngState.Reset();
            previouseSplitIndex = -1;
        }

        private void State_OnSkipSplit(object sender, EventArgs e)
        {
            int skippedSplitIndex = state.CurrentSplitIndex - 1;
            DeductionProcessor.ProcessSplitDeduction(state, rngState, skippedSplitIndex);
        }

        private void Settings_SettingsChanged(object sender, EventArgs e)
        {
            ReinitializeKeyboardHook();
        }

        private void ReinitializeKeyboardHook()
        {
            if (keyboardHook != null)
            {
                keyboardHook.KeyPressed -= OnKeyPressed;
                keyboardHook.Dispose();
                keyboardHook = null;
            }
            
            System.GC.Collect();
            System.GC.WaitForPendingFinalizers();
            
            InitializeKeyboardHook();
        }


        public void DrawHorizontal(Graphics g, LiveSplitState state, float height, Region clipRegion)
        {
            DrawLabels(g, state, HorizontalWidth, height);
        }

        public void DrawVertical(Graphics g, LiveSplitState state, float width, Region clipRegion)
        {
            DrawLabels(g, state, width, VerticalHeight);
        }

        private void DrawLabels(Graphics g, LiveSplitState state, float width, float height)
        {
            var currentFont = state.LayoutSettings.TextFont;
            
            using (var currentTextBrush = new SolidBrush(state.LayoutSettings.TextColor))
            {
                float lineHeight = height / 4;
                float currentY = 0;
                
                using (var centerFormat = new StringFormat 
                { 
                    LineAlignment = StringAlignment.Center,
                    Alignment = StringAlignment.Center
                })
                {
                    string titleText = Constants.Messages.TITLE_TEXT;
                    var titleRect = new RectangleF(0, currentY, width, lineHeight);
                    using (var titleFont = new Font(currentFont.FontFamily, currentFont.Size, FontStyle.Bold))
                    {
                        g.DrawString(titleText, titleFont, currentTextBrush, titleRect, centerFormat);
                    }
                    currentY += lineHeight;
                    
                    using (var separatorPen = new Pen(state.LayoutSettings.TextColor, 0.5f))
                    {
                        g.DrawLine(separatorPen, 10, currentY - 2, width - 10, currentY - 2);
                    }
                    
                    var counters = rngState.GetCounters();
                    string counterText = string.Format(Constants.Messages.COUNTERS_FORMAT, counters.Item1, counters.Item2, counters.Item3);
                    var counterRect = new RectangleF(0, currentY, width, lineHeight);
                    g.DrawString(counterText, currentFont, currentTextBrush, counterRect, centerFormat);
                    currentY += lineHeight;
                    
                    float totalReduction = rngState.GetTotalReduction();
                    string totalText = string.Format(Constants.Messages.TOTAL_REDUCTION_FORMAT, totalReduction);
                    var totalRect = new RectangleF(0, currentY, width, lineHeight);
                    g.DrawString(totalText, currentFont, currentTextBrush, totalRect, centerFormat);
                    currentY += lineHeight;
                    
                    if (state.CurrentPhase == TimerPhase.Running && state.CurrentSplitIndex >= 0)
                    {
                        string displayText;
                        
                        if (rngState.HasPendingDeduction(state.CurrentSplitIndex))
                        {
                            float deduction = rngState.GetPendingDeduction(state.CurrentSplitIndex);
                            string podiumText = PodiumTypeHelper.GetPodiumDisplayText(deduction);
                            displayText = Constants.Messages.CURRENT_SPLIT_PREFIX + podiumText;
                            
                            using (var displayBrush = new SolidBrush(Color.Orange))
                            {
                                var displayRect = new RectangleF(0, currentY, width, lineHeight);
                                g.DrawString(displayText, currentFont, displayBrush, displayRect, centerFormat);
                            }
                        }
                        else
                        {
                            displayText = Constants.Messages.CURRENT_SPLIT_PREFIX + Constants.Messages.NO_DEDUCTION_TEXT;
                            var displayRect = new RectangleF(0, currentY, width, lineHeight);
                            g.DrawString(displayText, currentFont, currentTextBrush, displayRect, centerFormat);
                        }
                    }
                }
            }
        }

        public Control GetSettingsControl(LayoutMode mode)
        {
            return settings;
        }

        public XmlNode GetSettings(XmlDocument document)
        {
            return settings.GetSettings(document);
        }

        public void SetSettings(XmlNode settingsNode)
        {
            settings.SetSettings(settingsNode);
            
            ReinitializeKeyboardHook();
        }

        public void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode)
        {
            int currentSplitIndex = state.CurrentSplitIndex;
            
            if (previouseSplitIndex != -1 && currentSplitIndex <= previouseSplitIndex)
            {
                HandleBackwardNavigation(currentSplitIndex);
            }
            
            previouseSplitIndex = currentSplitIndex;
            
            if (invalidator != null)
            {
                invalidator.Invalidate(0, 0, width, height);
            }
        }
        
        private void HandleBackwardNavigation(int currentSplitIndex)
        {
            var unappliedDeductions = rngState.UnapplyAllDeductionsFromAndAfter(currentSplitIndex);
            
            var gameTime = state.CurrentTime.GameTime;
            if (gameTime != null && unappliedDeductions.Count > 0)
            {
                float totalToRestore = 0f;
                foreach (var kvp in unappliedDeductions)
                {
                    totalToRestore += kvp.Value;
                }
                
                DeductionProcessor.RestoreDeductionToGameTime(state, totalToRestore);
            }
            
            var pendingDeductions = rngState.GetAllPendingDeductions();
            foreach (var kvp in pendingDeductions)
            {
                if (kvp.Key > currentSplitIndex)
                {
                    rngState.ClearPendingDeduction(kvp.Key);
                }
            }
        }

        public void Dispose()
        {
            try
            {
                if (state != null)
                {
                    state.OnSplit -= State_OnSplit;
                    state.OnReset -= State_OnReset;
                    state.OnStart -= State_OnStart;
                    state.OnSkipSplit -= State_OnSkipSplit;
                }
                
                if (settings != null)
                {
                    settings.SettingsChanged -= Settings_SettingsChanged;
                }
                
                if (keyboardHook != null)
                {
                    keyboardHook.KeyPressed -= OnKeyPressed;
                    keyboardHook.Dispose();
                    keyboardHook = null;
                }
                
                textFont = null;
                textBrush = null;
                settings = null;
                rngState = null;
                state = null;
            }
            catch
            {
            }
        }
    }
}
