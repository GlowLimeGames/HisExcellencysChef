using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class testButtonMenu : MonoBehaviour {

    // Use this for initialization
    void Start()
    {

    }
    void OnMouseDown()
    {
        Destroy(gameObject);
    }
}
