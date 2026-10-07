using DG.Tweening;
using TMPro;
using UnityEngine;
public class Vasos : MonoBehaviour
{
    int _cantidadCambios=10;
    public float _tiempoDeCambio;
    public Transform[] t_vasos;
    public Transform t_moneda;
    float[] v_posicionesx=new float[]{-1.6f,0,1.6f};
    float yPos=-1.90f;
    int[] actualPos=new int[]{0,1,2};
    bool listos;
    public SpriteRenderer sr_green,sr_red;
    float timer=5;
    bool ended;
    public Transform t_barraEnergia;
    public GameObject go_final;
    bool doneBefore;
    public TextMeshPro txt_final;
    bool vaso_elegido,vaso_correcto,resolviendo;
    void Start()
    {
        Inicio();
    }
    void Update()
    {
        if(timer>0 && !ended && listos && !vaso_elegido  && !GameManager.Instance.GetEnPausa())
        {  timer-=Time.deltaTime;  t_barraEnergia.localScale=new Vector2(timer/5,1);  }
        else
        if((timer<0 && !ended) || resolviendo)
        {  ended=true;
           go_final.SetActive(true);
           GameManager.Instance.SetEstado("pausa");
           if(!doneBefore)
           {  if(!vaso_correcto){  Lost();  }else{  Won();  }  }
           else
           {  JustEnd();  }
        }
    }
    async void Inicio()
    {
        await GameManager.Instance.Esperar(1);

        t_vasos[0].DOLocalMoveY(yPos,.2f);  t_vasos[1].DOLocalMoveY(yPos,.2f);  t_vasos[2].DOLocalMoveY(yPos,.2f);
        await GameManager.Instance.Esperar(.3f);
        
        t_moneda.parent=t_vasos[1];
        Cambio();
    }
    async void Cambio()
    {
        if(_cantidadCambios<1){  listos=true;  Habilitados();  return;  }
        // Debug.Log(actualPos[0]+""+actualPos[1]+""+actualPos[2]);
        _cantidadCambios-=1;
        int v1=Random.Range(0,3);
        int v2=v1;  int temp=actualPos[v1];
        while(v2==v1){  v2=Random.Range(0,3);  }

        t_vasos[v1].DOMoveX(v_posicionesx[actualPos[v2]],_tiempoDeCambio);
        t_vasos[v2].DOMoveX(v_posicionesx[actualPos[v1]],_tiempoDeCambio);
        actualPos[v1]=actualPos[v2];  actualPos[v2]=temp;
        await GameManager.Instance.Esperar(_tiempoDeCambio*1.1f);

        // Debug.Log(actualPos[0]+""+actualPos[1]+""+actualPos[2]);
        _tiempoDeCambio*=0.85f;  Cambio();
    }
    void Habilitados()
    {
        t_vasos[0].GetComponent<BoxCollider2D>().enabled=true;
        t_vasos[1].GetComponent<BoxCollider2D>().enabled=true;
        t_vasos[2].GetComponent<BoxCollider2D>().enabled=true;
    }
    public void Resolver(string nombre)
    {  int elegido=int.Parse(nombre);  ResolverReal(elegido);  }
    async void ResolverReal(int elegido)
    {
        t_moneda.parent=t_moneda.parent.parent;
        vaso_elegido=true;
        t_vasos[elegido].DOLocalMoveY(0,.3f);
        if(elegido==1)
        {  sr_green.DOFade(.4f,.4f).OnComplete(()=>{sr_green.DOFade(0,.1f);});  vaso_correcto=true;  }
        else
        {  sr_red.DOFade(.4f,.4f).OnComplete(()=>{sr_red.DOFade(0,.1f);});  }
        await GameManager.Instance.Esperar(.7f);

        t_vasos[0].DOLocalMoveY(0,.3f);  t_vasos[1].DOLocalMoveY(0,.3f);  t_vasos[2].DOLocalMoveY(0,.3f);
        await GameManager.Instance.Esperar(.7f);

        resolviendo=true;
    }
    void Lost()
    {  txt_final.text="quizás la próxima"; }
    void Won()
    {  txt_final.text="lo lograste !!";  GameManager.Instance.SetVasosDone(true);  }
    void JustEnd()
    {  txt_final.text="quizás la próxima";  }
}
