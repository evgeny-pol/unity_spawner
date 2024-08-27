using UnityEngine;

[RequireComponent(typeof(Creature))]
public class Patroller : MonoBehaviour
{
    [SerializeField, Min(0.1f)] private float _approachDistance = 1f;
    [SerializeField] private Transform[] _points;

    private int _currentPointIndex;
    private Creature _creature;

    private void Awake()
    {
        _creature = GetComponent<Creature>();
    }

    private void Start()
    {
        GoToCurrentPoint();
    }

    private void Update()
    {
        Transform currentPoint = _points[_currentPointIndex];

        if (VectorUtils.DistanceBetween(transform.position, currentPoint.position) <= _approachDistance)
        {
            _currentPointIndex = (_currentPointIndex + 1) % _points.Length;
            GoToCurrentPoint();
        }
    }

    private void GoToCurrentPoint()
    {
        if (_currentPointIndex >= _points.Length)
        {
            enabled = false;
            return;
        }

        _creature.Follow(_points[_currentPointIndex]);
    }
}
