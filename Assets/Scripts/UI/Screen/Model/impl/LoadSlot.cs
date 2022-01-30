using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadSlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI saveName;
    [SerializeField] private TextMeshProUGUI saveDate;

    private ISaveManager SaveManager;
    private ISceneLoader SceneLoader;

    public SaveFile SaveFile { get; private set; }

    private void Awake()
    {
        SceneLoader = CompositionRoot.GetSceneLoader();
        SaveManager = CompositionRoot.GetSaveManager();

        var button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        SaveManager.Load(SaveFile.Name);
        SceneLoader.LoadScene(EScenes.GameScene);
    }

    public void SetSaveFile(SaveFile saveFile)
    {
        this.SaveFile = saveFile;
        saveName.text = saveFile.Name.Replace(".json", "");
        saveDate.text = saveFile.DateTime.ToString();
    }
}
