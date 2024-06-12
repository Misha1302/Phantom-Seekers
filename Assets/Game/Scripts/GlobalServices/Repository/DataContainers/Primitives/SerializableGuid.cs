namespace Game.Scripts.GlobalServices.Repository.DataContainers.Primitives
{
    using System;
    using UnityEngine;

    /// <summary>
    ///     <a href="https://forum.unity.com/threads/cannot-serialize-a-guid-field-in-class.156862/">
    ///         code taken from here
    ///     </a>
    ///     Author: Searous
    /// </summary>
    [Serializable]
    public struct SerializableGuid : ISerializationCallbackReceiver
    {
        [SerializeField] private string guidAsStr;
        private Guid _guid;

        public SerializableGuid(Guid guid)
        {
            _guid = guid;
            guidAsStr = null;
        }

        public void OnAfterDeserialize() => _guid = Guid.Parse(guidAsStr);
        public void OnBeforeSerialize() => guidAsStr = _guid.ToString();

        public override bool Equals(object obj) => obj is SerializableGuid guid && _guid.Equals(guid._guid);
        public override int GetHashCode() => _guid.GetHashCode();

        public override string ToString() => _guid.ToString();

        public static bool operator ==(SerializableGuid a, SerializableGuid b) => a._guid == b._guid;
        public static bool operator !=(SerializableGuid a, SerializableGuid b) => a._guid != b._guid;
        public static implicit operator SerializableGuid(Guid guid) => new(guid);
        public static implicit operator Guid(SerializableGuid serializable) => serializable._guid;
    }
}