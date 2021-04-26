using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamMan : MonoBehaviour
{
    [SerializeField]GameObject[] zones_prefabs;
    int zone_age = 2;

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
            StartCoroutine(ZoneSpawn(Vector3.zero, Quaternion.identity));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenNewZone(Transform t)
    {
        NewZone(t);
        StartCoroutine(ZoneSpawn(t.position, t.rotation));
    }

    IEnumerator ZoneSpawn(Vector3 pos, Quaternion rot)
    {
        yield return new WaitForSeconds(1);
        zones.Add(Instantiate(zones_prefabs[Random.Range(0,zones_prefabs.Length)], pos, rot));
        zones[zones.Count - 1].GetComponentInChildren<Bridge>().GenerateZone += GenNewZone;
        ZoneDispose(zone_age);
    }

    void ZoneDispose(int age)
    {
        if(zones.Count >= age)
        {  
            Debug.Log("ZoneDisp - " + zones.Count);
            Destroy(zones[0]);
            zones.RemoveAt(0);
        }
    }
}
