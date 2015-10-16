using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RHYTHM_CIRCULATION_Tool
{
    public partial class Form1 : Form
    {
        private enum NoteType
        {
            TAP = 0,
            LONG,
            SLIDE,
            SHAKE,
        }

        private int m_bpm = 120;
        private int m_maxBeat = 16;
        private int m_nowBar = 1;
        private int m_nowBeat = 1;
        private NoteType m_noteType = NoteType.TAP;

        public Form1()
        {
            InitializeComponent();

            Init();
        }

        private void Init()
        {
            textBox_BPM.Text = m_bpm.ToString();
            textBox_MaxBeat.Text = m_maxBeat.ToString();
            m_nowBar = Convert.ToInt32(numericUpDown_Bar.Value);
            m_nowBeat = Convert.ToInt32(numericUpDown_Beat.Value);
            switch (m_noteType)
            {
                case NoteType.TAP:
                    radioButton_Tap.Checked = true;
                    break;
                case NoteType.LONG:
                    radioButton_Long.Checked = true;
                    break;
                case NoteType.SLIDE:
                    radioButton_Slide.Checked = true;
                    break;
                case NoteType.SHAKE:
                    radioButton_Shake.Checked = true;
                    break;
            }

            numericUpDown_Beat.Maximum = m_maxBeat;
        }

        // Value Changed
        private void textBox_BPM_TextChanged(object sender, EventArgs e)
        {
            m_bpm = int.Parse(textBox_BPM.Text);
        }

        private void textBox_MaxBeat_TextChanged(object sender, EventArgs e)
        {
            m_maxBeat = int.Parse(textBox_MaxBeat.Text);

            numericUpDown_Beat.Maximum = m_maxBeat;
        }

        private void numericUpDown_Bar_ValueChanged(object sender, EventArgs e)
        {
            m_nowBar = Convert.ToInt32(numericUpDown_Bar.Value);
        }

        private void numericUpDown_Beat_ValueChanged(object sender, EventArgs e)
        {
            m_nowBeat = Convert.ToInt32(numericUpDown_Beat.Value);
        }

        private void radioButton_Tap_Click(object sender, EventArgs e)
        {
            m_noteType = NoteType.TAP;
        }

        private void radioButton_Long_Click(object sender, EventArgs e)
        {
            m_noteType = NoteType.LONG;
        }

        private void radioButton_Slide_Click(object sender, EventArgs e)
        {
            m_noteType = NoteType.SLIDE;
        }
    }
}
