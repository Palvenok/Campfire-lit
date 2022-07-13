using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Transform holdPoint;
    [SerializeField] private List<GameObject> items = new List<GameObject>();

    private Stack<GameObject> _holdedItems;

    public bool IsEmpty => _holdedItems.Count == 0;

    private void Start()
    {
        _holdedItems = new Stack<GameObject>();

        foreach (var item in items)
        {
            PushItem(item);
        }

        items.Clear();
    }

    public void PushItem(GameObject item)
    {
        _holdedItems.Push(item);

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
            tempPos.y += Mathf.Sin(lerp * Mathf.PI) * .3f;

            item.transform.position = tempPos;

            lerp += Time.deltaTime;
            yield return null;
        }
    }

    public GameObject TakeItem()
    {
        var item = _holdedItems.Pop();
        item.transform.parent = null;

        if (item.TryGetComponent(out Rigidbody rb))
        {
            rb.isKinematic = false;
        }

        item.GetComponent<Collider>().enabled = true;

        return item;
    }
}
