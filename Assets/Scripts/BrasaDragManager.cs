using UnityEngine;
using UnityEngine.EventSystems;

public class BrasaDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform brasaTransform;
    private Canvas canvas;
    private Vector3 startPosition;
    private HeatMapCell currentCell;
    [SerializeField]
    private float distancePreset = 50f;

    void Start()
    {
        brasaTransform = GetComponent<RectTransform>();
        canvas = FindObjectOfType<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = brasaTransform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        brasaTransform.position += (Vector3)eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        HeatMapCell nearestCell = FindNearestHeatCell();
        if (currentCell != null)
        {
            ParrillaManager.Instance.SetHeat(currentCell.x, currentCell.y, 0);
            //una funcion para "apagar" las cells que rodean a la currentcell
            //Validar que las cells existan!!!!! --->>>>>
            ParrillaManager.Instance.SetHeat(currentCell.x - 1, currentCell.y, 0);
            ParrillaManager.Instance.SetHeat(currentCell.x + 1, currentCell.y, 0);
            ParrillaManager.Instance.SetHeat(currentCell.x, currentCell.y + 1, 0);
            ParrillaManager.Instance.SetHeat(currentCell.x, currentCell.y - 1, 0);
        }

        if (nearestCell != null)
        {
            brasaTransform.position = nearestCell.transform.position;
            ParrillaManager.Instance.SetHeat(nearestCell.x, nearestCell.y, 80);
            //una funcion para "encender" las cells que rodean a la currentcell
            //Validar que las cells existan!!!!! --->>>>>
            ParrillaManager.Instance.SetHeat(nearestCell.x-1, nearestCell.y, 60);
            ParrillaManager.Instance.SetHeat(nearestCell.x+1, nearestCell.y, 60);
            ParrillaManager.Instance.SetHeat(nearestCell.x, nearestCell.y +1, 60);
            ParrillaManager.Instance.SetHeat(nearestCell.x, nearestCell.y - 1, 60);

            currentCell = nearestCell;
        }
    }

    private HeatMapCell FindNearestHeatCell()
    {
        HeatMapCell[] cells = FindObjectsOfType<HeatMapCell>();
        HeatMapCell nearest = null;
        float minDistance = Mathf.Infinity;

        foreach (HeatMapCell cell in cells)
        {
            float distance = Vector3.Distance(brasaTransform.position, cell.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = cell;
            }
        }
        if (minDistance > distancePreset) 
            return null;

        return nearest;
    }
}
