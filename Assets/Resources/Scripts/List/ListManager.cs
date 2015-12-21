using UnityEngine;
using System.Collections;
//using UnityEngine.UI;

using LitJson;

public class ListManager : MonoBehaviour {

    public Transform listPanel;
    public ListUIButton scriptListUIButton;

    void Start()
    {
        TextAsset listTextAsset = Resources.Load<TextAsset>("Data/Information/List");
        JsonData listData = JsonMapper.ToObject(listTextAsset.text);

        int listNumber = listData.Count;

        listPanel.localScale = new Vector3(1.0f, listNumber * 160.0f, 1.0f);

        for (int i = 0; i < listNumber; i++)
        {
            TextAsset infoTextAsset = Resources.Load<TextAsset>("Data/Information/" + listData[i].ToString());
            JsonData infoData = JsonMapper.ToObject(infoTextAsset.text);

            GameObject listPrefab = Resources.Load<GameObject>("Prefabs/List");
            GameObject listObject = Instantiate<GameObject>(listPrefab);

            List list = listObject.GetComponent<List>();
            list.Info = infoData;
            list.Cover = Resources.Load<Sprite>("Images/List/Cover/" + infoData["List_Cover"].ToString());
            list.Name = infoData["Name"].ToString();
            list.Singer = infoData["Singer"].ToString();
            list.Level = "LEVEL " + infoData["Note"][0]["Level"].ToString();
            list.ButtonListener = () => scriptListUIButton.MusicInformationClick(list);

            float scaleY = listPanel.localScale.y;
            float positionY = (-80.0f - (160.0f * i)) / scaleY;

            listObject.transform.SetParent(listPanel);
            listObject.transform.localPosition = new Vector3(0.0f, positionY, 0.0f);
            listObject.transform.localScale = new Vector3(1.0f, 1.0f / scaleY, 1.0f);
        }
    }
}
