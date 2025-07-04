using Common;
using UnityEngine;

[CreateAssetMenu(menuName = "AnimationStateMachine/IdleState")]
public class IdleState : AnimState
{
    public Direction requiredDirection;

    public override bool ShouldEnter(IHasDirection entity)
    {
        return entity.CurrentDirection == requiredDirection;
    }

}
