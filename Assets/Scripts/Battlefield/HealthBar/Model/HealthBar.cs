using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private IAlive fighter;
    private IHealthBarView view;

    private void Awake()
    {
        fighter = transform.parent.GetComponent<IAlive>();
        fighter.HealthPercentChanged += OnHealthPercentChanged;

        view = GetComponent<HealthBarView>();
    }

    private void OnHealthPercentChanged(float fighterHealthPercent)
    {
        if (fighterHealthPercent < 1f && fighterHealthPercent > 0f)
        {
            if (!view.IsShown)
            {
                view.Show();
            } 
            view.SetHealthBarPercent(fighterHealthPercent);
        }
        else if (fighterHealthPercent <= 0f)
        {
            view.Hide();
        }
    }
}
