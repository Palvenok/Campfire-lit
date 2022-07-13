using System.Collections;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private float respawnDelay = 1f;
    [SerializeField] private Inventory inventory;
    [SerializeField] private Spawner spawner;

    private bool _isInUpload;
    private Coroutine _coroutine;

    private void Awake()
    {
        inventory.OnItemAdded.AddListener(UploadInventory);
    }
    private void Start()
    {
        UploadInventory();
    }
    private void UploadInventory()
    {
        if (_isInUpload) StopCoroutine(_coroutine);
        _isInUpload = true;
        _coroutine = StartCoroutine(UploadInventoryCor());
    }

    private IEnumerator UploadInventoryCor()
    {
        yield return new WaitForSeconds(respawnDelay);

        while (!inventory.IsEmpty)
        {
            var item = inventory.TakeItem();
            spawner.Spawn(item);

            yield return new WaitForSeconds(respawnDelay);
        }

        _isInUpload = false;
    }
}
