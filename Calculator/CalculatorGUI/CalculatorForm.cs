namespace CalculatorGUI
{
    using CalculatorLogic;

    public partial class CalculatorForm : Form
    {
        public CalculatorForm()
        {
            InitializeComponent();
            var screen = Screen.PrimaryScreen!.WorkingArea;

            int width = (int)(screen.Width * 0.25);
            int height = (int)(screen.Height * 0.6);

            this.Size = new Size(width, height);
            this.StartPosition = FormStartPosition.CenterScreen;
            CalculatorLogic.Calculator calculator = new();

            Button[] buttons =
            {
                delete, digitZero, division, addition,
                digitThree, digitTwo, digitOne, subtraction,
                digitSix, digitFive, digitFour, multiplication,
                digitNine, digitEight, digitSeven, comma
            };

            Font buttonFont = new("Segoe UI", 14, FontStyle.Regular);

            foreach (var btn in buttons)
            {
                btn.Font = buttonFont;
                btn.Click += (sender, e) =>
                {
                    Button clickedButton = (Button)sender!;
                    (string newExpression, string displayResult, bool isChanged) = 
                    calculator.UpdateExpression((ButtonType)clickedButton.Tag!, clickedButton.Text[0]);
                    if (isChanged)
                    {
                        this.expressionLabel.Text = newExpression == string.Empty && displayResult == string.Empty? "0": newExpression + " = " + displayResult;
                    }
                };
            }
        }
    }
}
