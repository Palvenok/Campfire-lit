using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public bool Run 
    {
        set; private get;
    }
    public bool Hold
    {
        set; private get;
    }


    private void Awake()
    {
        if (animator == null) animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        animator.SetBool("Run", Run);

        animator.SetFloat("Hold", Mathf.Lerp(
            Hold ? 1f : 0f,
            Hold ? 0f : 1f,
            0
            ));
    }
}
