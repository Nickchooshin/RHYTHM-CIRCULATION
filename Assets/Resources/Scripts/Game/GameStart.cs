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
    public CircleBeat circleBeat;

    public GameObject ReadyStartUI;
    public Text ReadyStartText;

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

        AudioManager.Instance.LoadSEClip("Narration/07. [narration] Rready", "Ready");
        AudioManager.Instance.LoadSEClip("Narration/08. [narration] Start", "Start");
        AudioManager.Instance.LoadSEClip("Narration/12. [narration] Result", "Result");
        AudioManager.Instance.LoadSEClip("SE/04. [SE] Perfect_01", "Perfect_01");
        AudioManager.Instance.LoadSEClip("SE/04. [SE] Perfect_02", "Perfect_02");
        AudioManager.Instance.LoadSEClip("SE/04. [SE] Perfect_03", "Perfect_03");
        AudioManager.Instance.LoadSEClip("SE/07. [SE] Other", "Other");
        AudioManager.Instance.LoadSEClip("SE/08. [SE] Miss", "Miss");
        
        ReadyStartUI.SetActive(true);
        StartCoroutine("Game_Ready");
    }

    private IEnumerator Game_Ready()
    {
        AudioManager.Instance.PlaySE("Ready");

        yield return new WaitForSeconds(2.0f);

        ReadyStartText.text = "- START! -";
        AudioManager.Instance.PlaySE("Start");

        yield return new WaitForSeconds(2.0f);

        ReadyStartUI.SetActive(false);
        noteManager.SendMessage("StartInit");
        circleBeat.SendMessage("StartBeat");
    }
}
