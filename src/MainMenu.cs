using System;
using System.Windows.Forms;

namespace Tetris_2
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Tetris Menu";
            this.ClientSize = new System.Drawing.Size(400, 300);

            Button playButton = new Button();
            playButton.Text = "Play";
            playButton.Location = new System.Drawing.Point(150, 50);
            playButton.Click += PlayButton_Click;

            Button optionsButton = new Button();
            optionsButton.Text = "Options";
            optionsButton.Location = new System.Drawing.Point(150, 100);
            optionsButton.Click += OptionsButton_Click;

            Button quitButton = new Button();
            quitButton.Text = "Quit";
            quitButton.Location = new System.Drawing.Point(150, 150);
            quitButton.Click += QuitButton_Click;

            this.Controls.Add(playButton);
            this.Controls.Add(optionsButton);
            this.Controls.Add(quitButton);
        }

        private void PlayButton_Click(object? sender, EventArgs e)
        {
            this.Hide();
            Form1 gameForm = new Form1();
            gameForm.ShowDialog();
            this.Show();
        }

        private void OptionsButton_Click(object? sender, EventArgs e)
        {
            MessageBox.Show("No option availble.");
        }

        private void QuitButton_Click(object? sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
