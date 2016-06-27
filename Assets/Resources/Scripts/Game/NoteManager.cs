using UnityEngine;
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
    private float m_musicTime = 0.0f;

    public Transform noteParent;
    
    public GameObject TapNotePrefab;
    public GameObject LongNotePrefab;
    public GameObject SlideNotePrefab;
    public GameObject SnapNotePrefab;
    public GameObject PathPrefab;
    public GameObject RoundTripNotePrefab;

    public Transform[] notePosition = new Transform[8];

    public ResultUI resultUIPanel;
    public Score score;

    void Awake()
    {
        // 게임 진입 Scene을 만들기 전까지 임시 방편으로 여기서 NoteDataLoader를 작동시킨다.
        //NoteDataLoader.Instance.LoadNoteData("hahi2");
        // 또한, 마찬가지로 임시 방편으로 자이로 센서를 여기서 작동시킨다.
        Input.gyro.enabled = true;
    }

    private void StartInit()
    {
        CreateNoteList();
        InsertNoteList();

        JsonData infoData = NoteDataLoader.Instance.InfoData;

        AudioManager.Instance.Play();

        m_startTime = Time.time + NoteDataLoader.Instance.NoteDelay;
        m_musicTime = (float)(int)infoData["Time"] + 1.5f;

        StartCoroutine("GameStart");
        StartCoroutine("ShowResult");
    }

    private IEnumerator GameStart()
    {
        while (true)
        {
            float nowTime = Time.time;

            for (int i = 0; i < m_noteList.Count; i++)
            {
                Note note = m_noteList[i];

                //if (nowTime >= (note.TimeSeen + m_startTime - Note.APPEAR_TIME))
                if (nowTime >= (m_startTime + note.TimeSeen) - Note.APPEAR_TIME)
                {
                    note.AddDelayedTime(m_startTime);
                    note.SetNoteActive(true);
                    m_noteList.Remove(note);
                    --i;
                }
                else
                    break;
            }

            yield return null;
        }
    }

    private IEnumerator ShowResult()
    {
        yield return new WaitForSeconds(m_musicTime);

        StopCoroutine("GameStart");

        AudioManager.Instance.Stop();
        AudioManager.Instance.SetAudioClip("BGM/04. [BGM] Result");
        AudioManager.Instance.Play();

        NoteDataLoader.DifficultyType noteDifficulty = NoteDataLoader.Instance.NoteDifficulty;

        JsonData infoData = NoteDataLoader.Instance.InfoData;
        float accuracy = score.Accuracy;

        resultUIPanel.Name = infoData["Name"].ToString();
        resultUIPanel.Singer = infoData["Singer"].ToString();
        resultUIPanel.Perfect = score.PerfectCount.ToString();
        resultUIPanel.Great = score.GreatCount.ToString();
        resultUIPanel.Good = score.GoodCount.ToString();
        resultUIPanel.Miss = score.MissCount.ToString();
        resultUIPanel.Accuracy = score.Accuracy.ToString("F1") + "%";
        resultUIPanel.Score = score.TotalScore.ToString();
        resultUIPanel.Difficulty = infoData["Note"][(int)noteDifficulty]["Difficulty"].ToString();
        if (accuracy >= 96.0f)
            resultUIPanel.Rank = "S";
        else if (accuracy >= 85.0f)
            resultUIPanel.Rank = "A";
        else if (accuracy >= 75.0f)
            resultUIPanel.Rank = "B";
        else if (accuracy >= 70.0f)
            resultUIPanel.Rank = "C";
        else
            resultUIPanel.Rank = "F";
        resultUIPanel.gameObject.SetActive(true);

        score.ScoreRecord(infoData["Name"].ToString(), infoData["Note"][(int)noteDifficulty]["Difficulty"].ToString());
    }

    private void CreateNoteList()
    {
        int bpm = NoteDataLoader.Instance.BPM;
        int maxBeat = NoteDataLoader.Instance.MaxBeat;

        JsonData jsonData = NoteDataLoader.Instance.NoteData;

        JsonData jsonNoteList = jsonData["Note"];

        for (int i = 0; i < jsonNoteList.Count; i++)
        {
            JsonData jsonNote = jsonNoteList[i];

            NoteType type = (NoteType)(int)jsonNote["Type"];
            int length = (int)jsonNote["Length"];
            int slideTime = (int)jsonNote["SlideTime"];
            NoteSlideWay slideWay = (NoteSlideWay)(int)jsonNote["SlideWay"];
            bool roundTrip = (bool)jsonNote["RoundTrip"];
            int number = (int)jsonNote["Number"];
            int bar = (int)jsonNote["Bar"];
            int beat = (int)jsonNote["Beat"];

            //float time = (float)(((60.0f / bpm) / (maxBeat / 4)) * ((i * maxBeat) + j));
            float time = (float)(((60.0f / bpm) / maxBeat) * ((bar * maxBeat) + beat));

            Note note = null;

            if (type == NoteType.TAP)
            {
                GameObject noteObject = Instantiate<GameObject>(TapNotePrefab);

                note = noteObject.GetComponent<TapNote>();
                note.Type = type;
                note.TimeSeen = time;
            }
            else if (type == NoteType.LONG)
            {
                GameObject noteObject = Instantiate<GameObject>(LongNotePrefab);

                note = noteObject.GetComponent<LongNote>();
                note.Type = type;
                note.Length = length;
                note.TimeSeen = time;
            }
            else if (type == NoteType.SLIDE)
            {
                GameObject noteObject = Instantiate<GameObject>(SlideNotePrefab);
                GameObject pathObject = null;

                CreateSlidePath(ref pathObject, length, slideWay, roundTrip, number);

                SlideNote slideNote = noteObject.transform.FindChild("NoteImage").GetComponent<SlideNote>();
                slideNote.maskImage = pathObject.GetComponent<Image>();
                slideNote.pathImage = pathObject.transform.FindChild("PathImage").GetComponent<Image>();

                note = slideNote;
                note.Type = type;
                note.Length = length;
                note.SlideTime = slideTime;
                note.SlideWay = slideWay;
                note.RoundTrip = roundTrip;
                note.TimeSeen = time;
            }
            else if (type == NoteType.SNAP)
            {
                GameObject noteObject = Instantiate<GameObject>(SnapNotePrefab);

                note = noteObject.GetComponent<SnapNote>();
                note.Type = type;
                note.TimeSeen = time;
            }

            if (note != null)
            {
                note.SetNoteActive(false);
                if (type == NoteType.SLIDE)
                {
                    note.transform.parent.position = new Vector3(0.0f, 0.0f, -1.0f);
                    note.transform.parent.eulerAngles = new Vector3(0.0f, 0.0f, -45.0f * number);
                    note.transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
                }
                else if (type == NoteType.SNAP)
                {
                    note.transform.position = new Vector3(0.0f, 0.0f, -1.0f);
                }
                else
                    note.transform.position = notePosition[number].position;

                m_noteList.Add(note);
            }
        }
    }

    private void InsertNoteList()
    {
        foreach (GameObject path in m_pathList)
        {
            path.transform.SetParent(noteParent);
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
                note.transform.parent.SetParent(noteParent);
                note.transform.parent.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
            else
            {
                note.transform.SetParent(noteParent);
                note.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
        }
    }

    private void CreateSlidePath(ref GameObject pathObject, int length, NoteSlideWay slideWay, bool roundTrip, int index)
    {
        pathObject = Instantiate<GameObject>(PathPrefab);

        pathObject.transform.position = new Vector3(0.0f, 0.0f, -0.99f);
        if (slideWay == NoteSlideWay.CLOCKWISE)
            pathObject.transform.eulerAngles = new Vector3(0.0f, 0.0f, (-45.0f * index) + 11.25f);
        else
            pathObject.transform.eulerAngles = new Vector3(0.0f, 180.0f, (45.0f * index) + 11.25f);

        for (int i = 0; i <= length; i++)
        {
            GameObject roundTripObject = Instantiate<GameObject>(RoundTripNotePrefab);
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
