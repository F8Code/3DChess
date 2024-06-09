using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public float rotationSpeed = 1f;
    public Transform pivotPoint;

    private Vector3 lastMousePosition;

    void Start()
    {
        if (pivotPoint == null)
        {
            pivotPoint = transform.parent;
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            float deltaX = Input.mousePosition.x - lastMousePosition.x;

            pivotPoint.Rotate(0f, deltaX * rotationSpeed, 0f);
        }
        lastMousePosition = Input.mousePosition;
    }
}
