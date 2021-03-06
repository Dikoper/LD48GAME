using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    public delegate void BridgeHandler(Transform pos);
    public event BridgeHandler GenerateZone;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Player")
        {
            GenerateZone(transform);
            gameObject.SetActive(false);
        }
    }
}
