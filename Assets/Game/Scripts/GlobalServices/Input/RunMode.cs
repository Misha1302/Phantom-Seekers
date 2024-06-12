namespace Game.Scripts.GlobalServices.Input
{
    using UnityEngine;
#if UNITY_EDITOR
    using SystemInfo = UnityEngine.Device.SystemInfo;
#endif


    public static class RunMode
    {
        public static bool IsEditorSimulator()
        {
#if UNITY_EDITOR
            return SystemInfo.deviceType != DeviceType.Desktop;
#else
            return false;
#endif
        }
    }
}