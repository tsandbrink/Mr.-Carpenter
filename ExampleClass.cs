using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleClass : MonoBehaviour
{

    public GameObject someObject;

    void Start()
    {
        // Instantiate an object to the right of the current object
        Vector3 thePosition = transform.TransformPoint(2, 0, 0);
        Instantiate(someObject, thePosition, someObject.transform.rotation);
    }
}

