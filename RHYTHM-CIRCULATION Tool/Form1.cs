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

using Rhythm_Circulation;
using LitJson;
using Media;

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
        private int m_slideTime = 1;
        private NoteSlideWay m_slideWay = NoteSlideWay.ANTI_CLOCKWISE;
        private bool m_roundTrip = false;

        private int m_noteCount = 0;

        private NoteData[,] m_noteList;
        private Image[] m_noteImageList = new Image[7];
        private PictureBox[] m_notePictureBoxList = new PictureBox[MAX_NOTE];

        private MP3Player m_mplayer;
        private bool m_isPlaying = false;

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

                numericUpDown_Beat.Maximum = value + 1;
            }
        }

        private bool Playing
        {
            get
            {
                return m_isPlaying;
            }
            set
            {
                m_isPlaying = value;

                if (m_isPlaying)
                    button_Play.Text = "||";
                else
                    button_Play.Text = "▶";

                textBox_BPM.Enabled = !m_isPlaying;
                textBox_MaxBeat.Enabled = !m_isPlaying;
                numericUpDown_Bar.Enabled = !m_isPlaying;
                numericUpDown_Beat.Enabled = !m_isPlaying;
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
            InitSettingValue();
            InitSettingNote();
            InitNote();
            InitNoteImage();

            groupBox_LongNoteOption.Enabled = false;
            groupBox_SlideNoteOption.Enabled = false;

            m_mplayer = new MP3Player();
            m_mplayer.OpenFile += new MP3Player.OpenFileEventHandler(mplayer_OpenFile);
            m_mplayer.PlayFile += new MP3Player.PlayFileEventHandler(mplayer_PlayFile);
            m_mplayer.StopFile += new MP3Player.StopFileEventHandler(mplayer_StopFile);
            m_mplayer.PauseFile += new MP3Player.PauseFileEventHandler(mplayer_PauseFile);
        }

        private void InitSettingValue()
        {
            BPM_Value = 120;
            MaxBeat_Value = 16;
            numericUpDown_Bar.Value = 1;
            numericUpDown_Bar.Maximum = MAX_BAR;
            numericUpDown_Beat.Value = 1;
        }

        private void InitSettingNote()
        {
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
                case NoteType.SNAP:
                    radioButton_Snap.Checked = true;
                    break;
            }
            textBox_LongNoteLength.Text = m_longNoteLength.ToString();
            textBox_SlideNoteLength.Text = m_slideNoteLength.ToString();
            textBox_SlideTime.Text = m_slideTime.ToString();
            switch (m_slideWay)
            {
                case NoteSlideWay.ANTI_CLOCKWISE:
                    radioButton_Anticlockwise.Checked = true;
                    break;
                case NoteSlideWay.CLOCKWISE:
                    radioButton_Clockwise.Checked = true;
                    break;
            }
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
            m_noteImageList[0] = System.Drawing.Image.FromFile("./Resources/Base.png");
            m_noteImageList[1] = System.Drawing.Image.FromFile("./Resources/Tap.png");
            m_noteImageList[2] = System.Drawing.Image.FromFile("./Resources/Long.png");
            m_noteImageList[3] = System.Drawing.Image.FromFile("./Resources/Slide.png");
            m_noteImageList[4] = System.Drawing.Image.FromFile("./Resources/Snap.png");
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
        }

        private void ChangePageValue(object sender, EventArgs e)
        {
            NumericUpDown numericUpDown = (NumericUpDown)sender;
            int value = Convert.ToInt32(numericUpDown.Value) - 1;

            if (numericUpDown.TabIndex == 0)
            {
                m_nowBar = value;
            }
            else
            {
                if (value == -1)
                {
                    if (m_nowBar != 0)
                    {
                        numericUpDown_Beat.Value = m_maxBeat;
                        numericUpDown_Bar.Value -= 1;
                    }
                    else
                        numericUpDown_Beat.Value = 1;
                }
                else if (value == m_maxBeat)
                {
                    numericUpDown_Beat.Value = 1;
                    numericUpDown_Bar.Value += 1;
                }
                else
                    m_nowBeat = value;
            }

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
                    m_noteType = NoteType.SNAP;
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
                    m_slideWay = NoteSlideWay.ANTI_CLOCKWISE;
                    break;
                case 1:
                    m_slideWay = NoteSlideWay.CLOCKWISE;
                    break;
                case 2:
                    break;
            }
        }

        private void textBox_SlideNoteLength_TextChanged(object sender, EventArgs e)
        {
            m_slideNoteLength = int.Parse(textBox_SlideNoteLength.Text);
        }

        private void checkBox_RoundTrip_CheckedChanged(object sender, EventArgs e)
        {
            m_roundTrip = checkBox_RoundTrip.Checked;
        }

        private void textBox_SlideTime_TextChanged(object sender, EventArgs e)
        {
            m_slideTime = int.Parse(textBox_SlideTime.Text);
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
                noteData.SlideTime = m_slideTime;
                noteData.SlideWay = m_slideWay;
                noteData.RoundTrip = m_roundTrip;

                int roundTripLength = 0;
                if (m_roundTrip)
                    roundTripLength = m_slideTime;

                for (int i = 0; i <= m_slideTime + roundTripLength; i++)
                {
                    for (int j = 0; j <= m_slideNoteLength; j++)
                    {
                        int noteNumIndex;

                        if (m_slideWay == NoteSlideWay.ANTI_CLOCKWISE)
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

                m_noteCount += m_slideNoteLength + roundTripLength;

                ReloadNote();
            }
            else if (noteType == NoteType.SNAP)
            {
                for (int i = 0; i < MAX_NOTE; i++)
                {
                    if (i != noteNum)
                    {
                        DeleteNote(index, i);
                        m_noteList[index, i].Type = NoteType.SNAP;
                    }
                }
                
                ReloadNote();
            }

            ++m_noteCount;
        }

        private void DeleteNote(int index, int noteNum)
        {
            NoteType type = m_noteList[index, noteNum].Type;
            int length = m_noteList[index, noteNum].Length;
            int slideTime = m_noteList[index, noteNum].SlideTime;
            NoteSlideWay slideWay = m_noteList[index, noteNum].SlideWay;
            bool roundTrip = m_noteList[index, noteNum].RoundTrip;

            m_noteList[index, noteNum].Type = NoteType.NONE;
            m_noteList[index, noteNum].Length = 0;
            m_noteList[index, noteNum].SlideTime = 0;
            m_noteList[index, noteNum].SlideWay = NoteSlideWay.ANTI_CLOCKWISE;
            m_noteList[index, noteNum].RoundTrip = false;

            // Delete Note Shadow(Long/Slide)
            if (type == NoteType.LONG)
            {
                for (int i = 1; i <= length; i++)
                    m_noteList[index + i, noteNum].Type = NoteType.NONE;
            }
            else if (type == NoteType.SLIDE)
            {
                int roundTripLength = 0;
                if (roundTrip)
                    roundTripLength = slideTime;

                for (int i = 0; i <= slideTime + roundTripLength; i++)
                {
                    for (int j = 0; j <= length; j++)
                    {
                        int noteNumIndex;

                        if (slideWay == NoteSlideWay.ANTI_CLOCKWISE)
                            noteNumIndex = (noteNum + (MAX_NOTE - j)) % MAX_NOTE;
                        else
                            noteNumIndex = (noteNum + j) % MAX_NOTE;

                        if (i != 0 || j != 0)
                            m_noteList[index + i, noteNumIndex].Type = NoteType.NONE;
                    }
                }

                ReloadNote();
            }
            else if (type == NoteType.SNAP)
            {
                for (int i = 0; i < MAX_NOTE; i++)
                {
                    m_noteList[index, i].Type = NoteType.NONE;
                }

                ReloadNote();
            }

            switch (type)
            {
                case NoteType.TAP:
                case NoteType.LONG:
                case NoteType.SLIDE:
                case NoteType.SNAP:
                    --m_noteCount;
                    break;
            }
        }

        // New
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitSettingValue();
            InitNote();

            ReloadNote();
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

            BPM_Value = (int)jsonData["BPM"];
            MaxBeat_Value = (int)jsonData["MaxBeat"];
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
                        m_noteList[(i * m_maxBeat) + j, k].SlideWay = (NoteSlideWay)(int)jsonNote[k]["SlideWay"];
                        m_noteList[(i * m_maxBeat) + j, k].RoundTrip = (bool)jsonNote[k]["RoundTrip"];
                    }
                }
            }

            reader.Close();

            ReloadNote();
        }

        // Save
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.FileName == "")
            {
                if (saveFileDialog1.ShowDialog() != DialogResult.OK)
                    return;
            }

            SaveFile(saveFileDialog1.FileName);
        }

        // Save As
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() != DialogResult.OK)
                return;

            SaveFile(saveFileDialog1.FileName);
        }

        // Save File
        private void SaveFile(string filePath)
        {
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
            jsonWriter.WritePropertyName("NoteCount");
            jsonWriter.Write(m_noteCount);
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

        private float GetBeatTime()
        {
            return (60.0f / m_bpm) / m_maxBeat;
        }



        // Music
        private void button_OpenMusic_Click(object sender, EventArgs e)
        {
            if (openFileDialog_Music.ShowDialog() != DialogResult.OK)
                return;
            string filePath = openFileDialog_Music.FileName;

            textBox_MusicName.Text = filePath.Substring(filePath.LastIndexOf("\\") + 1);

            m_mplayer.Open(filePath);

            ReloadNote();
        }

        private void button_Play_Click(object sender, EventArgs e)
        {
            if (Playing)
                m_mplayer.Pause();
            else
                m_mplayer.Play();
        }

        private void button_Stop_Click(object sender, EventArgs e)
        {
            Playing = false;
            m_mplayer.Stop();
        }

        private void trackBar_Music_MouseDown(object sender, MouseEventArgs e)
        {
            if(m_mplayer.IsPlaying)
                timer_Music.Enabled = false;
        }

        private void trackBar_Music_MouseUp(object sender, MouseEventArgs e)
        {
            m_mplayer.Seek((ulong)(trackBar_Music.Value * 1000));

            SetBarBeatPositionBasedByMusic();
            SetMusicPlayTime();

            if (m_mplayer.IsPlaying)
                timer_Music.Enabled = true;
        }

        private void timer_Music_Tick(object sender, EventArgs e)
        {
            trackBar_Music.Value = (int)(m_mplayer.CurrentPosition / 1000);

            SetBarBeatPositionBasedByMusic();
            SetMusicPlayTime();
        }

        private void mplayer_OpenFile(Object sender, MP3Player.OpenFileEventArgs e)
        {
            trackBar_Music.Maximum = (int)(m_mplayer.AudioLength / 1000);
            trackBar_Music.Value = 0;
            timer_Music.Enabled = false;
            Playing = false;
        }

        private void mplayer_PlayFile(Object sender, MP3Player.PlayFileEventArgs e)
        {
            trackBar_Music.Maximum = (int)(m_mplayer.AudioLength / 1000);
            timer_Music.Enabled = true;
            Playing = true;
        }

        private void mplayer_StopFile(Object sender, MP3Player.StopFileEventArgs e)
        {
            trackBar_Music.Value = 0;
            timer_Music.Enabled = false;
            Playing = false;
        }

        private void mplayer_PauseFile(Object sender, MP3Player.PauseFileEventArgs e)
        {
            timer_Music.Enabled = false;
            Playing = false;
        }

        private void SetBarBeatPositionBasedByMusic()
        {
            float beatTime = GetBeatTime() * 1000.0f;
            int index = (int)(m_mplayer.CurrentPosition / beatTime);
            int bar = index / m_maxBeat;
            int beat = index % m_maxBeat;

            numericUpDown_Bar.Value = bar + 1;
            numericUpDown_Beat.Value = beat + 1;

            ReloadNote();
        }

        private void SetMusicPlayTime()
        {
            int seconds = (int)(m_mplayer.CurrentPosition / 1000);
            int minutes = seconds / 60;
            seconds = seconds % 60;

            textBox_MusicPlayTime.Text = minutes.ToString("D2") + ":" + seconds.ToString("D2");
        }
    }
}
