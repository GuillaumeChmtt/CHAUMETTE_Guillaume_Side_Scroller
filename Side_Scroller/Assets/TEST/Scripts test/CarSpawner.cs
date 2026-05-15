using UnityEngine;
public class CarSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _carPrefabs;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private CameraChara _camera;
    [SerializeField] private FinishZone _finishZone;
    [SerializeField] private Timer _timer;
    [SerializeField] private LevelProgressBar _levelProgressBar;
    [SerializeField] private Speedometer _speedometer;
    [SerializeField] private NitroSystem _nitroSystem;
    [SerializeField] private TrickSystem _trickSystem;

    private void Start()
    {
        int index = CarData.GetSelectedCar();
        index = Mathf.Clamp(index, 0, _carPrefabs.Length - 1);
        GameObject car = Instantiate(_carPrefabs[index], _spawnPoint.position, Quaternion.identity);

        Debug.Log("voiture qui a spawn : " + car.name);

        Rigidbody2D carRB = car.GetComponent<Rigidbody2D>();
        Transform frontTire = GetTireTransform(car, "FRONTTire");
        Transform backTire = GetTireTransform(car, "BACKTire");

        Debug.Log("FrontTire trouvé : " + (frontTire != null ? frontTire.name : "noooonn"));
        Debug.Log("BackTire trouvé : " + (backTire != null ? backTire.name : "nooooon"));

        _camera.target = car.transform;
        _trickSystem.SetCar(carRB, frontTire, backTire);
        _levelProgressBar.SetCar(car.transform);
        _speedometer.SetCar(carRB);
        _nitroSystem.SetCar(frontTire.GetComponent<Rigidbody2D>(), backTire.GetComponent<Rigidbody2D>());
    }

    private Transform GetTireTransform(GameObject car, string tireName)
    {
        foreach (Transform child in car.GetComponentsInChildren<Transform>())
        {
            if (child.name == tireName) return child;
        }
        return null;
    }
}