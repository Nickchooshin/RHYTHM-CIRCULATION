using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class List : MonoBehaviour {

    public Image cover;
    public Text name;
    public Text singer;
    public Text level;

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
}
