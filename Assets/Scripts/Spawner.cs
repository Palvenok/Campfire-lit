using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Collider spawnArea;

    private Vector3 minBounds;
    private Vector3 maxBounds;

    private void Awake()
    {
        if (spawnArea == null) spawnArea = GetComponent<Collider>();
        spawnArea.isTrigger = true;

        minBounds = spawnArea.bounds.min;
        maxBounds = spawnArea.bounds.max;
    }

    public void Spawn(GameObject item)
    {
        Vector3 pos = Vector3.zero;

        pos.x = Random.Range(minBounds.x, maxBounds.x);
        pos.y = Random.Range(minBounds.y, maxBounds.y);
        pos.z = Random.Range(minBounds.z, maxBounds.z);

        item.transform.position = pos;

        item.transform.rotation = Random.rotation;

    }
}
