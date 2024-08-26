using UnityEngine;

public class GameArea : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out GameAreaObject _))
            Destroy(other.gameObject);
    }
}
