using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleObj : MonoBehaviour
{
    public GameObject parent;
    
    public void endEx()
    {
        Destroy(parent);
    }
}
