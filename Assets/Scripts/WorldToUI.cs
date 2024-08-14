using UnityEngine;

namespace Mtaka
{
    /// <summary>
    /// 傷害值管理器
    /// </summary>
    public class WorldToUI : MonoBehaviour
    {
        public float offset;
        public Transform targetPoint;

        [SerializeField, Header("是否縮放")]
        private bool canScale;
        [SerializeField, Header("縮放數值"), Range(0, 20)]
        private float scaleValue = 5;

        private RectTransform rectTransform;
        private Canvas canvas;
        private RectTransform rectCanvas;
        private Camera cameraCanvas;
        private Transform cameraPoint;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            canvas = transform.root.GetComponent<Canvas>();
            rectCanvas = canvas.GetComponent<RectTransform>();
            cameraCanvas = canvas.worldCamera;
            cameraPoint = Camera.main.transform;
        }

        private void Update()
        {
            SwitchPoint();
            Scale();
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

        private void Scale()
        {
            if (!canScale) return; 
            float distance = Vector3.Distance(targetPoint.position, cameraPoint.position);
            print($"<color=#3f3>與攝影機的距離：{distance}</color>");
            rectTransform.localScale = Vector2.one * (scaleValue / distance);
        }
    }
}