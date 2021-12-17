public interface IHealthBarView
{
    bool IsShown { get; }
    void Show();
    void Hide();
    void SetHealthBarPercent(float healthPercent);
}
