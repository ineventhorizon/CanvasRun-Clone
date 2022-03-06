using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGameScreen : UIBase
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField ]private int score = 0;


    public void SetScore(int amount)
    {
        score += amount;
        scoreText.SetText(score.ToString());
    }
    private void SetLevelText(int level)
    {
        levelText.SetText($"Level {level}");
    }
    public override void EnablePanel()
    {
        base.EnablePanel();
        SetLevelText(MySceneManager.Instance.CurrentLevel);
    }
    public override void DisablePanel()
    {
        base.DisablePanel();
        SetScore(-score);
    }


}
