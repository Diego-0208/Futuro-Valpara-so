using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    void Awake(){  if(Instance==null){  Instance=this;  DontDestroyOnLoad(gameObject);  }else{  Destroy(gameObject);  return;  }  }
    
    bool patineta_done;
    public bool GetPatinetaDone(){  return patineta_done;  } 
    public void SetPatinetaDone(bool x){  patineta_done=x;  if(x){  sr_actMapa.color=c_green;  sr_pvMapa.color=c_yellow;  }  }

    bool monumento_done;
    public bool GetMonumentoDone(){  return monumento_done;  } 
    public void SetMonumentoDone(bool x){  monumento_done=x;  if(x){  sr_pvMapa.color=c_green;  }  }

    
    [SerializeField]
    private string estado="mapa";
    public string GetEstado(){  return estado;  } 
    public void SetEstado(string x){  estado=x;  }
    public Color c_red,c_yellow,c_green;
    public SpriteRenderer sr_actMapa,sr_pvMapa;

}