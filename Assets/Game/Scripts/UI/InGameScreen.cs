using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGameScreen : UIBase
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField ]private int score = 0;


    public void SetScore(int amount)
    {
        score += amount;
        scoreText.SetText(score.ToString());
    }

   //public void MultiplyScore(int multiplier)
   //{
   //    score *= multiplier;
   //    scoreText.SetText(score.ToString());
   //}

    public override void DisablePanel()
    {
        base.DisablePanel();
        score = 0;
    }


}
