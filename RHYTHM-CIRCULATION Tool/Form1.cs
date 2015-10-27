﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;

using Rhythm_Circulation;
using LitJson;

namespace RHYTHM_CIRCULATION_Tool
{
    public partial class Form1 : Form
    {
        private const int MAX_BAR = 500;
        private const int MAX_NOTE = 8;

        private int m_bpm = 120;
        private int m_maxBeat = 16;
        private int m_nowBar = 0;
        private int m_nowBeat = 0;
        private NoteType m_noteType = NoteType.TAP;
        private int m_longNoteLength = 1;
        private int m_slideNoteLength = 1;
        private SlideWay m_slideWay = SlideWay.ANTI_CLOCKWISE;

        private NoteData[,] m_noteList;
        private Image[] m_noteImageList = new Image[7];
        private PictureBox[] m_notePictureBoxList = new PictureBox[MAX_NOTE];

        private int BPM_Value
        {
            get
            {
                return int.Parse(textBox_BPM.Text);
            }
            set
            {
                textBox_BPM.Text = value.ToString();
            }
        }

        private int MaxBeat_Value
        {
            get
            {
                return int.Parse(textBox_MaxBeat.Text);
            }
            set
            {
                textBox_MaxBeat.Text = value.ToString();

                numericUpDown_Beat.Maximum = value;
            }
        }

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

            groupBox_LongNoteOption.Enabled = false;
            groupBox_SlideNoteOption.Enabled = false;
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
            textBox_LongNoteLength.Text = m_longNoteLength.ToString();
            textBox_SlideNoteLength.Text = m_slideNoteLength.ToString();
            switch (m_slideWay)
            {
                case SlideWay.ANTI_CLOCKWISE:
                    radioButton_Anticlockwise.Checked = true;
                    break;
                case SlideWay.CLOCKWISE:
                    radioButton_Clockwise.Checked = true;
                    break;
            }

            numericUpDown_Beat.Maximum = m_maxBeat;
        }

