using UnityEngine;
using UnityEngine.UI;

public class LevelProgressBar : MonoBehaviour
{
    [SerializeField] private Transform _car;
    [SerializeField] private RectTransform _carIcon;
    [SerializeField] private float _levelStart = 0f;
    [SerializeField] private float _levelEnd = 1000f;

    private float _barWidth;

    private void Start()
    {
        _barWidth = GetComponent<RectTransform>().rect.width;
    }

    private void Update()
    {
        float progress = Mathf.InverseLerp(_levelStart, _levelEnd, _car.position.x);
        _carIcon.anchoredPosition = new Vector2(progress * _barWidth, _carIcon.anchoredPosition.y);
    }
}