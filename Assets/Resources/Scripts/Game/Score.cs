using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour, IJudgeReceiver {

    private int m_score = 0;
    private int m_noteJudgeCount = 0;
    private float m_noteJudgePercent = 0.0f;

    private int m_perfectCount = 0;
    private int m_greatCount = 0;
    private int m_goodCount = 0;
    private int m_missCount = 0;

    public Text scoreText;
    public Text percentText;

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

    void Start()
    {
        scoreText.text = "000000";
        percentText.text = "0.0 %";

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
                break;
            case Note.NoteJudge.GREAT:
                m_score += 800;
                ++m_greatCount;
                break;
            case Note.NoteJudge.GOOD:
                m_score += 500;
                ++m_goodCount;
                break;
            case Note.NoteJudge.BAD:
                ++m_missCount;
                break;
        }

        ++m_noteJudgeCount;

        scoreText.text = TotalScore.ToString("D6");
        percentText.text = Accuracy.ToString("F1") + " %";
    }
}
