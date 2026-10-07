using TMPro;
using UnityEngine;
public class Monumento : MonoBehaviour
{
    public Transform t_movible, t_val;
    public bool[] interactuado;
    public TextMeshPro txt_contador;
    public GameObject go_escape;
    bool canSwitch,switched,doneBefore;

    void Start()
    {  if(GameManager.Instance.GetMonumentoDone()){  interactuado[0]=true;  interactuado[1]=true;  interactuado[2]=true;  go_escape.SetActive(true);  }
        UpdateContador();  }

    void Update()
    {
        if(InputController.Instance.lLeftH.IsPressed() && t_movible.localPosition.x<0.145f)
        {  t_movible.localPosition=new Vector2(t_movible.localPosition.x+(Time.deltaTime*.25f),t_movible.position.y);  }
        if(InputController.Instance.lRightH.IsPressed() && t_movible.localPosition.x>-0.335f)
        {  t_movible.localPosition=new Vector2(t_movible.localPosition.x-(Time.deltaTime*.25f),t_movible.position.y);  }
        if(InputController.Instance.tab.WasPressedThisFrame() && canSwitch)
        {  Switch();  switched=!switched;  }
    }

    public void CheckDistance(float x, int i)
    {  float distancia= Mathf.Abs(t_val.position.x+x);  if(distancia<3.5f){  SetInteractuado(i);  }  }

    void SetInteractuado(int x){  interactuado[x]=true;  Debug.Log("interactuado["+x+"] activado");  UpdateContador();  }

    void UpdateContador()
    {
        int actuales=0;
        for(int i=0; i<interactuado.Length; i++)
        {  if(interactuado[i]){  actuales++;  }  }
        txt_contador.text=actuales+"/"+interactuado.Length;
        if(actuales==interactuado.Length)
        {  canSwitch=true;  }
    }

    void Switch()
    {
        if(!doneBefore){  doneBefore=true;  GameManager.Instance.SetMonumentoDone(true);  go_escape.SetActive(true);  }
        for(int i=0; i<interactuado.Length; i++)
        {  t_movible.GetChild(i).GetChild(switched?0:1).gameObject.SetActive(true);  t_movible.GetChild(i).GetChild(switched?1:0).gameObject.SetActive(false);  }
    }
}
