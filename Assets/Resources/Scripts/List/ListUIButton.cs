using UnityEngine;
using System.Collections;
using UnityEngine.Events;

using LitJson;

public class ListUIButton : MonoBehaviour {

    private bool m_isInformationShow = false;
    private JsonData m_infoData = null;
    private int m_noteDifficulty = 0;

    public InformationUI InformationUIPanel;

    public void BackButtonClick()
    {
        Application.LoadLevel("sceneTitle");
    }

    public void MusicInformationClick(List list)
    {
        JsonData infoData = list.Info;
        JsonData noteData = infoData["Note"][m_noteDifficulty];

        InformationUIPanel.Cover = Resources.Load<Sprite>("Images/List/Cover/" + infoData["Info_Cover"].ToString());
        InformationUIPanel.Name = infoData["Name"].ToString();
        InformationUIPanel.Singer = infoData["Singer"].ToString();
        InformationUIPanel.BPM = infoData["BPM"].ToString();
        InformationUIPanel.Time = infoData["Time"].ToString();
        InformationUIPanel.Difficulty = noteData["Difficulty"].ToString();
        InformationUIPanel.Level = noteData["Level"].ToString();

        m_infoData = infoData;

        ShowInformation();

        AudioManager.Instance.SetAudioClip("Music_Preview/" + infoData["Music_Preview"].ToString());
        AudioManager.Instance.Play();
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
        NoteDataLoader.Instance.LoadNoteData(m_infoData["Note"][m_noteDifficulty]["Json"].ToString());
        NoteDataLoader.Instance.InfoData = m_infoData;
        NoteDataLoader.Instance.NoteDifficulty = m_noteDifficulty;

        AudioManager.Instance.Stop();

        Application.LoadLevel("sceneGame");
    }
}
