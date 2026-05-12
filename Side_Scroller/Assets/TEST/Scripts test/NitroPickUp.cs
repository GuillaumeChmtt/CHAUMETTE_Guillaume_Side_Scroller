using UnityEngine;

public class NitroPickup : MonoBehaviour
{
    [SerializeField] private float _nitroAmount = 30f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Car")) return;

        NitroSystem nitro = other.GetComponentInParent<NitroSystem>();
        if (nitro == null)
            nitro = FindObjectOfType<NitroSystem>();
        if (nitro == null) return;

        nitro.AddNitro(_nitroAmount);
        Destroy(gameObject);
    }
}