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

    void Start()
    {
        username.text = "";
        DontDestroyOnLoad(this.gameObject);
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

    public void Load2DGameScene()
    {
        StartCoroutine(initDB(path));
        SceneManager.LoadScene("Game");
        StartCoroutine(db.downloadAndSaveImage());
        StartCoroutine(getAll2DPieces());
    }

    public void Load3DGameScene()
    {
        StartCoroutine(initDB(path));
        SceneManager.LoadScene("Game");
        StartCoroutine(db.downloadAndSaveImage());
        StartCoroutine(getAll3DPieces());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator initDB(string path)
    {
        yield return db.initFirebase(path);
    }

    IEnumerator getAll2DPieces()
    {
        yield return db.downloadAndSave2DPieces("BlackQueen", this);
        yield return db.downloadAndSave2DPieces("BlackKnight", this);
        yield return db.downloadAndSave2DPieces("BlackBishop", this);
        yield return db.downloadAndSave2DPieces("BlackKing", this);
        yield return db.downloadAndSave2DPieces("BlackRook", this);
        yield return db.downloadAndSave2DPieces("BlackPawn", this);
        yield return db.downloadAndSave2DPieces("WhiteQueen", this);
        yield return db.downloadAndSave2DPieces("WhiteKnight", this);
        yield return db.downloadAndSave2DPieces("WhiteBishop", this);
        yield return db.downloadAndSave2DPieces("WhiteKing", this);
        yield return db.downloadAndSave2DPieces("WhiteRook", this);
        yield return db.downloadAndSave2DPieces("WhitePawn", this);
    }

    IEnumerator getAll3DPieces()
    {
        yield return db.downloadAndSave3DPieces("BlackQueen", this);
        yield return db.downloadAndSave3DPieces("BlackKnight", this);
        yield return db.downloadAndSave3DPieces("BlackBishop", this);
        yield return db.downloadAndSave3DPieces("BlackKing", this);
        yield return db.downloadAndSave3DPieces("BlackRook", this);
        yield return db.downloadAndSave3DPieces("BlackPawn", this);
        yield return db.downloadAndSave3DPieces("WhiteQueen", this);
        yield return db.downloadAndSave3DPieces("WhiteKnight", this);
        yield return db.downloadAndSave3DPieces("WhiteBishop", this);
        yield return db.downloadAndSave3DPieces("WhiteKing", this);
        yield return db.downloadAndSave3DPieces("WhiteRook", this);
        yield return db.downloadAndSave3DPieces("WhitePawn", this);
    }

}
