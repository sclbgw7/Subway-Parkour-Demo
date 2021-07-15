using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : MonoBehaviour
{
    public Transform Ref1;

    private Rigidbody RB;
    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RB.AddForce(new Vector3(0, -9.8f * (Player.GameSpeed - 1f), 0), ForceMode.Acceleration);

        if(transform.position.y < 0.01f)
            RB.velocity = new Vector3(0, 3 * Mathf.Sqrt(Player.GameSpeed), 0);
    }
}
