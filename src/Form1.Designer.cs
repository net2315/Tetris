namespace Tetris_2
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private Panel gamePanel;
        private Label scoreLabel;
        private Label nextShapeLabel;
        private Panel nextShapePanel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 800);
            this.Text = "Tetris";

            this.gamePanel = new Panel();
            this.gamePanel.Location = new System.Drawing.Point(50, 50);
            this.gamePanel.Size = new System.Drawing.Size(400, 600);
            this.gamePanel.BackColor = System.Drawing.Color.Black;
            this.gamePanel.Paint += new PaintEventHandler(this.GamePanel_Paint);

            this.scoreLabel = new Label();
            this.scoreLabel.Location = new System.Drawing.Point(500, 50);
            this.scoreLabel.Size = new System.Drawing.Size(150, 30);
            this.scoreLabel.Text = "Score: 0";

            this.nextShapeLabel = new Label();
            this.nextShapeLabel.Location = new System.Drawing.Point(500, 100);
            this.nextShapeLabel.Size = new System.Drawing.Size(150, 30);
            this.nextShapeLabel.Text = "Next Shape:";

            this.nextShapePanel = new Panel();
            this.nextShapePanel.Location = new System.Drawing.Point(500, 140);
            this.nextShapePanel.Size = new System.Drawing.Size(150, 150);
            this.nextShapePanel.BackColor = System.Drawing.Color.Gray;
            this.nextShapePanel.Paint += new PaintEventHandler(this.NextShapePanel_Paint);

            this.Controls.Add(this.gamePanel);
            this.Controls.Add(this.scoreLabel);
            this.Controls.Add(this.nextShapeLabel);
            this.Controls.Add(this.nextShapePanel);
        }
    }
}
