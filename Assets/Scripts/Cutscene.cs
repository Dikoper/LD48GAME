using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour
{
    //public delegate void CutHandler();
    //public event CutHandler Over;

    G g;
    // Start is called before the first frame update
    void Start()
    {
        g = FindObjectOfType<G>().GetComponent<G>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void EndCutscene()
    {
        g.CutsceneIsOver();
    }
}
