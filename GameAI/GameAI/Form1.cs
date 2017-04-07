using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameAI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        ApplicationEngine applicationEngine;
        Timer timer;

        private void Form1_Load(object sender, EventArgs e)
        {
            applicationEngine = ApplicationEngine.GetInstance(pictureBoxWorld.Size);

            timer = new Timer();
            timer.Interval = 17;
            timer.Tick += Timer_Tick;
            timer.Start();

            
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            applicationEngine.DoLogic();
            pictureBoxWorld.Image = applicationEngine.Draw();
        }

        private void buttonAddExplorer_Click(object sender, EventArgs e)
        {
            applicationEngine.AddExplorer();
        }

        private void buttonAddMiner_Click(object sender, EventArgs e)
        {
            applicationEngine.AddMiner();
        }

        private void buttonAddTransporter_Click(object sender, EventArgs e)
        {
            applicationEngine.AddTransporter();
        }

        private void buttonShowPath_Click(object sender, EventArgs e)
        {
            bool show = buttonShowPath.Text == "Show Path";
            applicationEngine.ShowHideRobotPath(show);
            buttonShowPath.Text = show ? "Hide Path" : "Show Path";
        }
    }
}
