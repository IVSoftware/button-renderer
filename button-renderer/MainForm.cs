using IVSoftware.Portable;
using System.Windows.Forms.VisualStyles;

namespace button_renderer
{
    public partial class MainForm : Form
    {
        enum TimerMode { None, FocusNext, SelectNext, NextActive }
        public MainForm()
        {
            InitializeComponent();
            comboBoxTimers.Items.AddRange(Enum.GetValues(typeof(TimerMode)).OfType<object>().ToArray());
            comboBoxTimers.SelectedIndex = 0;
            comboBoxTimers.SelectionChangeCommitted += async(sender, e) =>
            {
                _timerMode = TimerMode.None;
                await _busy.WaitAsync();
                _timerMode = (TimerMode)(comboBoxTimers.SelectedItem ?? TimerMode.None);
                BeginInvoke(()=>ActiveControl = button1);
                string name;
                switch (_timerMode)
                {
                    case TimerMode.FocusNext:
                        do
                        {
                            await Task.Delay(1000);
                            if (!comboBoxTimers.DroppedDown)
                            {
                                name = ActiveControl?.Name ?? string.Empty;
                                switch (name)
                                {
                                    case nameof(button1): button2.Focus(); break;
                                    case nameof(button2): button3.Focus(); break;
                                    default: case nameof(button3): button1.Focus(); break;
                                }
                            }
                        } while (_timerMode == TimerMode.FocusNext);
                        _busy.Release();
                        break;
                    case TimerMode.SelectNext:
                        do
                        {
                            await Task.Delay(1000);
                            if (!comboBoxTimers.DroppedDown)
                            {
                                if (ActiveControl != null) SelectNextControl(
                                    ctl: ActiveControl,
                                    forward: true,
                                    tabStopOnly: true,
                                    nested: true,
                                    wrap: true
                                );
                            }
                        } while (_timerMode == TimerMode.SelectNext);
                        _busy.Release();
                        break;
                    case TimerMode.NextActive:
                        do
                        {
                            await Task.Delay(1000);
                            if (!comboBoxTimers.DroppedDown)
                            {
                                name = ActiveControl?.Name ?? string.Empty;
                                switch (name)
                                {
                                    case nameof(button1): ActiveControl = button2; break;
                                    case nameof(button2): ActiveControl = button3; break;
                                    default: case nameof(button3): ActiveControl = button1; break;
                                }
                            }
                        } while (_timerMode == TimerMode.NextActive);
                        _busy.Release();
                        break;
                }
            };
        }
        TimerMode _timerMode = TimerMode.None;
        SemaphoreSlim _busy = new SemaphoreSlim(1, 1);
    }
    class ButtonEx : Button
    {
        public Color FocusRectangleColor
        {
            get => _focusRectangleColor;
            set
            {
                if (!Equals(_focusRectangleColor, value))
                {
                    _focusRectangleColor = value;
                    Refresh();
                }   
            }
        }
        Color _focusRectangleColor = Color.Red;

        public Color FocusCueColor
        {
            get => _focusCueColor;
            set
            {
                if (!Equals(_focusCueColor, value))
                {
                    _focusCueColor = value;
                    Refresh();
                }
            }
        }
        Color _focusCueColor = default;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (Enabled && Focused && (MouseButtons == MouseButtons.None))
            {
                if (FocusRectangleColor != default)
                {
                    using (Pen dottedPen = new Pen(FocusRectangleColor, 2f))
                    {
                        dottedPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                        Rectangle focusRect = this.ClientRectangle;
                        focusRect.Inflate(-12, -12);
                        e.Graphics.DrawRectangle(dottedPen, focusRect);
                    }
                }
                if (FocusCueColor != default)
                {
                    using (Pen cuePen = new Pen(FocusCueColor, FlatAppearance.BorderSize))
                    {
                        Rectangle focusRect = this.ClientRectangle;
                        focusRect.Inflate(-2, -2);
                        e.Graphics.DrawRectangle(cuePen, focusRect);
                    }
                }
            }
        }
    }
}
