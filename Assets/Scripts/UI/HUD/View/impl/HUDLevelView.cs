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
        barTransform.localScale = new Vector3(levelPercent, 1);
    }
}
