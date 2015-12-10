using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class SnapNote : Note {

    void Start()
    {
        Init();
    }

    protected override void Init()
    {
        noteImage.fillAmount = 0.0f;

        StartCoroutine("NoteAppear");
        StartCoroutine("NoteJudge_Snap");
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
    }

    private IEnumerator NoteJudge_Snap()
    {
        while (true)
        {
            float snap = Vector3.Distance(Vector3.zero, Input.gyro.rotationRate);

            if (snap >= 2.5f)
                DeleteNote();

            yield return null;
        }
    }
}
