using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class FinishZone : MonoBehaviour
{
    [SerializeField] private GameObject _finishCanvas;
    [SerializeField] private LayerMask _carLayer;
    [SerializeField] private CameraChara _camera;
    [SerializeField] private float _delayBeforeMenu = 2.5f;
    [SerializeField] private Timer _timer;
    [SerializeField] private ScoreboardManager _scoreboard;

    private bool _finished = false;

    private void Start()
    {
        _finishCanvas.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_finished) return;

        if ((_carLayer.value & (1 << other.gameObject.layer)) != 0)
        {
            _finished = true;
            StartCoroutine(FinishSequence());
        }
    }

    private IEnumerator FinishSequence()
    {
        _camera.StopFollowing();
        _timer.StopTimer();
        CoinManager.Instance.SaveSessionCoins();
        yield return new WaitForSeconds(_delayBeforeMenu);
        _scoreboard.ShowScoreboard();
        _finishCanvas.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}