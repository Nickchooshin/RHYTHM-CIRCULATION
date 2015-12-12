using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CreditExitButton : MonoBehaviour, IPointerClickHandler {

    public GameObject CreditUIPanel;

    public void OnPointerClick(PointerEventData eventData)
    {
        CreditUIPanel.SetActive(false);
    }
}
