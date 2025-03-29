using System.Text.RegularExpressions;
using UnityEngine;

public class CarneDragManager : MonoBehaviour
{
    private Camera mainCamera;
    private float yOffset = 0f;

    private void Start()
    {
        mainCamera = Camera.main;
        yOffset = transform.position.y;
    }

    void OnMouseDrag()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, new Vector3(0, yOffset, 0)); 
        if (plane.Raycast(ray, out float distance))
        {
            Vector3 targetPosition = ray.GetPoint(distance);
            transform.position = new Vector3(targetPosition.x, yOffset, targetPosition.z);
        }
    }

    void OnMouseUp()
    {
        Ray rayDown = new Ray(transform.position, Vector3.down); 
        RaycastHit hit;

        if (Physics.Raycast(rayDown, out hit))
        {
            if (hit.collider.CompareTag("ParrillaCell"))
            {
                Vector3 parrillaCellPosition = hit.collider.transform.position;
                transform.position = new Vector3(parrillaCellPosition.x, yOffset, parrillaCellPosition.z);
                string parrillaCellName = hit.collider.gameObject.name;

                Regex regex = new Regex(@"Cell \[(\d+),(\d+)\]");
                Match match = regex.Match(parrillaCellName);

                if (match.Success)
                {
                    
                    int x = int.Parse(match.Groups[1].Value);
                    int y = int.Parse(match.Groups[2].Value);
                    Debug.Log($"Encontre una parrilla cell en coordenadas: X = {x}, Y = {y}!!!!");
                    //obtener el "calor" de la cell en esa coordenada??
                }
            }
        }
    }
}
