using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathsParent : MonoBehaviour
{
    public List<Transform> PathPoints = new List<Transform>();
    private Transform _cachedPoint;

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            PathPoints.Add(transform.GetChild(i));
        }
    }

    public Transform GetPath()
    {
        if(_cachedPoint != null) {PathPoints.Add(_cachedPoint);}

        Transform point = PathPoints[Random.Range(0, PathPoints.Count)];
        _cachedPoint = point;
        PathPoints.Remove(point);
        return point;
    }
}
