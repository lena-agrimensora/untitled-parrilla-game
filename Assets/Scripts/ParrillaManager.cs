using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class ParrillaManager : MonoBehaviour
{ 

    [SerializeField]
    private int width = 7;
    [SerializeField]
    private int height = 3;
    [SerializeField]
    private float cellOffset = 3.2f;
    [SerializeField]
    private GameObject cellPrefab;

    public GameObject heatMapCellPrefab;
    public Transform heatMapGrid;

    private float[,] heatMap;
    private GameObject[,] cells;

    [SerializeField]
    private int cellXTest;
    [SerializeField]
    private int cellYTest;

    private Image[,] heatMapCells;
    public static ParrillaManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        heatMap = new float[width, height];
        cells = new GameObject[width, height];
        heatMapCells = new Image[width, height];

        GenerateGrid();
        GenerateHeatMapUI();
    }

    void GenerateGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 position = new Vector3(x * cellOffset, 0, y * cellOffset);
                GameObject cell = Instantiate(cellPrefab, position, Quaternion.identity, transform);
                cell.name = $"Cell [{x},{y}]";
                cells[x, y] = cell;

                heatMap[x, y] = 0;
                UpdateCellColor(x, y);
            }
        }
    }

    void GenerateHeatMapUI()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject uiCell = Instantiate(heatMapCellPrefab, heatMapGrid);
                uiCell.name = $"HeatMapCell [{x},{y}]";
                heatMapCells[x, y] = uiCell.GetComponent<Image>();
                HeatMapCell cellScript = uiCell.GetComponent<HeatMapCell>();

                if (cellScript != null)
                {
                    cellScript.SetCoordinates(x, y);
                    //heatMapCells[x, y] = cellScript;
                }
                UpdateHeatMapColor(x, y);
            }
        }
    }

    void UpdateCellColor(int x, int y)
    {
        Renderer renderer = cells[x, y].GetComponent<Renderer>();
        if (renderer != null)
        {
            float heat = Mathf.Clamp01(heatMap[x, y] / 100f);
            renderer.material.color = Color.Lerp(Color.blue, Color.red, heat);
        }
    }

    void UpdateHeatMapColor(int x, int y)
    {
        float heat = Mathf.Clamp01(heatMap[x, y] / 100f);
        if (heatMapCells[x, y] != null)
        {
            heatMapCells[x, y].color = Color.Lerp(Color.blue, Color.red, heat);
        }
    }

    public void SetHeat(int x, int y, float heat)
    {
        heatMap[x, y] = Mathf.Clamp(heat, 0, 100);
        UpdateCellColor(x, y);
        UpdateHeatMapColor(x, y);
    }

    public void DebugIncreaseHeat()
    {
        Debug.Log("Clickee algo!!! :3");
        SetHeat(cellXTest, cellYTest, 100);
    }
}
