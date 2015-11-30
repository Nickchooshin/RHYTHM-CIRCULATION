using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using Rhythm_Circulation;

public class SlideNote : Note {

    public Image maskImage;
    public Image pathImage;

    private float m_pathAmountMax = 0.0f;

    void Start()
    {
        Init();
    }

    protected override void Init()
    {
        m_pathAmountMax = Length * 0.125f;

        noteImage.fillAmount = 0.0f;
        pathImage.fillAmount = m_pathAmountMax;

        gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        if (SlideWay == NoteSlideWay.ANTI_CLOCKWISE)
            maskImage.transform.transform.localEulerAngles = new Vector3(0.0f, 180.0f, 0.0f);

        StartCoroutine("NoteAppear");
    }

    protected override IEnumerator NoteAppear()
    {
        while (true)
        {
            float time = Time.time;

            if (time <= m_noteTimeSeen)
            {
                float fillAmount = (APPEAR_TIME - (m_noteTimeSeen - time)) / APPEAR_TIME;

                noteImage.fillAmount = fillAmount;
                maskImage.fillAmount = fillAmount * m_pathAmountMax;
            }
            else
            {
                noteImage.fillAmount = 1.0f;
                maskImage.fillAmount = m_pathAmountMax;

                yield return new WaitForSeconds((APPEAR_TIME / 2.0f) - (time - m_noteTimeSeen));

                DeleteNote();
            }

            yield return null;
        }
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

        Debug.Log("fdsfsf");
    }
}
