using DG.Tweening;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
public class Mousey : MonoBehaviour
{
    public GameObject pref_patineta,pref_vasos,pref_bicicleta,
                      pref_monumento,pref_ascensor,pref_lancha;
    public GameObject go_menu, go_mapa, go_pref, go_activo, go_pausa, go_pausamapa, go_pausavisual, go_intro;
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
        if(InputController.Instance.escape.WasPressedThisFrame() && GameManager.Instance.GetEstado()=="visual")
        {
            GameManager.Instance.SetEstado("pausa-visual");  GameManager.Instance.SetEnPausa(true);  go_pausavisual.SetActive(true);
        }
        if(InputController.Instance.escape.WasPressedThisFrame() && GameManager.Instance.GetEstado()=="mapa")
        {
            GameManager.Instance.SetEstado("pausa-mapa");  GameManager.Instance.SetEnPausa(true);  go_pausamapa.SetActive(true);
        }
        if(InputController.Instance.escape.WasPressedThisFrame() && GameManager.Instance.GetEstado()=="pre")
        {
            GameManager.Instance.SetEstado("mapa");  go_pref=null;  Destroy(go_activo);  go_mapa.SetActive(true);
        }

        if(InputController.Instance.space.WasPressedThisFrame() && GameManager.Instance.GetEstado()=="intro")
        {
            GameManager.Instance.SetEstado("mapa");  GameManager.Instance.SetPrimera(false);  go_intro.SetActive(false);  go_mapa.SetActive(true);
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
        // Debug.Log(GameManager.Instance.GetEstado());
        // if(col!=null){Debug.Log(col.name);}else{Debug.Log("nada linda");}
        if(col && GameManager.Instance.GetEstado()=="menu")
        {
            if(col.name=="0")
            {   Intro();   }//mapa
            else
            // if(col.name=="1")
            // {  GameManager.Instance.SetEstado("ajustes");  }//ajustes
            // else
            if(col.name=="2")
            {  Application.Quit();  }//salir
        }
        if(col && GameManager.Instance.GetEstado()=="mapa")
        {
            if(col.name=="act_patineta")
            {    GameManager.Instance.SetEstado("pre");  go_pref=pref_patineta;  go_activo=Instantiate(go_pref);  go_mapa.SetActive(false);  /*sr_mouse.enabled=false;*/  }else
            if(col.name=="act_vasos")
            {    GameManager.Instance.SetEstado("pre");  go_pref=pref_vasos;  go_activo=Instantiate(go_pref);  go_mapa.SetActive(false);  }else
            if(col.name=="act_bicicleta")
            {    GameManager.Instance.SetEstado("pre");  go_pref=pref_bicicleta;  go_activo=Instantiate(go_pref);  go_mapa.SetActive(false);  /*sr_mouse.enabled=false;*/  }else

            if(col.name=="pv_ascensor" && GameManager.Instance.GetPatinetaDone() /*&& GameManager.Instance.GetBicicletaDone()*/)
            {    GameManager.Instance.SetEstado("visual");  go_pref=pref_ascensor;  go_activo=Instantiate(go_pref);  go_mapa.SetActive(false);   }
            if(col.name=="pv_monumento" && GameManager.Instance.GetVasosDone() /*&& GameManager.Instance.GetBicicletaDone()*/)
            {    GameManager.Instance.SetEstado("visual");  go_pref=pref_monumento;  go_activo=Instantiate(go_pref);  go_mapa.SetActive(false);   }
            if(col.name=="pv_lancha" && GameManager.Instance.GetBicicletaDone() /*&& GameManager.Instance.GetBicicletaDone()*/)
            {    GameManager.Instance.SetEstado("visual");  go_pref=pref_lancha;  go_activo=Instantiate(go_pref);  go_mapa.SetActive(false);   }
        }else
        if(col && GameManager.Instance.GetEstado()=="actividad")
        {
            if(go_activo.name=="patineta(Clone)"){    }else
            if(go_activo.name=="vasos(Clone)"){  col.parent.parent.GetComponent<Vasos>().Resolver(col.name);   }else
            if(go_activo.name=="bicicleta(Clone)"){    }
        }else
        if(col && GameManager.Instance.GetEstado()=="visual")
        {
            if(go_activo.name=="ascensor(Clone)"){  go_activo.GetComponent<Ascensor>().SetInteractuado(int.Parse(col.name));   }else
            if(go_activo.name=="monumento(Clone)"){  go_activo.GetComponent<Monumento>().SetInteractuado(int.Parse(col.name));   }else
            if(go_activo.name=="lancha(Clone)"){  go_activo.GetComponent<Lancha>().SetInteractuado(int.Parse(col.name));   }
        }else
        if(col && GameManager.Instance.GetEstado()=="pausa")
        {
            if(col.name=="0")
            {   GameManager.Instance.SetEstado("actividad");  go_pausa.SetActive(false);  GameManager.Instance.SetEnPausa(false);  }//sacardepausa
            else
            if(col.name=="1")
            {   GameManager.Instance.SetEstado("pre");  Destroy(go_activo);  go_activo=Instantiate(go_pref);  go_pausa.SetActive(false);  GameManager.Instance.SetEnPausa(false);  }//reiniciar
            else
            if(col.name=="2")
            {   GameManager.Instance.SetEstado("mapa");  go_mapa.SetActive(true);  Destroy(go_activo);  go_activo=null;  sr_mouse.enabled=true;  go_pausa.SetActive(false);  GameManager.Instance.SetEnPausa(false);   }//mapa
        }else
        if(col && GameManager.Instance.GetEstado()=="pausa-visual")
        {
            if(col.name=="0")
            {   GameManager.Instance.SetEstado("visual");  go_pausavisual.SetActive(false);  GameManager.Instance.SetEnPausa(false);  }//sacardepausa
            else
            if(col.name=="1")
            {   GameManager.Instance.SetEstado("mapa");  go_mapa.SetActive(true);  Destroy(go_activo);  go_activo=null;  sr_mouse.enabled=true;  go_pausavisual.SetActive(false);  GameManager.Instance.SetEnPausa(false);   }//mapa
        }else
        if(col && GameManager.Instance.GetEstado()=="pausa-mapa")
        {
            if(col.name=="0")
            {    GameManager.Instance.SetEstado("mapa");  go_pausamapa.SetActive(false);  GameManager.Instance.SetEnPausa(false);  }//mapa
            else
            if(col.name=="1")
            {    GameManager.Instance.SetEstado("menu");  go_pausamapa.SetActive(false);  GameManager.Instance.SetEnPausa(false);  go_menu.SetActive(true);  go_mapa.SetActive(false);  }//menu
        }
    }
    async void Intro()
    {
        if(GameManager.Instance.GetPrimera())
        {
            GameManager.Instance.SetEstado("intro");  go_menu.SetActive(false);  go_intro.SetActive(true);
            await GameManager.Instance.Esperar(2);
            go_intro.transform.GetChild(0).GetComponent<TextMeshPro>().DOColor(Color.white,.5f);
        }
        else
        {
            GameManager.Instance.SetEstado("mapa");  go_menu.SetActive(false);  go_mapa.SetActive(true);
        }
    }
}
