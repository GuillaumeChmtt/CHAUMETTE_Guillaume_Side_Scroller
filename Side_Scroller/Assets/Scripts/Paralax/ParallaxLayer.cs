using UnityEngine;

[System.Serializable]

public class ParallaxLayer
{
    public float speedX = 0.5f;
    public float speedY = 0.2f;

    private Transform transform;
    private Vector3 targetPosition;

    private SpriteRenderer sprite;
    private float spriteWidth;
    private bool infiniteX;

    public ParallaxLayer(Transform t)
    {
        transform = t;
        targetPosition = t.position;

        sprite = t.GetComponent<SpriteRenderer>();

        if (sprite != null)
        {
            spriteWidth = sprite.bounds.size.x;
            //infiniteX = spriteWidth > 0f;
        }

        var settings = t.GetComponent<ParallaxLayerSettings>();

        if (settings != null)
        {
            speedX = settings.speedX;
            speedY = settings.speedY;
        }

    }

    public void Move(Vector3 delta, bool vertical, float smoothing)
    {
        float moveX = delta.x * (1f - speedX);
        float moveY = vertical ? delta.y * (1f - speedY) : 0f;

        targetPosition += new Vector3(moveX, moveY, 0f);
        transform.position = smoothing > 0f ? Vector3.Lerp(transform.position, targetPosition, smoothing) : targetPosition;

        if (infiniteX)
        {
            WrapHorizontal();
        }
    }

    private void WrapHorizontal()
    {
        float camX = Camera.main.transform.position.x;
        float diffX = camX - transform.position.x;

        if(Mathf.Abs(diffX) >= spriteWidth)
        {
            float offset = diffX > 0 ? spriteWidth : - spriteWidth;
            transform.position += new Vector3(offset, 0f, 0f);
        }
    }
}
    