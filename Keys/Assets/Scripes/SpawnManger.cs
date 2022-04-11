using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManger : MonoBehaviour
{
    TrackSpawner trackSpawner;

    void Start()
    {
        trackSpawner = GetComponent<TrackSpawner>();
    }

    public void SpawnTriggerEntered()
    {
        trackSpawner.MoveTrack();
    }
}
