using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButtonUI : MonoBehaviour
{
    [Header("Scene")]
    [SerializeField] string sceneName;

    [Header("Sound")]
    [SerializeField] AudioClip clickSound;
    [SerializeField] float clickVolume = 0.7f;

    Button button;
    AudioSource audioSource;

    void Start()
    {
        button = GetComponent<Button>();
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        if (button == null)
        {
            Debug.LogError("Button component missing on " + gameObject.name);
            return;
        }

        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        if (clickSound != null)
            audioSource.PlayOneShot(clickSound, clickVolume);

        StartCoroutine(LoadScene());
    }

    System.Collections.IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene(sceneName);
    }
}