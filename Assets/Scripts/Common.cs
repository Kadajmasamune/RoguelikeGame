using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

namespace Common
{
    public enum Direction
    {
        None,
        Up,
        Down,
        Left,
        Right,
        UpRight,
        BottomRight,
        UpLeft,
        BottomLeft,
    }


    public interface IHasDirection
    {
        Direction CurrentDirection { get; }
    }


    public class AnimHashGenerator
    {
        public void GenerateAnimHash(Dictionary<string, int> AnimationClipHashes , AnimatorController AnimController)
        {
            AnimationClipHashes.Clear();
            var clips = AnimController.animationClips;

            foreach (var clip in clips)
            {
                AnimationClipHashes[clip.name] = Animator.StringToHash(clip.name);
            }
        }
    }

}
