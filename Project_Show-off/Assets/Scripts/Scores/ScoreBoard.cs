using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] TMP_InputField nameField;
    [SerializeField] TMP_Text scoreLabel;
    [SerializeField] RectTransform prefab;
    [SerializeField] Vector2 offset;

    string winnerName;

    public void BuildBoard()
    {
        SetWinnerName();
        scoreLabel.text = $"Score: {FormatTime(ScoreManager.instance.GetScore(GameplayManager.instance.winner))}";
        //create list
        List<JsonInterfacer.NamedScore> scores = JsonInterfacer.GetSortedScores();
        for (int i = 0; i < Mathf.Min(5, scores.Count); i++) {
            RectTransform rt = CreateLabel(offset * i);
            SetupLabel(rt.GetComponent<ScoreLabel>(), scores[i]);
        }
    }
    void SetWinnerName()
    {
        winnerName = GameplayManager.instance.winner.name;
        nameField.text = winnerName;
    }

    RectTransform CreateLabel(Vector2 offset)
    {
        RectTransform label = Instantiate(prefab, transform);
        label.anchoredPosition += offset;
        return label;
    }

    void SetupLabel(ScoreLabel label, JsonInterfacer.NamedScore namedScore)
    {
        label.nameLabel.text = $"{namedScore.name}:";
        label.scoreLabel.text = FormatTime(namedScore.score);
    }

    string FormatTime(float seconds)
    {
        string output = seconds.ToString();
        return output.Substring(0, output.IndexOf('.') + 3);
    }

    //-------------------------register name---------------
    public void SetScoreName(string s)
    {
        winnerName = s;
        StartCoroutine(SetNameCo());
    }
    IEnumerator SetNameCo()
    {
        yield return null;
        nameField.interactable = false;
    }

    public void SaveScore()
    {
        Player winner = GameplayManager.instance.winner;
        JsonInterfacer.AddScore(winnerName, ScoreManager.instance.playerScores[winner]);
    }
}
