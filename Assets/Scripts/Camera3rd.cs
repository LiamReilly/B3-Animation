using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Camera3rd : MonoBehaviour
{
    public Transform Father;
    public float yOffset = 1.5f;
    public float xDownRot = 0;
    Quaternion originalRot;
    Vector3 offset;
    

    // Start is called before the first frame update
    void Start()
    {
        Father = GameObject.FindGameObjectWithTag("Controllable").GetComponent<Transform>();
        originalRot = Father.rotation; 
        offset = new Vector3(0, yOffset, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
        Quaternion target = Father.rotation;
        transform.position = Father.position - Father.transform.forward*2 + new Vector3(0, yOffset, 0);
        transform.rotation = Father.rotation;
        //Point down at player
        transform.Rotate(xDownRot, 0, 0);
    }

    //Removes the Y component of a vector and normalizes it to prevent drift up and down when moving
    Vector3 fixYAxis(Vector3 vector)
    {   
        Vector3 returnee = new Vector3(vector.x, 0, vector.z);
        returnee.Normalize();
        return returnee;

    }
   
    
}
