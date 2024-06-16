namespace Game.GameLogic.Scripts
{
    public class CursorInitializer
    {
        [Init]
        private void Initialize()
        {
            new InjectField<SceneService>().Value.OnSceneChanged += scene =>
            {
                var cursorService = new InjectField<CursorService>().Value;
                if (scene == "Core")
                    cursorService.Hide();
                else cursorService.Show();
            };
        }
    }
}