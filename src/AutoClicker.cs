using System.Diagnostics;
using System.Windows.Input;
using Timer = System.Windows.Forms.Timer;

namespace AutoClicker
{
    public partial class AutoClicker : Form
    {
        // The text that explains the numeric up and down
        readonly Label textBox1 = new()
        { 
            Text = "Time between clicks (in ms)",
            Location = new Point(10, 10),
            Size = new Size(160, 20),
        };
        // The text that explains the drop down
        readonly Label textBox2 = new()
        {
            Text = "Mouse Buttons",
            Location = new Point(200, 10),
            Size = new Size(160, 20),
        };
        // The drop down to chose the mouse button being clicked
        readonly ComboBox comboBox = new() 
        { 
            Location = new Point(200, 30),
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
        // The button that starts the clicker
        readonly Button StartStopButton = new()
        {
            Location = new Point(10, 60),
            Size = new Size(150, 30),
            Text = "Start/Stop",
        };
        // The timer that controls the clicks
        readonly Timer timer1 = new();
        // The timer that checks input
        readonly Timer timer2 = new();
        // A variable to know the clicker start or stop
        bool stop = true;

        // Initiate the form
        public AutoClicker()
        {
            InitializeComponent();

            // Initiate the drop down
            string[] mouseButtons = new string[] { "Left Mouse Click", "Middle Mouse Click", "Right Mouse Click" };
            comboBox.Items.AddRange(mouseButtons);
            comboBox.SelectedIndex = 0;

            // Initialize the controls
            Controls.Add(textBox1);
            Controls.Add(textBox2);
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
        private void timer1_Tick(object sender, EventArgs e)
        {
            if(comboBox.SelectedIndex == 0)
            {
                MouseScript.LeftClick(new Point(MousePosition.X, MousePosition.Y));
                return;
            }
            if (comboBox.SelectedIndex == 1)
            {
                MouseScript.MiddleClick(new Point(MousePosition.X, MousePosition.Y));
                return;
            }
            MouseScript.RightClick(new Point(MousePosition.X, MousePosition.Y));
        }

        // The timer that reads input
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (numericUpDown.Value == 0)
            {
                Trace.WriteLine("Don't use 0");
                numericUpDown.Value = 1;
            }

            if (Keyboard.IsKeyDown(Key.F6))
            {
                StartTimer((int)numericUpDown.Value);
            }
            if (Keyboard.IsKeyDown(Key.F5))
            {
                StopTimer((int)numericUpDown.Value);
            }
        }

        // Initiate the timers
        private void AutoClicker_Load(object sender, EventArgs e)
        {
            #pragma warning disable CS8622
            timer1.Tick += new EventHandler(timer1_Tick);
            timer2.Tick += new EventHandler(timer2_Tick);
            StartStopButton.Click += new EventHandler(StartStopButton_Click);
            #pragma warning restore CS8622

            timer2.Enabled = true;
            timer2.Interval = 1;
            timer2.Start();
        }

        // Start the clicker
        void StartTimer(int timeBetwenClicks)
        {
            timer1.Interval = timeBetwenClicks;
            timer1.Start();
        }

        // Stop the clicker
        void StopTimer(int timeBetwenClicks)
        {
            timer1.Interval = timeBetwenClicks;
            timer1.Stop();
        }
    }
}
