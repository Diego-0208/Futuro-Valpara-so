using UnityEngine;
public class Feria : MonoBehaviour
{
    public GameObject go_final;
    bool terminado,doneBefore;
    void Update()
    {
        if(InputController.Instance.tab.WasPressedThisFrame() && !terminado)
        {   terminado=true;   Won();   }
    }
    void Won()
    {
        // DOTween.KillAll();
        terminado=true;
        go_final.SetActive(true);
        GameManager.Instance.SetEstado("pausa");
        if(!doneBefore){   GameManager.Instance.SetFeriaDone(true);   }
    }
    // void Lost()
    // {
    //     DOTween.KillAll();
    //     terminado=true;
    //     go_perdiste.SetActive(true);
    //     GameManager.Instance.SetEstado("pausa");
    // }
}
