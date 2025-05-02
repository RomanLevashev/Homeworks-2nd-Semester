namespace RunawayButton
{
    using System.Windows.Forms;

    /// <summary>
    /// Represents the main window of the application with a button that avoids the user's cursor.
    /// When the button is clicked, the application exits.
    /// </summary>
    public partial class MainForm : Form
    {
        private Button runButton;
        private Random random = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            this.Text = "Catch me";
            this.Size = new Size(600, 400);

            this.runButton = new();
            this.runButton.Text = "Click on me";
            this.MouseMove += this.RunButtonMouseMove;
            this.runButton.Click += this.RunButtonClick;
            this.runButton.Location = new Point(150, 150);

            this.Controls.Add(this.runButton);

            this.UpdateButtonSize();

            this.Resize += (s, e) => this.UpdateButtonSize();
        }

        private void UpdateButtonSize()
        {
            int buttonWidth = (int)(this.ClientSize.Width * 0.3);
            int buttonHeight = (int)(this.ClientSize.Height * 0.15);

            this.runButton.Size = new Size(buttonWidth, buttonHeight);
        }

        private void RunButtonMouseMove(object? sender, MouseEventArgs e)
        {
            var cursorPosition = this.PointToClient(Cursor.Position);
            var buttonBounds = this.runButton.Bounds;

            if (!buttonBounds.Contains(cursorPosition) && this.DistanceToRectangle(cursorPosition, buttonBounds) < 50)
            {
                this.MoveButton();
            }
        }

        private double DistanceToRectangle(Point pos, Rectangle rect)
        {
            int xDifference = Math.Max(rect.Left - pos.X, Math.Max(0, pos.X - rect.Right));
            int yDifference = Math.Max(rect.Top - pos.Y, Math.Max(0, pos.Y - rect.Bottom));
            return Math.Sqrt((xDifference * xDifference) + (yDifference * yDifference));
        }

        private void MoveButton()
        {
            int maximumX = this.ClientSize.Width - this.runButton.Width;
            int maximumY = this.ClientSize.Height - this.runButton.Height;

            int newX = this.random.Next(maximumX);
            int newY = this.random.Next(maximumY);

            this.runButton.Location = new Point(newX, newY);
        }

        private void RunButtonClick(object? sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
