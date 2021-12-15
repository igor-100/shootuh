using UnityEngine;

public class HealthBarView : MonoBehaviour, IHealthBarView
{
    [SerializeField] private Vector3 relatedPos;
    [SerializeField] private Vector3 rotation;
    [SerializeField] private Transform barTransform;
    [SerializeField] private Transform containerTransform;

    public bool IsShown { get; private set; }

    private void Update()
    {
        var fixedPosition = transform.parent.position + relatedPos;
        transform.position = fixedPosition;
        transform.rotation = Quaternion.Euler(rotation);
    }

    public void SetHealthBarPercent(float healthPercent)
    {
        barTransform.localScale = new Vector3(healthPercent, 1);
    }

    public void Show()
    {
        IsShown = true;
        containerTransform.gameObject.SetActive(true);
    }
    public void Hide()
    {
        IsShown = false;
        containerTransform.gameObject.SetActive(false);
    }
}
