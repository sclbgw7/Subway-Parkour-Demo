using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 deltaZ = new Vector3(0, 0, Player.GameSpeed * Time.deltaTime);
        transform.position -= deltaZ;

        if(transform.position.z < -20f)
            Destroy(gameObject);
    }
}
