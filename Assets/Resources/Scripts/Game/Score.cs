using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour, IJudgeReceiver {

    private int m_score = 0;
    private int m_noteJudgeCount = 0;
    private float m_noteJudgePercent = 0.0f;

    public Text scoreText;
    public Text percentText;

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
                break;
            case Note.NoteJudge.GREAT:
                m_score += 800;
                break;
            case Note.NoteJudge.GOOD:
                m_score += 500;
                break;
        }

        ++m_noteJudgeCount;

        float percent = (m_noteJudgePercent / m_noteJudgeCount);

        scoreText.text = m_score.ToString("D6");
        percentText.text = percent.ToString() + " %";
    }
}
