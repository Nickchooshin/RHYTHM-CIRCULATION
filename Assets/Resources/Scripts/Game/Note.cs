using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using Rhythm_Circulation;

public class Note : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    enum NoteJudge
    {
        PERFECT,
        GREAT,
        GOOD,
        BAD
    }

    public const float APPEAR_TIME = 0.2f;

    public const float PERFECT_TIMING = 0.1f;
    public const float GREAT_TIMING = 0.2f;
    public const float GOOD_TIMING = 0.3f;

    private NoteData m_noteData = new NoteData();
    NoteJudge m_noteJudge = NoteJudge.BAD;
    private float m_noteTimeSeen = 0.0f;

    public Image noteImage;
    public Image iconImage;

    public NoteType Type
    {
        get
        {
            return m_noteData.Type;
        }
        set
        {
            m_noteData.Type = value;

            switch (m_noteData.Type)
            {
                case NoteType.TAP:
                    noteImage.sprite = Resources.Load("Images/Game/Note/note_base_tap", typeof(Sprite)) as Sprite;
                    iconImage.sprite = Resources.Load("Images/Game/Note/note_icon_tap", typeof(Sprite)) as Sprite;
                    break;

                case NoteType.LONG:
                    noteImage.sprite = Resources.Load("Images/Game/Note/note_base_long", typeof(Sprite)) as Sprite;
                    iconImage.sprite = Resources.Load("Images/Game/Note/note_icon_long", typeof(Sprite)) as Sprite;

                    GameObject gaugeObject = Instantiate(Resources.Load<GameObject>("Prefabs/UI_Image"));
                    Image gaugeImage = gaugeObject.GetComponent<Image>();

                    gaugeImage.sprite = Resources.Load("Images/Game/Note/note_long", typeof(Sprite)) as Sprite;
                    gaugeImage.type = Image.Type.Filled;
                    gaugeImage.fillMethod = Image.FillMethod.Radial360;
                    gaugeImage.fillOrigin = 2;
                    gaugeImage.fillClockwise = true;
                    gaugeImage.fillAmount = 0.0f;

                    gaugeImage.name = "Gauge";
                    gaugeImage.transform.SetParent(gameObject.transform);
                    break;

                case NoteType.SLIDE:
                    noteImage.sprite = Resources.Load("Images/Game/Note/note_base_slide", typeof(Sprite)) as Sprite;
                    iconImage.sprite = Resources.Load("Images/Game/Note/note_icon_slide", typeof(Sprite)) as Sprite;
                    break;
            }
        }
    }

    public int Length
    {
        get
        {
            return m_noteData.Length;
        }
        set
        {
            m_noteData.Length = value;
        }
    }

    public SlideWay SlideWay
    {
        get
        {
            return m_noteData.SlideWay;
        }
        set
        {
            m_noteData.SlideWay = value;
        }
    }

    public float TimeSeen
    {
        get
        {
            return m_noteTimeSeen;
        }
        set
        {
            m_noteTimeSeen = value;
        }
    }

    public void AddDelayedTime(float delayedTime)
    {
        m_noteTimeSeen += delayedTime;
    }

    void Start()
    {
        noteImage = gameObject.GetComponent<Image>();
        noteImage.fillAmount = 0.0f;

        gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        StartCoroutine("NoteAppear");
    }

    IEnumerator NoteAppear()
    {
        while (true)
        {
            float time = Time.time;

            if (time <= m_noteTimeSeen)
            {
                float fillAmount = (APPEAR_TIME - (m_noteTimeSeen - time)) / APPEAR_TIME;

                noteImage.fillAmount = fillAmount;
            }
            else
            {
                noteImage.fillAmount = 1.0f;

                yield return new WaitForSeconds((APPEAR_TIME / 2.0f) - (time - m_noteTimeSeen));

                DeleteNote();
            }

            yield return null;
        }
    }

    IEnumerator NoteJudge_Long()
    {
        int bpm = NoteDataLoader.Instance.BPM;
        int maxBeat = NoteDataLoader.Instance.MaxBeat;
        float timeLength = ((60.0f / bpm) / (maxBeat / 4)) * m_noteData.Length;
        float endTime = Time.time + timeLength;

        Image gaugeImage = gameObject.transform.FindChild("Gauge").GetComponent<Image>();

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

    private void DeleteNote()
    {
        Debug.Log(m_noteJudge);

        Destroy(gameObject);
    }

    // Event System

    public void OnPointerDown(PointerEventData eventData)
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

        switch (m_noteData.Type)
        {
            case NoteType.TAP:
                DeleteNote();
                break;

            case NoteType.LONG:
                StopCoroutine("NoteAppear");
                StartCoroutine("NoteJudge_Long");
                break;

            case NoteType.SLIDE:
                break;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        DeleteNote();
    }
}
