using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    [SerializeField]Text newRecordText;
    [SerializeField]Text finalScoreText;
    [SerializeField]Text highScoreText;

    void Start()
    {
        newRecordText.gameObject.SetActive(ScoreManager.Instance.CheckNewRecord());
        finalScoreText.text = $"Final score: {ScoreManager.Instance.CurrentScore}";
        highScoreText.text = $"Highest score: {ScoreManager.Instance.HighestScore}";
    }

    public void TryAgain()
    {
        ScoreManager.Instance.Reset();
        SceneManager.LoadScene("Main");
    }
}
