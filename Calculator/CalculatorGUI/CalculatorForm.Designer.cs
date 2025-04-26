namespace CalculatorGUI
{
    using CalculatorLogic;
    partial class CalculatorForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            mainLayout = new TableLayoutPanel();
            delete = new Button();
            digitZero = new Button();
            division = new Button();
            addition = new Button();
            digitThree = new Button();
            digitTwo = new Button();
            digitOne = new Button();
            subtraction = new Button();
            digitSix = new Button();
            digitFive = new Button();
            digitFour = new Button();
            multiplication = new Button();
            digitNine = new Button();
            digitEight = new Button();
            digitSeven = new Button();
            expressionLabel = new Label();
            comma = new Button();
            mainLayout.SuspendLayout();
            SuspendLayout();
            //
            // mainLayout
            //
            mainLayout.AutoScroll = true;
            mainLayout.ColumnCount = 4;
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            mainLayout.Controls.Add(delete, 0, 4);
            mainLayout.Controls.Add(digitZero, 1, 4);
            mainLayout.Controls.Add(division, 3, 4);
            mainLayout.Controls.Add(addition, 3, 3);
            mainLayout.Controls.Add(digitThree, 2, 3);
            mainLayout.Controls.Add(digitTwo, 1, 3);
            mainLayout.Controls.Add(digitOne, 0, 3);
            mainLayout.Controls.Add(subtraction, 3, 2);
            mainLayout.Controls.Add(digitSix, 2, 2);
            mainLayout.Controls.Add(digitFive, 1, 2);
            mainLayout.Controls.Add(digitFour, 0, 2);
            mainLayout.Controls.Add(multiplication, 3, 1);
            mainLayout.Controls.Add(digitNine, 2, 1);
            mainLayout.Controls.Add(digitEight, 1, 1);
            mainLayout.Controls.Add(digitSeven, 0, 1);
            mainLayout.Controls.Add(expressionLabel, 0, 0);
            mainLayout.Controls.Add(comma, 2, 4);
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.ForeColor = Color.White;
            mainLayout.Location = new Point(0, 0);
            mainLayout.Name = "mainLayout";
            mainLayout.RowCount = 4;
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            mainLayout.Size = new Size(624, 929);
            mainLayout.TabIndex = 0;
            // 
            // delete
            // 
            delete.BackColor = Color.FromArgb(0, 64, 64);
            delete.Dock = DockStyle.Fill;
            delete.Location = new Point(3, 743);
            delete.Name = "delete";
            delete.Size = new Size(150, 183);
            delete.TabIndex = 20;
            delete.Tag = ButtonType.Delete;
            delete.Text = "⌫";
            delete.UseVisualStyleBackColor = false;
            // 
            // digitZero
            // 
            digitZero.BackColor = Color.Purple;
            digitZero.Dock = DockStyle.Fill;
            digitZero.Location = new Point(159, 743);
            digitZero.Name = "digitZero";
            digitZero.Size = new Size(150, 183);
            digitZero.TabIndex = 18;
            digitZero.Tag = ButtonType.Digit;
            digitZero.Text = "0";
            digitZero.UseVisualStyleBackColor = false;
            // 
            // division
            // 
            division.BackColor = Color.FromArgb(0, 64, 64);
            division.Dock = DockStyle.Fill;
            division.Location = new Point(471, 743);
            division.Name = "division";
            division.Size = new Size(150, 183);
            division.TabIndex = 17;
            division.Tag = ButtonType.Operator;
            division.Text = "÷";
            division.UseVisualStyleBackColor = false;
            // 
            // addition
            // 
            addition.BackColor = Color.FromArgb(0, 64, 64);
            addition.Dock = DockStyle.Fill;
            addition.Location = new Point(471, 558);
            addition.Name = "addition";
            addition.Size = new Size(150, 179);
            addition.TabIndex = 16;
            addition.Tag = ButtonType.Operator;
            addition.Text = "+";
            addition.UseVisualStyleBackColor = false;
            // 
            // digitThree
            // 
            digitThree.BackColor = Color.Purple;
            digitThree.Dock = DockStyle.Fill;
            digitThree.Location = new Point(315, 558);
            digitThree.Name = "digitThree";
            digitThree.Size = new Size(150, 179);
            digitThree.TabIndex = 15;
            digitThree.Tag = ButtonType.Digit;
            digitThree.Text = "3";
            digitThree.UseVisualStyleBackColor = false;
            // 
            // digitTwo
            // 
            digitTwo.BackColor = Color.Purple;
            digitTwo.Dock = DockStyle.Fill;
            digitTwo.Location = new Point(159, 558);
            digitTwo.Name = "digitTwo";
            digitTwo.Size = new Size(150, 179);
            digitTwo.TabIndex = 14;
            digitTwo.Tag = ButtonType.Digit;
            digitTwo.Text = "2";
            digitTwo.UseVisualStyleBackColor = false;
            // 
            // digitOne
            // 
            digitOne.BackColor = Color.Purple;
            digitOne.Dock = DockStyle.Fill;
            digitOne.Location = new Point(3, 558);
            digitOne.Name = "digitOne";
            digitOne.Size = new Size(150, 179);
            digitOne.TabIndex = 13;
            digitOne.Tag = ButtonType.Digit;
            digitOne.Text = "1";
            digitOne.UseVisualStyleBackColor = false;
            // 
            // subtraction
            // 
            subtraction.BackColor = Color.FromArgb(0, 64, 64);
            subtraction.Dock = DockStyle.Fill;
            subtraction.Location = new Point(471, 373);
            subtraction.Name = "subtraction";
            subtraction.Size = new Size(150, 179);
            subtraction.TabIndex = 12;
            subtraction.Tag = ButtonType.Operator;
            subtraction.Text = "−";
            subtraction.UseVisualStyleBackColor = false;
            // 
            // digitSix
            // 
            digitSix.BackColor = Color.Purple;
            digitSix.Dock = DockStyle.Fill;
            digitSix.Location = new Point(315, 373);
            digitSix.Name = "digitSix";
            digitSix.Size = new Size(150, 179);
            digitSix.TabIndex = 11;
            digitSix.Tag = ButtonType.Digit;
            digitSix.Text = "6";
            digitSix.UseVisualStyleBackColor = false;
            // 
            // digitFive
            // 
            digitFive.BackColor = Color.Purple;
            digitFive.Dock = DockStyle.Fill;
            digitFive.Location = new Point(159, 373);
            digitFive.Name = "digitFive";
            digitFive.Size = new Size(150, 179);
            digitFive.TabIndex = 10;
            digitFive.Tag = ButtonType.Digit;
            digitFive.Text = "5";
            digitFive.UseVisualStyleBackColor = false;
            // 
            // digitFour
            // 
            digitFour.BackColor = Color.Purple;
            digitFour.Dock = DockStyle.Fill;
            digitFour.Location = new Point(3, 373);
            digitFour.Name = "digitFour";
            digitFour.Size = new Size(150, 179);
            digitFour.TabIndex = 9;
            digitFour.Tag = ButtonType.Digit;
            digitFour.Text = "4";
            digitFour.UseVisualStyleBackColor = false;
            // 
            // multiplication
            // 
            multiplication.BackColor = Color.FromArgb(0, 64, 64);
            multiplication.Dock = DockStyle.Fill;
            multiplication.Location = new Point(471, 188);
            multiplication.Name = "multiplication";
            multiplication.Size = new Size(150, 179);
            multiplication.TabIndex = 8;
            multiplication.Tag = ButtonType.Operator;
            multiplication.Text = "×";
            multiplication.UseVisualStyleBackColor = false;
            // 
            // digitNine
            // 
            digitNine.BackColor = Color.Purple;
            digitNine.Dock = DockStyle.Fill;
            digitNine.Location = new Point(315, 188);
            digitNine.Name = "digitNine";
            digitNine.Size = new Size(150, 179);
            digitNine.TabIndex = 7;
            digitNine.Tag = ButtonType.Digit;
            digitNine.Text = "9";
            digitNine.UseVisualStyleBackColor = false;
            // 
            // digitEight
            // 
            digitEight.BackColor = Color.Purple;
            digitEight.Dock = DockStyle.Fill;
            digitEight.Location = new Point(159, 188);
            digitEight.Name = "digitEight";
            digitEight.Size = new Size(150, 179);
            digitEight.TabIndex = 6;
            digitEight.Tag = ButtonType.Digit;
            digitEight.Text = "8";
            digitEight.UseVisualStyleBackColor = false;
            // 
            // digitSeven
            // 
            digitSeven.BackColor = Color.Purple;
            digitSeven.Dock = DockStyle.Fill;
            digitSeven.Location = new Point(3, 188);
            digitSeven.Name = "digitSeven";
            digitSeven.Size = new Size(150, 179);
            digitSeven.TabIndex = 5;
            digitSeven.Tag = ButtonType.Digit;
            digitSeven.Text = "7";
            digitSeven.UseVisualStyleBackColor = false;
            // 
            // expressionLabel
            // 
            expressionLabel.BackColor = Color.FromArgb(64, 0, 0);
            mainLayout.SetColumnSpan(expressionLabel, 4);
            expressionLabel.Dock = DockStyle.Fill;
            expressionLabel.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            expressionLabel.Location = new Point(3, 0);
            expressionLabel.Name = "expressionLabel";
            expressionLabel.Size = new Size(618, 185);
            expressionLabel.TabIndex = 0;
            expressionLabel.Text = "0";
            expressionLabel.TextAlign = ContentAlignment.BottomRight;
            // 
            // comma
            // 
            comma.BackColor = Color.FromArgb(0, 64, 64);
            comma.Dock = DockStyle.Fill;
            comma.Location = new Point(315, 743);
            comma.Name = "comma";
            comma.Size = new Size(150, 183);
            comma.TabIndex = 21;
            comma.Tag = ButtonType.Comma;
            comma.Text = ",";
            comma.UseVisualStyleBackColor = false;
            // 
            // CalculatorForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 0, 0);
            ClientSize = new Size(624, 929);
            Controls.Add(mainLayout);
            Name = "CalculatorForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            mainLayout.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel mainLayout;
        private Label expressionLabel;
        private Button delete;
        private Button digitZero;
        private Button division;
        private Button addition;
        private Button digitThree;
        private Button digitTwo;
        private Button digitOne;
        private Button subtraction;
        private Button digitSix;
        private Button digitFive;
        private Button digitFour;
        private Button multiplication;
        private Button digitNine;
        private Button digitEight;
        private Button digitSeven;
        private Button comma;
    }
}
