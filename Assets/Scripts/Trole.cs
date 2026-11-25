using DG.Tweening;
using TMPro;
using UnityEngine;
public class Trole : MonoBehaviour
{
    public GameObject go_inputsPar;
    public SpriteRenderer[] sr_inputs;
    public Sprite[] sp_inputs;
    int[] aEsperar;
    int lleva;
    public Transform t_val;
    float timer,timerMax=50,timer2=5,inclitimer=10,espera=5;
    bool esperando,terminado,doneBefore;
    public Transform t_barraEnergia,t_barraEnergia2;
    public GameObject go_final,go_perdiste;

    void Start()
    {   Inclinarse();   timer=timerMax;   doneBefore=GameManager.Instance.GetTroleDone();   }
    void Update()
    {
        Tiempo();
        CheckEsperando();
    }
    async void Inclinarse()
    {
        await GameManager.Instance.Esperar(espera);
        if(terminado){   return;   }
        bool lado=Random.Range(0,2)==0;
        t_val.DOLocalRotate(new Vector3(0,0,20f*(lado?1:-1)),inclitimer).OnComplete(()=>{   RecibirInputs();   });
    }
    void Enderezarse()
    {
        if(terminado){   return;   }
        t_barraEnergia2.gameObject.SetActive(false);
        t_val.DOLocalRotate(new Vector3(0,0,0),inclitimer).OnComplete(()=>{   Inclinarse();   });
        timer2=5;   lleva=0;
    }
    void RecibirInputs()
    {
        t_barraEnergia2.gameObject.SetActive(true);
        esperando=true;
        aEsperar=new int[]{Random.Range(0,4),Random.Range(0,4),Random.Range(0,4),Random.Range(0,4)};
        go_inputsPar.SetActive(true);
        for(int i=0; i<4; i++){   sr_inputs[i].sprite=sp_inputs[aEsperar[i]];   }
    }
    void Tiempo()
    {
        if(timer>0 && !terminado && !GameManager.Instance.GetEnPausa())
        {   timer-=Time.deltaTime;  t_barraEnergia.localScale=new Vector2(timer/timerMax,1);   }else
        if(timer<=0 && !terminado)
        {  
           Won();
        }
    }
    void CheckEsperando()
    {
        if(esperando && !terminado && !GameManager.Instance.GetEnPausa())
        {
            if(timer2>0)
            {
                timer2-=Time.deltaTime;
                t_barraEnergia2.localScale=new Vector2(timer2/5,1);

                if(InputController.Instance.w.WasPressedThisFrame())
                {   if(aEsperar[lleva]==0){   sr_inputs[lleva].sprite=null;   lleva++;   }
                    else{   Lost();   }   }else
                if(InputController.Instance.a.WasPressedThisFrame())
                {   if(aEsperar[lleva]==1){   sr_inputs[lleva].sprite=null;   lleva++;   }
                    else{   Lost();   }   }else
                if(InputController.Instance.s.WasPressedThisFrame())
                {   if(aEsperar[lleva]==2){   sr_inputs[lleva].sprite=null;   lleva++;   }
                    else{   Lost();   }   }else
                if(InputController.Instance.d.WasPressedThisFrame())
                {   if(aEsperar[lleva]==3){   sr_inputs[lleva].sprite=null;   lleva++;   }
                    else{   Lost();   }   }

                if(lleva==4)
                {   esperando=false;   Enderezarse();   }
            }else
            {   Lost();   }
        }
    }
    void Won()
    {
        DOTween.KillAll();
        terminado=true;
        go_final.SetActive(true);
        GameManager.Instance.SetEstado("pausa");
        if(!doneBefore){   GameManager.Instance.SetTroleDone(true);   }
    }
    void Lost()
    {
        DOTween.KillAll();
        terminado=true;
        go_perdiste.SetActive(true);
        GameManager.Instance.SetEstado("pausa");
    }
}
