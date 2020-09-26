using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyMovement2 : MonoBehaviour
{
    [SerializeField] float dwellTime = 1f;
    [SerializeField] [Range(1, 50)] float speed = 1f;
    [SerializeField] [Range(0, 5)] float timeBetweenEachBlock = 1f;
    public Waypoint currentNode, nextNode, endNode;
    private List<Waypoint> path;
    Pathfinder2 pathfinder;


    float lerpDuration = 0.5f;

    void Start()
    {

        pathfinder = GetComponent<Pathfinder2>();
        path = pathfinder.GetBestPath(currentNode);
        
        //StartCoroutine(FollowPath(path));
        //StartCoroutine(Move(path));
        //StartCoroutine(MultipleLerp(path, speed));
        //StartCoroutine(WaitAndExecute(path,speed));
        StartCoroutine(MoveUnit());
    }

	private IEnumerator MoveUnit()
	{
        //throw new NotImplementedException();
        yield return new WaitForSeconds(2f);
        UpdatePath();
        MoveOneBlock();
        StartCoroutine(MoveUnit());

    }

	private void MoveOneBlock()
	{
        //throw new NotImplementedException();
        Vector3 startPos = currentNode.transform.position;
        Vector3 endPos = path[1].transform.position;
        float timer = 0f;
        while (timer < timeBetweenEachBlock)
		{
            timer += Time.deltaTime * 1.0f;
            Vector3 newPos = Vector3.Lerp(startPos, endPos, timer);
            transform.position = newPos;
            currentNode = path[1];

            //timer += Time.deltaTime * _speed;
            //Vector3 newPos = Vector3.Lerp(startPos, waypoint.transform.position, timer);
            //transform.position = newPos;
            //yield return new WaitForEndOfFrame();
        }
	}

	private void UpdatePath()
	{
        //throw new NotImplementedException();
        Pathfinder2 pathfinder = GetComponent<Pathfinder2>(); //FindObjectOfType<Pathfinder2>();
        path = pathfinder.GetBestPath(currentNode);
        //print(path);
        foreach(Waypoint p in path)
		{
            print(p.name);
		}
        print("Next!");


    }



	// Update is called once per frame
	void Update()
    {

    }

 //   private void MoveOneBlock(Waypoint start, Waypoint finish)
	//{
 //       currentNode = start;
 //       Vector3 startPos = start.transform.position;
 //       float timer = 0f;
 //       while (timer <= timeBetweenEachBlock)
 //       {
 //           timer += Time.deltaTime * 1.0f;
 //           Vector3 newPos = Vector3.Lerp(startPos, finish.transform.position, timer);
 //           transform.position = newPos;
 //           currentNode = finish;
 //       }

 //   }

    IEnumerator WaitAndExecute(List<Waypoint> waypoints, float _speed)
    {
        

        foreach (Waypoint waypoint in waypoints.ToList<Waypoint>())
        {
            Vector3 startPos = transform.position;
            float timer = 0f;
            while (timer <= timeBetweenEachBlock)
            {
                timer += Time.deltaTime * _speed;
                Vector3 newPos = Vector3.Lerp(startPos, waypoint.transform.position, timer);
                transform.position = newPos;


                yield return new WaitForEndOfFrame();
                currentNode = waypoint;
            }
            transform.position = waypoint.transform.position;
            Pathfinder2 pathfinder = GetComponent<Pathfinder2>();//FindObjectOfType<Pathfinder>();
            List<Waypoint> path = pathfinder.GetBestPath(currentNode);
            StartCoroutine(WaitAndExecute(path, _speed));



        }
        yield return false;




    }

    // original movement method, move 1 block at a time
    private IEnumerator FollowPath(List<Waypoint> path)
    {
        foreach (Waypoint waypoint in path)
        {
            transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(dwellTime);
        }
    }

    //this method makes movement more smooth
    private IEnumerator Move(List<Waypoint> path)
    {
        foreach (Waypoint waypoint in path)
        {
            float elapsedTime = 0;
            Vector3 endValue = waypoint.transform.position;
            while (elapsedTime < lerpDuration)
            {
                transform.position = Vector3.Lerp(transform.position, waypoint.transform.position, elapsedTime);
                elapsedTime += Time.deltaTime * speed;

                yield return null;
            }
            transform.position = endValue;
        }
    }

    private IEnumerator MultipleLerp(List<Waypoint> waypoints, float _speed)
    {
        foreach (Waypoint waypoint in waypoints)
        {
            Vector3 startPos = transform.position;
            float timer = 0f;
            while (timer <= timeBetweenEachBlock)
            {
                timer += Time.deltaTime * _speed;
                Vector3 newPos = Vector3.Lerp(startPos, waypoint.transform.position, timer);
                transform.position = newPos;
                yield return new WaitForEndOfFrame();
            }
            transform.position = waypoint.transform.position;
            currentNode = waypoint;
        }
        yield return false;
    }


}

