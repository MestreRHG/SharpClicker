using System.Diagnostics;
using System.Windows.Input;
using Timer = System.Windows.Forms.Timer;

namespace AutoClicker
{
    public partial class AutoClicker : Form
    {
        Label textBox = new Label()
        { 
            Text = "Time between clicks (in ms)",
            Location = new Point(10, 10),
            Size = new Size(160, 20),
        };
        NumericUpDown numericUpDown = new NumericUpDown()
        {
            Value = 1,
            Location = new Point(10, 30),
            Size = new Size(150, 30),
        };
        Button StartStopButton = new Button()
        {
            Location = new Point(10, 60),
            Size = new Size(150, 30),
            Text = "Start/Stop",
        };
        Timer timer1 = new();
        Timer timer2 = new();

        public AutoClicker()
        {
            InitializeComponent();
            Controls.Add(textBox);
            Controls.Add(numericUpDown);
            Controls.Add(StartStopButton);
        }

        bool stop = true;
        private void StartStopButton_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(100);
            stop = !stop;
            if (!stop) StartTimer((int)numericUpDown.Value);
            if (stop) StopTimer((int)numericUpDown.Value);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            MouseScript.leftclick(new Point(MousePosition.X, MousePosition.Y));
        }

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

        void StartTimer(int timeBetwenClicks)
        {
            timer1.Interval = timeBetwenClicks;
            timer1.Start();
        }

        void StopTimer(int timeBetwenClicks)
        {
            timer1.Interval = timeBetwenClicks;
            timer1.Stop();
        }

    }
}
