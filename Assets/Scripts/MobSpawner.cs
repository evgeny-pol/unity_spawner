using System.Collections;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    [SerializeField, Min(0.0f)] private float _spawnInterval = 1.0f;
    [SerializeField] private SpawnPoint[] _spawnPoints;
    [Tooltip("0 = Unlimited")]
    [SerializeField, Min(0)] private int _maxSpawnCount;

    private Coroutine _spawnCoroutine;
    private int _spawnedCount;

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
            if (_maxSpawnCount > 0 && _spawnedCount >= _maxSpawnCount)
                yield break;

            SpawnPoint spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
            spawnPoint.Spawn();

            if (_maxSpawnCount > 0)
                ++_spawnedCount;

            yield return delay;
        }
    }
}
