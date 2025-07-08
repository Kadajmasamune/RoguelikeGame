using System.Collections.Generic;
using Common;
using UnityEngine;

[RequireComponent(typeof(AnimatorC))]
public abstract class EntityAnimationController : MonoBehaviour
{
    [SerializeField] private AnimStateMachine animStateMachine;

    private AnimatorC animatorC;
    private IHasDirection directionProvider;
    private IHasVelocity VelocityProvider;
    private IHasBooleans booleansProvider;

    private string currentAnimationName = "";



    private void Start()
    {
        animatorC = GetComponent<AnimatorC>();
        directionProvider = GetComponent<IHasDirection>();
        VelocityProvider = GetComponent<IHasVelocity>();
        booleansProvider = GetComponent<IHasBooleans>();


        if (directionProvider == null)
        {
            Debug.LogError($"{name} must implement IHasDirection.");
        }
    }



    public virtual void Animate(Dictionary<string, int> animationDict)
    {

        if (animStateMachine == null || directionProvider == null || VelocityProvider == null || booleansProvider == null) return;

        var SortedStates = new List<AnimState>(animStateMachine.states);
        SortedStates.Sort((a, b) => b.Priority.CompareTo(a.Priority));



        foreach (var state in SortedStates)
        {
            // Debug.Log($"Checking state: {state.AnimationName}, ShouldEnter: {state.ShouldEnter(directionProvider, VelocityProvider.CurrentVelocity)}, Current: {currentAnimationName}");
            if (state.ShouldEnter(directionProvider, VelocityProvider.CurrentVelocity, booleansProvider) && state.AnimationName != currentAnimationName)
            {
                if (animationDict.TryGetValue(state.AnimationName, out int hash))
                {
                    Debug.Log($"Switching to animation: {state.AnimationName}");

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