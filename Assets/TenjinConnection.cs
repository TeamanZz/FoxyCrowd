using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TenjinConnection : MonoBehaviour
{
    void Start()
    {
        TenjinConnect();
    }

    public void TenjinConnect()
    {
        BaseTenjin instance = Tenjin.getInstance("FTNSVXSVBJYARJM4HPHM8X9PYIPDWO1V");

        // Sends install/open event to Tenjin
        instance.Connect();
    }
}
