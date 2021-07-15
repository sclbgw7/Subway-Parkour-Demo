using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceManager : MonoBehaviour
{
    public int FenceNum;
    public float Distance;
    public GameObject Cube1, Cube2;
    private GameObject[] Cubes;
    //public Player player;
    // Start is called before the first frame update
    void Start()
    {
        if(FenceNum == 0)
            return;
        Cubes = new GameObject[FenceNum * 2];
        Cubes[0] = Instantiate(Cube1);
        Cubes[0].SetActive(true);
        Cubes[1] = Instantiate(Cube2);
        Cubes[1].SetActive(true);
        for(int i = 1; i < FenceNum; i++) {
            Cubes[i * 2] = Instantiate(Cubes[0]);
            Cubes[i * 2].transform.position += new Vector3(0, 0, Distance * i);
            Cubes[i * 2 + 1] = Instantiate(Cubes[1]);
            Cubes[i * 2 + 1].transform.position += new Vector3(0, 0, Distance * i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 deltaZ = new Vector3(0, 0, Player.GameSpeed * Time.deltaTime);
        for(int i = 0; i < FenceNum * 2; i++) {
            Cubes[i].transform.position -= deltaZ;
            if(Cubes[i].transform.position.z <= -10f) {
                Cubes[i].transform.position += new Vector3(0, 0, FenceNum * Distance);
            }
        }
    }
}
