using UnityEngine;
using UnityEngine.InputSystem;


public class HandController : MonoBehaviour
{
    public InputActionReference gripInput;
    public InputActionReference triggerInput;
    
    private Animator animator;
    public bool isGrabbing = false;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!animator) return;

        float grip = gripInput.action.ReadValue<float>();
        float trigger = triggerInput.action.ReadValue<float>();

        animator.SetFloat("Grip", grip);
        animator.SetFloat("Trigger", trigger);

    if (grip ==1.0f){
        isGrabbing = true;
    }
    else{
        isGrabbing = false;
    }

    }
}
