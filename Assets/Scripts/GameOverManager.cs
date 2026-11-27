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
        bool highScoreAchieved = ScoreManager.Instance.CheckNewRecord();
        newRecordText.gameObject.SetActive(highScoreAchieved);
        finalScoreText.text = $"Final score: {ScoreManager.Instance.CurrentScore}";
        highScoreText.text = $"Highest score: {ScoreManager.Instance.HighestScore}";
        if (highScoreAchieved)
        {
            SFXManager.Instance.PlayAudio(SFXManager.Instance.GameOverHappy, Vector3.zero, 0f, 1f);
        }
        else
        {
            SFXManager.Instance.PlayAudio(SFXManager.Instance.GameOverSad, Vector3.zero, 0f, 0.9f);
        }
    }

    public void TryAgain()
    {
        SFXManager.Instance.PlayAudio(SFXManager.Instance.ButtonPress, Vector3.zero, 0f, 0.7f, 1f, true);
        ScoreManager.Instance.Reset();
        SceneManager.LoadScene("Main");
    }
}
