using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Vector3 relatedPos;
    [SerializeField] private Vector3 rotation;

    private IAlive fighter;
    private float fighterHealthPercent;
    private Transform barTransform;
    private Transform containerTransform;

    private void Start()
    {
        fighter = transform.parent.GetComponent<IAlive>();
        containerTransform = transform.Find("Container");
        barTransform = containerTransform.Find("Bar");
    }

    // Update is called once per frame
    void Update()
    {
        fighterHealthPercent = fighter.HealthPercent;
        // Implement Event OnHit
        if (fighterHealthPercent < 1f && fighterHealthPercent > 0f)
        {
            containerTransform.gameObject.SetActive(true);
            var fixedPosition = transform.parent.position + relatedPos;
            transform.position = fixedPosition;
            transform.rotation = Quaternion.Euler(rotation);
            barTransform.localScale = new Vector3(fighterHealthPercent, 1);
        }
        else if (fighterHealthPercent <= 0f)
        {
            containerTransform.gameObject.SetActive(false);
        }
    }
}
