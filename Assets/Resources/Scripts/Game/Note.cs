﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using Rhythm_Circulation;

public abstract class Note : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    public enum NoteJudge
    {
        PERFECT,
        GREAT,
        GOOD,
        BAD
    }

    public const float APPEAR_TIME = 0.35f;

    public const float PERFECT_TIMING = 0.1f;
    public const float GREAT_TIMING = 0.2f;
    //public const float GOOD_TIMING = 0.3f;
    public const float GOOD_TIMING = 0.35f;

    protected NoteData m_noteData = new NoteData();
    protected NoteJudge m_noteJudge = NoteJudge.BAD;
    protected float m_noteTimeSeen = 0.0f;

    public Image noteImage;

    public NoteType Type
    {
        get
        {
            return m_noteData.Type;
        }
        set
        {
            m_noteData.Type = value;
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

    public int SlideTime
    {
        get
        {
            return m_noteData.SlideTime;
        }
        set
        {
            m_noteData.SlideTime = value;
        }
    }

    public NoteSlideWay SlideWay
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

    public bool RoundTrip
    {
        get
        {
            return m_noteData.RoundTrip;
        }
        set
        {
            m_noteData.RoundTrip = value;
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

    protected virtual void Init()
    {
        noteImage.fillAmount = 0.0f;

        StartCoroutine("NoteAppear");
    }

    public virtual void SetNoteActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }

    protected virtual void DeleteNote()
    {
        Debug.Log(m_noteJudge);
        NoteJudgeReceiver.Instance.SendNoteJudge(m_noteJudge);

        Destroy(gameObject);
    }

    protected virtual IEnumerator NoteAppear()
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

    // Event System

    public abstract void OnPointerDown(PointerEventData eventData);
    public virtual void OnPointerUp(PointerEventData eventData)
    {
        DeleteNote();
    }
}
