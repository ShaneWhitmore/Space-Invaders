using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
    public static RoundManager instance { get; private set; }


    [Header("Text Fields")]
    [SerializeField] private TextMeshProUGUI roundTextField;
    [SerializeField] private TextMeshProUGUI gameOverTextField;
    [SerializeField] private TextMeshProUGUI retryTextField;

    [SerializeField] private TextMeshProUGUI livesTextField;
    [SerializeField] private TextMeshProUGUI pointsTextField;





    [SerializeField] private int round = 0;
    [SerializeField] private int current_Lives = 4;
    [SerializeField] private int current_Points = 0;




    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        current_Lives = GameManager.instance.ReturnPlayerLivesCount();
        roundTextField.text = "Round: " + round.ToString();
        gameOverTextField.text = null;
        livesTextField.text = "Lives: " + current_Lives.ToString();
        retryTextField.text = null;
        pointsTextField.text = "Points: " + current_Points.ToString();

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void ChangeRound(int new_round)
    {
        round = new_round;
        roundTextField.text = "Round: " + round.ToString();
    }

    public void ChangeLivesCounter(int lives)
    {
        current_Lives = lives;
        livesTextField.text = "Lives: " + current_Lives.ToString();

    }

    public void ChangePoints(int points)
    {
        current_Points = points;
        pointsTextField.text = "Points: " + current_Points.ToString();
    }


    public void GameOver(bool isGameOver)
    {
        if (isGameOver)
        {
            gameOverTextField.text = "Game Over";
            retryTextField.text = "Press 'Space Bar' to retry";
        }
        else
        {
            gameOverTextField.text = null;
            retryTextField.text = null;
        }
    }
}
