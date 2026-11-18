using UnityEngine;

public class OpenMsgsButton : MonoBehaviour
{
    private GameObject LastFrameOpened;

    public void OpenFrame(GameObject FrameTarget)
    {
        if (LastFrameOpened == FrameTarget)
        {
            FrameTarget.SetActive(false);
            LastFrameOpened = null;
            return;
        }

        if (LastFrameOpened != null)
            LastFrameOpened.SetActive(false);

        FrameTarget.SetActive(true);
        LastFrameOpened = FrameTarget;
    }
}