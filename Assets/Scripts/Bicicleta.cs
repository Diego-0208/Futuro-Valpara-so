using DG.Tweening;
using TMPro;
using UnityEngine;
public class Bicicleta : MonoBehaviour
{
    public Transform t_val;
    float[] _posicionesx=new float[]{-3.7f,0,3.7f};
    int pos_actual=1;
    bool ended,listo;
    public Transform t_movible;
    public Transform[] t_spawn; 
    float timer,espera=1; //20s
    public GameObject go_caja;
    public TextMeshPro txt_distancia;
    float aceleracion=-1.5f,velocidad_final,alos15;
    public SpriteRenderer sr_green,sr_red;
    void Start()
    {
        velocidad_final=aceleracion*15f;
        alos15=.5f*aceleracion*Mathf.Pow(15f, 2f)-5;
        Spawn();
    }
    void Update()
    {
        if(!ended && !GameManager.Instance.GetEnPausa())
        {
            if(InputController.Instance.lLeft.WasPressedThisFrame() && pos_actual>0)
            {  pos_actual--;  t_val.DOLocalMoveX(_posicionesx[pos_actual],.3f);  }
            if(InputController.Instance.lRight.WasPressedThisFrame() && pos_actual<2)
            {  pos_actual++;  t_val.DOLocalMoveX(_posicionesx[pos_actual],.3f);  }

            timer+=Time.deltaTime;  Debug.Log((int)timer);
            
            float eje_y;
            if (timer<15)
            {  eje_y=.5f*aceleracion*Mathf.Pow(timer, 2f)-5;  txt_distancia.text=-(int)(t_movible.position.y*.5f)+"m\n/ 150m";  }else
            {  float extraTime=timer-15f;  eje_y=alos15+(velocidad_final*extraTime);  txt_distancia.text=-(int)(t_movible.position.y*.5f)+"m";  }
            
            if(timer>=15 && !listo){  listo=true;  sr_green.DOFade(.4f,.4f).OnComplete(()=>{sr_green.DOFade(0,.1f);});  }

            t_movible.position=new Vector3(0,eje_y,0);
            



        }
    }
    async void Spawn()
    {
        await GameManager.Instance.Esperar(espera);
        GameObject n=Instantiate(go_caja,t_spawn[Random.Range(0,3)]);
        n.transform.parent=t_movible;
        Spawn();
    } 
}
