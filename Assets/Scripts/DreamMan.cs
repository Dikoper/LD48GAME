using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamMan : MonoBehaviour
{
    [SerializeField]GameObject[] zones_prefabs;
    [SerializeField] int zone_age = 3;

    public delegate void NewZoneHandler(Transform pos);
    public event NewZoneHandler NewZone;
    List<GameObject> zones = new List<GameObject>();
    //GameObject cur_zone;
    // Start is called before the first frame update
    void Start()
    {
        if(zones_prefabs.Length < 1)
            throw new System.Exception("Need to add some zones");

        if(zones.Count < 1)
            zones.Add(Instantiate(zones_prefabs[Random.Range(0,zones_prefabs.Length)], Vector3.zero, Quaternion.identity));

        ZoneCatch();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ZoneCatch()
    {
        zones[zones.Count - 1].GetComponentInChildren<Bridge>().GenerateZone += GenNewZone;
    }

    void GenNewZone(Transform pos)
    {
        NewZone(pos);
        zones.Add(Instantiate(zones_prefabs[Random.Range(0,zones_prefabs.Length)], pos.position, pos.rotation));
        ZoneCatch();
        ZoneDispose(zone_age);
    }

    void ZoneDispose(int age)
    {
        if(zones.Count % age < 1)
        {
            Destroy(zones[0]);
            zones.RemoveAt(0);
        }
    }
}
