using UnityEditor.Animations;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private AnimatorC animatorC;
    private PlayerController playerController;

    private string PlayerSpritesheet = "";

    private readonly int IDLE = Animator.StringToHash("");


    AnimatorController CreateAnimatorController()
    {
        return null;
    }

    AnimationClip GenerateAnimationClips()
    {
        return null;
    }
}
