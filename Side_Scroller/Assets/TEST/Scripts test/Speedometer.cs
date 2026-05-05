using UnityEngine;

public class Speedometer : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _carRB;
    [SerializeField] private RectTransform _needle;
    [SerializeField] private float _minAngle = 230f;  
    [SerializeField] private float _maxAngle = 0f; 
    [SerializeField] private float _maxSpeed = 500f;

    private void Update()
    {
        float speed = _carRB.linearVelocity.magnitude * 10f;
        float angle = Mathf.Lerp(_minAngle, _maxAngle, speed / _maxSpeed);
        _needle.localEulerAngles = new Vector3(0, 0, angle);
    }
}