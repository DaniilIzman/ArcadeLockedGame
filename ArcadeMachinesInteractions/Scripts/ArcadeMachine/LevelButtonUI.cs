using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LevelButtonUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Scene")]
    [SerializeField] string sceneName;

    [Header("Sound")]
    [SerializeField] AudioClip clickSound;
    [SerializeField] AudioClip hoverSound;
    [SerializeField] float clickVolume = 0.7f;
    [SerializeField] float hoverVolume = 0.5f;

    [Header("Hover Effect")]
    [SerializeField] float hoverScale = 1.15f;
    [SerializeField] float hoverAnimationDuration = 0.2f;
    [SerializeField] Color hoverColor = Color.yellow;

    [Header("Delay")]
    [SerializeField] float sceneLoadDelay = 1f;

    Button button;
    AudioSource audioSource;
    Image buttonImage;

    Vector3 originalScale;
    Color originalColor;

    Coroutine hoverCoroutine;

    void Start()
    {
        button = GetComponent<Button>();

        audioSource = GetComponent<AudioSource>();

        buttonImage = GetComponent<Image>();

        originalScale = transform.localScale;

        if (buttonImage != null)
        {
            originalColor = buttonImage.color;
        }

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        if (button == null)
        {
            Debug.LogError("Button component missing on " + gameObject.name);
            return;
        }

        button.onClick.AddListener(OnClick);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!button.interactable)
        {
            return;
        }

        if (hoverSound != null)
        {
            audioSource.PlayOneShot(hoverSound, hoverVolume);
        }

        if (hoverCoroutine != null)
        {
            StopCoroutine(hoverCoroutine);
        }

        hoverCoroutine = StartCoroutine(AnimateHover(true));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (hoverCoroutine != null)
        {
            StopCoroutine(hoverCoroutine);
        }

        hoverCoroutine = StartCoroutine(AnimateHover(false));
    }

    System.Collections.IEnumerator AnimateHover(bool isEntering)
    {
        float elapsedTime = 0f;

        Vector3 startScale = transform.localScale;
        Color startColor = buttonImage.color;

        Vector3 targetScale;

        if (isEntering)
        {
            targetScale = originalScale * hoverScale;
        }
        else
        {
            targetScale = originalScale;
        }

        Color targetColor;

        if (isEntering)
        {
            targetColor = hoverColor;
        }
        else
        {
            targetColor = originalColor;
        }

        while (elapsedTime < hoverAnimationDuration)
        {
            elapsedTime += Time.deltaTime;

            float progress = elapsedTime / hoverAnimationDuration;

            transform.localScale =
                Vector3.Lerp(startScale, targetScale, progress);

            if (buttonImage != null)
            {
                buttonImage.color =
                    Color.Lerp(startColor, targetColor, progress);
            }

            yield return null;
        }

        transform.localScale = targetScale;

        if (buttonImage != null)
        {
            buttonImage.color = targetColor;
        }
    }

    void OnClick()
    {
        button.interactable = false;

        Cursor.lockState = CursorLockMode.Locked;

        Cursor.visible = false;

        if (clickSound != null)
        {
            audioSource.PlayOneShot(clickSound, clickVolume);
        }

        StartCoroutine(LoadScene());
    }

    System.Collections.IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(sceneLoadDelay);

        SceneManager.LoadScene(sceneName);
    }
}