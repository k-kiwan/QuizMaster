using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int correctAnswers = 0;
    int questionsSeen = 0;
    int questionsAnswered = 0;
    int questionsSkipped = 0;

    public int GetCorrectAnswers()
    {
        return correctAnswers;
    }

    public void IncrementCorrectAnswers()
    {
        correctAnswers++;
    }

    public int GetQuestionsSeen()
    {
        return questionsSeen;
    }

    public void IncrementQuestionsSeen()
    {
        questionsSeen++;
    }

    public void DecrementQuestionsSeen()
    {
        questionsSeen--;
    }

    public void IncrementQuestionsAnswered()
    {
        questionsAnswered++;
    }

    public void IncrementQuestionsSkipped()
    {
        questionsSkipped++;
    }

    public int QuestionsSeenCount()
    {
        return questionsSeen;
    }

    public int QuestionsAnsweredCount()
    {
        return questionsAnswered;
    }

    public int QuestionsSkippedCount()
    {
        return questionsSkipped;
    }

    public int CalculateScore()
    {
        return Mathf.RoundToInt(correctAnswers / (float)questionsAnswered * 100);
    }
}
