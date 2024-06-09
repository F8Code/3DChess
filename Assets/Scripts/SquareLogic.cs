using UnityEngine;

public class ChessSquare : MonoBehaviour
{
    public GameObject knight;
    public bool accessible = false;
    public bool readyToReceive = false;
    private bool selected = false;

    private Color originalColor;
    private Renderer squareRenderer;

    private void Start()
    {
        squareRenderer = GetComponent<Renderer>();
        originalColor = squareRenderer.material.color;
    }

    private void Update()
    {
        if (selected)
            return;

        if (accessible)
        {
            squareRenderer.material.color = Color.green;
        }
        else
        {
            squareRenderer.material.color = originalColor;
        }
    }

    private void OnMouseEnter()
    {
        selected = true;
        if (accessible)
        {
            squareRenderer.material.color = Color.yellow;
        }
    }

    private void OnMouseExit()
    {
        selected = false;
        if (accessible)
        {
            squareRenderer.material.color = Color.green;
        }
        else
        {
            squareRenderer.material.color = originalColor;
        }
    }

    private void OnMouseDown()
    {
        if (accessible)
        {
            readyToReceive = true;
        }
    }
}
