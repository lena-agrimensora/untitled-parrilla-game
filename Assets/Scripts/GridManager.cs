using UnityEngine;
using UnityEngine.UIElements;

public class GridManager : MonoBehaviour
{
    [SerializeField]
    private int cols = 4;
    [SerializeField]
    private int cellsCount = 20;
    [SerializeField]
    private Vector2 cellSize;
    [SerializeField]
    private Vector2 cellSpacing;
    [SerializeField]
    private GameObject cellGO;

    private void Start()
    {
        for (int i = 0; i < cellsCount; i++) 
        {
            int row = i / cols;
            int col = i % cols;

            Vector3 newPosition = new Vector3(col * (cellSize.x + cellSpacing.x), -row * (cellSize.y + cellSpacing.y), 0);

            Instantiate(cellGO, newPosition, Quaternion.identity);
        }
    }

}