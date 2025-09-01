using UnityEngine;

[ExecuteAlways]
public class FanCardsLayout : MonoBehaviour
{
    [Header("Arc Settings")]
    public float radius = 200f;
    public bool clockwise = true;

    [Header("Spacing")]
    public float padding = 10f; // space between edges of elements

    [Header("Offset")]
    public Vector2 offset = Vector2.zero; // move the whole arc

    [Header("Rotation")]
    public bool rotateAlongArc = true; // rotate elements to match the curve

    private void Update()
    {
        ArrangeChildren();
    }

    private void ArrangeChildren()
    {
        int count = transform.childCount;
        if (count == 0) return;

        // Calculate angular size for each element based on width + padding
        float totalArc = 0f;
        float[] angles = new float[count];

        for (int i = 0; i < count; i++)
        {
            RectTransform rect = transform.GetChild(i) as RectTransform;
            if (rect == null) continue;

            float elementWidth = rect.rect.width;
            float elementAngle = Mathf.Rad2Deg * (elementWidth + padding) / radius;
            angles[i] = elementAngle;
            totalArc += elementAngle;
        }

        // Start angle so the arc is centered
        float currentAngle = -totalArc / 2f;

        for (int i = 0; i < count; i++)
        {
            RectTransform rect = transform.GetChild(i) as RectTransform;
            if (rect == null) continue;

            // Move to the center of this element
            currentAngle += angles[i] / 2f;

            float rad = currentAngle * Mathf.Deg2Rad;
            Vector3 pos = new Vector3(Mathf.Sin(rad) * radius, Mathf.Cos(rad) * radius, 0f);
            pos += (Vector3)offset;
            rect.localPosition = pos;

            // Rotate to face toward the center
            if (rotateAlongArc)
            {
                float angle = currentAngle;
                if (!clockwise) angle = -angle;
                rect.localRotation = Quaternion.Euler(0f, 0f, angle); // flip to point inward
            }

            // Advance to start of next element
            currentAngle += angles[i] / 2f;
        }
    }
}
