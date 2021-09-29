using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    Fox fox;
    bool wasEntered = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Fox>(out fox) && !wasEntered)
        {
            wasEntered = true;
            SFXHandler.sFXHandler.PlayEndHoleEnter();
        }
    }
}
