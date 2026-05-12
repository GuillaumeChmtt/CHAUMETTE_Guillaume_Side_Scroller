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


    private void Awake()
    {
        Debug.Log("NitroSystem créé sur : " + gameObject.name + " | Path : " + transform.parent?.name);
    }
    private void Update()
    {
        Debug.Log("FrontTireRB : " + _frontTireRB + " | BackTireRB : " + _backTireRB + " | Nitro : " + _nitro);
        if (_nitroSlider != null)
        {
            _nitroSlider.value = _nitro / 100f;
            Debug.Log("Slider value : " + _nitroSlider.value);
            _nitroSlider.fillRect.gameObject.SetActive(_nitro > 0);
        }

        if (_frontTireRB == null || _backTireRB == null) return;

        _moveInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetKey(KeyCode.LeftShift) && _nitro > 0)
            _nitro -= 20f * Time.deltaTime;

        _nitro = Mathf.Clamp(_nitro, 0, 100f);
    }



    private void FixedUpdate()
    {
        if (_frontTireRB == null || _backTireRB == null) return;

        if (Input.GetKey(KeyCode.LeftShift) && _nitro > 0)
        {
            _frontTireRB.AddTorque(-_moveInput * _nitroBoost * Time.fixedDeltaTime);
            _backTireRB.AddTorque(-_moveInput * _nitroBoost * Time.fixedDeltaTime);
        }
    }

    public void AddNitro(float amount)
    {
        _nitro = Mathf.Clamp(_nitro + amount, 0, 100f);
        Debug.Log("Nitro ajouté : " + amount + " | Nitro total : " + _nitro);
    }

    public void SetCar(Rigidbody2D frontTireRB, Rigidbody2D backTireRB)
    {
        _frontTireRB = frontTireRB;
        _backTireRB = backTireRB;
    }


}