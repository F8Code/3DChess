using UnityEngine;
using System.Collections.Generic;

public class ChessBoardLogic : MonoBehaviour
{
    public ChessSquare[,] chessSquares = new ChessSquare[8, 8];
    private bool isMoving = false;
    private ChessSquare fromSquare;
    private ChessSquare toSquare;
    private float moveSpeed = 5.0f;

    private void Start()
    {
        Transform boardModel = transform.Find("ChessBoardModel");
        Transform[] squares = boardModel.GetComponentsInChildren<Transform>();

        foreach (Transform child in squares)
        {
            ChessSquare square = child.GetComponent<ChessSquare>();
            if (square != null)
            {
                string[] coords = square.name.Split('_');
                int x = int.Parse(coords[1]);
                int z = int.Parse(coords[2]);
                chessSquares[x, z] = square;
            }
        }
    }

    private void Update()
    {
        if(isMoving) 
        {
            PerformMovement();
        }

        ChessSquare knightSquare = FindKnightSquare();
        if (knightSquare != null)
        {
            SetAccessibleSquares(knightSquare);
        }

        foreach (ChessSquare square in chessSquares)
        {
            if (square != null && square.readyToReceive)
            {
                StartMovement(knightSquare, square);
                break;
            }
        }
    }

    private ChessSquare FindKnightSquare()
    {
        foreach (ChessSquare square in chessSquares)
        {
            if (square != null && square.knight != null)
            {
                return square;
            }
        }
        return null;
    }

    private void SetAccessibleSquares(ChessSquare knightSquare)
    {
        foreach (ChessSquare square in chessSquares)
        {
            if (square != null)
            {
                square.accessible = false;
            }
        }

        List<Vector2Int> moves = CalculateKnightMoves(knightSquare);
        foreach (Vector2Int move in moves)
        {
            int x = move.x;
            int z = move.y;
            if (x >= 0 && x < 8 && z >= 0 && z < 8)
            {
                chessSquares[x, z].accessible = true;
            }
        }
    }

    private List<Vector2Int> CalculateKnightMoves(ChessSquare knightSquare)
    {
        List<Vector2Int> moves = new List<Vector2Int>();
        string[] coords = knightSquare.name.Split('_');
        int x = int.Parse(coords[1]);
        int z = int.Parse(coords[2]);

        int[] dx = { 1, 2, 2, 1, -1, -2, -2, -1 };
        int[] dz = { 2, 1, -1, -2, -2, -1, 1, 2 };

        for (int i = 0; i < 8; i++)
        {
            moves.Add(new Vector2Int(x + dx[i], z + dz[i]));
        }

        return moves;
    }

    private void StartMovement(ChessSquare fromSquare, ChessSquare toSquare)
    {
        this.fromSquare = fromSquare;
        this.toSquare = toSquare;
        isMoving = true;
    }

    private void PerformMovement() 
    {
        float step = moveSpeed * Time.deltaTime;
        fromSquare.knight.transform.position = Vector3.MoveTowards(fromSquare.knight.transform.position, toSquare.transform.position, step);

        if (Vector3.Distance(fromSquare.knight.transform.position, toSquare.transform.position) < 0.001f)
        {
            isMoving = false;

            toSquare.knight = fromSquare.knight;
            fromSquare.knight = null;
            toSquare.readyToReceive = false;
        }        
    }
}
