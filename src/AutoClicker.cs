using System.Diagnostics;
using System.Windows.Controls.Ribbon;
using System.Windows.Input;
using Timer = System.Windows.Forms.Timer;

namespace AutoClicker
{
    public partial class AutoClicker : Form
    {
        // The text that explains the numeric up and down
        readonly Label numericUpAndDownText = new()
        { 
            Text = "Time between clicks (in ms)",
            Location = new Point(10, 10),
            Size = new Size(160, 20),
        };
        // The text that explains the drop down
        readonly Label dropDownText = new()
        {
            Text = "Mouse Buttons",
            Location = new Point(220, 10),
            Size = new Size(160, 20),
        };
        // The text that explains the second numeric up and down
        readonly Label stopAfterClicksText = new()
        {
            Text = "Stop after amount of clicks",
            Location = new Point(10, 60),
            Size = new Size(160, 20),
        };
        // The text that explains the third numeric up and down 
        readonly Label stopAfterTimeText = new()
        {
            Text = "Stop after amount of time (in seconds)",
            Location = new Point(220, 60),
            Size = new Size(160, 20),
        };

        // The drop down to chose the mouse button being clicked
        readonly ComboBox comboBox = new() 
        { 
            Location = new Point(220, 30),
            Size = new Size(150, 10),
            DropDownStyle = ComboBoxStyle.DropDownList,
        };
        // The field the users use to change the time between clicks
        readonly NumericUpDown numericUpDown = new()
        {
            Value = 1,
            Location = new Point(10, 30),
            Size = new Size(150, 30),
            Maximum = 1000,
        };
        // Get the amount of clicks before ending the clicker
        readonly NumericUpDown stopAfterClicks = new()
        {
            Location = new Point(10, 80),
            Size = new Size(150, 10),
        };
        // Get the amount of time before ending the clicker
        readonly NumericUpDown stopAfterTime = new()
        {
            Location = new Point(220, 80),
            Size = new Size(150, 10),
        };

        // The button that starts the clicker
        readonly Button StartStopButton = new()
        {
            Location = new Point(110, 120),
            Size = new Size(150, 30),
            Text = "Start/Stop",
        };
        // The timer that controls the clicks
        readonly Timer timer1 = new();
        // The timer that checks input
        readonly Timer timer2 = new();

        // A variable to know if the clicker should start or stop
        bool stop = true;
        // A variable to know if the user can click F6
        bool canClick = true;

        // The amount of clicks
        int clickAmount = 0;
        // The amount of time passed
        int timePassed = 0;

        // Initiate the form
        public AutoClicker()
        {
            InitializeComponent();

            // Initiate the drop down
            string[] mouseButtons = new string[] { "Left Mouse Click", "Middle Mouse Click", "Right Mouse Click" };
            comboBox.Items.AddRange(mouseButtons);
            comboBox.SelectedIndex = 0;

            // Initialize the controls
            Controls.Add(numericUpAndDownText);
            Controls.Add(dropDownText);
            Controls.Add(stopAfterClicksText);
            Controls.Add(stopAfterTimeText);
            Controls.Add(stopAfterClicks);
            Controls.Add(stopAfterTime);
            Controls.Add(comboBox);
            Controls.Add(numericUpDown);
            Controls.Add(StartStopButton);
        }

        // The start/stop button
        private void StartStopButton_Click(object sender, EventArgs e)
        {
            // Wait a bit so the user doesn't click the box with the autoclicker
            Thread.Sleep(100);
            // See if you should stop or start the clicker and doing so
            stop = !stop;
            if (!stop) StartTimer((int)numericUpDown.Value);
            if (stop) StopTimer((int)numericUpDown.Value);
        }

        // The timer that clicks the mouse
        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (comboBox.SelectedIndex == 0)
            {
                MouseScript.LeftClick(new Point(MousePosition.X, MousePosition.Y));
                clickAmount++;
                timePassed++;
                return;
            }
            if (comboBox.SelectedIndex == 1)
            {
                MouseScript.MiddleClick(new Point(MousePosition.X, MousePosition.Y));
                clickAmount++;
                timePassed++;
                return;
            }
            MouseScript.RightClick(new Point(MousePosition.X, MousePosition.Y));
            clickAmount++;
            timePassed++;
        }

        // The timer that reads input
        private void Timer2_Tick(object sender, EventArgs e)
        {
            if (numericUpDown.Value == 0)
            {
                Trace.WriteLine("Don't use 0");
                numericUpDown.Value = 1;
            }

            if (Keyboard.IsKeyUp(Key.F6)) canClick = true;

            if (Keyboard.IsKeyDown(Key.F6) && canClick)
            {
                stop = !stop;
                if (!stop)
                {
                    StartTimer((int)numericUpDown.Value);
                }
                else StopTimer((int)numericUpDown.Value);
                canClick = false;
            }

            if (ShouldStop()) StopTimer((int)numericUpDown.Value);

            Debug.WriteLine(timePassed);
        }

        // Initiate the timers
        private void AutoClicker_Load(object sender, EventArgs e)
        {
            #pragma warning disable CS8622
            timer1.Tick += new EventHandler(Timer1_Tick);
            timer2.Tick += new EventHandler(Timer2_Tick);
            StartStopButton.Click += new EventHandler(StartStopButton_Click);
            #pragma warning restore CS8622

            timer2.Enabled = true;
            timer2.Interval = 1;
            timer2.Start();
        }

        // Start the clicker
        void StartTimer(int timeBetwenClicks)
        {
            stop = false;
            timer1.Interval = timeBetwenClicks;
            timer1.Start();
        }

        // Stop the clicker
        void StopTimer(int timeBetwenClicks)
        {
            stop = true;
            clickAmount = 0;
            timePassed = 0;

            timer1.Interval = timeBetwenClicks;
            timer1.Stop();
        }

        // Detect if either the clicker has clicked enough or has passed enough time
        bool ShouldStop()
        {
            // Check if should stop after clicks
            if(stopAfterClicks.Value != 0)
            {
                // Check if the clicker has clicked enough
                if (stopAfterClicks.Value <= clickAmount) return true;
            }
            // Check if should stop after time has passed
            if(stopAfterTime.Value != 0)
            {
                // Check if all the time has passed
                if ((stopAfterTime.Value * 50) <= timePassed) return true;
            }
            return false;
        }
    }
}
