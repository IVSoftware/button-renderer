# Custom color for Button focus rectangle.

If you "have to custom paint that" it doesn't have to be a _bad_ thing. You go in your designer.cs file and manually swap out all `Button` for `ButtonEx` then you can have a design-time property for the focus color.

[![design time property][1]][1]

[![tab action][2]][2]

Then make sure the first button is the "one and only" control with `TabIndex=0`.

___

```
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
                focusRect.Inflate(-6, -6);
                e.Graphics.DrawRectangle(focusPen, focusRect);
            }
        }
    }
}
```
___
**Testing**

The code as written should navigate correctly when:

 - The **[Tab]** key is used to navigate.
 - The mouse is used to click the button.
 - `button.Focus()` is called programmatically.
 - `SelectNextControl()` is called from the currently focused control.
 - `ActiveControl` is set programmatically.
___
In order to behave:

 - The `TabOrder` of the controls needs to be set correctly 
 - The `TabStop` property should be `true` for controls you want to tab otherwise false.
 - The button container (e.g. Form) must be active.
 - The button that should have focus must have the lowest tab index.

**Testbench**

Here's the code I used to test this answer with with the values for `TabIndex` as shown in VS Menu\View\Tab Order. [Clone](https://github.com/IVSoftware/button-renderer.git).

[![tab order][3]][3]

```
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
                    } while (_timerMode == TimerMode.SelectNext);
                    _busy.Release();
                    break;
            }
        };
    }
    TimerMode _timerMode = TimerMode.None;
    SemaphoreSlim _busy = new SemaphoreSlim(1, 1);
}
```



  [1]: https://i.stack.imgur.com/011V4.png
  [2]: https://i.stack.imgur.com/p5JJd.png
  [3]: https://i.stack.imgur.com/iRKVO.png