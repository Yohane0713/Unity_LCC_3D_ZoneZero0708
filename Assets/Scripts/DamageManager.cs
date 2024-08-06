using UnityEngine;

namespace Mtaka
{
    /// <summary>
    /// �ˮ`�Ⱥ޲z��
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
            print($"<color=#77f>���󪺮y�СG{targetPoint.position}</color>");
            // 3D �ର 2D�y��
            // ViewportPoint ���U�������I (0, 0) �k�W�����̤j�� (1, 1)
            // Vector3 print2D = Camera.main.WorldToViewportPoint(targetPoint.position);
            // ScreenPoint ���U�������I (0, 0) �k�W�����̤j�� (�ù���e�̤j�e��, �ù���e�̤j����)
            Vector3 point2D = Camera.main.WorldToScreenPoint(targetPoint.position);
            // print($"<color=#77f>���󪺮y�СG{point2D}</color>");

            // ���������I���������A�k�W�����̤j�� (�ù���e�̤j�e��/2, �ù���e�̤j����/2)
            // ���������I���������A���U�����̤j�� (-�ù���e�̤j�e��/2, -�ù���e�̤j����/2)

            // �ù��y���ର�����y��(�e�����ܧΡA�ù��y�СA�e������v���A�ഫ�᪺�����y��)
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rectCanvas, point2D ,cameraCanvas, out Vector2 pointUI);
            rectTransform.anchoredPosition = pointUI;
            pointUI.y += offset;
            rectTransform.anchoredPosition = pointUI;
        }
    }
}