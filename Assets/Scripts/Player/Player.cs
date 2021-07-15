using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public LayerMask Ground;
    public float JumpForce;
    public Transform Ref1, MoveRef;
    public HorizontalMove MoveHori;
    [HideInInspector] public static float GameSpeed;
    [HideInInspector] public bool IsLying;
    public Canvas DeadUI;
    public CameraFollow Camera;

    private Collider CL;
    private Rigidbody RB;
    private int groundLayerNum;
    private float gravityScale;
    private float baseHorizontal;
    private float MoveCD;
    private int preCommandMove;
    private float JumpCD;
    private int preCommandJump;
    private bool HurtFlag;
    private float timeSinceLog;
    private bool dead;
    // Start is called before the first frame update
    void Start()
    {
        CL = GetComponent<Collider>();
        RB = GetComponent<Rigidbody>();

        GameSpeed = 2.5f;
        IsLying = false;
        int intGround = Ground;
        for(groundLayerNum = 0; intGround != 1; intGround >>= 1) {
            groundLayerNum++;
        }

        gravityScale = 1f;
        baseHorizontal = 0;
        MoveCD = 0;
        preCommandMove = 0;
        JumpCD = 0;
        preCommandJump = 0;
        HurtFlag = false;
        dead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(dead)
            return;
        //Debug.Log(GameSpeed);

        if(GameSpeed < 5f) {
            GameSpeed += Time.deltaTime / 10f;
            timeSinceLog = 31.6f;
        }
        else {
            timeSinceLog += Time.deltaTime;
            GameSpeed = 5 * Mathf.Log10(timeSinceLog) / 1.5f;
        }
        if(IsOnGround())
            gravityScale = 1f;
        RB.AddForce(new Vector3(0, -9.8f * (GameSpeed - 1f) * gravityScale, 0) * Time.deltaTime * 60, ForceMode.Acceleration);

        transform.position = new Vector3(baseHorizontal + MoveRef.position.x, transform.position.y, Mathf.Lerp(transform.position.z, -6, 1.0f * Time.deltaTime));
        transform.rotation = MoveRef.rotation;

        if(transform.position.z > -6.1f)
            HurtFlag = false;
        if(transform.position.z < -6.5f)
            Hurt();
        if(transform.position.z < -7.5f)
            Die(0);

        MoveCD -= Time.deltaTime;
        JumpCD -= Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.W))
            preCommandJump = 1;
        if(Input.GetKeyDown(KeyCode.A))
            preCommandMove = 1;
        if(Input.GetKeyDown(KeyCode.D))
            preCommandMove = 2;
        if(Input.GetKeyDown(KeyCode.S))
            preCommandJump = 2;
        HandleHorizontal();
        HandleVertical();
    }

    void HandleHorizontal() {
        if(MoveCD > 0)
            return;
        if(preCommandMove == 1) {
            if(baseHorizontal > -1f) {
                MoveCD = 0.21f;
                baseHorizontal -= 3.33f;
                MoveHori.Move(-1);
            }
        }
        if(preCommandMove == 2) {
            if(baseHorizontal < 1f) {
                MoveCD = 0.21f;
                baseHorizontal += 3.33f;
                MoveHori.Move(1);
            }
        }
        preCommandMove = 0;
    }

    void HandleVertical() {
        if(JumpCD > 0)
            return;
        float rotX = transform.rotation.eulerAngles.x;
        if(Mathf.Abs(rotX) < 0.01f || Mathf.Abs(rotX - 360f) < 0.01f) {
            IsLying = false;
        }
        if(preCommandJump == 1) {
            if(IsOnGround()) {
                RB.velocity = new Vector3(0, JumpForce * Mathf.Sqrt(GameSpeed), 0);
                MoveRef.DORotate(new Vector3(0, 0, 0), 0.2f).SetId("Rotate");
                IsLying = false;
                JumpCD = 0.2f;
            }
            preCommandJump = 0;
        }
        if(preCommandJump == 2) {
            gravityScale = 5f;
            MoveHori.Rotate();
            IsLying = true;
            JumpCD = 0.2f;
            preCommandJump = 0;
        }
    }

    public bool IsOnGround() {
        if(IsLying)
            return Physics.CheckSphere(transform.position - new Vector3(0, 0.35f, 0), 0.1f, Ground);
        else
            return Physics.CheckSphere(transform.position - new Vector3(0, 0.7f, 0), 0.1f, Ground);
    }

    public void Hurt() {
        if(HurtFlag)
            return;
        Debug.Log("Hurt");
        Camera.Shake(0.5f);
        HurtFlag = true;
    }

    public void Die(float shakePower = 1f) {
        GameSpeed = 0;
        dead = true;
        DeadUI.gameObject.SetActive(true);
        Camera.Shake(1f * shakePower);
        GameManager.Instance.RemakeAfterSpace();
    }
}
