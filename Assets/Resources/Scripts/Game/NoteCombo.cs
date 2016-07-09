using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using Rhythm_Circulation;

public class NoteCombo : MonoBehaviour, IJudgeReceiver {

    public Text countText;
    public Text comboText;

    private int m_comboCount = 0;
    bool m_isEnabled = true;

    void Start()
    {
        TextEnabled(false);

        NoteJudgeReceiver.Instance.AddJudgeReceiver(this);
    }

    public void Receive(Note.NoteJudge noteJudge)
    {
        switch (noteJudge)
        {
            case Note.NoteJudge.PERFECT:
            case Note.NoteJudge.GREAT:
            case Note.NoteJudge.GOOD:
                ++m_comboCount;
                break;
            case Note.NoteJudge.BAD:
                m_comboCount = 0;
                break;
        }

        if (m_comboCount != 0)
        {
            TextEnabled(true);
            countText.text = m_comboCount.ToString();
        }
        else
            TextEnabled(false);
    }

    private void TextEnabled(bool isEnabled)
    {
        if (m_isEnabled != isEnabled)
        {
            m_isEnabled = isEnabled;
            countText.enabled = m_isEnabled;
            comboText.enabled = m_isEnabled;
        }
    }
}
