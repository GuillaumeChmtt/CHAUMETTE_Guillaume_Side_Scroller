using UnityEngine;
using UnityEngine.UI;

public class NitroSystem : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _frontTireRB;
    [SerializeField] private Rigidbody2D _backTireRB;
    [SerializeField] private float _nitroBoost = 200f;
    [SerializeField] private Slider _nitroSlider;

    private float _nitro = 100f;
    private float _moveInput;

    private void Update()
    {
        _moveInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKey(KeyCode.LeftShift) && _nitro > 0)
            _nitro -= 20f * Time.deltaTime;
        else
            _nitro += 0f * Time.deltaTime;

        _nitro = Mathf.Clamp(_nitro, 0, 100f);
        _nitroSlider.value = _nitro / 100f;
        _nitroSlider.fillRect.gameObject.SetActive(_nitro > 0);
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftShift) && _nitro > 0)
        {
            _frontTireRB.AddTorque(-_moveInput * _nitroBoost * Time.fixedDeltaTime);
            _backTireRB.AddTorque(-_moveInput * _nitroBoost * Time.fixedDeltaTime);
        }
    }

    public void AddNitro(float amount)
    {
        _nitro = Mathf.Clamp(_nitro + amount, 0, 100f);
    }
}