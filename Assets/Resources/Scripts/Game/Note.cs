using UnityEngine;
using System.Collections;

using Rhythm_Circulation;

public class Note : MonoBehaviour {

    public const float APPEAR_TIME = 0.2f;

    private NoteData m_noteData = new NoteData();
    private float m_noteTimeSeen = 0.0f;
    private bool m_isAppear = false;

    public NoteType Type
    {
        get
        {
            return m_noteData.Type;
        }
        set
        {
            m_noteData.Type = value;

            SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            Sprite sprite = null;

            switch (m_noteData.Type)
            {
                case NoteType.TAP:
                    sprite = Resources.Load("Images/Game/Note/note_base_tap", typeof(Sprite)) as Sprite;
                    spriteRenderer.sprite = sprite;
                    break;

                case NoteType.LONG:
                    sprite = Resources.Load("Images/Game/Note/note_base_long", typeof(Sprite)) as Sprite;
                    spriteRenderer.sprite = sprite;
                    break;

                case NoteType.SLIDE:
                    sprite = Resources.Load("Images/Game/Note/note_base_slide", typeof(Sprite)) as Sprite;
                    spriteRenderer.sprite = sprite;
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
        gameObject.transform.localScale = new Vector3(0.0f, 0.0f, 1.0f);
    }

    void Update()
    {
        float time = Time.time;

        if (m_noteData.Type == NoteType.TAP)
        {
            if (!m_isAppear)
            {
                if (time <= m_noteTimeSeen)
                {
                    float scale = (APPEAR_TIME - (m_noteTimeSeen - time)) / APPEAR_TIME;

                    gameObject.transform.localScale = new Vector3(scale, scale, 1.0f);

                    return;
                }
                else
                {
                    gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

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
        else if (m_noteData.Type == NoteType.LONG)
        {
        }
        else if (m_noteData.Type == NoteType.SLIDE)
        {
        }
    }

    void OnMouseDown()
    {
        Debug.Log("down");
        Destroy(gameObject);
    }
}
