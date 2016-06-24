using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LongNote : Note {

    public Image gaugeImage;

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
        StartCoroutine("NoteJudge_Long");
    }

    private IEnumerator NoteJudge_Long()
    {
        int bpm = NoteDataLoader.Instance.BPM;
        int maxBeat = NoteDataLoader.Instance.MaxBeat;
        float timeLength = ((60.0f / bpm) / maxBeat) * m_noteData.Length;
        float endTime = Time.time + timeLength;

        noteImage.fillAmount = 1.0f;

        while (true)
        {
            float time = endTime - Time.time;

            gaugeImage.fillAmount = 1.0f - (time / timeLength);

            if (time < 0.0f)
                DeleteNote();

            yield return null;
        }
    }
}
