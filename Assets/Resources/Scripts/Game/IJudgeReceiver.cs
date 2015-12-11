using UnityEngine;
using System.Collections;

public interface IJudgeReceiver {

    void Receive(Note.NoteJudge noteJudge);
}
