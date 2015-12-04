﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using System.Collections.Generic;

using LitJson;
using Rhythm_Circulation;

public class NoteManager : MonoBehaviour {

    private List<Note> m_noteList = new List<Note>();
    private List<GameObject> m_pathList = new List<GameObject>();
    private Dictionary<GameObject, GameObject> m_roundTripDictionary = new Dictionary<GameObject, GameObject>();
    private float m_startTime = 0.0f;

    public Transform[] notePosition = new Transform[8];

    public Canvas canvas;

    void Start()
    {
        // 게임 진입 Scene을 만들기 전까지 임시 방편으로 여기서 NoteDataLoader를 작동시킨다.
        NoteDataLoader.Instance.LoadNoteData("hahi2");
        CreateNoteList();
        InsertNoteList();
        
        m_startTime = Time.time;
    }

    void Update()
    {
        float nowTime = Time.time;

        for (int i = 0; i < m_noteList.Count; i++)
        {
            Note note = m_noteList[i];

            if (nowTime >= (note.TimeSeen + m_startTime - Note.APPEAR_TIME))
            {
                note.AddDelayedTime(m_startTime);
                note.SetNoteActive(true);
                m_noteList.Remove(note);
                --i;
            }
            else
                break;
        }
    }

    private void CreateNoteList()
    {
        int bpm = NoteDataLoader.Instance.BPM;
        int maxBeat = NoteDataLoader.Instance.MaxBeat;

        JsonData jsonData = NoteDataLoader.Instance.NoteData;

        JsonData jsonBar = jsonData["Note"];
        for (int i = 0; i < jsonBar.Count; i++)
        {
            JsonData jsonBeat = jsonBar[i];
            for (int j = 0; j < jsonBeat.Count; j++)
            {
                JsonData jsonNote = jsonBeat[j];
                for (int k = 0; k < jsonNote.Count; k++)
                {
                    JsonData jsonNoteData = jsonNote[k];

                    NoteType type = (NoteType)(int)jsonNoteData["Type"];
                    int length = (int)jsonNoteData["Length"];
                    NoteSlideWay slideWay = (NoteSlideWay)(int)jsonNoteData["SlideWay"];
                    bool roundTrip = (bool)jsonNoteData["RoundTrip"];

                    Note note = null;

                    if (type == NoteType.TAP)
                    {
                        GameObject notePrefab = Resources.Load<GameObject>("Prefabs/TapNote");
                        GameObject noteObject = Instantiate<GameObject>(notePrefab);

                        note = noteObject.GetComponent<TapNote>();
                        note.Type = type;
                        note.TimeSeen = ((60.0f / bpm) / (maxBeat / 4)) * ((i * maxBeat) + j);
                    }
                    else if (type == NoteType.LONG)
                    {
                        GameObject notePrefab = Resources.Load<GameObject>("Prefabs/LongNote");
                        GameObject noteObject = Instantiate<GameObject>(notePrefab);

                        note = noteObject.GetComponent<LongNote>();
                        note.Type = type;
                        note.Length = length;
                        note.TimeSeen = ((60.0f / bpm) / (maxBeat / 4)) * ((i * maxBeat) + j);
                    }
                    else if (type == NoteType.SLIDE)
                    {
                        GameObject notePrefab = Resources.Load<GameObject>("Prefabs/SlideNote");
                        GameObject noteObject = Instantiate<GameObject>(notePrefab);
                        GameObject pathObject = null;

                        CreateSlidePath(ref pathObject, length, slideWay, roundTrip, k);

                        SlideNote slideNote = noteObject.transform.FindChild("NoteImage").GetComponent<SlideNote>();
                        slideNote.maskImage = pathObject.GetComponent<Image>();
                        slideNote.pathImage = pathObject.transform.FindChild("PathImage").GetComponent<Image>();

                        note = slideNote;
                        note.Type = type;
                        note.Length = length;
                        note.SlideWay = slideWay;
                        note.RoundTrip = roundTrip;
                        note.TimeSeen = ((60.0f / bpm) / (maxBeat / 4)) * ((i * maxBeat) + j);
                    }

                    if (note != null)
                    {
                        note.SetNoteActive(false);
                        if (type == NoteType.SLIDE)
                        {
                            note.transform.parent.position = new Vector3(0.0f, 0.0f, -0.99f);
                            note.transform.parent.eulerAngles = new Vector3(0.0f, 0.0f, -45.0f * k);
                            note.transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
                        }
                        else
                            note.transform.position = notePosition[k].position;

                        m_noteList.Add(note);
                    }
                }
            }
        }
    }

    private void InsertNoteList()
    {
        foreach (GameObject path in m_pathList)
        {
            path.transform.SetParent(canvas.transform);
            path.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }

        foreach (KeyValuePair<GameObject, GameObject> roundTripPair in m_roundTripDictionary)
        {
            GameObject roundTripObject = roundTripPair.Key;
            GameObject pathObject = roundTripPair.Value;

            roundTripObject.transform.SetParent(pathObject.transform);
            roundTripObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }

        m_pathList.Clear();
        m_roundTripDictionary.Clear();

        foreach (Note note in m_noteList)
        {
            if (note.Type == NoteType.SLIDE)
            {
                note.transform.parent.SetParent(canvas.transform);
                note.transform.parent.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
            else
            {
                note.transform.SetParent(canvas.transform);
                note.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
        }
    }

    private void CreateSlidePath(ref GameObject pathObject, int length, NoteSlideWay slideWay, bool roundTrip, int index)
    {
        GameObject pathPrefab = Resources.Load<GameObject>("Prefabs/Path");
        GameObject roundTripPrefab = Resources.Load<GameObject>("Prefabs/RoundTripNote");

        pathObject = Instantiate<GameObject>(pathPrefab);

        pathObject.transform.position = new Vector3(0.0f, 0.0f, -0.99f);
        if (slideWay == NoteSlideWay.CLOCKWISE)
            pathObject.transform.eulerAngles = new Vector3(0.0f, 0.0f, (-45.0f * index) + 11.25f);
        else
            pathObject.transform.eulerAngles = new Vector3(0.0f, 180.0f, (45.0f * index) + 11.25f);

        for (int i = 0; i <= length; i++)
        {
            GameObject roundTripObject = Instantiate<GameObject>(roundTripPrefab);
            roundTripObject.transform.position = notePosition[i + 1].position;
            m_roundTripDictionary.Add(roundTripObject, pathObject);

            if (roundTrip)
            {
                if (slideWay == NoteSlideWay.CLOCKWISE && i == length)
                    roundTripObject.transform.FindChild("Icon").GetComponent<Image>().enabled = true;
                else if (slideWay == NoteSlideWay.ANTI_CLOCKWISE && i == 0)
                    roundTripObject.transform.FindChild("Icon").GetComponent<Image>().enabled = true;
            }
        }

        m_pathList.Add(pathObject);
    }
}
