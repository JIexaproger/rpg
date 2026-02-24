using UnityEngine;

public class UIRotator : MonoBehaviour
{
    private Transform cameraTransform;
    private Vector3 offset = new(0, 180, 0);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.LookAt(cameraTransform);
        transform.Rotate(offset);
    }
}
