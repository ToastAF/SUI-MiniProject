using UnityEngine;

public class AnimationCalls : MonoBehaviour
{
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void switchPlusOff()
    {
        anim.SetBool("Switch to +", false);
    }

    public void switchMinusOff()
    {
        anim.SetBool("Switch to -", false);
    }
}
