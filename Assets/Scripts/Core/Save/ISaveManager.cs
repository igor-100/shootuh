public interface ISaveManager
{
    bool IsLoading { get; }

    void AddToSaveRegistry<T>(T saveObject) where T : ISaveable;
    string LoadData<T>(T saveObject) where T : ISaveable;
    void ResetSaveRegistry();
    
    void Save();
    void Load();
    void Load(string fileName);
    void New();
}
