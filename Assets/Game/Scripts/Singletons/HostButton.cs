namespace Game.Scripts.Singletons
{
    using Fusion;
    using Game.GameLogic.Scripts;
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(Button))]
    public class HostButton : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(() =>
                BasicSpawner.Instance.StartGame(GameMode.Host)
            );
        }
    }
}