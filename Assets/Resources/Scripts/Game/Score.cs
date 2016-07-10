using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using Rhythm_Circulation;

public class Score : MonoBehaviour, IJudgeReceiver {

    private int m_score = 0;
    private int m_noteJudgeCount = 0;
    private float m_noteJudgePercent = 0.0f;
    private int m_noteNumber = 0;

    private int m_perfectCount = 0;
    private int m_greatCount = 0;
    private int m_goodCount = 0;
    private int m_missCount = 0;
    private int m_combo = 0;
    private int m_maxCombo = 0;

    public Text scoreText;
    public Text percentText;
    public Image progressGauge;

    public int PerfectCount
    {
        get
        {
            return m_perfectCount;
        }
    }

    public int GreatCount
    {
        get
        {
            return m_greatCount;
        }
    }

    public int GoodCount
    {
        get
        {
            return m_goodCount;
        }
    }

    public int MissCount
    {
        get
        {
            return m_missCount;
        }
    }

    public float Accuracy
    {
        get
        {
            return (m_noteJudgePercent / m_noteJudgeCount) * 100.0f;
        }
    }

    public int TotalScore
    {
        get
        {
            return m_score;
        }
    }

    public string Mastery
    {
        get
        {
            float comboPercent = ((float)m_maxCombo / (float)m_noteNumber) * 100.0f;

            if (comboPercent >= 100.0f)
                return "MASTER";
            else if (comboPercent >= 80.0f)
                return "EXPERT";
            else if (comboPercent >= 60.0f)
                return "BEGINNER";

            return "";
        }
    }

    void Start()
    {
        scoreText.text = "000000";
        percentText.text = "0.0 %";
        progressGauge.fillAmount = 0.0f;

        m_noteNumber = NoteDataLoader.Instance.NoteData["Note"].Count;

        NoteJudgeReceiver.Instance.AddJudgeReceiver(this);
    }

    public void Receive(Note.NoteJudge noteJudge)
    {
        switch (noteJudge)
        {
            case Note.NoteJudge.PERFECT:
                m_noteJudgePercent += 1.0f;
                m_score += 1000;
                ++m_perfectCount;
                ++m_combo;
                break;
            case Note.NoteJudge.GREAT:
                m_noteJudgePercent += 0.7f;
                m_score += 800;
                ++m_greatCount;
                ++m_combo;
                break;
            case Note.NoteJudge.GOOD:
                m_noteJudgePercent += 0.35f;
                m_score += 500;
                ++m_goodCount;
                ++m_combo;
                break;
            case Note.NoteJudge.BAD:
                ++m_missCount;
                m_combo = 0;
                break;
        }

        if (m_combo >= m_maxCombo)
            m_maxCombo = m_combo;

        ++m_noteJudgeCount;

        scoreText.text = TotalScore.ToString("D6");
        percentText.text = Accuracy.ToString("F1") + " %";
        progressGauge.fillAmount = (m_noteJudgePercent / (float)m_noteNumber);
    }

    public void ScoreRecord(string name, string difficulty, string rank, string mastery)
    {
        int highScore = HighScoreManager.Instance.GetHighScore(name, difficulty);

        if (m_score > highScore)
        {
            HighScoreManager.Instance.SetHighScore(name, difficulty, m_score, rank, mastery);
            HighScoreManager.Instance.SaveHighScore();
        }
    }
}
