namespace Game.Scripts.EditorHelpers
{
    using System;
    using Game.Scripts.Extensions;
    using UnityEngine;
    using Random = Game.Scripts.Helpers.Random;

    public class EditorRandomChildrenImagesFlipper : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField] private Transform parentOfSprites;
        [SerializeField] private bool changeToFlip;
        [SerializeField] private bool changeToReset;

        private void OnValidate()
        {
            if (changeToFlip) FlipSprites();
            if (changeToReset) ResetSprites();
            changeToFlip = changeToReset = false;
        }

        private void ResetSprites()
        {
            ForAllSprites(x => x.flipX = x.flipY = false);
        }

        private void FlipSprites()
        {
            ForAllSprites(x =>
            {
                x.flipX = Random.Bool(50f);
                x.flipY = Random.Bool(50f);
            });
        }

        private void ForAllSprites(Action<SpriteRenderer> act)
        {
            parentOfSprites.GetComponentsInChildren<SpriteRenderer>().ForAll(act);
        }
#endif
    }
}