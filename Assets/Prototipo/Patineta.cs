using TMPro;
using DG.Tweening;
using UnityEngine;
public class Patineta : MonoBehaviour
{
    float timer=10;
    bool ended;
    int which=-1,lastWhich=-1;
    int score;
    public Transform t_barraEnergia;
    public SpriteRenderer sr_red,sr_green;
    public SpriteRenderer sr_direccion;
    public Sprite[] spr_direccion;
    public TextMeshPro txt_puntaje,txt_final;
    public GameObject go_final;
    bool doneBefore;
    void Start()
    {  SpawnDir();  sr_direccion.enabled=true;
       doneBefore=GameManager.Instance.GetPatinetaDone();
       UpdateScore();  }
    void Update()
    {
        if(timer>0 && !ended)
        {  timer-=Time.deltaTime;  t_barraEnergia.localScale=new Vector2(timer/10,1);  CheckInput();  }
        else
        if(timer<0 && !ended)
        {  ended=true;  sr_direccion.enabled=false;
           go_final.SetActive(true);
           if(!doneBefore)
           {  if(score<10){  Lost();  }else{  Won();  }  }
           else
           {  JustEnd();  }
        }
        if(InputController.Instance.space.WasPressedThisFrame())
        {  SpawnDir();  }
    }
    void CheckInput()
    {
        if(InputController.Instance.lUp.WasPressedThisFrame())   {  CheckIfCorrect(0);  }else
        if(InputController.Instance.lLeft.WasPressedThisFrame()) {  CheckIfCorrect(1);  }else
        if(InputController.Instance.lDown.WasPressedThisFrame()) {  CheckIfCorrect(2);  }else
        if(InputController.Instance.lRight.WasPressedThisFrame()){  CheckIfCorrect(3);  }
    }
    void CheckIfCorrect(int w)
    {
        if(w==which){  sr_red.DOFade(0,0);  sr_green.DOFade(.4f,.2f).OnComplete(()=>{sr_green.DOFade(0,.075f);});  score++;  UpdateScore();  }
                else{  sr_green.DOFade(0,0);  sr_red.DOFade(.4f,.2f).OnComplete(()=>{sr_red.DOFade(0,.075f);});  timer-=0.2f;  }
        SpawnDir();
    }
    void UpdateScore()
    {
        if(score<10 && !doneBefore){  txt_puntaje.text=score.ToString()+"/10";  }else{  txt_puntaje.text=score.ToString();  }
    }
    void SpawnDir()
    {
        while(which==lastWhich)
        {  which=Random.Range(0,4);  }
        lastWhich=which;
        sr_direccion.sprite=spr_direccion[which];
    }
    void Lost()
    {  txt_final.text="quizás la próxima"; }
    void Won()
    {  txt_final.text="lo lograste !!";  GameManager.Instance.SetPatinetaDone(true);  }
    void JustEnd()
    {  txt_final.text="puntaje: "+score;  }
}
