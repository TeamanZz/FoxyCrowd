using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollowing : MonoBehaviour
{
    private CrowdController crowdController;
    public Transform rotateTargetTransform;
    public Rigidbody foxesRigidbody;
    private Vector3 lastMousePos = default;

    private void Awake()
    {
        crowdController = GameObject.FindObjectOfType<CrowdController>();
    }

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
            float leftmostX = crowdController.GetLeftmostElement();
            float rightmostX = crowdController.GetRightmostElement();

            var currentMousePos = new Vector3(hit.point.x, 0, 0);

            if (lastMousePos == default)
                lastMousePos = currentMousePos;

            var deltaX = currentMousePos.x - lastMousePos.x;

            if (rotateTargetTransform.position.x > -2 && rotateTargetTransform.position.x < 2 && isNotLeftBorder(leftmostX) && isNotRightBorder(rightmostX))
            {
                rotateTargetTransform.position = new Vector3(rotateTargetTransform.position.x + deltaX, rotateTargetTransform.position.y, rotateTargetTransform.position.z);
                foxesRigidbody.MovePosition(new Vector3(rotateTargetTransform.position.x, transform.position.y, transform.position.z));
            }
            else
            {
                if (deltaX > 0 && rotateTargetTransform.position.x <= -2)
                {
                    rotateTargetTransform.position = new Vector3(rotateTargetTransform.position.x + deltaX, rotateTargetTransform.position.y, rotateTargetTransform.position.z);
                    foxesRigidbody.MovePosition(new Vector3(rotateTargetTransform.position.x, transform.position.y, transform.position.z));
                }

                if (deltaX < 0 && rotateTargetTransform.position.x >= 2)
                {
                    rotateTargetTransform.position = new Vector3(rotateTargetTransform.position.x + deltaX, rotateTargetTransform.position.y, rotateTargetTransform.position.z);
                    foxesRigidbody.MovePosition(new Vector3(rotateTargetTransform.position.x, transform.position.y, transform.position.z));
                }
            }
            lastMousePos = currentMousePos;
        }
    }

    private bool isNotLeftBorder(float xValue)
    {
        return (xValue - 0.2f > -2f);
    }

    private bool isNotRightBorder(float xValue)
    {
        return (xValue + 0.2f < 2);
    }
}