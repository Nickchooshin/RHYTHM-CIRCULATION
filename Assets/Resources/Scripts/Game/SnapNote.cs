using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class SnapNote : Note
{

    void Start()
    {
        Init();
    }

    protected override void Init()
    {
        noteImage.fillAmount = 0.0f;

        StartCoroutine("NoteAppear");
        StartCoroutine("NoteJudge_Snap");
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
    }

    private IEnumerator NoteJudge_Snap()
    {
        while (true)
        {
            float snap = Vector3.Distance(Vector3.zero, Input.gyro.rotationRate);

            if (snap >= 2.5f)
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

                DeleteNote();
            }

            yield return null;
        }
    }
}