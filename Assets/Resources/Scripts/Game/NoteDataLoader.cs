using UnityEngine;
using System.Collections;

using LitJson;

public class NoteDataLoader {

    private static readonly NoteDataLoader m_instance = new NoteDataLoader();
    
    private int m_bpm = 60;
    private int m_maxBeat = 0;
    private JsonData m_noteData = null;

    public static NoteDataLoader Instance
    {
        get
        {
            return m_instance;
        }
    }

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

    public JsonData NoteData
    {
        get
        {
            return m_noteData;
        }
    }

    private NoteDataLoader()
    {
    }

    ~NoteDataLoader()
    {
    }

    public void LoadNoteData(string filePath)
    {
        TextAsset textAsset = Resources.Load("Notes/" + filePath) as TextAsset;
        //TextReader textReader = new StringReader(textAsset.text);

        if (m_noteData != null)
            m_noteData.Clear();
        m_noteData = JsonMapper.ToObject(textAsset.text);

        m_bpm = (int)m_noteData["BPM"];
        m_maxBeat = (int)m_noteData["MaxBeat"];
    }
}
