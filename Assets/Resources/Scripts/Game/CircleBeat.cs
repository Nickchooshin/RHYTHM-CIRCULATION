﻿using UnityEngine;
using System.Collections;

public class CircleBeat : MonoBehaviour {

    private float m_beatTime = 0.0f;
    private float m_time = 0.0f;
    private bool m_isScale = false;

    private const float SCALE_UP_VALUE = 1.2f;
    private const float SCALE_DOWN_VALUE = 0.9f;

    void Start()
    {
        //float bpm = NoteDataManager.Instance.BPM;
        float bpm = 120.0f;
        m_beatTime = 60.0f / bpm;
    }

    void Update()
    {
        m_time += Time.deltaTime;

        if (m_time >= m_beatTime)
        {
            int quotient = (int)(m_time / m_beatTime);

            m_time -= (float)(quotient * m_beatTime);

            if (!m_isScale)
            {
                m_isScale = true;
                gameObject.transform.localScale = new Vector3(SCALE_UP_VALUE, SCALE_UP_VALUE, 1.0f);
            }
        }
        else
        {
            if (m_isScale)
            {
                m_isScale = false;
                gameObject.transform.localScale = new Vector3(SCALE_DOWN_VALUE, SCALE_DOWN_VALUE, 1.0f);
            }
            else
            {
                gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
        }
    }
}
