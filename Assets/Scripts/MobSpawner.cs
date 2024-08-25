using System.Collections;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    private const float CircleDegrees = 360f;

    [SerializeField, Min(0.0f)] private float _spawnInterval = 1.0f;
    [SerializeField] private Mob _objectToSpawn;
    [SerializeField] private Transform[] _spawnPoints;

    private Coroutine _spawnCoroutine;

    private void OnEnable()
    {
        _spawnCoroutine = StartCoroutine(SpawnObjects());
    }

    private void OnDisable()
    {
        if (_spawnCoroutine != null)
            StopCoroutine(_spawnCoroutine);
    }

    private IEnumerator SpawnObjects()
    {
        var delay = new WaitForSeconds(_spawnInterval);

        while (enabled)
        {
            Transform spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
            Mob mob = Instantiate(_objectToSpawn, spawnPoint.position, Quaternion.identity);
            mob.Move(GetRandomDirection());
            yield return delay;
        }
    }

    private Vector3 GetRandomDirection()
    {
        Quaternion rotation = Quaternion.Euler(0, Random.value * CircleDegrees, 0);
        return rotation * Vector3.forward;
    }
}
