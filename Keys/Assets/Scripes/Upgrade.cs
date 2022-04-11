using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public GameObject Upgrade1;
    

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Upgrade1.SetActive(true);
        }
          
    }

}
