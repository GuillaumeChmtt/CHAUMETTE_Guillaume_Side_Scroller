using UnityEngine;

public class NitroPickup : MonoBehaviour
{
    [SerializeField] private float _nitroAmount = 30f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        NitroSystem nitro = other.GetComponent<NitroSystem>();
        if (nitro == null) return;

        nitro.AddNitro(_nitroAmount);
        Destroy(gameObject);
    }
}
