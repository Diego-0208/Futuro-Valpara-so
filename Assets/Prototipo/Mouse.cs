using UnityEngine;
using UnityEngine.InputSystem;
public class Mousey : MonoBehaviour
{
    public GameObject pref_patineta,pref_monumento;
    public GameObject go_activo, go_mapa;
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
            GameManager.Instance.SetEstado("mapa");  go_mapa.SetActive(true);  Destroy(go_activo);  go_activo=null;  sr_mouse.enabled=true;
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
        if(col!=null){Debug.Log(col.name);}else{Debug.Log("nada linda");}
        if(col && GameManager.Instance.GetEstado()=="mapa")
        {
            if(col.name=="act_patineta")
            {  GameManager.Instance.SetEstado("actividad");  go_activo=Instantiate(pref_patineta);  go_mapa.SetActive(false);  sr_mouse.enabled=false;  }else
            if(col.name=="pv_monumento" && GameManager.Instance.GetPatinetaDone())
            {  GameManager.Instance.SetEstado("actividad");  go_activo=Instantiate(pref_monumento);  go_mapa.SetActive(false);  }
        }else
        if(col && GameManager.Instance.GetEstado()=="actividad")
        {
            if(go_activo.name=="actividad(Clone)"){    }else
            if(go_activo.name=="visual(Clone)"){  go_activo.GetComponent<Monumento>().CheckDistance(col.transform.position.x,int.Parse(col.name));  }
        }
    }
}
