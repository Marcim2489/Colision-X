using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance {get; private set;}

    int currentScore;

    public int CurrentScore {
        get{return currentScore;}
        private set
        {
            currentScore = value;
            if (currentScore <= 0)
            {
                currentScore = 0;
            }
            scoreChanged.Invoke(currentScore);
        }
    }

    public event UnityAction<int> scoreChanged = delegate {};

    public int HighestScore {get; private set;}

    const string RECORD_VAR_NAME = "highest_score";

    int currentCombo = 0;

    public int CurrentCombo{
        get{return currentCombo;} 
        private set
        {
            currentCombo = value;
            currentComboChanged.Invoke(currentCombo);
        }
    }

    public event UnityAction<int> currentComboChanged = delegate {};

    float comboWaitTime = 2f;
    public float ComboWaitTime => comboWaitTime;

    float comboTimer;
    public float ComboTimer => comboTimer;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            HighestScore = PlayerPrefs.GetInt(RECORD_VAR_NAME);
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(gameObject);
    }

    void Update()
    {
        if (comboTimer > 0)
        {
            comboTimer -= Time.deltaTime;
            if (comboTimer <= 0)
            {
                comboTimer = 0;
                CurrentCombo = 0;
            }
        }
    }

    public void Reset()
    {
        CurrentScore = 0;
        CurrentCombo = 0;
        comboTimer = 0f;
    }

    public void AddScore(int amount)
    {
        // Debug.Log(amount);
        CurrentScore += (int)(amount * (1 + CurrentCombo/10f));
        CurrentCombo++;
        comboTimer = ComboWaitTime;
    }

    public bool CheckNewRecord()
    {
        if (CurrentScore > HighestScore)
        {
            PlayerPrefs.SetInt(RECORD_VAR_NAME, CurrentScore);
            HighestScore = CurrentScore;
            return true;
        }
        return false;
    }
}
