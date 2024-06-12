namespace Game.Scripts.GlobalServices.Input.Services
{
    using System;
    using Game.Scripts.GlobalServices.Input.Axis;

    public interface IInputService : IService
    {
        public Axis2D Input { get; }
        public bool Jump { get; }
        public Action OnPause { get; set; }
    }
}