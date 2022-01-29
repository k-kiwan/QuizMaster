using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalScoreText;
    [SerializeField] TextMeshProUGUI finalCountsText;
    ScoreKeeper scoreKeeper;
    
    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    public void ShowFinalScore()
    {
        if (scoreKeeper.CalculateScore() < 30)
        {
            finalScoreText.text =   "You got a score of " + 
                                scoreKeeper.CalculateScore() + "%\nKeep on practicing!";
        }
        else if (scoreKeeper.CalculateScore() < 50)
        {
            finalScoreText.text =   "You got a score of " + 
                                scoreKeeper.CalculateScore() + "%\nYou can do better!";
        }
        else if (scoreKeeper.CalculateScore() < 80)
        {
            finalScoreText.text =   "You got a score of " + 
                                scoreKeeper.CalculateScore() + "%\nGreat result, congratulations!";
        }
        else if (scoreKeeper.CalculateScore() < 99)
        {
            finalScoreText.text =   "You got a score of " + 
                                scoreKeeper.CalculateScore() + "%\nAwesome!";
        } 
        else 
        {
            finalScoreText.text =   "You got a score of " + 
                                scoreKeeper.CalculateScore() + "%\nRockstar!";
        }
        
    }

    public void ShowFinalCounts()
    {
        finalCountsText.text = "blah";
    }

}
