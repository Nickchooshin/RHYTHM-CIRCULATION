using UnityEngine;
using System.Collections;

using Rhythm_Circulation;

public interface IJudgeReceiver {

    void Receive(Note.NoteJudge noteJudge);
}
