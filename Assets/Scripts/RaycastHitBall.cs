using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastHitBall : MonoBehaviour
{
    private float timer;
    public const float STAY_TIME = 1;

    // Start is called before the first frame update
    void Start()
    {
        timer = STAY_TIME;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0){
            Destroy(this.gameObject);
        }
    }
}
