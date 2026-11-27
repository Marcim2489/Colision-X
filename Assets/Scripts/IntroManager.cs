using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    [SerializeField]Image logo;
    [SerializeField]float delta = 0.08f;
    [SerializeField]Vector3 targetScale = new Vector3(0.5f, 0.5f, 0.5f);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(IntroCoroutine());
        StartCoroutine(CarefulCoroutine());
    }

    void Update()
    {
        logo.transform.localScale = Vector3.Lerp(logo.transform.localScale, targetScale, delta/40f);
    }

    IEnumerator IntroCoroutine()
    {
        while (logo.color.a < 1)
        {
            logo.color += new Color(0f,0f,0f, delta);
            if (logo.color.a > 1)
            {
                logo.color = new Color(1f,1f,1f, 1f);
            }
            yield return null;
        }
        while (logo.color.a > 0)
        {
            logo.color -= new Color(0f,0f,0f, delta/1.5f);
            if (logo.color.a < 0)
            {
                logo.color = new Color(1f,1f,1f, 0f);
            }
            yield return null;
        }

        SceneManager.LoadScene("Main");
    }

    IEnumerator CarefulCoroutine()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Main");
    }
}
