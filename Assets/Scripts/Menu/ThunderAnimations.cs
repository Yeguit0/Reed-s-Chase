using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ThunderAnimations : MonoBehaviour
{
    [SerializeField] Light2D Thunderlight;

    [SerializeField] AudioSource Thunder1Sound;
    [SerializeField] AudioSource Thunder2Sound;

    private bool waitingforthunder = false;
    private IEnumerator IntensityTween(float TargetIntensity, float AnimationDuration)
    {
        float start = Thunderlight.intensity;
        float elapsed = 0f;
        while (elapsed < AnimationDuration)
        {
            elapsed += Time.deltaTime;

            float t = Mathf.Clamp01(elapsed / AnimationDuration);
            t = Mathf.SmoothStep(0f, 1f, t);

            Thunderlight.intensity = Mathf.Lerp(start, TargetIntensity, t);

            yield return null;
        }

        Thunderlight.intensity = TargetIntensity;
    }

    private void ChangeIntensity(float target, float duration)
    {
        StartCoroutine(IntensityTween(target, duration));
    }

    private IEnumerator FirstThunderAnimation()
    {
        yield return new WaitForSeconds(1f);
        ChangeIntensity(2.5f, 0.05f);
        yield return new WaitForSeconds(0.2f);
        Thunder1Sound.Play();
        ChangeIntensity(0f, 0.15f);
        yield return new WaitForSeconds(0.1f);
    }

    private IEnumerator SecondThunderAnimation()
    {
        yield return new WaitForSeconds(0.2f);
        ChangeIntensity(2.5f, 0.05f);
        yield return new WaitForSeconds(0.15f);
        ChangeIntensity(0f, 0.05f);
        yield return new WaitForSeconds(0.25f);
        ChangeIntensity(2.5f, 0.05f);
        yield return new WaitForSeconds(0.2f);
        Thunder2Sound.Play();
        ChangeIntensity(0f, 0.15f);
        yield return new WaitForSeconds(0.1f);
    }

    private IEnumerator SelectThunder()
    {
        waitingforthunder = true;
        int RandomSelected = Random.Range(1, 3);
        if (RandomSelected == 1)
        {
            StartCoroutine(FirstThunderAnimation());
        }
        else
        {
            StartCoroutine(SecondThunderAnimation());
        }
        yield return new WaitForSeconds(Random.Range(35f, 120f));
        waitingforthunder = false;
    }
    private void Update()
    {
        if (!waitingforthunder)
        {
            StartCoroutine(SelectThunder());
        }
    }
}

