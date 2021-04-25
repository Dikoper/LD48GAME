using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogEnabler : MonoBehaviour
{
    public RenderSettings rs;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Enabler()
    {
        Debug.Log("Fog enabled");
        RenderSettings.fog = true;
        RenderSettings.fogMode = FogMode.Linear;
        RenderSettings.fogDensity = 10f;
        RenderSettings.fogStartDistance = 1f;
        RenderSettings.fogEndDistance = 10f;
        RenderSettings.fogColor = Color.black;
        //RenderSettings.fogStartDistance
    }
}
