using TMPro;
using DG.Tweening;
using UnityEngine;
public class Patineta : MonoBehaviour
{
    float timer,maxTimer=10;
    int which=-1,lastWhich=-1;
    int score,maxScore=10;
    public Transform t_barraEnergia;
    public SpriteRenderer sr_red,sr_green;
    public SpriteRenderer sr_direccion;
    public Sprite[] spr_direccion;
    public Transform t_progreso;
    public GameObject go_final,go_perdiste;
    bool terminado,doneBefore;
    void Start()
    {   timer=maxTimer;
        SpawnDir();  sr_direccion.enabled=true;
        doneBefore=GameManager.Instance.GetPatinetaDone();
        UpdateScore();  }
    void Update()
    {
        Tiempo();
    }
    void Tiempo()
    {
        if(timer>0 && !terminado && !GameManager.Instance.GetEnPausa())
        {   timer-=Time.deltaTime;   t_barraEnergia.localScale=new Vector2(timer/10,1);   CheckInput();   }else
        if(timer<0 && !terminado)
        {   Lost();   }
    }
    void CheckInput()
    {
        if(InputController.Instance.w.WasPressedThisFrame()){  CheckIfCorrect(0);  }else
        if(InputController.Instance.a.WasPressedThisFrame()){  CheckIfCorrect(1);  }else
        if(InputController.Instance.s.WasPressedThisFrame()){  CheckIfCorrect(2);  }else
        if(InputController.Instance.d.WasPressedThisFrame()){  CheckIfCorrect(3);  }
    }
    void CheckIfCorrect(int w)
    {
        if(w==which){  sr_red.DOFade(0,0);  sr_green.DOFade(.4f,.2f).OnComplete(()=>{sr_green.DOFade(0,.075f);});  score++;  UpdateScore();  }
                else{  sr_green.DOFade(0,0);  sr_red.DOFade(.4f,.2f).OnComplete(()=>{sr_red.DOFade(0,.075f);});  timer-=0.2f;  }
        SpawnDir();
    }
    void UpdateScore()
    {   t_progreso.DOLocalMoveX(-3.1f+(6.2f/maxScore*score),0.1f).OnComplete(()=>{   if(score==maxScore){   Won();   }   });   }
    void SpawnDir()
    {
        if(score==maxScore){   sr_direccion.gameObject.SetActive(false);   return;   }
        while(which==lastWhich){   which=Random.Range(0,4);   }
        lastWhich=which;
        sr_direccion.sprite=spr_direccion[which];
    }
    void Won()
    {
        DOTween.KillAll();
        terminado=true;
        go_final.SetActive(true);
        GameManager.Instance.SetEstado("pausa");
        if(!doneBefore){   GameManager.Instance.SetPatinetaDone(true);   }
    }
    void Lost()
    {
        DOTween.KillAll();
        terminado=true;
        go_perdiste.SetActive(true);
        GameManager.Instance.SetEstado("pausa");
    }
}
