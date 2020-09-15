using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
[RequireComponent(typeof(Waypoint))]
public class CubeEditor : MonoBehaviour
{

    
    Waypoint waypoint;

    private void Awake()
    {
        waypoint = GetComponent<Waypoint>();
    }

    // Update is called once per frame
    void Update()
    {
        SnapToGrid();
        UpdateLabel();
    }

    private void SnapToGrid()
    {
        //int gridSize = waypoint.GetGridSize();

        transform.position = new Vector3
        (
            waypoint.GetGridPos().x,
            0f, 
            waypoint.GetGridPos().y
        );
    }

    private void UpdateLabel()
    {
        int gridSize = waypoint.GetGridSize();
        Vector2Int gridPosition = waypoint.GetGridPos();
        TextMesh textMesh = GetComponentInChildren<TextMesh>();
        string labelText = (gridPosition.x / gridSize) + "," + (gridPosition.y / gridSize);
        textMesh.text = labelText;
        gameObject.name = "Cube: " + labelText;
    }
}
