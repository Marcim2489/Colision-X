using UnityEngine;
using UnityEngine.UI;

public class ComboDisplay : MonoBehaviour
{
    [SerializeField]Text display;
    [SerializeField]RectTransform timeBar;
    float initialTimeBarWidth;
    ScoreManager scoreManager;

    void Start()
    {
        scoreManager = ScoreManager.Instance;
        initialTimeBarWidth = timeBar.rect.width;
        ChangeTimeBarSize();
        ChangeDisplay(scoreManager.CurrentCombo);
        scoreManager.currentComboChanged += ChangeDisplay;
    }

    void Update()
    {
        ChangeTimeBarSize();
    }

    void ChangeTimeBarSize()
    {
        timeBar.sizeDelta = new Vector2((scoreManager.ComboTimer/scoreManager.ComboWaitTime) * initialTimeBarWidth, timeBar.rect.height);
    }

    void ChangeDisplay(int currentCombo)
    {
        if (currentCombo == 0)
        {
            display.text = "";
        }
        else
        {
            display.text = $"{1+currentCombo/10f}X";
        }
    }

    void OnDestroy()
    {
        scoreManager.currentComboChanged -= ChangeDisplay;
    }
}