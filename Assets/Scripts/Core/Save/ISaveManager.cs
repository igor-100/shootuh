public interface ISaveManager
{
    void AddToSaveRegistry<T>(T saveObject) where T : ISaveable;
    string LoadData<T>(T saveObject) where T : ISaveable;
    void ResetSaveRegistry();
    void Save();
    void Load();
    void New();
}
