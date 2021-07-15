using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HorizontalMove : MonoBehaviour
{
    public Transform Ref1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(int dir) { // -1 or 1
        transform.position = new Vector3(-dir * 3.33f, 0, 0);
        transform.DOMove(Ref1.position, 0.2f);
    }

    public void Rotate() {
        transform.DORotate(new Vector3(-90, 0, 0), 0.2f).SetId("Rotate");
        float lieTime = 2f / Player.GameSpeed;
        StartCoroutine(RotateBackCoroutine(lieTime));
    }

    IEnumerator RotateBackCoroutine(float lieTime)
    {
        yield return new WaitForSeconds(lieTime);
        transform.DORotate(new Vector3(0, 0, 0), 0.2f).SetId("Rotate");
    }
}
