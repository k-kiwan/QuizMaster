using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO currentQuestion;


    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;
    bool hasAnsweredEarly = true;


    [Header("Buttons")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;
    [SerializeField] Sprite wrongAnswerSprite;


    [Header("Timer")] 
    [SerializeField] Image timerImage;
    Timer timer;


    [Header("Scoring")] 
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;


    [Header("Progress Bar")]
    [SerializeField] Slider progressBar;

    [Header("Question Count")] 
    [SerializeField] TextMeshProUGUI questionCountText;

    public bool isComplete;


    
    void Awake()
    {
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        progressBar.maxValue = questions.Count;
        progressBar.value = 0;
    }

    void Update()
    {
        timerImage.fillAmount = timer.fillFraction;

        if(timer.loadNextQuestion)
        {
            if(progressBar.value == progressBar.maxValue)
            {
                isComplete = true;
                return;
            }
            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }
        else if (!hasAnsweredEarly && !timer.isAnsweringQuestion)
        {
            DisplayAnswer(-1); // -1 as index, because this is definitely not the correct answer, therefore else block evoked.
            SetButtonState(false);
        }

        questionCountText.text = "Questions seen: " + scoreKeeper.QuestionsSeenCount() 
                                + "\nQuestions anwered: " + scoreKeeper.QuestionsAnsweredCount() 
                                + "\nQuestions skipped: " + scoreKeeper.QuestionsSkippedCount()
                                + "\nQuestions remaining: " + questions.Count;
    }

    public void OnAnswerSelected(int index)
    {
        hasAnsweredEarly = true;
        DisplayAnswer(index);
        scoreKeeper.IncrementQuestionsAnswered();

        SetButtonState(false);
        timer.CancelTimer();

        scoreText.text = "Score: " + scoreKeeper.CalculateScore() + "%";
    }

    public void NextQuestionButton()
    {
        if(questions.Count > 0)
        {
            GetNextQuestion();
            scoreKeeper.IncrementQuestionsSkipped();
        }
        else 
        {
            scoreKeeper.IncrementQuestionsSkipped();
            isComplete = true;
            return;
        }
    }

    public void GetNextQuestion()
    {   
        if(questions.Count > 0)
        {
            SetButtonState(true);
            setDefaultButtionSprites();
            GetRandomQuestion();
            DisplayQuestions();
            progressBar.value++;
            scoreKeeper.IncrementQuestionsSeen();
        }
        
    }

    public void EndGameButton()
    {
        isComplete = true;
        return;
    }

    void GetRandomQuestion()
    {
        int index = Random.Range(0, questions.Count);
        currentQuestion = questions[index];

        if(questions.Contains(currentQuestion))
        {
            questions.Remove(currentQuestion);
        }
        
    }


    void DisplayAnswer(int index)
    {
        Image correctImageButton;
        Image wrongImageButton;

        if (index == currentQuestion.GetCorrectAnswerIndex())
        {
            questionText.text = "Correct!!";
            correctImageButton = answerButtons[index].GetComponent<Image>();
            correctImageButton.sprite = correctAnswerSprite;
            scoreKeeper.IncrementCorrectAnswers();
        }
        else
        {
            correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
            string correctAnswer = currentQuestion.GetAnswer(correctAnswerIndex);
            questionText.text = "Wrong! The correct answer is: \n" + correctAnswer;

            correctImageButton = answerButtons[correctAnswerIndex].GetComponent<Image>();
            correctImageButton.sprite = correctAnswerSprite;
            wrongImageButton = answerButtons[index].GetComponent<Image>();
            wrongImageButton.sprite = wrongAnswerSprite;
        }
    }

    void DisplayQuestions()
    {
        questionText.text = currentQuestion.GetQuestion();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswer(i);
        }
    }



    void SetButtonState(bool state)
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    void setDefaultButtionSprites()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Image imageButton = answerButtons[i].GetComponent<Image>();
            imageButton.sprite = defaultAnswerSprite;
        }
    }
}
