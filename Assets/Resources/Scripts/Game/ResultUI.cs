using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResultUI : MonoBehaviour {

    public Text name;
    public Text singer;
    public Text perfect;
    public Text great;
    public Text good;
    public Text miss;
    public Text accuracy;
    public Text score;
    public Text mastery;
    public Text rank;

    public string Name
    {
        set
        {
            name.text = value;
        }
    }

    public string Singer
    {
        set
        {
            singer.text = value;
        }
    }

    public string Perfect
    {
        set
        {
            perfect.text = value;
        }
    }

    public string Great
    {
        set
        {
            great.text = value;
        }
    }

    public string Good
    {
        set
        {
            good.text = value;
        }
    }

    public string Miss
    {
        set
        {
            miss.text = value;
        }
    }

    public string Accuracy
    {
        set
        {
            accuracy.text = value;
        }
    }

    public string Score
    {
        set
        {
            score.text = value;
        }
    }

    public string Mastery
    {
        set
        {
            mastery.text = value;
        }
    }

    public string Rank
    {
        set
        {
            rank.text = value;
        }
    }
}
