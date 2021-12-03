using UnityEngine;

public abstract class BaseView : MonoBehaviour, IView
{
    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void SetParent(Transform parentCanvas)
    {
        transform.SetParent(parentCanvas, false);
    }
}
