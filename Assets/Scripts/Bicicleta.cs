using DG.Tweening;
using TMPro;
using UnityEngine;
public class Bicicleta : MonoBehaviour
{
    public Transform t_val;
    float[] _posicionesx=new float[]{-2.3f,2.3f};
    int pos_actual=0;
    bool terminado;
    public Transform[] t_spawn; 
    float timer,timerMax=15;
    public GameObject go_caja;
    public SpriteRenderer sr_green,sr_red;
    float duracionDesplazo=4f,espera=2.75f;
    public Transform t_barraEnergia;
    public GameObject go_final,go_perdiste;
    bool doneBefore;
    void Start()
    {
        timer=timerMax;   doneBefore=GameManager.Instance.GetBicicletaDone();
        Spawn();
        CambiarVelocidad();
    }
    void Update()
    {
        if(!terminado && !GameManager.Instance.GetEnPausa())
        {
            Tiempo();
            CheckInputs();      
        }
    }
    void Tiempo()
    {
        if(timer>0){   timer-=Time.deltaTime;   t_barraEnergia.localScale=new Vector2(timer/timerMax,1);   }
        else{   terminado=true;   Won();   sr_green.DOFade(.4f,.4f).OnComplete(()=>{   sr_green.DOFade(0,.1f);   });   }
    }
    void CheckInputs()
    {
        if(InputController.Instance.a.WasPressedThisFrame() && pos_actual==1)
        {  pos_actual--;  t_val.DOLocalMoveX(_posicionesx[pos_actual],.3f);  }
        if(InputController.Instance.d.WasPressedThisFrame() && pos_actual==0)
        {  pos_actual++;  t_val.DOLocalMoveX(_posicionesx[pos_actual],.3f);  }
    }
    void CambiarVelocidad()
    {
        DOTween.To(()=>duracionDesplazo,x=>duracionDesplazo=x,2f,13f).SetEase(Ease.OutQuad);
        DOTween.To(()=>espera,x=>espera=x,1f,13f).SetEase(Ease.OutQuad);
    }
    async void Spawn()
    {
        if(terminado){   return;   }
        int dir=Random.Range(0,2);
        GameObject n=Instantiate(go_caja,t_spawn[dir]);
        n.transform.DOLocalMove(new Vector2(3*(dir==0?-1:1),-10.93f),duracionDesplazo).OnComplete(()=>{   Destroy(n);   });
        n.transform.DOScale(new Vector2(1.5f,1.5f),duracionDesplazo);
        await GameManager.Instance.Esperar(espera);
        Spawn();
    }
    void Won()
    {
        DOTween.KillAll();
        terminado=true;
        go_final.SetActive(true);
        GameManager.Instance.SetEstado("pausa");
        if(!doneBefore){   GameManager.Instance.SetBicicletaDone(true);   }
    }
    void Lost()
    {
        DOTween.KillAll();
        terminado=true;
        go_perdiste.SetActive(true);
        GameManager.Instance.SetEstado("pausa");
    }
}