        private void InitNote()
        {
            m_noteList = new NoteData[MAX_BAR * m_maxBeat, MAX_NOTE];

            for (int i = 0; i < MAX_BAR; i++)
            {
                for (int j = 0; j < m_maxBeat; j++)
                {
                    for (int k = 0; k < MAX_NOTE; k++)
                    {
                        m_noteList[(i * m_maxBeat) + j, k] = new NoteData();
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
            m_noteImageList[4] = System.Drawing.Image.FromFile("./Resources/Shake.png");
            m_noteImageList[5] = System.Drawing.Image.FromFile("./Resources/Long_Shadow.png");
            m_noteImageList[6] = System.Drawing.Image.FromFile("./Resources/Slide_Shadow.png");

            m_notePictureBoxList[0] = pictureBox_Note1;
            m_notePictureBoxList[1] = pictureBox_Note2;
            m_notePictureBoxList[2] = pictureBox_Note3;
            m_notePictureBoxList[3] = pictureBox_Note4;
            m_notePictureBoxList[4] = pictureBox_Note5;
            m_notePictureBoxList[5] = pictureBox_Note6;
            m_notePictureBoxList[6] = pictureBox_Note7;
            m_notePictureBoxList[7] = pictureBox_Note8;

            ReloadNote();
        }

        // Value Changed
        private void textBox_BPM_TextChanged(object sender, EventArgs e)
        {
            m_bpm = BPM_Value;
        }

        private void textBox_MaxBeat_TextChanged(object sender, EventArgs e)
        {
            m_maxBeat = MaxBeat_Value;
            MaxBeat_Value = m_maxBeat;
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

        private void ChangeNoteType(object sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;

            groupBox_LongNoteOption.Enabled = false;
            groupBox_SlideNoteOption.Enabled = false;

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
                    groupBox_LongNoteOption.Enabled = true;
                    break;
                case 3:
                    m_noteType = NoteType.SLIDE;
                    groupBox_SlideNoteOption.Enabled = true;
                    break;
                case 4:
                    //m_noteType = NoteType.SHAKE;
                    break;
            }
        }

        private void textBox_LongNoteLength_TextChanged(object sender, EventArgs e)
        {
            m_longNoteLength = int.Parse(textBox_LongNoteLength.Text);
        }

        private void ChangeSlideWay(object sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;

            switch (radioButton.TabIndex)
            {
                case 0:
                    m_slideWay = SlideWay.ANTI_CLOCKWISE;
                    break;
                case 1:
                    m_slideWay = SlideWay.CLOCKWISE;
                    break;
            }
        }

        private void textBox_SlideNoteLength_TextChanged(object sender, EventArgs e)
        {
            m_slideNoteLength = int.Parse(textBox_SlideNoteLength.Text);
        }

        // Note
        private void ReloadNote()
        {
            for (int i = 0; i < MAX_NOTE; i++)
            {
                int index = GetNowBarBeatIndex();
                int imageNum = (int)m_noteList[index, i].Type;
                m_notePictureBoxList[i].Image = m_noteImageList[imageNum];

                if (imageNum >= (int)NoteType.LONG_SHADOW)
                    m_notePictureBoxList[i].Enabled = false;
                else
                    m_notePictureBoxList[i].Enabled = true;
            }
        }

        private void FixedNote(object sender, EventArgs e)
        {
            PictureBox pictureBox = (PictureBox)sender;
            int noteNum = int.Parse((string)pictureBox.Tag);
            int index = GetNowBarBeatIndex();
            //NoteData noteData = m_noteList[index, noteNum];

            DeleteNote(index, noteNum);
            InsertNote(index, noteNum, m_noteType);
            pictureBox.Image = m_noteImageList[(int)m_noteType];
        }

        private void InsertNote(int index, int noteNum, NoteType noteType)
        {
            NoteData noteData = m_noteList[index, noteNum];

            noteData.Type = noteType;

            // Insert Note Shadow(Long/Slide)
            if (noteType == NoteType.LONG)
            {
                noteData.Length = m_longNoteLength;
                for (int i = 1; i <= m_longNoteLength; i++)
                {
                    DeleteNote(index + i, noteNum);
                    m_noteList[index + i, noteNum].Type = NoteType.LONG_SHADOW;
                }
            }
            else if (noteType == NoteType.SLIDE)
            {
                noteData.Length = m_slideNoteLength;
                noteData.SlideWay = m_slideWay;

                for (int i = 0; i <= m_slideNoteLength; i++)
                {
                    for (int j = 0; j <= m_slideNoteLength; j++)
                    {
                        int noteNumIndex;

                        if (m_slideWay == SlideWay.ANTI_CLOCKWISE)
                            noteNumIndex = (noteNum + (MAX_NOTE - j)) % MAX_NOTE;
                        else
                            noteNumIndex = (noteNum + j) % MAX_NOTE;

                        if (i != 0 || j != 0)
                        {
                            DeleteNote(index + i, noteNumIndex);
                            m_noteList[index + i, noteNumIndex].Type = NoteType.SLIDE_SHADOW;
                        }
                    }
                }

                ReloadNote();
            }
        }

        private void DeleteNote(int index, int noteNum)
        {
            NoteType type = m_noteList[index, noteNum].Type;
            SlideWay slideWay = m_noteList[index, noteNum].SlideWay;
            int length = m_noteList[index, noteNum].Length;

            m_noteList[index, noteNum].Type = NoteType.NONE;
            m_noteList[index, noteNum].Length = 0;
            m_noteList[index, noteNum].SlideWay = SlideWay.ANTI_CLOCKWISE;

            // Delete Note Shadow(Long/Slide)
            if (type == NoteType.LONG)
            {
                for (int i = 1; i <= length; i++)
                    m_noteList[index + i, noteNum].Type = NoteType.NONE;
            }
            else if (type == NoteType.SLIDE)
            {
                for (int i = 0; i <= length; i++)
                {
                    for (int j = 0; j <= length; j++)
                    {
                        int noteNumIndex;

                        if (slideWay == SlideWay.ANTI_CLOCKWISE)
                            noteNumIndex = (noteNum + (MAX_NOTE - j)) % MAX_NOTE;
                        else
                            noteNumIndex = (noteNum + j) % MAX_NOTE;

                        if (i != 0 || j != 0)
                            m_noteList[index + i, noteNumIndex].Type = NoteType.NONE;
                    }
                }

                ReloadNote();
            }
        }

        // Open
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
                return;
            string filePath = openFileDialog1.FileName;

            FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(file);

            JsonReader jsonReader = new JsonReader(reader);
            JsonData jsonData = JsonMapper.ToObject(jsonReader);

            m_bpm = (int)jsonData["BPM"];
            m_maxBeat = (int)jsonData["MaxBeat"];
            BPM_Value = m_bpm;
            MaxBeat_Value = m_maxBeat;
            InitNote();

            JsonData jsonBar = jsonData["Note"];

            for (int i = 0; i < jsonBar.Count; i++) // Bar
            {
                JsonData jsonBeat = jsonBar[i];

                for (int j = 0; j < jsonBeat.Count; j++) // Beat
                {
                    JsonData jsonNote = jsonBeat[j];

                    for (int k = 0; k < jsonNote.Count; k++) // Note
                    {
                        m_noteList[(i * m_maxBeat) + j, k].Type = (NoteType)(int)jsonNote[k]["Type"];
                        m_noteList[(i * m_maxBeat) + j, k].Length = (int)jsonNote[k]["Length"];
                        m_noteList[(i * m_maxBeat) + j, k].SlideWay = (SlideWay)(int)jsonNote[k]["SlideWay"];
                    }
                }
            }

            reader.Close();

            ReloadNote();
        }

        // Save
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() != DialogResult.OK)
                return;
            string filePath = saveFileDialog1.FileName;

            // Find last bar/beat number
            int lastBar = 0;
            int lastBeat = 0;
            for (int i = MAX_BAR - 1; i >= 0; i--)
            {
                for (int j = m_maxBeat - 1; j >= 0; j--)
                {
                    for (int k = 0; k < MAX_NOTE; k++)
                    {
                        if (m_noteList[(i * m_maxBeat) + j, k].Type != NoteType.NONE)
                        {
                            lastBar = i + 1;
                            lastBeat = j + 1;
                            break;
                        }
                    }

                    if (lastBar != 0 || lastBeat != 0)
                        break;
                }

                if (lastBar != 0 || lastBeat != 0)
                    break;
            }

            FileStream file = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(file);

            JsonWriter jsonWriter = new JsonWriter(writer);
            jsonWriter.PrettyPrint = true;

            jsonWriter.WriteObjectStart();

            jsonWriter.WritePropertyName("BPM");
            jsonWriter.Write(m_bpm);
            jsonWriter.WritePropertyName("MaxBeat");
            jsonWriter.Write(m_maxBeat);
            jsonWriter.WritePropertyName("Note");

            jsonWriter.WriteArrayStart();

            for (int i = 0; i < lastBar; i++) // Bar
            {
                jsonWriter.WriteArrayStart();

                for (int j = 0; j < m_maxBeat; j++) // Beat
                {
                    jsonWriter.WriteArrayStart();

                    for (int k = 0; k < MAX_NOTE; k++) // Note
                    {
                        NoteData noteData = m_noteList[(i * m_maxBeat) + j, k];
                        JsonMapper.ToJson(noteData, jsonWriter);
                    }

                    jsonWriter.WriteArrayEnd();

                    if (i == (lastBar - 1) && j == (lastBeat - 1))
                        break;
                }

                jsonWriter.WriteArrayEnd();
            }

            jsonWriter.WriteArrayEnd();

            jsonWriter.WriteObjectEnd();

            writer.Close();
        }

        // Utils
        private int GetNowBarBeatIndex()
        {
            return (m_nowBar * m_maxBeat) + m_nowBeat;
        }
    }
}
