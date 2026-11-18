using UnityEngine;

public class OpenMsgsButton : MonoBehaviour
{
    public void OpenFrame(GameObject FrameTarget)
    {
        FrameTarget.SetActive(!FrameTarget.activeInHierarchy);
    }
}
