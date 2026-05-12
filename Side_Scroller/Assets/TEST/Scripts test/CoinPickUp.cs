using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((1 << other.gameObject.layer) == LayerMask.GetMask("Car"))
        {
            CoinManager.Instance.AddCoins(1);
            Destroy(gameObject);
        }
    }
}