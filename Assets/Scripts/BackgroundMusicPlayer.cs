using System.Collections;
using UnityEngine;

public class BackgroundMusicPlayer : MonoBehaviour
{
    [SerializeField]AudioSource introSource;
    [SerializeField]AudioSource loopSource;

    void Start()
    {
        introSource.Play();
        loopSource.Stop();
        // introSource.
        StartCoroutine(WaitForLoopCoroutine());
    }

    IEnumerator WaitForLoopCoroutine()
    {
        yield return new WaitForSeconds(introSource.clip.length);
        // introSource.Stop();
        loopSource.Play();
    }

}