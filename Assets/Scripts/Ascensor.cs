using DG.Tweening;
using TMPro;
using UnityEngine;
public class Ascensor : MonoBehaviour
{
    public Transform t_items;
    public bool[] interactuado;
    public TextMeshPro txt_contador;
    public GameObject go_escape;
    bool canSwitch,switched,doneBefore;

    void Start()
    {  if(GameManager.Instance.GetAscensorDone())
       {  interactuado[0]=true;  interactuado[1]=true;  interactuado[2]=true;  go_escape.SetActive(true);  }
       UpdateContador();  }
    void Update()
    {
        if(InputController.Instance.tab.WasPressedThisFrame() && canSwitch)
        {  Switch();  switched=!switched;  }
    }
    public void SetInteractuado(int x){  interactuado[x]=true;  Debug.Log("interactuado["+x+"] activado");  UpdateContador();  }
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
        if(!doneBefore){  doneBefore=true;  GameManager.Instance.SetAscensorDone(true);  Completado();    }
        for(int i=0; i<interactuado.Length; i++)
        {  t_items.GetChild(i).GetChild(switched?0:1).gameObject.SetActive(true);  t_items.GetChild(i).GetChild(switched?1:0).gameObject.SetActive(false);   }
    }
    async void Completado()
    {
        go_escape.SetActive(true);
        await GameManager.Instance.Esperar(3);
        go_escape.GetComponent<TextMeshPro>().DOColor(Color.clear,.5f);
    }
}
