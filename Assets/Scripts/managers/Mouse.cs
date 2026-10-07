using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
public class Mousey : MonoBehaviour
{
    public GameObject pref_patineta,pref_vasos,pref_bicicleta,pref_monumento;
    public GameObject go_menu, go_mapa, go_pref, go_activo, go_pausa;
    public Transform t_mouse;
    public SpriteRenderer sr_mouse;
    void Start()
    {
        Cursor.visible=false;
    }
    void Update()
    {
        if(InputController.Instance.escape.WasPressedThisFrame() && GameManager.Instance.GetEstado()=="actividad")
        {
            GameManager.Instance.SetEstado("pausa");  GameManager.Instance.SetEnPausa(true);  go_pausa.SetActive(true);
        }
        if(InputController.Instance.lClick.WasPressedThisFrame())
        {
            CheckFakeButtons();
        }
        MoveMouse();
    }
    void MoveMouse()
    {
        Vector3 mouseScreenPosition=Mouse.current.position.ReadValue();
        Vector3 mouseWorldPosition=Camera.main.ScreenToWorldPoint(new Vector2(mouseScreenPosition.x,mouseScreenPosition.y));
        t_mouse.position=new Vector3(mouseWorldPosition.x,mouseWorldPosition.y,0);
    }
    void CheckFakeButtons()
    {
        Ray ray=Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit2D hit=Physics2D.Raycast(ray.origin,ray.direction);
        Transform col=hit?hit.collider.transform:null;
        Debug.Log(GameManager.Instance.GetEstado());
        if(col!=null){Debug.Log(col.name);}else{Debug.Log("nada linda");}
        if(col && GameManager.Instance.GetEstado()=="menu")
        {
            if(col.name=="0")
            {  GameManager.Instance.SetEstado("mapa");  go_menu.SetActive(false);  go_mapa.SetActive(true);  }//mapa
            else
            // if(col.name=="1")
            // {  GameManager.Instance.SetEstado("pre");  }//ajustes
            // else
            if(col.name=="2")
            {  Application.Quit();  }//salir
        }
        if(col && GameManager.Instance.GetEstado()=="mapa")
        {
            if(col.name=="act_patineta")
            {  GameManager.Instance.SetEstado("pre");  go_pref=pref_patineta;  go_activo=Instantiate(go_pref);  go_mapa.SetActive(false);  /*sr_mouse.enabled=false;*/  }else
            if(col.name=="act_vasos")
            {  GameManager.Instance.SetEstado("pre");  go_pref=pref_vasos;  go_activo=Instantiate(go_pref);  go_mapa.SetActive(false);  }else
            if(col.name=="act_bicicleta")
            {  GameManager.Instance.SetEstado("pre");  go_pref=pref_bicicleta;  go_activo=Instantiate(go_pref);  go_mapa.SetActive(false);  /*sr_mouse.enabled=false;*/  }else

            if(col.name=="pv_monumento" && GameManager.Instance.GetPatinetaDone() /*&& GameManager.Instance.GetBicicletaDone()*/)
            {  GameManager.Instance.SetEstado("actividad");  go_pref=pref_monumento;  go_activo=Instantiate(go_pref);  go_mapa.SetActive(false);  }
        }else
        if(col && GameManager.Instance.GetEstado()=="actividad")
        {
            if(go_activo.name=="patineta(Clone)"){    }else
            if(go_activo.name=="vasos(Clone)"){  col.parent.parent.GetComponent<Vasos>().Resolver(col.name);  }else
            if(go_activo.name=="bicicleta(Clone)"){    }else
            if(go_activo.name=="visual(Clone)"){  go_activo.GetComponent<Monumento>().CheckDistance(col.transform.position.x,int.Parse(col.name));  }
        }else
        if(col && GameManager.Instance.GetEstado()=="pausa")
        {
            if(col.name=="0")
            {  GameManager.Instance.SetEstado("actividad");  go_pausa.SetActive(false);  GameManager.Instance.SetEnPausa(false);  }//sacardepausa
            else
            if(col.name=="1")
            {  GameManager.Instance.SetEstado("pre");  Destroy(go_activo);  go_activo=Instantiate(go_pref);  go_pausa.SetActive(false);  GameManager.Instance.SetEnPausa(false);  }//reiniciar
            else
            if(col.name=="2")
            {  GameManager.Instance.SetEstado("mapa");  go_mapa.SetActive(true);  Destroy(go_activo);  go_activo=null;  sr_mouse.enabled=true;  go_pausa.SetActive(false);  }//mapa
        }
    }
}
