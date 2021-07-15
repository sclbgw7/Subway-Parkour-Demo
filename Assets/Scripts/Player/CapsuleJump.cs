using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleJump : MonoBehaviour
{
    public Player player;
    public Transform Ref1, JumpRef;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player.IsOnGround() && !player.IsLying)
            transform.position = player.transform.position + JumpRef.position - Ref1.position;
        else
            transform.position = player.transform.position;
    }
}
