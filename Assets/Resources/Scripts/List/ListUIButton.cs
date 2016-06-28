using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;

using LitJson;

public class ListUIButton : MonoBehaviour {

    private bool m_isInformationShow = false;
    private JsonData m_infoData = null;

    public InformationUI InformationUIPanel;
    public GameObject[] DifficultyBackground = new GameObject[3];
    public Button[] DifficultyButton = new Button[3];
    public Sprite[] ButtonImageNormal = new Sprite[3];
    public Sprite[] ButtonImageActive = new Sprite[3];

    void Start()
    {
        ChangeDifficulty(NoteDataLoader.Instance.NoteDifficulty);
    }

    public void BackButtonClick()
    {
        Application.LoadLevel("sceneTitle");
    }

    public void BasicButtonClick()
    {
        ChangeDifficulty(NoteDataLoader.DifficultyType.BASIC);
    }

    public void AdvancedButtonClick()
    {
        ChangeDifficulty(NoteDataLoader.DifficultyType.ADVANCED);
    }

    public void ExtremeButtonClick()
    {
        ChangeDifficulty(NoteDataLoader.DifficultyType.EXTREME);
    }

    private void ChangeDifficulty(NoteDataLoader.DifficultyType noteDifficulty)
    {
        NoteDataLoader.DifficultyType prevNoteDifficulty = NoteDataLoader.Instance.NoteDifficulty;

        DifficultyBackground[(int)prevNoteDifficulty].SetActive(false);
        DifficultyBackground[(int)noteDifficulty].SetActive(true);

        DifficultyButton[(int)prevNoteDifficulty].image.sprite = ButtonImageNormal[(int)prevNoteDifficulty];
        DifficultyButton[(int)noteDifficulty].image.sprite = ButtonImageActive[(int)noteDifficulty];

        NoteDataLoader.Instance.NoteDifficulty = noteDifficulty;
    }

    public void MusicInformationClick(List list)
    {
        NoteDataLoader.DifficultyType noteDifficulty = NoteDataLoader.Instance.NoteDifficulty;

        JsonData infoData = list.Info;
        JsonData noteData = infoData["Note"][(int)noteDifficulty];

        AudioManager.Instance.SetAudioClip("Music/" + infoData["Music"].ToString());

        InformationUIPanel.Cover = Resources.Load<Sprite>("Images/List/Cover/" + infoData["Info_Cover"].ToString());
        InformationUIPanel.Name = infoData["Name"].ToString();
        InformationUIPanel.Singer = infoData["Singer"].ToString();
        InformationUIPanel.BPM = infoData["BPM"].ToString();
        InformationUIPanel.Time = AudioManager.Instance.GetLength().ToString("00.0") + " s";
        InformationUIPanel.Difficulty = noteData["Difficulty"].ToString();
        InformationUIPanel.Level = noteData["Level"].ToString();
        InformationUIPanel.HighScore = HighScoreManager.Instance.GetHighScore(InformationUIPanel.Name, InformationUIPanel.Difficulty).ToString();

        m_infoData = infoData;

        ShowInformation();

        float previewStartTime = (float)((double)infoData["Preview_Start"]);
        float previewEndTime = (float)((double)infoData["Preview_End"]);
        AudioManager.Instance.PlaySection(previewStartTime, previewEndTime, true);
    }

    public void ShowInformation()
    {
        m_isInformationShow = !m_isInformationShow;
        InformationUIPanel.gameObject.SetActive(m_isInformationShow);

        AudioManager.Instance.Stop();
        if (!m_isInformationShow)
        {
            AudioManager.Instance.SetAudioClip("BGM/03. [BGM] List");
            AudioManager.Instance.Play();
        }
    }

    public void StartButtonClick()
    {
        NoteDataLoader.DifficultyType noteDifficulty = NoteDataLoader.Instance.NoteDifficulty;

        NoteDataLoader.Instance.LoadNoteData(m_infoData["Note"][(int)noteDifficulty]["Json"].ToString());
        NoteDataLoader.Instance.InfoData = m_infoData;

        AudioManager.Instance.Stop();

        Application.LoadLevel("sceneGame");
    }
}
