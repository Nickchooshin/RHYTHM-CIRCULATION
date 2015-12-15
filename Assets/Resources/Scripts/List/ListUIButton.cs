using UnityEngine;
using System.Collections;
using UnityEngine.Events;

using LitJson;

public class ListUIButton : MonoBehaviour {

    private bool m_isInformationShow = false;
    private string m_stringJson = "";

    public InformationUI InformationUIPanel;

    public void BackButtonClick()
    {
        Application.LoadLevel("sceneTitle");
    }

    public void MusicInformationClick(List list)
    {
        JsonData infoData = list.Info;
        JsonData noteData = infoData["Note"][0];

        InformationUIPanel.Cover = Resources.Load<Sprite>("Images/List/Cover/" + infoData["Info_Cover"].ToString());
        InformationUIPanel.Name = infoData["Name"].ToString();
        InformationUIPanel.Singer = infoData["Singer"].ToString();
        InformationUIPanel.BPM = infoData["BPM"].ToString();
        InformationUIPanel.Time = infoData["Time"].ToString();
        InformationUIPanel.Difficulty = noteData["Difficulty"].ToString();
        InformationUIPanel.Level = noteData["Level"].ToString();

        m_stringJson = noteData["Json"].ToString();

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
        NoteDataLoader.Instance.LoadNoteData(m_stringJson);

        AudioManager.Instance.Stop();
        Application.LoadLevel("sceneGame");
    }
}
