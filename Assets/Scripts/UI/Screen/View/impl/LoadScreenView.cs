using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadScreenView : BaseView, ILoadScreenView
{
    public event Action BackClicked = () => { };

    [SerializeField] private Button backButton;
    [SerializeField] private Transform slotsContainer;

    private IResourceManager ResourceManager;

    private void Awake()
    {
        ResourceManager = CompositionRoot.GetResourceManager();

        backButton.onClick.AddListener(OnBackClicked);
    }

    private void OnBackClicked()
    {
        BackClicked();
    }

    public void DisplayLoadSlots(List<SaveFile> saveFiles)
    {
        int heightPosition = 320;
        int heightGap = 160;
        foreach (var saveFile in saveFiles)
        {
            var gameObject = ResourceManager.CreatePrefabInstance(EViews.Load_Slot);
            gameObject.transform.SetParent(slotsContainer);

            var rectTransform = gameObject.GetComponent<RectTransform>();
            rectTransform.localScale = Vector3.one;
            rectTransform.localPosition = new Vector3(0, heightPosition);
            heightPosition -= heightGap;

            var loadSlot = gameObject.GetComponent<LoadSlot>();
            loadSlot.SetSaveFile(saveFile);
        }
    }
}
