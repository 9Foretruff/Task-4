namespace Task4
{
     static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }

    public partial class MainForm : Form
    {
        private TextBox hoursInput;
        private TextBox minutesInput;
        private TextBox ampmInput;
        private TextBox outputTime;
        private TextBox outputRemaining;
        private Button executeButton;
        private Button aboutButton;

        public MainForm()
        {
            InitializeControls();
        }
        
        private void InitializeControls()
        {
            this.SuspendLayout();
            this.ClientSize = new System.Drawing.Size(400, 300);
            this.Text = "Конвертер времени";
            this.ResumeLayout(false);

            hoursInput = new TextBox { Location = new Point(10, 10), Size = new Size(50, 20) };
    minutesInput = new TextBox { Location = new Point(70, 10), Size = new Size(50, 20) };
    ampmInput = new TextBox { Location = new Point(130, 10), Size = new Size(50, 20) };
    outputTime = new TextBox { Location = new Point(10, 40), Size = new Size(80, 20), ReadOnly = true };
    outputRemaining = new TextBox { Location = new Point(100, 40), Size = new Size(80, 20), ReadOnly = true };
    executeButton = new Button { Text = "Выполнить", Location = new Point(10, 70), Size = new Size(100, 30) };
    aboutButton = new Button { Text = "О программе", Location = new Point(120, 70), Size = new Size(100, 30) };

    Controls.Add(hoursInput);
    Controls.Add(minutesInput);
    Controls.Add(ampmInput);
    Controls.Add(outputTime);
    Controls.Add(outputRemaining);
    Controls.Add(executeButton);
    Controls.Add(aboutButton);

    // Добавим текстовые метки для объяснения каждого поля ввода
    Label hoursLabel = new Label { Text = "Часы (1-12):", Location = new Point(10, 10), AutoSize = true };
    Label minutesLabel = new Label { Text = "Минуты (0-59):", Location = new Point(70, 10), AutoSize = true };
    Label ampmLabel = new Label { Text = "AM/PM:", Location = new Point(130, 10), AutoSize = true };
    Label outputTimeLabel = new Label { Text = "Время (24H):", Location = new Point(10, 40), AutoSize = true };
    Label outputRemainingLabel = new Label { Text = "До полуночи:", Location = new Point(100, 40), AutoSize = true };

    // Добавим текстовые метки на форму
    Controls.Add(hoursLabel);
    Controls.Add(minutesLabel);
    Controls.Add(ampmLabel);
    Controls.Add(outputTimeLabel);
    Controls.Add(outputRemainingLabel);

    // Переместим текстовые метки под кнопки
    hoursLabel.Location = new Point(10, 110);
    minutesLabel.Location = new Point(10, 140);
    ampmLabel.Location = new Point(10, 170);
    outputTimeLabel.Location = new Point(10, 200);
    outputRemainingLabel.Location = new Point(10, 230);

    executeButton.Click += ExecuteButton_Click;
    aboutButton.Click += AboutButton_Click;
        }

        private void ExecuteButton_Click(object sender, EventArgs e)
        {
            ClearOutputs();

            if (!ValidateInput())
                return;

            int hours = Convert.ToInt32(hoursInput.Text);
            int minutes = Convert.ToInt32(minutesInput.Text);
            bool isPM = ampmInput.Text.ToUpper() == "PM";

            if (isPM && hours < 12)
                hours += 12;
            else if (!isPM && hours == 12)
                hours = 0;

            outputTime.Text = $"{hours:D2}:{minutes:D2}";
            outputRemaining.Text = $"{(24 - hours):D2}:{(60 - minutes):D2}";
        }

        private bool ValidateInput()
        {
            if (!int.TryParse(hoursInput.Text, out int hours) || hours < 1 || hours > 12)
            {
                hoursInput.ForeColor = Color.Red;
                hoursInput.Text = "0";
                return false;
            }

            if (!int.TryParse(minutesInput.Text, out int minutes) || minutes < 0 || minutes > 59)
            {
                minutesInput.ForeColor = Color.Red;
                minutesInput.Text = "0";
                return false;
            }

            if (ampmInput.Text.ToUpper() != "AM" && ampmInput.Text.ToUpper() != "PM")
            {
                ampmInput.ForeColor = Color.Red;
                ampmInput.Text = "AM";
                return false;
            }

            hoursInput.ForeColor = minutesInput.ForeColor = ampmInput.ForeColor = Color.Black;
            return true;
        }

        private void ClearOutputs()
        {
            outputTime.Text = outputRemaining.Text = "";
        }

        private void AboutButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Программа для конвертации времени из 12-часового формата в 24-часовой\n\nАвтор: Рокітько Максим Володимирович", "О программе");
        }
    }
}