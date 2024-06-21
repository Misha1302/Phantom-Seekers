namespace Game.GameLogic.Scripts.Services
{
    using System;
    using System.Threading.Tasks;
    using UnityEngine.SceneManagement;

    public class SceneService
    {
        public Action<string> OnSceneChanged;

        public async Task LoadScene(string name)
        {
            var loading = SceneManager.LoadSceneAsync(name);
            while (!loading!.isDone)
                await Task.Yield();

            OnSceneChanged?.Invoke(name);
        }
    }
}