using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float dwellTime = 1f;
	[SerializeField][Range(1,50)] float speed = 1f;
    [SerializeField] [Range(0, 5)] float timeBetweenEachBlock = 1f;

    float lerpDuration = 0.5f;

    void Start()
    {

        Pathfinder pathfinder = FindObjectOfType<Pathfinder>();
        List<Waypoint> path = pathfinder.GetBestPath();
        //StartCoroutine(FollowPath(path));
        //StartCoroutine(Move(path));
        StartCoroutine(MultipleLerp(path, speed));
    }



    // Update is called once per frame
    void Update()
    {
        
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
        foreach(Waypoint waypoint in path)
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
        foreach(Waypoint waypoint in waypoints)
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
        }
        yield return false;
    }


}
