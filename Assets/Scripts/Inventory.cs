using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    [HideInInspector] public UnityEvent OnItemAdded;

    [SerializeField] private Transform holdPoint;
    [SerializeField] private bool isPlayer;
    [SerializeField] private List<GameObject> _holdedItems = new List<GameObject>();

    public bool IsEmpty => _holdedItems.Count == 0;

    public void PushItem(GameObject item)
    {
        _holdedItems.Add(item);

        item.GetComponent<Collider>().enabled = false;

        if(item.TryGetComponent(out Rigidbody rb))
        {
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
        }


        StartCoroutine(MoveToInventory(item));
    }

    private IEnumerator MoveToInventory(GameObject item)
    {
        item.transform.parent = holdPoint;
        item.transform.forward = holdPoint.forward;

        float lerp = 0;

        while (lerp < 1)
        {
            Vector3 tempPos = Vector3.Lerp(item.transform.position, holdPoint.position, lerp);
            tempPos.y += Mathf.Sin(lerp * Mathf.PI) * .7f;

            item.transform.position = tempPos;

            lerp += Time.deltaTime;
            yield return null;
        }

        if (!isPlayer) item.SetActive(false);

        OnItemAdded?.Invoke();
    }

    public GameObject TakeItem()
    {
        var item = _holdedItems[0];
        item.transform.parent = null;

        if (item.TryGetComponent(out Rigidbody rb))
        {
            rb.isKinematic = false;
        }

        item.GetComponent<Collider>().enabled = true;

        _holdedItems.RemoveAt(0);
        return item;
    }

    private void OnDestroy()
    {
        OnItemAdded.RemoveAllListeners();
    }
}
