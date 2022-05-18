using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonInterfacer : MonoBehaviour
{
    public static string filePath = "/scores.json";

    [System.Serializable]
    public struct Scores
    {
        public List<NamedScore> namedScores;
    }

    [System.Serializable]
    public struct NamedScore
    {
        public string name;
        public float score;
    }

    public static void AddScore(string name, int score)
    {
        List<NamedScore> namedScores = LoadScores().namedScores; //load scores
        namedScores.Add(new NamedScore { name = name, score = score }); //add new score
        namedScores.Sort((NamedScore a, NamedScore b) => -a.score.CompareTo(b.score)); //sort scores
        SaveScores(new Scores { namedScores = namedScores }); //save scores
    }
    public static List<NamedScore> GetSortedScores()
    {
        Scores scores = LoadScores();
        scores.namedScores.Sort((NamedScore a, NamedScore b) => -a.score.CompareTo(b.score));
        return scores.namedScores;
    }

    public static Scores LoadScores()
    {
        Scores scores;
        try {
            string input = File.ReadAllText(Application.dataPath + filePath);
            scores = JsonUtility.FromJson<Scores>(input);
        }
        catch { //file doesn't exist
            scores = new Scores { namedScores = new() };
        }
        return scores;
    }

    static void SaveScores(Scores scores)
    {
        string output = JsonUtility.ToJson(scores);
        File.WriteAllText(Application.dataPath + filePath, output);
    }
}
