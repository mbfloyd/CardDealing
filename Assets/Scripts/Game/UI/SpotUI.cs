using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotUI : MonoBehaviour
{

    public void Remove()
    {
        GameObject.DestroyImmediate(gameObject);
    }
    
}
