using TMPro;
using UnityEngine;

public class HUDLevelView : BaseView, IHUDLevelView
{
    [SerializeField] private TextMeshProUGUI levelTextComponent;
    [SerializeField] private Transform barTransform;

    public void SetLevelText(string levelText)
    {
        levelTextComponent.text = levelText;
    }

    public void SetLevelBarPercent(float levelPercent)
    {
        if (1 == levelPercent)
        {
            barTransform.localScale = new Vector3(0, 1);
        }
        else
        {
            barTransform.localScale = new Vector3(levelPercent, 1);
        }
    }
}
