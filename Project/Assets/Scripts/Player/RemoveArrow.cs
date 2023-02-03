using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveArrow : MonoBehaviour
{
    private int numberOfChildren;

    // Start is called before the first frame update
    void Start()
    {
        numberOfChildren = transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (numberOfChildren < transform.childCount)
        {
            foreach (Transform item in transform)
            {
                if (item.tag == "Arrow")
                {
                    Destroy(item.gameObject);
                    return;
                }
            }
        }
    }
}
