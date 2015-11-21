using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using Rhythm_Circulation;

public class Note : MonoBehaviour, IPointerDownHandler {

    public const float APPEAR_TIME = 0.2f;

    private NoteData m_noteData = new NoteData();
    private float m_noteTimeSeen = 0.0f;
    private bool m_isAppear = false;

    private Image m_image;

    public NoteType Type
    {
        get
        {
            return m_noteData.Type;
        }
        set
        {
            m_noteData.Type = value;

            Image noteImage = gameObject.GetComponent<Image>();
            Image iconImage = gameObject.transform.FindChild("Icon").GetComponent<Image>();
            Sprite noteSprite = null;
            Sprite iconSprite = null;

            switch (m_noteData.Type)
            {
                case NoteType.TAP:
                    noteSprite = Resources.Load("Images/Game/Note/note_base_tap", typeof(Sprite)) as Sprite;
                    iconSprite = Resources.Load("Images/Game/Note/note_icon_tap", typeof(Sprite)) as Sprite;
                    noteImage.sprite = noteSprite;
                    iconImage.sprite = iconSprite;
                    break;

                case NoteType.LONG:
                    noteSprite = Resources.Load("Images/Game/Note/note_base_long", typeof(Sprite)) as Sprite;
                    iconSprite = Resources.Load("Images/Game/Note/note_icon_long", typeof(Sprite)) as Sprite;
                    noteImage.sprite = noteSprite;
                    iconImage.sprite = iconSprite;
                    break;

                case NoteType.SLIDE:
                    noteSprite = Resources.Load("Images/Game/Note/note_base_slide", typeof(Sprite)) as Sprite;
                    iconSprite = Resources.Load("Images/Game/Note/note_icon_slide", typeof(Sprite)) as Sprite;
                    noteImage.sprite = noteSprite;
                    iconImage.sprite = iconSprite;
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
        m_image = gameObject.GetComponent<Image>();
        m_image.fillAmount = 0.0f;

        gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }

    void Update()
    {
        float time = Time.time;

        if (m_noteData.Type == NoteType.TAP || m_noteData.Type == NoteType.LONG)
        {
            if (!m_isAppear)
            {
                if (time <= m_noteTimeSeen)
                {
                    float fillAmount = (APPEAR_TIME - (m_noteTimeSeen - time)) / APPEAR_TIME;

                    m_image.fillAmount = fillAmount;

                    return;
                }
                else
                {
                    m_image.fillAmount = 1.0f;

                    m_isAppear = true;
                }
            }

            if (m_isAppear)
            {
                if (time > m_noteTimeSeen + (APPEAR_TIME / 2.0f))
                {
                    Destroy(gameObject);
                }
            }
        }
        //else if (m_noteData.Type == NoteType.LONG)
        //{
        //}
        else if (m_noteData.Type == NoteType.SLIDE)
        {
        }
    }

    // Event System

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Down");
        Destroy(gameObject);
    }
}
