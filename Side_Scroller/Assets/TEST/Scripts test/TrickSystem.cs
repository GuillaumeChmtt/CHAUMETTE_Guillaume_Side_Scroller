using UnityEngine;
using TMPro;
using System.Collections;

public class TrickSystem : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D _carRB;
    [SerializeField] private Transform _frontTire;
    [SerializeField] private Transform _backTire;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _trickText;

    [Header("Settings")]
    [SerializeField] private float _groundCheckRadius = 0.5f;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _wheelieDelay = 0.5f;

    private int _score;
    private float _combo = 1f;
    private float _comboTimer;
    private float _rotation;
    private float _lastRot;
    private float _airTime;
    private bool _wasInAir = false;
    private LayerMask _finalMask;

    private float _wheelieTimer = 0f;
    private float _frontWheelieTimer = 0f;
    private float _wheelieScoreTimer = 0f;
    private float _frontWheelieScoreTimer = 0f;

    private bool FrontGrounded =>
        Physics2D.OverlapCircle(_frontTire.position, _groundCheckRadius, _finalMask) != null;

    private bool BackGrounded =>
        Physics2D.OverlapCircle(_backTire.position, _groundCheckRadius, _finalMask) != null;

    private void Start()
    {
        _trickText.gameObject.SetActive(false);
        _finalMask = _groundLayer & ~LayerMask.GetMask("Car");
    }

    private void Update()
    {
        bool front = FrontGrounded;
        bool back = BackGrounded;
        bool inAir = !front && !back;

        // Big Air
        if (inAir)
        {
            _airTime += Time.deltaTime;
            if (_airTime >= 2f && (_airTime % 0.5f) < Time.deltaTime)
                AddTrick("BIG AIR", 20, 0.5f);
        }
        else
        {
            _airTime = 0f;
        }

        // Flip
        if (inAir)
        {
            float delta = Mathf.DeltaAngle(_lastRot, _carRB.rotation);
            _rotation += delta;
            if (_rotation <= -360f) { AddTrick("BACKFLIP", 150); _rotation = 0; }
            if (_rotation >= 360f) { AddTrick("FRONTFLIP", 160); _rotation = 0; }
        }
        else
        {
            _rotation = 0;
        }

        // Wheelie
        bool justLanded = _wasInAir && !inAir;
        if (!justLanded)
        {
            if (back && !front)
            {
                _wheelieTimer += Time.deltaTime;
                _frontWheelieTimer = 0f;

                if (_wheelieTimer >= _wheelieDelay)
                {
                    _wheelieScoreTimer += Time.deltaTime;
                    if (_wheelieScoreTimer >= 0.5f)
                    {
                        AddTrick("WHEELIE", 15, 0.5f);
                        _wheelieScoreTimer = 0f;
                    }
                }
            }
            else
            {
                _wheelieTimer = 0f;
                _wheelieScoreTimer = 0f;
            }

            if (front && !back)
            {
                _frontWheelieTimer += Time.deltaTime;
                _wheelieTimer = 0f;

                if (_frontWheelieTimer >= _wheelieDelay)
                {
                    _frontWheelieScoreTimer += Time.deltaTime;
                    if (_frontWheelieScoreTimer >= 0.5f)
                    {
                        AddTrick("FRONT WHEELIE", 15, 0.5f);
                        _frontWheelieScoreTimer = 0f;
                    }
                }
            }
            else
            {
                _frontWheelieTimer = 0f;
                _frontWheelieScoreTimer = 0f;
            }
        }

        // Combo timer
        _comboTimer -= Time.deltaTime;
        if (_comboTimer <= 0) _combo = 1f;

        _lastRot = _carRB.rotation;
        _wasInAir = inAir;
    }

    private void AddTrick(string name, int points, float displayDuration = 2f)
    {
        int earned = Mathf.RoundToInt(points * _combo);
        _score += earned;
        _combo = Mathf.Min(_combo + (points > 10 ? 0.5f : 0), 5f);
        _comboTimer = 3f;

        _scoreText.text = "Score: " + _score;

        if (points > 10)
        {
            StopAllCoroutines();
            StartCoroutine(ShowTrick(name + " +" + earned + (_combo > 1 ? "\nx" + _combo.ToString("0.0") : ""), displayDuration));
        }
    }

    private IEnumerator ShowTrick(string text, float duration)
    {
        _trickText.gameObject.SetActive(true);
        _trickText.text = text;
        yield return new WaitForSeconds(duration);
        _trickText.gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        if (_frontTire == null || _backTire == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_frontTire.position, _groundCheckRadius);
        Gizmos.DrawWireSphere(_backTire.position, _groundCheckRadius);
    }
}