using UnityEngine;

public class MouseAnimation : MonoBehaviour
{
    private Animator anim;
    private bool isClickable = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("isClickable", isClickable);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Boton"))
        {
            isClickable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Boton"))
        {
            isClickable = false;
        }
    }
}
