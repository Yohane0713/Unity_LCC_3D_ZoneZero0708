using UnityEngine;

namespace Mtaka
{
    /// <summary>
    /// 傷害值管理器
    /// </summary>
    public class DamageManager : MonoBehaviour
    {
        public float offset;
        public Transform targetPoint;

        private RectTransform rectTransform;
        private Canvas canvas;
        private RectTransform rectCanvas;
        private Camera cameraCanvas;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            canvas = transform.root.GetComponent<Canvas>();
            rectCanvas = canvas.GetComponent<RectTransform>();
            cameraCanvas = canvas.worldCamera;
        }

        private void Update()
        {
            SwitchPoint();
        }

        private void SwitchPoint()
        {
            print($"<color=#77f>物件的座標：{targetPoint.position}</color>");
            // 3D 轉為 2D座標
            // ViewportPoint 左下角為原點 (0, 0) 右上角為最大值 (1, 1)
            // Vector3 print2D = Camera.main.WorldToViewportPoint(targetPoint.position);
            // ScreenPoint 左下角為原點 (0, 0) 右上角為最大值 (螢幕當前最大寬度, 螢幕當前最大高度)
            Vector3 point2D = Camera.main.WorldToScreenPoint(targetPoint.position);
            // print($"<color=#77f>物件的座標：{point2D}</color>");

            // 介面的原點為正中央，右上角為最大值 (螢幕當前最大寬度/2, 螢幕當前最大高度/2)
            // 介面的原點為正中央，左下角為最大值 (-螢幕當前最大寬度/2, -螢幕當前最大高度/2)

            // 螢幕座標轉為介面座標(畫布的變形，螢幕座標，畫布的攝影機，轉換後的介面座標)
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rectCanvas, point2D ,cameraCanvas, out Vector2 pointUI);
            rectTransform.anchoredPosition = pointUI;
            pointUI.y += offset;
            rectTransform.anchoredPosition = pointUI;
        }
    }
}