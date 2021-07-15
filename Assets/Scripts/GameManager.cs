using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance => instance;
    private static GameManager instance;

    private bool Dying;

    void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Dying = false;
        Application.targetFrameRate = 65;
    }

    // Update is called once per frame
    void Update()
    {
        if(Dying) {
            if(Input.GetKeyDown(KeyCode.Space)) {
                Dying = false;
                Remake();
            }
        }
    }

    private void ReloadGame()
    {
        DOTween.KillAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Remake()
    {
        ReloadGame();
    }

    public void RemakeAfterSpace()
    {
        Dying = true;
    }
}
