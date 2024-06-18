namespace Game.GameLogic.Scripts
{
    using Fusion;
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(Button))]
    public class ClientButton : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(() =>
                BasicSpawner.Instance.StartGame(GameMode.Shared)
            );
        }
    }
}