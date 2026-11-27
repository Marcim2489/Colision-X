using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance {get; private set;}

    [SerializeField]AudioClip playerShoot;
    [SerializeField]AudioClip asteroidExplosion;
    [SerializeField]AudioClip shootingStarScream;
    [SerializeField]AudioClip shootingStarExplosion;
    [SerializeField]AudioClip genericExplosion;
    [SerializeField]AudioClip estrelaoExplosion;
    [SerializeField]AudioClip estrelaoShoot;
    [SerializeField]AudioClip playerExplosion;
    [SerializeField]AudioClip buttonPress;
    [SerializeField]AudioClip gameOverSad;
    [SerializeField]AudioClip gameOverHappy;

    public AudioClip PlayerShoot => playerShoot;
    public AudioClip AsteroidExplosion => asteroidExplosion;
    public AudioClip ShootingStarScream => shootingStarScream;
    public AudioClip ShootingStarExplosion => shootingStarExplosion;
    public AudioClip EstrelaoExplosion => estrelaoExplosion;
    public AudioClip GenericExplosion => genericExplosion;
    public AudioClip PlayerExplosion => playerExplosion;
    public AudioClip EstrelaoShoot => estrelaoShoot;
    public AudioClip ButtonPress => buttonPress;
    public AudioClip GameOverSad => gameOverSad;
    public AudioClip GameOverHappy => gameOverHappy;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(gameObject);
    }

    public void PlayAudio(AudioClip clip, Vector3 position, float spatialBlend = 0f, float volume = 1f, float pitch = 1f, bool sceneIndependent = false)
    {
        GameObject gameObject = new GameObject("One shot audio");
        gameObject.transform.position = position;
        AudioSource audioSource = (AudioSource)gameObject.AddComponent(typeof(AudioSource));
        audioSource.clip = clip;
        audioSource.spatialBlend = spatialBlend;
        audioSource.volume = volume;
        audioSource.pitch = pitch;
        audioSource.Play();
        if (sceneIndependent)
        {
            DontDestroyOnLoad(audioSource.gameObject);
        }
        Destroy(gameObject, clip.length * ((Time.timeScale < 0.01f) ? 0.01f : Time.timeScale));
    }
}
