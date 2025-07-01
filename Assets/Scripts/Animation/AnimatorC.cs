using System.Collections;
using UnityEngine;


public class AnimatorC : MonoBehaviour
{
    private Animator animator;
    private int currentAnimation;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator not found! Please attach it to the GameObject.");
        }
    }

    public void ChangeAnimation(int Animation, float time = 0.0f,  float crossfade = 0.2f )
    {
        //Get time by AnimatorStateInfo

        if (time > 0 )
        {
            StartCoroutine(Wait());
        }
        else
        {
            Validate();
        }

        IEnumerator Wait()
        {
            yield return new WaitForSeconds(time - crossfade);
            Validate();
        }

        void Validate()
        {
            if (currentAnimation != Animation)
            {
                currentAnimation = Animation;
                animator.CrossFade(Animation, crossfade);
            }
        }
    }


}