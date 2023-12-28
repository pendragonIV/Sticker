using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class ObjectRotator : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    #region Rotation
    private Vector3 previousPosition;
    private Vector3 direction;

    private const float MAX_ROTATION = 720f;

    #endregion

    private void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            direction = previousPosition - cam.ScreenToViewportPoint(Input.mousePosition);
            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            this.transform.Rotate(new Vector3(0, 1, 0), -direction.x * MAX_ROTATION, Space.World);
        }
        else
        {
            this.transform.Rotate(new Vector3(1, 0, 0), -direction.y * MAX_ROTATION, Space.Self);
        }
        direction = Vector3.zero;
    }

}
