using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public float length;
    public float width;
    public float thickness;
    public Material boardMaterial;
    public Transform boardTransform;
    public Quaternion boardRotation;

    // Start is called before the first frame update
    void Awake()
    {
        length = transform.localScale.z;
        width = transform.localScale.x;
        thickness = transform.localScale.y;
    }
    
    void Start()
    {
        boardMaterial = gameObject.GetComponent<Renderer>().material;
        boardTransform = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
