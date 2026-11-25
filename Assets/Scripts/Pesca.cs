using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
public class Pesca : MonoBehaviour
{
    public Transform t_movible,t_pez,t_barrapez;
    float timer,timerMax=20,captura;
    bool derecha,dentro;
    public Transform t_barraEnergia;
    bool terminado;
    bool doneBefore;
    public GameObject go_final,go_perdiste;
    void Start()
    {
        if(GameManager.Instance.GetPescaDone()){   doneBefore=true;   }
        timer=timerMax;
        Pez();
    }
    void Update()
    {
        Tiempo();
        Jugador();
        CheckAdentro();
        Llenar();
    }
    void Tiempo()
    {
        if(timer>0 && !terminado && !GameManager.Instance.GetEnPausa() && captura<1)
        {   timer-=Time.deltaTime;  t_barraEnergia.localScale=new Vector2(timer/timerMax,1);   }else
        if(timer<=0 && !terminado)
        {   terminado=true;   Lost();   }
    }
    void Jugador()
    {
        if(timer<=0 || captura>=1){   return;   }
        if(timer>0 && t_movible.localPosition.x>.26 && !derecha && captura<1)
        {   t_movible.localPosition=new Vector2(t_movible.localPosition.x-(Time.deltaTime*2),0);   }else
        if(timer>0 && t_movible.localPosition.x<2.3f && derecha && captura<1)
        {   t_movible.localPosition=new Vector2(t_movible.localPosition.x+(Time.deltaTime*2),0);   }
        if(InputController.Instance.space.WasPressedThisFrame())
        {  derecha=!derecha;  }
    }
    async void Pez()
    {
        if(timer<=0 || captura>=1){   return;   }
        float newPos=t_pez.localPosition.x;
        while(Mathf.Abs((newPos+t_pez.localPosition.x)/2-newPos)<.15f)
        {   newPos=Random.Range(.3f,2.2f);   }
        float tiempo=Random.Range(2,3);

        t_pez.DOLocalMoveX(newPos,tiempo/2);
        await GameManager.Instance.Esperar(tiempo);
        Pez();
    }
    void CheckAdentro()
    {
        if(timer<=0 || captura>=1){   return;   }
        float cPos=t_movible.localPosition.x, pPos=t_pez.localPosition.x;
        if(Mathf.Abs((Mathf.Abs(cPos)+Mathf.Abs(pPos))/2-Mathf.Abs(cPos))<.14f)
        {   dentro=true;   }else
        {   dentro=false;   }
    }
    void Llenar()
    {
        if(timer<=0 || captura>=1){   return;   }
        if(dentro)
        {   captura+=Time.deltaTime*.2f;   }else
        if(!dentro && captura>0)
        {   captura-=Time.deltaTime*.07f;   }
        t_barrapez.localScale=new Vector3(captura,1);
        if(captura>=1)
        {   Won();   }
    }
    void Won()
    {
        DOTween.KillAll();
        terminado=true;
        go_final.SetActive(true);
        GameManager.Instance.SetEstado("pausa");
        if(!doneBefore){   GameManager.Instance.SetPescaDone(true);   }
    }
    void Lost()
    {
        DOTween.KillAll();
        terminado=true;
        go_perdiste.SetActive(true);
        GameManager.Instance.SetEstado("pausa");
    }
}
