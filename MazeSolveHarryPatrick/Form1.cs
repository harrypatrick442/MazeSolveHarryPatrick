using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
namespace MazeSolveHarryPatrick
{
    public partial class Form1 : Form
    {
        private Result _LatestResult;
        private Maze _LatestMaze;
        public Form1()
        {
            InitializeComponent();
            comboBox.SelectedIndex = 0;
        }
        private void Solve() {
            bool doBreadthFirst = comboBox.SelectedIndex <= 0;
            consoleControl.ClearOutput();
            consoleControl.WriteOutput("Solving...", Color.White);
            new Thread(() => {//don't freeze the GUI for very large mazes.
                try
                {
                    _LatestMaze = new Maze(File.ReadAllText(openFileDialog.FileName));
                }
                catch
                {
                    consoleControl.WriteOutput("Failed to open the file!\n", Color.Red);
                    return;
                }
                _LatestResult = doBreadthFirst ? Solver.SolveBreadthFirst(_LatestMaze) : Solver.SolveDepthFirst(_LatestMaze);
                ShowResult(_LatestResult, _LatestMaze);
            }).Start();
        }
        private void ShowResult(Result result, Maze maze) {
            consoleControl.Invoke(new Action(() =>//can be called by another thread and invokes onto UI thread.
            {
                //make sure window is large enough so maze lines fit on a single line!
                this.MinimumSize = new Size((int)(maze.Width * 6.4)+150, this.MinimumSize.Height);
                consoleControl.ClearOutput();
                consoleControl.WriteOutput(result.ToString()+'\n', Color.White);
            }));
        }

        private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            Solve();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_LatestResult != null)
                saveFileDialog.ShowDialog();
            else
                consoleControl.WriteOutput("No results to be saved!\n", Color.White);
        }

        private void saveFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            File.WriteAllText(saveFileDialog.FileName, _LatestResult.ToString());
            consoleControl.WriteOutput("Saved to: " + saveFileDialog.FileName, Color.White);
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            consoleControl.WriteOutput("Welcome to my maze solver. Please click open to open a maze!\n", Color.White);
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_LatestMaze != null)
                Solve();
        }
    }
}
