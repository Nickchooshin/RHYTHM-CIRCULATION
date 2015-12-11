using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using Rhythm_Circulation;

public class SlideNote : Note, IPointerExitHandler {
    
    public Image maskImage;
    public Image pathImage;

    private float m_pathAmountMax = 0.0f;

    void Start()
    {
        Init();
    }

    protected override void Init()
    {
        m_pathAmountMax = Length * 0.125f + 0.0625f;

        noteImage.fillAmount = 0.0f;
        pathImage.fillAmount = m_pathAmountMax - 0.0625f;

        StartCoroutine("NoteAppear");
    }

    public override void SetNoteActive(bool isActive)
    {
        gameObject.transform.parent.gameObject.SetActive(isActive);

        maskImage.gameObject.SetActive(isActive);
    }

    protected override void DeleteNote()
    {
        Debug.Log(m_noteJudge);
        NoteJudgeReceiver.Instance.SendNoteJudge(m_noteJudge);

        Destroy(maskImage.gameObject);
        Destroy(gameObject.transform.parent.gameObject);
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

        Debug.Log("Slide");

        StopCoroutine("NoteAppear");
        StartCoroutine("NoteJudge_Slide");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DeleteNote();
    }

    private IEnumerator NoteJudge_Slide()
    {
        int bpm = NoteDataLoader.Instance.BPM;
        int maxBeat = NoteDataLoader.Instance.MaxBeat;
        float timeLength = ((60.0f / bpm) / (maxBeat / 4)) * m_noteData.Length;
        float endTime = Time.time + timeLength;
        Vector3 startAngle = gameObject.transform.parent.eulerAngles;
        bool isRoundTrip = false;

        noteImage.fillAmount = 1.0f;

        while (true)
        {
            float time = endTime - Time.time;

            float angle = (-45.0f * Length) * (1.0f - (time / timeLength));
            Vector3 vectorAngle;

            if (isRoundTrip)
                angle = (-45.0f * Length) - angle;
            vectorAngle = new Vector3(0.0f, 0.0f, angle);

            if (SlideWay == NoteSlideWay.CLOCKWISE)
                gameObject.transform.parent.eulerAngles = startAngle + vectorAngle;
            else
                gameObject.transform.parent.eulerAngles = startAngle - vectorAngle;

            if (time < 0.0f)
            {
                if (RoundTrip && !isRoundTrip)
                {
                    isRoundTrip = true;
                    endTime += timeLength;
                }
                else
                    DeleteNote();
            }

            yield return null;
        }
    }
}
