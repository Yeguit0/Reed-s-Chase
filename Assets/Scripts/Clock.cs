
using UnityEngine;
using TMPro;

public class Clock : MonoBehaviour
{
    [Header("Reloj")]
    [SerializeField] private TMP_Text clockText;

    [Header("Configuracion")]
    [SerializeField] private int startHour = 16;
    [SerializeField] private int endHour = 22;
    [SerializeField] private float durationMinutes = 15f;

    [Header("Eventos")]
    [SerializeField] private GameObject objectToActivate;

    private float elapsedTime;
    private float totalGameMinutes;
    private bool clockRunning = true;
    private bool objectActivated = false;

    void Start()
    {
        totalGameMinutes = (endHour - startHour) * 60f;

        UpdateClock(startHour, 0);
    }

    void Update()
    {
        if (!clockRunning) return;

        elapsedTime += Time.deltaTime;

        float progress = elapsedTime / (durationMinutes * 60f);
        progress = Mathf.Clamp01(progress);

        float currentMinutes = startHour * 60f +
                               totalGameMinutes * progress;

        int hours = Mathf.FloorToInt(currentMinutes / 60f);
        int minutes = Mathf.FloorToInt(currentMinutes % 60f);

        UpdateClock(hours, minutes);

        if (!objectActivated && currentMinutes >= 18 * 60)
        {
            objectToActivate.SetActive(true);
            objectActivated = true;
        }

        if (progress >= 1f)
        {
            clockRunning = false;
        }
    }

    void UpdateClock(int hours, int minutes)
    {
        clockText.text = $"{hours:00}:{minutes:00}";
    }
}
