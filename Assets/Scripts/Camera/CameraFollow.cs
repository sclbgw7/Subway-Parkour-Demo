using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraFollow : MonoBehaviour
{
    public Transform Player;
    public Transform ShakeRef;
    private Vector3 playerRef, selfRef;
    // Start is called before the first frame update
    void Start()
    {
        playerRef = Player.position;
        selfRef = transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = selfRef + Player.position - playerRef + ShakeRef.position;
        transform.position = new Vector3(transform.position.x / 1.3f, transform.position.y, selfRef.z);
    }

    public void Shake(float power) {
        ShakeRef.DOShakePosition(0.6f, power, 20, 80, false, true);
    }
}
