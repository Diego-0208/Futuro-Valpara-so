using TMPro;
using UnityEngine;
public class Pregame : MonoBehaviour
{
    float timer=8;
    bool ended;
    public MonoBehaviour _script;
    public TextMeshPro txt_contador;
    void Update()
    {
        if(timer>0){  timer-=Time.deltaTime;  }else
        if(!ended){  ended=true;  txt_contador.text="Presiona\n[ Espacio ]\npara jugar.";  }
        if(ended && InputController.Instance.space.WasPressedThisFrame())
        {  _script.enabled=true;  Destroy(gameObject);  GameManager.Instance.SetEstado("actividad");  }
    }
}
