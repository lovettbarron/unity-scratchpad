using UnityEngine.SceneManagement;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    AudioSource aud;
    [SerializeField] ParticleSystem successParticle;
    [SerializeField] ParticleSystem crashParticle;
    [SerializeField] AudioClip landSound;
    [SerializeField] AudioClip dieSound;
    bool isTransitioning = false;
    bool isGodMode = false;

    void Start()
    {
        aud = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.C))
        {
            isGodMode = !isGodMode;
        }
        if (Input.GetKey(KeyCode.L))
        {
            LoadNextLevel();
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning) return;
        if (!isGodMode) CheckHit(other);
    }

    private void CheckHit(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Hit Friendly");
                break;
            case "Debris":
                StartCrashSequence();
                break;
            case "Ground":
                StartSuccessSequence(other);
                break;

            default:
                Debug.Log("Ow");
                break;

        }
    }

    void StartSuccessSequence(Collision other)
    {
        isTransitioning = true;
        Debug.Log("Ground Velocity:" + other.relativeVelocity.magnitude);
        successParticle.Play();
        aud.Stop();
        aud.PlayOneShot(landSound);
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", landSound.length);
    }

    void StartCrashSequence()
    {
        Debug.Log("Damage");
        isTransitioning = true;
        crashParticle.Play();
        aud.Stop();
        aud.PlayOneShot(dieSound);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", dieSound.length);
    }

    void LoadNextLevel()
    {
        isTransitioning = true;
        int totalLevels = SceneManager.sceneCountInBuildSettings;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene((currentSceneIndex + 1) % totalLevels);
    }
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
