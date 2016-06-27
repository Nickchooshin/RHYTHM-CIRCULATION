using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using LitJson;

public class GameStart : MonoBehaviour {

    public SpriteRenderer cover;
    public Text name;
    public Text singer;
    public Text difficulty;
    public NoteManager noteManager;

    void Start()
    {
        NoteDataLoader.DifficultyType noteDifficulty = NoteDataLoader.Instance.NoteDifficulty;

        JsonData infoData = NoteDataLoader.Instance.InfoData;
        JsonData difficultyData = infoData["Note"][(int)noteDifficulty];

        cover.sprite = Resources.Load<Sprite>("Images/List/Cover/" + infoData["List_Cover"].ToString());
        name.text = infoData["Name"].ToString();
        singer.text = infoData["Singer"].ToString();
        difficulty.text = difficultyData["Difficulty"].ToString() + " / LEVEL : " + difficultyData["Level"].ToString();

        AudioManager.Instance.SetAudioClip("Music/" + infoData["Music"].ToString());

        noteManager.SendMessage("StartInit");
    }
}
