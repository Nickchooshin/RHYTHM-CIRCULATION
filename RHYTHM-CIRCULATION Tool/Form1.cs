﻿using System;
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
            NONE = 0,
            TAP,
            LONG,
            SLIDE,
            SHAKE,
        }

        private int m_bpm = 120;
        private int m_maxBeat = 16;
        private int m_nowBar = 0;
        private int m_nowBeat = 0;
        private NoteType m_noteType = NoteType.TAP;

        private NoteType[,,] m_noteList;
        private Image[] m_noteImageList = new Image[4];
        private PictureBox[] m_notePictureBoxList = new PictureBox[9];

        public Form1()
        {
            InitializeComponent();

            Init();
        }

        // Init
        private void Init()
        {
            InitSetting();
            InitNote();
            InitNoteImage();
        }

        private void InitSetting()
        {
            textBox_BPM.Text = m_bpm.ToString();
            textBox_MaxBeat.Text = m_maxBeat.ToString();
            m_nowBar = Convert.ToInt32(numericUpDown_Bar.Value) - 1;
            m_nowBeat = Convert.ToInt32(numericUpDown_Beat.Value) - 1;
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

        private void InitNote()
        {
            m_noteList = new NoteType[500, m_maxBeat, 9];

            for (int i = 0; i < 500; i++)
            {
                for (int j = 0; j < m_maxBeat; j++)
                {
                    for (int k = 0; k < 9; k++)
                    {
                        m_noteList[i, j, k] = NoteType.NONE;
                    }
                }
            }
        }

        private void InitNoteImage()
        {
            m_noteImageList[0] = System.Drawing.Image.FromFile("./Resources/Normal.png");
            m_noteImageList[1] = System.Drawing.Image.FromFile("./Resources/Tap.png");
            m_noteImageList[2] = System.Drawing.Image.FromFile("./Resources/Long.png");
            m_noteImageList[3] = System.Drawing.Image.FromFile("./Resources/Slide.png");

            m_notePictureBoxList[0] = pictureBox_Note1;
            m_notePictureBoxList[1] = pictureBox_Note2;
            m_notePictureBoxList[2] = pictureBox_Note3;
            m_notePictureBoxList[3] = pictureBox_Note4;
            m_notePictureBoxList[4] = pictureBox_Note5;
            m_notePictureBoxList[5] = pictureBox_Note6;
            m_notePictureBoxList[6] = pictureBox_Note7;
            m_notePictureBoxList[7] = pictureBox_Note8;
            m_notePictureBoxList[8] = pictureBox_Note9;

            ReloadNote();
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

        private void ChangePageValue(object sender, EventArgs e)
        {
            NumericUpDown numericUpDown = (NumericUpDown)sender;
            int value = Convert.ToInt32(numericUpDown.Value) - 1;

            if (numericUpDown.TabIndex == 0)
                m_nowBar = value;
            else
                m_nowBeat = value;

            ReloadNote();
        }

        // Note
        private void ChangeNoteType(object sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;

            switch (radioButton.TabIndex)
            {
                case 0:
                    m_noteType = NoteType.NONE;
                    break;
                case 1:
                    m_noteType = NoteType.TAP;
                    break;
                case 2:
                    m_noteType = NoteType.LONG;
                    break;
                case 3:
                    m_noteType = NoteType.SLIDE;
                    break;
                case 4:
                    //m_noteType = NoteType.SHAKE;
                    break;
            }
        }

        private void ReloadNote()
        {
            for (int i = 0; i < 9; i++)
                m_notePictureBoxList[i].Image = m_noteImageList[(int)m_noteList[m_nowBar, m_nowBeat, i]];
        }

        private void FixedNote(object sender, EventArgs e)
        {
            PictureBox pictureBox = (PictureBox)sender;
            int noteNum = int.Parse((string)pictureBox.Tag);

            pictureBox.Image = m_noteImageList[(int)m_noteType];
            m_noteList[m_nowBar, m_nowBeat, noteNum] = m_noteType;
        }
    }
}
