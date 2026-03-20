using UnityEngine;
using UnityEngine.UI;

public class NitroSystem : MonoBehaviour
{
    [Header("Nitro")]
    [SerializeField] float nitroSpeedBonus = 10f;
    [SerializeField] string nitroBarName = "NitroFill"; 

    Image nitroBar;
    float nitro = 1f;

    public float SpeedBonus => Input.GetKey(KeyCode.LeftShift) && nitro > 0 ? nitroSpeedBonus : 0f;

    void Start()
    {
        GameObject obj = GameObject.Find(nitroBarName);
        nitroBar = obj.GetComponent<Image>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && nitro > 0)
            nitro -= Time.deltaTime * 0.3f;
        else
            nitro += Time.deltaTime * 0.1f;

        nitro = Mathf.Clamp01(nitro);

        if (nitroBar != null)
            nitroBar.fillAmount = nitro;
    }
}