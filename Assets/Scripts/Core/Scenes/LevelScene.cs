using System.Collections;
using UnityEngine;

public class LevelScene : MonoBehaviour
{
    private IGameCamera gameCam;
    private IWarrior warrior;

    private void Awake()
    {
        gameCam = CompositionRoot.GetGameCamera();
        warrior = CompositionRoot.GetWarrior();
    }

    private void Start()
    {
        gameCam.SetTarget(warrior.Transform);
    }
}
