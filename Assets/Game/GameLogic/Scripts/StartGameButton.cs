namespace Game.GameLogic.Scripts
{
    using Fusion;
    using UnityEngine;
    using UnityEngine.UI;

    public class StartGameButton : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(() =>
                BasicSpawner.Instance.StartGame(GameMode.Shared)
            );
        }
    }
}