using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class ChessBoardConstructor : MonoBehaviour
{
    public GameObject darkSquarePrefab;
    public GameObject lightSquarePrefab;
    public GameObject chessKnightWhitePrefab;

    public void UpdateChessBoard()
    {
        RemovePreviousChessBoard();
        GenerateNewChessBoard();
    }

    public void RemovePreviousChessBoard() 
    {
        List<Transform> children = new List<Transform>();
        foreach (Transform child in transform)
        {
            children.Add(child);
        }

        foreach (Transform child in children)
        {
            DestroyImmediate(child.gameObject);
        }
    }

    public void GenerateNewChessBoard() 
    {
        GameObject boardParent = new GameObject("ChessBoardModel"); 
        boardParent.transform.parent = transform;

        GameObject piecesParent = new GameObject("ChessBoardPieces"); 
        piecesParent.transform.parent = transform;

        float squareSize = lightSquarePrefab.GetComponent<Renderer>().bounds.size.x;
        for (int x = 0; x < 8; x++)
        {
            for (int z = 0; z < 8; z++)
            {
                GameObject squarePrefab = (x + z) % 2 == 0 ? lightSquarePrefab : darkSquarePrefab;
                float posX = x * squareSize - 3.5f * squareSize;
                float posZ = z * squareSize - 3.5f * squareSize;
                GameObject square = Instantiate(squarePrefab, new Vector3(posX, 0, posZ), Quaternion.identity, boardParent.transform);
                square.name = $"Square_{x}_{z}";

                ChessSquare squareScript = square.GetComponent<ChessSquare>();

                if (x == 0 && z == 1)
                {
                    GameObject knight = Instantiate(chessKnightWhitePrefab, square.transform.position, chessKnightWhitePrefab.transform.rotation, piecesParent.transform);
                    knight.transform.localScale = new Vector3(18F * squareSize, 18F * squareSize, 18F * squareSize);
                    knight.name = "ChessKnightWhite";
                    squareScript.knight = knight;
                }
            }
        }
    }
}
