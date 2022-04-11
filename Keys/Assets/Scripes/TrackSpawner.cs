using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackSpawner : MonoBehaviour
{
    public List<GameObject> Tracks;
    float offset = 200f;

    void Start()
    {
        if(Tracks != null && Tracks.Count > 0)
        {
            Tracks = Tracks.OrderBy(ref => ref.transform.position.z).ToList();
        }

    }

    
    void Update()
    {
        GameObject movedTrack = Tracks[0];
        Tracks.Remove(movedTrack);
        float newZ = Tracks[Tracks.Count - 1].transform.position.z + offset
    }
}
