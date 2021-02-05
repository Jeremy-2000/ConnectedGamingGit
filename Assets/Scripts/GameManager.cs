using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public InputField username;
    public Button Set2D, Set3D;
    public FirebaseScript db;

    string path = "https://jeremyattardunity.firebaseio.com/";

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("MainCamera");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        DontDestroyOnLoad(this);

        Camera.main.gameObject.AddComponent<FirebaseScript>();
        db = Camera.main.GetComponent<FirebaseScript>();
    }

    // Update is called once per frame
    void Update()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "MainMenu")
        {
            try
            {
                if (username.text != "")
                {
                    Set2D.GetComponent<Button>().interactable = true;
                    Set3D.GetComponent<Button>().interactable = true;
                }

                else
                {
                    Set2D.GetComponent<Button>().interactable = false;
                    Set3D.GetComponent<Button>().interactable = false;
                }
            }
            catch (Exception e)
            {

            }

        }
    }

    public void LoadGameScene()
    {
        StartCoroutine(initDB(path));
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator clearDB()
    {
        yield return db.clearFirebase();
        Application.Quit();
    }

    IEnumerator initDB(string path)
    {
        yield return db.initFirebase(path);
    }
}
