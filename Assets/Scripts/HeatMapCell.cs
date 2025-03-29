using UnityEngine;

public class HeatMapCell : MonoBehaviour
{
    public int x;
    public int y;

    public void SetCoordinates(int _x, int _y)
    {
        x = _x;
        y = _y;
        gameObject.name = $"HeatMapCell [{x},{y}]";
    }
}