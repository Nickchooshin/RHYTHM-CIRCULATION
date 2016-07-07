using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using LitJson;

public class List : MonoBehaviour {

    private JsonData m_info;

    public Image cover;
    public Text name;
    public Text singer;
    public Text level;
    public Button button;
    public Text mastery;
    public Text rank;

    public JsonData Info
    {
        get
        {
            return m_info;
        }
        set
        {
            m_info = value;
        }
    }

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

    public UnityEngine.Events.UnityAction ButtonListener
    {
        set
        {
            button.onClick.AddListener(value);
        }
    }

    public string Mastery
    {
        get
        {
            return mastery.text;
        }
        set
        {
            mastery.text = value;
        }
    }

    public string Rank
    {
        get
        {
            return rank.text;
        }
        set
        {
            rank.text = value;
        }
    }
}
