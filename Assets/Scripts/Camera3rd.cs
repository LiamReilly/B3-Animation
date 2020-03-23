using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Camera3rd : MonoBehaviour
{
    public Transform Father;
    Quaternion originalRot;

    // Start is called before the first frame update
    void Start()
    {
        Father = GameObject.FindGameObjectWithTag("Controllable").GetComponent<Transform>();
        originalRot = Father.rotation; 
    }

    // Update is called once per frame
    void Update()
    {
        
        Quaternion target = Father.rotation;
        transform.position = Father.position + new Vector3(0.4f, 2.45f, -2.35f);
        transform.rotation = originalRot;
    }
   
    
}
