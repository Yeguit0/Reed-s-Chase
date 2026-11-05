
using UnityEngine;
using UnityEngine.UI;

public class ShaderManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private RectTransform mainPanel;
    [SerializeField] private RectTransform currentDragObject;

    [Header("Mask Material")]
    [SerializeField] private Material maskMaterial;

    private static readonly int HoleRect =
        Shader.PropertyToID("_HoleRect");

    private static readonly int HoleEnabled =
        Shader.PropertyToID("_HoleEnabled");

    private readonly Vector3[] panelCorners = new Vector3[4];
    private readonly Vector3[] dragCorners = new Vector3[4];

    private Canvas canvas;

    private void Awake()
    {
        canvas = mainPanel.GetComponentInParent<Canvas>();

        if (maskMaterial != null)
            maskMaterial.SetFloat(HoleEnabled, 0);
    }

    private void LateUpdate()
    {
        UpdateDragMask();
    }

    public void SetCurrentDragObject(RectTransform dragObject)
    {
        currentDragObject = dragObject;
    }

    public void ClearCurrentDragObject()
    {
        currentDragObject = null;

        if (maskMaterial != null)
            maskMaterial.SetFloat(HoleEnabled, 0);
    }

    private void UpdateDragMask()
    {
        if (mainPanel == null ||
            currentDragObject == null ||
            maskMaterial == null)
        {
            maskMaterial.SetFloat(HoleEnabled, 0);
            return;
        }

        Camera cam = canvas.renderMode == RenderMode.ScreenSpaceOverlay
            ? null
            : canvas.worldCamera;

        Rect panelRect = GetScreenRect(mainPanel, cam, panelCorners);
        Rect dragRect = GetScreenRect(currentDragObject, cam, dragCorners);

        float xMin = Mathf.Max(panelRect.xMin, dragRect.xMin);
        float yMin = Mathf.Max(panelRect.yMin, dragRect.yMin);
        float xMax = Mathf.Min(panelRect.xMax, dragRect.xMax);
        float yMax = Mathf.Min(panelRect.yMax, dragRect.yMax);

        if (xMin >= xMax || yMin >= yMax)
        {
            maskMaterial.SetFloat(HoleEnabled, 0);
            return;
        }

        Vector4 hole = new Vector4(
            xMin / Screen.width,
            yMin / Screen.height,
            xMax / Screen.width,
            yMax / Screen.height
        );

        maskMaterial.SetVector(HoleRect, hole);
        maskMaterial.SetFloat(HoleEnabled, 1);
    }

    private Rect GetScreenRect(
        RectTransform rect,
        Camera cam,
        Vector3[] corners)
    {
        rect.GetWorldCorners(corners);

        Vector2 min = new Vector2(float.MaxValue, float.MaxValue);
        Vector2 max = new Vector2(float.MinValue, float.MinValue);

        for (int i = 0; i < 4; i++)
        {
            Vector2 point = RectTransformUtility.WorldToScreenPoint(
                cam, corners[i]);

            min = Vector2.Min(min, point);
            max = Vector2.Max(max, point);
        }

        return Rect.MinMaxRect(min.x, min.y, max.x, max.y);
    }

    private void OnDisable()
    {
        if (maskMaterial != null)
            maskMaterial.SetFloat(HoleEnabled, 0);
    }
}
