using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFollowing : MonoBehaviour
{
    public Vector3 lastMousePos = default;
    public Vector3 currentTestPos = default;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
            lastMousePos = default;

        if (!Input.GetKey(KeyCode.Mouse0))
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var currentMousePos = new Vector3(hit.point.x, 0, 0);
            currentTestPos = currentMousePos;

            if (lastMousePos == default)
                lastMousePos = currentMousePos;

            var deltaX = currentMousePos.x - lastMousePos.x;

            // transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + deltaX, transform.position.y, transform.position.z), 0.1f);
            transform.position = new Vector3(transform.position.x + deltaX, transform.position.y, transform.position.z);

            lastMousePos = currentMousePos;
        }
    }
}

