using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessMan : MonoBehaviour
{
    public GameObject controller;
    public GameObject movePlate;

    private int xBoard = -1;
    private int yBoard = -1;

    private string player;

    public Sprite BlackQueen, BlackKnight, BlackBishop, BlackKing, BlackRook, BlackPawn;
    public Sprite WhiteQueen, WhiteKnight, WhiteBishop, WhiteKing, WhiteRook, WhitePawn;

    public void Activate()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
        SetCoords();

        switch (this.name)
        {
            case "BlackQueen":
                this.gameObject.name = "BlackQueen";
                this.gameObject.GetComponent<SpriteRenderer>().sprite = BlackQueen;
                player = "Black";
                break;

            case "BlackKnight":
                this.gameObject.name = "BlackKnight";
                this.gameObject.GetComponent<SpriteRenderer>().sprite = BlackKnight;
                player = "Black";
                break;

            case "BlackBishop":
                this.gameObject.name = "BlackBishop";
                this.gameObject.GetComponent<SpriteRenderer>().sprite = BlackBishop;
                player = "Black";
                break;

            case "BlackKing":
                this.gameObject.name = "BlackKing";
                this.gameObject.GetComponent<SpriteRenderer>().sprite = BlackKing;
                player = "Black";
                break;

            case "BlackRook":
                this.gameObject.name = "BlackRook";
                this.gameObject.GetComponent<SpriteRenderer>().sprite = BlackRook;
                player = "Black";
                break;

            case "BlackPawn":
                this.gameObject.name = "BlackPawn";
                this.gameObject.GetComponent<SpriteRenderer>().sprite = BlackPawn;
                player = "Black";
                break;

            case "WhiteQueen":
                this.gameObject.name = "WhiteQueen";
                this.gameObject.GetComponent<SpriteRenderer>().sprite = WhiteQueen;
                player = "White";
                break;

            case "WhiteKnight":
                this.gameObject.name = "WhiteKnight";
                this.gameObject.GetComponent<SpriteRenderer>().sprite = WhiteKnight;
                player = "White";
                break;

            case "WhiteBishop":
                this.gameObject.name = "WhiteBishop";
                this.gameObject.GetComponent<SpriteRenderer>().sprite = WhiteBishop;
                player = "White";
                break;

            case "WhiteKing":
                this.gameObject.name = "WhiteKing";
                this.gameObject.GetComponent<SpriteRenderer>().sprite = WhiteKing;
                player = "White";
                break;

            case "WhiteRook":
                this.gameObject.name = "WhiteRook";
                this.gameObject.GetComponent<SpriteRenderer>().sprite = WhiteRook;
                player = "White";
                break;

            case "WhitePawn":
                this.gameObject.name = "WhitePawn";
                this.gameObject.GetComponent<SpriteRenderer>().sprite = WhitePawn;
                player = "White";
                break;
        }
    }
    public void SetCoords()
    {
        float x = xBoard;
        float y = yBoard;

        x *= 0.66f;
        y *= 0.66f;

        x += -2.3f;
        y += -2.3f;

        this.transform.position = new Vector3(x, y, -1.0f);
    }

    public int GetXBoard()
    {
        return xBoard;
    }

    public int getYBoard()
    {
        return yBoard;
    }

    public void SetXBoard(int x)
    {
        xBoard = x;
    }

    public void SetYBoard(int y)
    {
        yBoard = y;
    }
    private void OnMouseUp()
    {
        if (!controller.GetComponent<Game>().IsGameOver() && controller.GetComponent<Game>().GetCurrentPlayer() == player)
        {
            DestroyMovePlates();

            InitiateMovePlates();
        }
    }

    public void DestroyMovePlates()
    {
        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        for (int i = 0; i < movePlates.Length; i++)
        {
            Destroy(movePlates[i]);
        }
    }

    public void InitiateMovePlates()
    {
        switch (this.name)
        {
            case "BlackQueen":
            case "WhiteQueen":
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(1, 1);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);
                LineMovePlate(-1, -1);
                LineMovePlate(-1, 1);
                LineMovePlate(1, -1);
                break;

            case "BlackKnight":
            case "WhiteKnight":
                LMovePlate();
                break;

            case "BlackBishop":
            case "WhiteBishop":
                LineMovePlate(1, 1);
                LineMovePlate(1, -1);
                LineMovePlate(-1, 1);
                LineMovePlate(-1, -1);
                break;

            case "BlackKing":
            case "WhiteKing":
                SurroundMovePlate();
                break;

            case "BlackRook":
            case "WhiteRook":
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);
                break;

            case "BlackPawn":
                PawnMovePlate(xBoard, yBoard - 1);
                break;

            case "WhitePawn":
                PawnMovePlate(xBoard, yBoard + 1);
                break;
        }
    }

    public void LineMovePlate(int xIncrement, int yIncrement)
    {
        Game sc = controller.GetComponent<Game>();

        int x = xBoard + xIncrement;
        int y = yBoard + yIncrement;

        while (sc.PositionOnBoard(x, y) && sc.GetPosition(x, y) == null)
        {
            MovePlateSpawn(x, y);
            x += xIncrement;
            y += yIncrement;
        }

        if (sc.PositionOnBoard(x, y) && sc.GetPosition(x, y).GetComponent<ChessMan>().player != player)
        {
            MovePlateAttackSpawn(x, y);
        }
    }

    public void LMovePlate()
    {
        PointMovePlate(xBoard + 1, yBoard + 2);
        PointMovePlate(xBoard - 1, yBoard + 2);
        PointMovePlate(xBoard + 2, yBoard + 1);
        PointMovePlate(xBoard + 2, yBoard - 1);
        PointMovePlate(xBoard + 1, yBoard - 2);
        PointMovePlate(xBoard - 1, yBoard - 2);
        PointMovePlate(xBoard - 2, yBoard + 1);
        PointMovePlate(xBoard - 2, yBoard - 1);
    }

    public void SurroundMovePlate()
    {
        PointMovePlate(xBoard, yBoard + 1);
        PointMovePlate(xBoard, yBoard - 1);
        PointMovePlate(xBoard - 1, yBoard - 1);
        PointMovePlate(xBoard - 1, yBoard - 0);
        PointMovePlate(xBoard - 1, yBoard + 1);
        PointMovePlate(xBoard + 1, yBoard - 1);
        PointMovePlate(xBoard + 1, yBoard - 0);
        PointMovePlate(xBoard + 1, yBoard + 1);
    }

    public void PointMovePlate(int x, int y)
    {
        Game sc = controller.GetComponent<Game>();
        if (sc.PositionOnBoard(x, y))
        {
            GameObject cp = sc.GetPosition(x, y);

            if (cp == null)
            {
                MovePlateSpawn(x, y);
            }
            else if (cp.GetComponent<ChessMan>().player != player)
            {
                MovePlateAttackSpawn(x, y);
            }
        }
    }

    public void PawnMovePlate(int x, int y)
    {
        Game sc = controller.GetComponent<Game>();
        if (sc.PositionOnBoard(x, y))
        {
            if (sc.GetPosition(x, y) == null)
            {
                MovePlateSpawn(x, y);
            }

            if (sc.PositionOnBoard(x + 1, y) && sc.GetPosition(x + 1, y) != null && sc.GetPosition(x + 1, y).GetComponent<ChessMan>().player != player)
            {
                MovePlateAttackSpawn(x + 1, y);
            }

            if (sc.PositionOnBoard(x - 1, y) && sc.GetPosition(x - 1, y) != null && sc.GetPosition(x - 1, y).GetComponent<ChessMan>().player != player)
            {
                MovePlateAttackSpawn(x - 1, y);
            }
        }
    }

    public void MovePlateSpawn(int matrixX, int matrixY)
    {
        float x = matrixX;
        float y = matrixY;

        x *= 0.66f;
        y *= 0.66f;

        x += -2.3f;
        y += 0.71f;

        GameObject mp = Instantiate(movePlate, new Vector3(x, y - 3.0f), Quaternion.identity);

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }

    public void MovePlateAttackSpawn(int matrixX, int matrixY)
    {
        float x = matrixX;
        float y = matrixY;

        x *= 0.66f;
        y *= 0.66f;

        x += -2.3f;
        y += 0.71f;

        GameObject mp = Instantiate(movePlate, new Vector3(x, y - 3.0f), Quaternion.identity);

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.attack = true;
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }
}
