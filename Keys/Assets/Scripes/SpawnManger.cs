using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManger : MonoBehaviour
{
    TrackSpawner trackSpawner;
    PlotSpawner plotSpawner;

    void Start()
    {
        trackSpawner = GetComponent<TrackSpawner>();
        plotSpawner = GetComponent<PlotSpawner>();
    }

    public void SpawnTriggerEntered()
    {
        trackSpawner.MoveTrack();
        plotSpawner.SpawnPlot();
    }
}
