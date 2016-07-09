using UnityEngine;
using System.Collections;

using Rhythm_Circulation;

public class NoteJudgeUI : MonoBehaviour, IJudgeReceiver {

    private float m_beatTime = 0.0f;

    public SpriteRenderer noteFrame;

    public Color perfectColor = new Color(243.0f / 255.0f, 183.0f / 255.0f, 235.0f / 255.0f);
    public Color greateColor = new Color(225.0f / 255.0f, 222.0f / 255.0f, 169.0f / 255.0f);
    public Color goodColor = new Color(142.0f / 255.0f, 207.0f / 255.0f, 153.0f / 255.0f);
    public Color badColor = new Color(151.0f / 255.0f, 212.0f / 255.0f, 255.0f / 255.0f);

    private Color m_color = Color.white;

    void Start()
    {
        float bpm = NoteDataLoader.Instance.BPM;
        float maxBeat = NoteDataLoader.Instance.MaxBeat;
        m_beatTime = (60.0f / bpm) / (maxBeat / 4);

        NoteJudgeReceiver.Instance.AddJudgeReceiver(this);
    }

    public void Receive(Note.NoteJudge noteJudge)
    {
        switch (noteJudge)
        {
            case Note.NoteJudge.PERFECT:
                m_color = perfectColor;
                break;
            case Note.NoteJudge.GREAT:
                m_color = greateColor;
                break;
            case Note.NoteJudge.GOOD:
                m_color = goodColor;
                break;
            case Note.NoteJudge.BAD:
                m_color = badColor;
                break;
        }

        StopCoroutine("JudgeEffect");
        StartCoroutine("JudgeEffect");
    }

    private IEnumerator JudgeEffect()
    {
        float endTime = Time.time + m_beatTime;

        while (true)
        {
            float time = endTime - Time.time;

            if (time >= 0.0f)
            {
                float percent = (time / m_beatTime);
                noteFrame.color = Color.Lerp(Color.white, m_color, percent);
            }
            else
            {
                noteFrame.color = Color.white;
                break;
            }

            yield return null;
        }
    }
}
