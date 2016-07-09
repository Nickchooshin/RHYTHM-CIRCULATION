using UnityEngine;
using System.Collections;

public class CreditUIButton : MonoBehaviour {

    private bool m_isCreditShow = false;

    public GameObject CreditUIPanel;

    public void StartButtonClick()
    {
        Application.LoadLevel("sceneList");
    }

    public void ShowCredit()
    {
        m_isCreditShow = !m_isCreditShow;
        CreditUIPanel.SetActive(m_isCreditShow);
    }
}
