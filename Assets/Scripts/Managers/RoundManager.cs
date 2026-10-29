using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
    public static RoundManager instance { get; private set; }


    [Header("Text Fields")]
    [SerializeField] private TextMeshProUGUI roundTextField;


    [SerializeField] private int round = 0;


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
}
