using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField]Text display;

    void Start()
    {
        ChangeDisplay(ScoreManager.Instance.CurrentScore);
        ScoreManager.Instance.scoreChanged += ChangeDisplay;
    }

    void ChangeDisplay(int currentScore)
    {
        display.text = currentScore.ToString();
    }

    void OnDestroy()
    {
        ScoreManager.Instance.scoreChanged -= ChangeDisplay;
    }
}