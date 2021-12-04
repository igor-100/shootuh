using UnityEngine;

public class HealthBarView : MonoBehaviour, IHealthBarView
{
    [SerializeField] private Vector3 relatedPos;
    [SerializeField] private Vector3 rotation;
    [SerializeField] private Transform barTransform;
    [SerializeField] private Transform containerTransform;

    public bool IsShown { get => isShown; }

    private bool isShown;

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
        isShown = true;
        containerTransform.gameObject.SetActive(true);
    }
    public void Hide()
    {
        isShown = false;
        containerTransform.gameObject.SetActive(false);
    }
}
