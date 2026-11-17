using UnityEngine;
using UnityEngine.InputSystem;

public class Testeo : MonoBehaviour
{
    public Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnStartity(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            anim.SetTrigger("Start");
        }
    }
}
