using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotSpawner : MonoBehaviour
{
    private int initAmount = 1;
    private float plotsize = 1000;
    private float xPosleft = 110.4f;
    private float xPosRight = -110.4f;
    private float lastZPos = 0f;

    public List<GameObject> plots;

    void Start()
    {
        for (int i = 0; i < initAmount; i++)
        {
            SpawnPlot();
        }    
    }

  
    void Update()
    {
        
    }

    public void SpawnPlot()
    {
        GameObject plotLeft = plots[Random.Range(0, plots.Count)];
        GameObject plotRight = plots[Random.Range(0, plots.Count)];

        float zPos = lastZPos + plotsize;

        Instantiate(plotLeft, new Vector3(xPosleft, 0.025f, zPos), plotLeft.transform.rotation);
        Instantiate(plotRight, new Vector3(xPosRight, 0.025f, zPos), new Quaternion(0, 180, 0, 0));

        //lastZPos += plotsize;
    }
}
