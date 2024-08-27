using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private Creature _objectToSpawn;
    [SerializeField] private Transform _target;

    public void Spawn()
    {
        Creature creature = Instantiate(_objectToSpawn, transform.position, Quaternion.identity);
        creature.Follow(_target);
    }
}
