using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using System.IO;
using System.Collections.Generic;

using LitJson;
using Rhythm_Circulation;

public class NoteDataManager : MonoBehaviour {

    //private static readonly NoteDataManager m_instance = new NoteDataManager();
    private int m_bpm = 60;
    private int m_maxBeat = 0;
    private List<Note> m_noteList = new List<Note>();
    private float m_startTime = 0.0f;

    public Transform[] notePosition = new Transform[8];

    public Canvas canvas;

    /*
    public static NoteDataManager Instance
    {
        get
        {
            return m_instance;
        }
    }
    */

    public int BPM
    {
        get
        {
            return m_bpm;
        }
    }

    public int MaxBeat
    {
        get
        {
            return m_maxBeat;
        }
    }

    public List<Note> NoteList
    {
        get
        {
            return m_noteList;
        }
    }

    /*
    private NoteDataManager()
    {
    }
    
    ~NoteDataManager()
    {
    }
    */

    public void LoadNoteData(string filePath)
    {
        TextAsset textAsset = Resources.Load("Notes/" + filePath) as TextAsset;
        //TextReader textReader = new StringReader(textAsset.text);

        ClearNoteList();

        JsonData jsonData = JsonMapper.ToObject(textAsset.text);

        m_bpm = (int)jsonData["BPM"];
        m_maxBeat = (int)jsonData["MaxBeat"];

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

                    if (type == NoteType.TAP || type == NoteType.LONG || type == NoteType.SLIDE)
                    {
                        GameObject notePrefab = Resources.Load("Prefabs/Note") as GameObject;
                        GameObject noteObject = MonoBehaviour.Instantiate(notePrefab) as GameObject;

                        Note note = noteObject.GetComponent<Note>();
                        note.Type = type;
                        note.Length = length;
                        note.SlideWay = slideWay;
                        note.TimeSeen = ((60.0f / BPM) / (MaxBeat / 4)) * ((i * MaxBeat) + j);

                        note.gameObject.SetActive(false);
                        note.gameObject.transform.position = notePosition[k].position;

                        note.gameObject.transform.SetParent(canvas.transform);

                        m_noteList.Add(note);
                    }
                }
            }
        }
    }

    public void ClearNoteList()
    {
        m_noteList.Clear();
    }

    public void Start()
    {
        LoadNoteData("hahi");

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
}
