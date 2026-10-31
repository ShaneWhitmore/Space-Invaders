using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
    public static RoundManager instance { get; private set; }


    [Header("Text Fields")]
    [SerializeField] private TextMeshProUGUI roundTextField;
    [SerializeField] private TextMeshProUGUI gameOverTextField;
    [SerializeField] private TextMeshProUGUI livesTextField;




    [SerializeField] private int round = 0;
    [SerializeField] private int current_Lives = 3;


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
        roundTextField.text = "Round: " + round.ToString();
        gameOverTextField.text = null;
        livesTextField.text = "Lives: " + current_Lives.ToString();

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


    public void GameOver()
    {

        gameOverTextField.text = "Game Over";

    }
}
