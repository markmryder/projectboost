using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] List<Waypoint> path;
    [SerializeField] float dwellTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(FollowPath());
    }



    // Update is called once per frame
    void Update()
    {
        
    }




    private IEnumerator FollowPath()
    {
        foreach (Waypoint waypoint in path)
        {
            transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(dwellTime);
        }
    }

}
