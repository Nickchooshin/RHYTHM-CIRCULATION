using UnityEngine;
using System.Collections;

public class ScoreData {

    private int m_score = 0;
    private string m_rank = "";
    private string m_mastery = "";

    public int Score
    {
        get
        {
            return m_score;
        }
        set
        {
            m_score = value;
        }
    }

    public string Rank
    {
        get
        {
            return m_rank;
        }
        set
        {
            m_rank = value;
        }
    }

    public string Mastery
    {
        get
        {
            return m_mastery;
        }
        set
        {
            m_mastery = value;
        }
    }

    public ScoreData()
    {
    }

    ~ScoreData()
    {
    }
}
