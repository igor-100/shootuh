public interface ISaveManager
{
    void AddToSaveRegistry<T>(T saveObject) where T : ISaveable;
    bool TryLoading<T>(T saveObject) where T : ISaveable;
    void ResetSaveRegistry();
    void Save();
    void Load();
    void New();
}
