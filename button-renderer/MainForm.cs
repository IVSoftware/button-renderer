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
                BeginInvoke(()=>ActiveControl = button1);
                string name;
                var timerMode = (TimerMode)(comboBoxTimers.SelectedItem ?? TimerMode.None);
                switch (timerMode)
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
                        } while (timerMode == TimerMode.FocusNext);
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
                        } while (timerMode == TimerMode.SelectNext);
                        break;
                    case TimerMode.NextActive:
                        do
                        {
                            if (!comboBoxTimers.DroppedDown)
                            {
                                await Task.Delay(1000);
                                name = ActiveControl?.Name ?? string.Empty;
                                switch (name)
                                {
                                    case nameof(button1): ActiveControl = button2; break;
                                    case nameof(button2): ActiveControl = button3; break;
                                    default: case nameof(button3): ActiveControl = button1; break;
                                }
                            }
                        } while (timerMode == TimerMode.SelectNext);
                        break;
                }
            };
        }
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
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (Focused)
            {
                using (Pen focusPen = new Pen(FocusRectangleColor, 2f))
                {
                    focusPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                    Rectangle focusRect = this.ClientRectangle;
                    focusRect.Inflate(-7, -7);
                    e.Graphics.DrawRectangle(focusPen, focusRect);
                }
            }
        }
    }
}
