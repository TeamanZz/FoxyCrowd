using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollowing : MonoBehaviour
{
    [SerializeField] private Transform rotateTargetTransform;

    private CrowdController crowdController;
    private Rigidbody foxesRigidbody;
    private Vector3 lastMousePos = default;

    private void Awake()
    {
        crowdController = GameObject.FindObjectOfType<CrowdController>();
        foxesRigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        lastMousePos = default;
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

            //Если не достигли края экрана, до двигаем
            if (isNotLeftBorder(leftmostX) && isNotRightBorder(rightmostX))
            {
                foxesRigidbody.MovePosition(new Vector3(foxesRigidbody.transform.position.x + deltaX, transform.position.y, transform.position.z));
                // rotateTargetTransform.position = new Vector3(foxesRigidbody.transform.position.x, rotateTargetTransform.position.y, rotateTargetTransform.position.z);
            }
            //Если достигли края экрана:
            else
            {
                //Если это правый край экрана, разрешаем движение только влево
                if (deltaX > 0 && foxesRigidbody.transform.position.x < 0)
                {
                    foxesRigidbody.MovePosition(new Vector3(foxesRigidbody.transform.position.x + deltaX, transform.position.y, transform.position.z));
                    // rotateTargetTransform.position = new Vector3(rotateTargetTransform.position.x + deltaX, rotateTargetTransform.position.y, rotateTargetTransform.position.z);
                }

                //Если это левый край экрана, разрешаем движение только вправо
                if (deltaX < 0 && foxesRigidbody.transform.position.x > 0)
                {
                    foxesRigidbody.MovePosition(new Vector3(foxesRigidbody.transform.position.x + deltaX, transform.position.y, transform.position.z));
                    // rotateTargetTransform.position = new Vector3(rotateTargetTransform.position.x + deltaX, rotateTargetTransform.position.y, rotateTargetTransform.position.z);
                }
            }
            lastMousePos = currentMousePos;
        }
    }

    private bool isNotLeftBorder(float xValue)
    {
        return (xValue - 0.4f > -2f);
    }

    private bool isNotRightBorder(float xValue)
    {
        return (xValue + 0.4f < 2);
    }
}