using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour
{
    [SerializeField]int initialHealth = 1;

    int currentHealth;

    public int CurrentHealth {
        get{return currentHealth;}
        private set
        {
            currentHealth = value;
            if (currentHealth <= 0 && alreadyDead == false)
            {
                currentHealth = 0;
                alreadyDead = true;
                died.Invoke();
            }
            healthChanged.Invoke(currentHealth);
        }
    }

    bool alreadyDead = false;
    public bool Dead => alreadyDead;
    public event UnityAction<int> healthChanged = delegate {};
    public event UnityAction died = delegate{};

    void Start()
    {
        SetHealth(initialHealth);
    }

    public void ChangeHealth(int amount)
    {
        if (alreadyDead)
        {
            return;
        }
        CurrentHealth += amount;
    }

    public void SetHealth(int newHealth)
    {
        if (alreadyDead)
        {
            return;
        }
        CurrentHealth = newHealth;
    }
}
