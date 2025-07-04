using System.Collections.Generic;
using Common;
using UnityEngine;

[RequireComponent(typeof(AnimatorC))]
public abstract class EntityAnimationController : MonoBehaviour
{
    [SerializeField] private AnimStateMachine animStateMachine;

    private AnimatorC animatorC;
    private IHasDirection directionProvider;

    private string currentAnimationName = "";






    private void Start()
    {
        animatorC = GetComponent<AnimatorC>();
        directionProvider = GetComponent<IHasDirection>();

        if (directionProvider == null)
        {
            Debug.LogError($"{name} must implement IHasDirection.");
        }
    }






    public virtual void Animate(Dictionary<string, int> animationDict)
    {
        if (animStateMachine == null || directionProvider == null) return;

        foreach (var state in animStateMachine.states)
        {
            if (state.ShouldEnter(directionProvider) && state.AnimationName != currentAnimationName)
            {
                if (animationDict.TryGetValue(state.AnimationName, out int hash))
                {
                    animatorC.ChangeAnimation(animationDict, hash);
                    currentAnimationName = state.AnimationName;
                }
                else
                {
                    Debug.LogWarning($"Animation '{state.AnimationName}' not found in dictionary.");
                }
                break;
            }
        }
    }
}