using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ThunderAnimations : MonoBehaviour
{
    [SerializeField] Light2D Thunderlight;

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
        ChangeIntensity(2.5f, 0.15f);
        yield return new WaitForSeconds(0.25f);
        ChangeIntensity(0f, 0.15f);
        yield return new WaitForSeconds(0.1f);
    }

    private void Start()
    {
        StartCoroutine(FirstThunderAnimation());
    }
}

