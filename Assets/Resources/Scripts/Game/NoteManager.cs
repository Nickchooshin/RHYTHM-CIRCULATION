using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using System.Collections.Generic;

using LitJson;
using Rhythm_Circulation;

public class NoteManager : MonoBehaviour {

    private List<Note> m_noteList = new List<Note>();
    private float m_startTime = 0.0f;

    public Transform[] notePosition = new Transform[8];

    public Canvas canvas;

    public void Start()
    {
        // 게임 진입 Scene을 만들기 전까지 임시 방편으로 여기서 NoteDataLoader를 작동시킨다.
        NoteDataLoader.Instance.LoadNoteData("hahi");
        CreateNoteList();

        m_startTime = Time.time;
    }

    public void Update()
    {
        float nowTime = Time.time;

        for (int i = 0; i < m_noteList.Count; i++)
        {
            Note note = m_noteList[i];

            if (nowTime >= (note.TimeSeen + m_startTime - Note.APPEAR_TIME))
            {
                note.AddDelayedTime(m_startTime);
                note.gameObject.SetActive(true);
                m_noteList.Remove(note);
                --i;
            }
            else
                break;
        }
    }

    public void CreateNoteList()
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
                    SlideWay slideWay = (SlideWay)(int)jsonNoteData["SlideWay"];
                    bool roundTrip = (bool)jsonNoteData["RoundTrip"];

                    GameObject notePrefab = null;
                    GameObject noteObject = null;
                    Note note = null;

                    if (type == NoteType.TAP)
                    {
                        notePrefab = Resources.Load<GameObject>("Prefabs/TapNote");
                        noteObject = Instantiate<GameObject>(notePrefab);

                        note = noteObject.GetComponent<TapNote>();
                        note.Type = type;
                        note.TimeSeen = ((60.0f / bpm) / (maxBeat / 4)) * ((i * maxBeat) + j);
                    }
                    else if (type == NoteType.LONG)
                    {
                        notePrefab = Resources.Load<GameObject>("Prefabs/LongNote");
                        noteObject = Instantiate<GameObject>(notePrefab);

                        note = noteObject.GetComponent<LongNote>();
                        note.Type = type;
                        note.Length = length;
                        note.TimeSeen = ((60.0f / bpm) / (maxBeat / 4)) * ((i * maxBeat) + j);
                    }
                    else if (type == NoteType.SLIDE)
                    {
                        notePrefab = Resources.Load<GameObject>("Prefabs/SlideNote");
                        noteObject = Instantiate<GameObject>(notePrefab);

                        note = noteObject.GetComponent<SlideNote>();
                        note.Type = type;
                        note.Length = length;
                        note.SlideWay = slideWay;
                        note.RoundTrip = roundTrip;
                        note.TimeSeen = ((60.0f / bpm) / (maxBeat / 4)) * ((i * maxBeat) + j);
                    }

                    if (noteObject != null)
                    {
                        noteObject.SetActive(false);
                        noteObject.transform.position = notePosition[k].position;
                        noteObject.transform.SetParent(canvas.transform);

                        m_noteList.Add(note);
                    }
                }
            }
        }
    }
}
