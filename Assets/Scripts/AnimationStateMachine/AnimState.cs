using UnityEngine;
using Common;


[CreateAssetMenu(menuName = "AnimationStateMachine/AnimState")]
public abstract class AnimState : ScriptableObject
{
    public string AnimationName;
    
    public abstract bool ShouldEnter(IHasDirection entity);
}
