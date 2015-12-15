using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InformationUI : MonoBehaviour {

    public Image cover;
    public Text name;
    public Text singer;
    public Text bpm;
    public Text time;
    public Text difficulty;
    public Text level;
    public Text highScore;
    public Text message;

    public Sprite Cover
    {
        set
        {
            cover.sprite = value;
        }
    }

    public string Name
    {
        get
        {
            return name.text;
        }
        set
        {
            name.text = value;
        }
    }

    public string Singer
    {
        get
        {
            return singer.text;
        }
        set
        {
            singer.text = value;
        }
    }

    public string BPM
    {
        get
        {
            return bpm.text;
        }
        set
        {
            bpm.text = value;
        }
    }

    public string Time
    {
        get
        {
            return time.text;
        }
        set
        {
            time.text = value;
        }
    }

    public string Difficulty
    {
        get
        {
            return difficulty.text;
        }
        set
        {
            difficulty.text = value;
        }
    }

    public string Level
    {
        get
        {
            return level.text;
        }
        set
        {
            level.text = value;
        }
    }

    public string HighScore
    {
        get
        {
            return highScore.text;
        }
        set
        {
            highScore.text = value;
        }
    }

    public string Message
    {
        get
        {
            return message.text;
        }
        set
        {
            message.text = value;
        }
    }
}
