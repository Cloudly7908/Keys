using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Territ : MonoBehaviour
{

    public Transform Player;
    public GameObject Beam;

    void Start()
    {
        transform.LookAt(Player.transform);
    }

    void Update()
    {
       

    }
    private void OnTriggerExit(Collider trigger)
    {

            Beam.SetActive(false);
            Debug.Log("beam off");
        
    }
    void OnTriggerStay(Collider trigger)
   {
        if (trigger.CompareTag("Player"))
        {
            Debug.Log("Territ rang");
            transform.LookAt(Player.transform);
            Beam.SetActive(true);
        }
        
        
        

       
   }
}
