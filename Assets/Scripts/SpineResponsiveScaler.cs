using UnityEngine;

[ExecuteInEditMode] // Позволяет видеть магию прямо в редакторе без запуска игры!
public class SpineResponsiveScaler : MonoBehaviour
{
    [Header("UI References")]
    [Tooltip("Перетащите сюда ваш Base_Art")]
    public RectTransform referenceBackground; 

    [Header("Settings")]
    [Tooltip("Оригинальная ширина Base_Art (как в PSD)")]
    public float originalArtWidth = 1080f; 

    void LateUpdate()
    {
        if (referenceBackground != null)
        {
            // Вычисляем, во сколько раз фон стал больше или меньше оригинала
            float ratio = referenceBackground.rect.width / originalArtWidth;
            
            // Применяем этот коэффициент к масштабу Spine
            transform.localScale = new Vector3(ratio, ratio, 1f);
        }
    }
}