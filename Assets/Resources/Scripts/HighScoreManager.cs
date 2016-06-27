using UnityEngine;
using System.Collections;

using System.Collections.Generic;
using System.IO;

using LitJson;

public class HighScoreManager {

    private static readonly HighScoreManager m_instance = new HighScoreManager();

    private Dictionary<string, int> m_highScore = new Dictionary<string, int>();

    public static HighScoreManager Instance
    {
        get
        {
            return m_instance;
        }
    }

    private HighScoreManager()
    {
    }

    ~HighScoreManager()
    {
    }

    public void SaveHighScore()
    {
        string path = pathForDocumentsFile("highscore.json");

        FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write);
        StreamWriter writer = new StreamWriter(file);

        JsonWriter jsonWriter = new JsonWriter(writer);
        jsonWriter.PrettyPrint = true;

        jsonWriter.WriteArrayStart();

        foreach (KeyValuePair<string, int> items in m_highScore)
        {
            jsonWriter.WriteObjectStart();

            jsonWriter.WritePropertyName("Name");
            jsonWriter.Write(items.Key);
            jsonWriter.WritePropertyName("Score");
            jsonWriter.Write(items.Value);

            jsonWriter.WriteObjectEnd();
        }

        jsonWriter.WriteArrayEnd();

        writer.Close();
        file.Close();
    }

    public void LoadHighScore()
    {
        string path = pathForDocumentsFile("highscore.json");

        FileStream file = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read);
        StreamReader reader = new StreamReader(file);

        JsonReader jsonReader = new JsonReader(reader);
        JsonData jsonData = JsonMapper.ToObject(jsonReader);

        if (jsonData.IsArray)
        {
            for (int i = 0; i < jsonData.Count; i++)
            {
                JsonData jsonScore = jsonData[i];
                string name = jsonScore["Name"].ToString();
                int score = (int)jsonScore["Score"];

                m_highScore[name] = score;
            }
        }

        reader.Close();
        file.Close();
    }

    public void SetHighScore(string name, string difficulty, int score)
    {
        string key = name + "_" + difficulty;

        m_highScore[key] = score;
    }

    public int GetHighScore(string name, string difficulty)
    {
        string key = name + "_" + difficulty;

        if (m_highScore.ContainsKey(key))
            return m_highScore[key];

        return 0;
    }

    private string pathForDocumentsFile(string filename)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            string path = Application.dataPath.Substring(0, Application.dataPath.Length - 5);
            path = path.Substring(0, path.LastIndexOf('/'));
            return Path.Combine(Path.Combine(path, "Documents"), filename);
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            string path = Application.persistentDataPath;
            path = path.Substring(0, path.LastIndexOf('/'));
            return Path.Combine(path, filename);
        }
        else
        {
            string path = Application.dataPath;
            path = path.Substring(0, path.LastIndexOf('/'));
            return Path.Combine(path, filename);
        }
    }
}