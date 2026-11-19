using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonSpeech : MonoBehaviour
{
    public Animator anim;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Botoncio()
    {
        anim.SetTrigger("Boton");
    }

    public void CambiarEscena()
    {
        SceneManager.LoadScene("FinalSpeech");
    }
}
