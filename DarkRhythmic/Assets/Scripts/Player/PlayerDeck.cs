using UnityEngine;
using UnityEngine.UI;

public class PlayerDeck : MonoBehaviour {
    public float radius;
    public float angleOffset;

    private void Update() {
        FanOut();
    }

    public void FanOut() {
        // Get all child images
        int childCount = transform.childCount;
        angleOffset = childCount * 4;
        radius = Camera.main.scaledPixelHeight * 2;
        float angleStep = angleOffset / childCount;
        float startAngle = -angleOffset / 2;

        for (int i = 0; i < childCount; i++)
        {
            RectTransform child = transform.GetChild(i) as RectTransform;
            if (child != null)
            {
                float angle = startAngle + ((i + .5f )* angleStep);
                Vector2 position = Vector2.down * radius * 0.95f + new Vector2(Mathf.Sin(Mathf.Deg2Rad * angle) * radius, Mathf.Cos(Mathf.Deg2Rad * angle) * radius);
                child.localPosition = position;

                // Optional: Rotate the images to face outward
                child.localRotation = Quaternion.Euler(0, 0, -angle);
            }
        }
    }
}