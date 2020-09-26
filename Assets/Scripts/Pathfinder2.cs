using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder2 : MonoBehaviour
{
    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    [SerializeField] Waypoint startWaypoint, endWaypoint;

    Queue<Waypoint> queue = new Queue<Waypoint>();
    bool isRunning = true;

    Waypoint searchCenter;

    public List<Waypoint> path = new List<Waypoint>();

    Vector2Int[] directions =
    {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };

    // Start is called before the first frame update
    void Start()
    {

    }

    private void CreatePath()
    {
        path.Add(endWaypoint);

        Waypoint exploredFrom = endWaypoint.exploredFrom;
        while (exploredFrom != startWaypoint)
        {
            path.Add(exploredFrom);
            exploredFrom = exploredFrom.exploredFrom;
        }
        path.Add(startWaypoint);
        path.Reverse();
    }

    private void BreadthFirstSearch()
    {
        queue.Enqueue(startWaypoint);
        while (queue.Count > 0 && isRunning)
        {
            searchCenter = queue.Dequeue();
            HalfIfEndFound();
            ExploreNeighbour();
            searchCenter.isExplored = true;

        }
    }

    private void HalfIfEndFound()
    {
        if (searchCenter == endWaypoint)
        {
            isRunning = false;
        }
    }

    private void ExploreNeighbour()
    {
        if (!isRunning)
        {
            return;
        }
        foreach (Vector2Int direction in directions)
        {

            Vector2Int neighbourCoordinates = searchCenter.GetGridPos() + direction;
            if (grid.ContainsKey(neighbourCoordinates))
            {
                QueueNewNeighbours(neighbourCoordinates);
            }
        }
    }

    private void QueueNewNeighbours(Vector2Int neighbourCoordinates)
    {
        Waypoint neighbour = grid[neighbourCoordinates];
        if (neighbour.isExplored || queue.Contains(neighbour))
        {

        }
        else
        {
            queue.Enqueue(neighbour);
            neighbour.exploredFrom = searchCenter;
        }


    }

    private void ColorStartAndEnd()
    {
        startWaypoint.SetTopColor(Color.green);
        endWaypoint.SetTopColor(Color.red);
    }

    private void LoadBlocks()
    {
        var waypoints = FindObjectsOfType<Waypoint>();
        foreach (Waypoint waypoint in waypoints)
        {
            bool isOverlapping = grid.ContainsKey(waypoint.GetGridPos());
            waypoint.isExplored = false;
            var gridPosition = waypoint.GetGridPos();
            if (isOverlapping)
            {
                Debug.LogWarning("Skipping duplicate block at: " + waypoint);
            }
            else
            {
                grid.Add(gridPosition, waypoint);
            }

        }
    }

    public List<Waypoint> GetBestPath(Waypoint startNode)
    {
        startWaypoint = startNode;
        LoadBlocks();
        ColorStartAndEnd();
        BreadthFirstSearch();
        CreatePath();
        return path;
    }

    // Update is called once per frame
    void Update()
    {

    }
}

