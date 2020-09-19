using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    [SerializeField] Waypoint startWaypoint, endWaypoint;

    Queue<Waypoint> queue = new Queue<Waypoint>();
    bool isRunning = true;

    Waypoint searchCenter;

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
        LoadBlocks();
        ColorStartAndEnd();
        Pathfind();
    }

	private void Pathfind()
	{
        queue.Enqueue(startWaypoint);
        while(queue.Count > 0 && isRunning)
		{
            searchCenter = queue.Dequeue();
            HalfIfEndFound();
            ExploreNeighbour();
            searchCenter.isExplored = true;
 
		}
        //todo work out path
	}

	private void HalfIfEndFound()
	{
		if(searchCenter == endWaypoint)
		{
            isRunning = false;
            //print("End found at: " + search.GetGridPos());
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
			try
			{
				QueueNewNeighbours(neighbourCoordinates);
			}
			catch
			{

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
        foreach(Waypoint waypoint in waypoints)
        {
            bool isOverlapping = grid.ContainsKey(waypoint.GetGridPos());
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
