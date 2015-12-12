using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CreditButton : MonoBehaviour, IPointerClickHandler {

    public GameObject CreditUIPanel;

    public void OnPointerClick(PointerEventData eventData)
    {
        CreditUIPanel.SetActive(true);
    }
}
