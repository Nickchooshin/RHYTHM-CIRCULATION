using UnityEngine;
using System.Collections;
using System.IO;

using LitJson;

public class NoteDataLoader {

    private static readonly NoteDataLoader m_instance = new NoteDataLoader();
    
    private int m_bpm = 60;
    private int m_maxBeat = 0;
    private int m_noteCount = 0;
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

    public int NoteCount
    {
        get
        {
            return m_noteCount;
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
        TextAsset textAsset = Resources.Load("Data/Notes/" + filePath) as TextAsset;
        TextReader textReader = new StringReader(textAsset.text);
        //FileStream file = new FileStream("./note.json", FileMode.Open);
        //StreamReader reader = new StreamReader(file);

        if (m_noteData != null)
            m_noteData.Clear();
        m_noteData = JsonMapper.ToObject(textAsset.text);
        //m_noteData = JsonMapper.ToObject(reader.ReadToEnd());

        m_bpm = (int)m_noteData["BPM"];
        m_maxBeat = (int)m_noteData["MaxBeat"];
        m_noteCount = (int)m_noteData["NoteCount"];
    }
}
