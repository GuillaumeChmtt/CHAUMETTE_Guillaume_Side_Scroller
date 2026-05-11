using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;
    private float _elapsed;
    private bool _running = true;

    public float Elapsed => _elapsed;

    public void StopTimer() => _running = false;

    private void Update()
    {
        if (!_running) return;
        _elapsed += Time.deltaTime;
        int minutes = Mathf.FloorToInt(_elapsed / 60f);
        int seconds = Mathf.FloorToInt(_elapsed % 60f);
        int centiseconds = Mathf.FloorToInt((_elapsed * 100f) % 100f);
        _timerText.text = $"{minutes:00}:{seconds:00}:{centiseconds:00}";
    }
}