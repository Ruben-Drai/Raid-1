using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private bool IsActivated = false;
    [SerializeField] private List<Transform> wayPoints;
    [SerializeField] private float PlatformSpeed = 1.0f;
    [SerializeField] private float WaitTimeBewteenWayPoints = 1.0f;

    private int currentWaypointIndex = 0;
    private Coroutine MoveRoutine = null;

    //Basically just loops through the list of waypoints and moves towards the current one
    //When near enough, go to the next, if at the last one, loop back to the beginning
    //This is done only while IsActivated is true, which prevents MoveRoutine from being set to null
    private void Update()
    {
        MoveRoutine = (IsActivated && MoveRoutine == null) ? StartCoroutine(IMoveRoutine()) : (!IsActivated && MoveRoutine != null) ? null : MoveRoutine;
    }
    private IEnumerator IMoveRoutine()
    {
        yield return null;

        while (MoveRoutine != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, wayPoints[currentWaypointIndex].position, PlatformSpeed * Time.deltaTime);

            yield return null;
            if (Vector2.Distance(transform.position, wayPoints[currentWaypointIndex].position) < 0.5f)
            {
                yield return new WaitForSeconds(WaitTimeBewteenWayPoints);
                //add one, or if at end of list go back to 0
                currentWaypointIndex += currentWaypointIndex == (wayPoints.Count - 1) ? -currentWaypointIndex : 1;
            }
        }
    }
}
