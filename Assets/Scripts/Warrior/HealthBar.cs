using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Vector3 relatedPos;
    [SerializeField] private Vector3 rotation;
    private IAlive aliveObject;

    private void Start()
    {
        aliveObject = transform.parent.GetComponent<IAlive>();
    }

    // Update is called once per frame
    void Update()
    {
        var fixedPosition = transform.parent.position + relatedPos;
        transform.position = fixedPosition;
        transform.rotation = Quaternion.Euler(rotation);
        float healthPercent = aliveObject.GetHealthPercent();
        transform.Find("Bar").localScale = new Vector3(aliveObject.GetHealthPercent(), 1);
    }
}
