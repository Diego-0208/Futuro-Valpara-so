using DG.Tweening;
using TMPro;
using UnityEngine;
public class Metro : MonoBehaviour
{
    public Transform t_borde,t_barraEnergia;
    Transform[] t_direcciones=new Transform[4];
    SpriteRenderer[] sr_direcciones=new SpriteRenderer[4];
    int _posActual=-1;
    public Color c_off,c_on;
    int _score;
    public TextMeshPro txt_score;
    bool terminado,doneBefore;
    float timer=20;
    public GameObject go_final,go_perdiste;
    void Start()
    {
        if(GameManager.Instance.GetMetroDone()){   doneBefore=true;   }

        for(int i=0; i<4; i++)
        {   t_direcciones[i]=t_borde.parent.GetChild(i);  sr_direcciones[i]=t_borde.parent.GetChild(i).GetComponent<SpriteRenderer>();   }
        CambiarPosicion();
    }
    void Update()
    {   Tiempo();   Inputs();   }
    void Tiempo()
    {
        if(timer>0 && !terminado && !GameManager.Instance.GetEnPausa() && _score<30)
        {   timer-=Time.deltaTime;  t_barraEnergia.localScale=new Vector2(timer/20,1);   }else
        if(timer<=0 && !terminado)
        {   Lost();   }else
        if(_score==30 && !terminado)
        {   Won();   }
    }
    void Inputs()
    {
        if(InputController.Instance.w.WasPressedThisFrame() && _posActual==0 && !terminado)
        {   SumarPuntaje();   }
        if(InputController.Instance.a.WasPressedThisFrame() && _posActual==1 && !terminado)
        {   SumarPuntaje();   }
        if(InputController.Instance.s.WasPressedThisFrame() && _posActual==2 && !terminado)
        {   SumarPuntaje();   }
        if(InputController.Instance.d.WasPressedThisFrame() && _posActual==3 && !terminado)
        {   SumarPuntaje();   }
    }
    async void CambiarPosicion()
    {
        int nuevo=_posActual;
        while(nuevo==_posActual){   nuevo=Random.Range(0,4);   }
        _posActual=nuevo;
        t_borde.DOMove(t_direcciones[nuevo].position,.15f);
        for(int i=0; i<4; i++){   sr_direcciones[i].color=c_off;   }
        sr_direcciones[nuevo].color=c_on;
        await GameManager.Instance.Esperar(Random.Range(2,2));
        if(!terminado){   CambiarPosicion();   }
    }
    void SumarPuntaje()
    {
        _score++;
        if(_score<30)
        {   txt_score.text=_score+"/30";   }else{   txt_score.text="30/30";   Won();   }
    }
    void Won()
    {
        DOTween.KillAll();
        terminado=true;
        go_final.SetActive(true);
        GameManager.Instance.SetEstado("pausa");
        if(!doneBefore){   GameManager.Instance.SetMetroDone(true);   }
    }
    void Lost()
    {
        DOTween.KillAll();
        terminado=true;
        go_perdiste.SetActive(true);
        GameManager.Instance.SetEstado("pausa");
    }
}
