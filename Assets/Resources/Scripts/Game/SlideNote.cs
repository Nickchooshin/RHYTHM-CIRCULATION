using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class SlideNote : Note {

    void Start()
    {
        Init();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        float timing = Time.time - m_noteTimeSeen;

        if (timing < 0.0f)
            timing = -timing;

        if (timing <= PERFECT_TIMING)
            m_noteJudge = NoteJudge.PERFECT;
        else if (timing <= GREAT_TIMING)
            m_noteJudge = NoteJudge.GREAT;
        else if (timing <= GOOD_TIMING)
            m_noteJudge = NoteJudge.GOOD;

        StopCoroutine("NoteAppear");
        //StartCoroutine("NoteJudge_Slide");
    }
}
