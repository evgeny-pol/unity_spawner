using UnityEngine;

public class GameArea : MonoBehaviour
{
    [SerializeField] private string _objectTag;

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(_objectTag))
        {
            Destroy(other.gameObject);
        }
    }
}
