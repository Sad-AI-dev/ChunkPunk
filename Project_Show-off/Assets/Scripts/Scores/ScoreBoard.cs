using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] RectTransform prefab;
    [SerializeField] Vector2 offset;

    public void BuildBoard()
    {
        List<JsonInterfacer.NamedScore> scores = JsonInterfacer.GetSortedScores();
        for (int i = 0; i < Mathf.Min(5, scores.Count); i++) {
            RectTransform rt = CreateLabel(offset * i);
            SetupLabel(rt.GetComponent<ScoreLabel>(), scores[i]);
        }
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
        label.scoreLabel.text = $"{namedScore.score}";
    }
}
