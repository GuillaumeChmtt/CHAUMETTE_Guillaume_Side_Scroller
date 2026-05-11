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

    private void Start()
    {
        _finishCanvas.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((_carLayer.value & (1 << other.gameObject.layer)) != 0)
        {
            StartCoroutine(FinishSequence());
        }
    }

    private IEnumerator FinishSequence()
    {
        Debug.Log("FinishSequence démarré");
        _camera.StopFollowing();
        _timer.StopTimer();
        Debug.Log("Timer arrêté");
        yield return new WaitForSeconds(_delayBeforeMenu);
        Debug.Log("Délai terminé, affichage scoreboard");
        _scoreboard.ShowScoreboard();
        _finishCanvas.SetActive(true);
        Time.timeScale = 0f;
        Debug.Log("TimeScale à 0");
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