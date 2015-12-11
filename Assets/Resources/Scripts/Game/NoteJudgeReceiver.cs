using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NoteJudgeReceiver {

    private static readonly NoteJudgeReceiver m_instance = new NoteJudgeReceiver();

    private List<IJudgeReceiver> m_judgeReceiverList = new List<IJudgeReceiver>();

    public static NoteJudgeReceiver Instance
    {
        get
        {
            return m_instance;
        }
    }

    private NoteJudgeReceiver()
    {
    }

    ~NoteJudgeReceiver()
    {
    }

    public void AddJudgeReceiver(IJudgeReceiver judgeReceiver)
    {
        m_judgeReceiverList.Add(judgeReceiver);
    }

    public void ClearJudgeReceiver()
    {
        m_judgeReceiverList.Clear();
    }

    public void SendNoteJudge(Note.NoteJudge noteJudge)
    {
        foreach(IJudgeReceiver judgeReceiver in m_judgeReceiverList)
        {
            judgeReceiver.Receive(noteJudge);
        }
    }
}
