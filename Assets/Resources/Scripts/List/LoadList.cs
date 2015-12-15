using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using LitJson;

public class LoadList : MonoBehaviour {

    public Transform listPanel;

    void Start()
    {
        TextAsset listTextAsset = Resources.Load<TextAsset>("Data/Information/List");
        JsonData listData = JsonMapper.ToObject(listTextAsset.text);

        int listNumber = listData.Count;

        for (int i = 0; i < listNumber; i++)
        {
            TextAsset infoTextAsset = Resources.Load<TextAsset>("Data/Information/" + listData[i].ToString());
            JsonData infoData = JsonMapper.ToObject(infoTextAsset.text);

            GameObject listPrefab = Resources.Load<GameObject>("Prefabs/List");
            GameObject listObject = Instantiate<GameObject>(listPrefab);

            List list = listObject.GetComponent<List>();
            list.Cover = Resources.Load<Sprite>("Images/List/Cover/" + infoData["List_Cover"].ToString());
            list.Name = infoData["Name"].ToString();
            list.Singer = infoData["Singer"].ToString();
            list.Level = "LEVEL " + infoData["Note"][0]["Level"].ToString();

            listObject.transform.SetParent(listPanel);
            listObject.transform.localPosition = new Vector3(0.0f, 401.0f - (160.0f * i), 0.0f);
            listObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
    }
}
