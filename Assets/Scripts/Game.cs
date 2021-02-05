using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject chessPiece;
    private GameObject[,] positions = new GameObject[8, 8];
    private GameObject[] playerBlack = new GameObject[16];
    private GameObject[] playerWhite = new GameObject[16];

    private string currentPlayer = "White";
    public bool gameOver = false;


    // Start is called before the first frame update
    void Start()
    {
        playerWhite = new GameObject[]
        {
            Create ("WhiteRook", 0,0), Create ("WhiteKnight", 1,0), Create ("WhiteBishop", 2,0), Create ("WhiteQueen", 3,0),
            Create ("WhiteKing", 4,0), Create ("WhiteBishop", 5,0), Create ("WhiteKnight", 6,0), Create ("WhiteRook", 7,0),
            Create ("WhitePawn", 0,1), Create ("WhitePawn", 1,1), Create ("WhitePawn", 2,1), Create ("WhitePawn", 3,1),
            Create ("WhitePawn", 4,1), Create ("WhitePawn", 5,1), Create ("WhitePawn", 6,1), Create ("WhitePawn", 7,1)
        };

        playerBlack = new GameObject[]
    {
            Create ("BlackRook", 0,7), Create ("BlackKnight", 1,7), Create ("BlackBishop", 2,7), Create ("BlackQueen", 3,7),
            Create ("BlackKing", 4,7), Create ("BlackBishop", 5,7), Create ("BlackKnight", 6,7), Create ("BlackRook", 7,7),
            Create ("BlackPawn", 0,6), Create ("BlackPawn", 1,6), Create ("BlackPawn", 2,6), Create ("BlackPawn", 3,6),
            Create ("BlackPawn", 4,6), Create ("BlackPawn", 5,6), Create ("BlackPawn", 6,6), Create ("BlackPawn", 7,6)
    };

        for (int i = 0; i < playerBlack.Length; i++)
        {
            setPosition(playerBlack[i]);
            setPosition(playerWhite[i]);
        }
    }
    public GameObject Create(string name, int x, int y)
    {
        GameObject obj = Instantiate(chessPiece, new Vector3(0, 0, -1), Quaternion.identity);
        ChessMan cm = obj.GetComponent<ChessMan>();
        cm.name = name;
        cm.SetXBoard(x);
        cm.SetYBoard(y);
        cm.Activate();
        return obj;
    }
    public void setPosition(GameObject obj)
    {
        ChessMan cm = obj.GetComponent<ChessMan>();
        positions[cm.GetXBoard(), cm.getYBoard()] = obj;
    }

    public void SetPositionEmpty(int x, int y)
    {
        positions[x, y] = null;
    }

    public GameObject GetPosition(int x, int y)
    {
        return positions[x, y];
    }

    public bool PositionOnBoard(int x, int y)
    {
        if (x < 0 || y < 0 || x >= positions.GetLength(0) || y >= positions.GetLength(1))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public string GetCurrentPlayer()
    {
        return currentPlayer;
    }

    public bool IsGameOver()
    {
        return gameOver;
    }

    public void NextTurn()
    {
        if (currentPlayer == "White")
        {
            currentPlayer = "Black";
        }

        else
        {
            currentPlayer = "White";
        }
    }

    public void Update()
    {
        if (gameOver == true)
        {
            StartCoroutine(LoadMainMenu());
        }
    }

    public void Winner(string playerWinner)
    {
        gameOver = true;
        GameObject.FindGameObjectWithTag("WinnerText").GetComponent<Text>().enabled = true;
        GameObject.FindGameObjectWithTag("WinnerText").GetComponent<Text>().text = playerWinner + " is the Winner";
    }

    IEnumerator LoadMainMenu()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("MainMenu");
    }
}
