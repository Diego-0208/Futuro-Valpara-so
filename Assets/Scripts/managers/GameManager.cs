using UnityEngine;
using Cysharp.Threading.Tasks;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    void Awake(){  if(Instance==null){  Instance=this;  DontDestroyOnLoad(gameObject);  }else{  Destroy(gameObject);  return;  }  }

    bool patineta_done;
    public bool GetPatinetaDone(){  return patineta_done;  } 
    public void SetPatinetaDone(bool x){  patineta_done=x;  if(x){  sr_patineta.color=c_green;  CheckCompletos();  }  }

    bool vasos_done;
    public bool GetVasosDone(){  return vasos_done;  } 
    public void SetVasosDone(bool x){  vasos_done=x;  if(x){  sr_vasos.color=c_green;  CheckCompletos();  }  }

    bool bicicleta_done=true;
    public bool GetBicicletaDone(){  return bicicleta_done;  } 
    public void SetBicicletaDone(bool x){  bicicleta_done=x;  if(x){  sr_bicicleta.color=c_green;  CheckCompletos();  }  }

    bool monumento_done;
    public bool GetMonumentoDone(){  return monumento_done;  } 
    public void SetMonumentoDone(bool x){  monumento_done=x;  if(x){  sr_pvMonumento.color=c_green;  }  }

    bool ascensor_done;
    public bool GetAscensorDone(){  return ascensor_done;  } 
    public void SetAscensorDone(bool x){  ascensor_done=x;  if(x){  sr_pvAscensor.color=c_green;  }  }

    bool lancha_done;
    public bool GetLanchaDone(){  return lancha_done;  } 
    public void SetLanchaDone(bool x){  lancha_done=x;  if(x){  sr_pvLancha.color=c_green;  }  }

    public bool en_pausa;
    public bool GetEnPausa(){  return en_pausa;  }
    public void SetEnPausa(bool x){  en_pausa=x;  }

    bool primeravez=true;
    public bool GetPrimera(){  return primeravez;  }
    public void SetPrimera(bool x){  primeravez=x;  }


    void CheckCompletos()
    {
        if(vasos_done /*&& bicicleta_done*/){  sr_pvMonumento.color=c_yellow;  }
        if(bicicleta_done /*&& bicicleta_done*/){  sr_pvLancha.color=c_yellow;  }
        if(patineta_done /*&& bicicleta_done*/){  sr_pvAscensor.color=c_yellow;  }
    }

    [SerializeField]
    private string estado="menu";
    public string GetEstado(){  return estado;  } 
    public void SetEstado(string x){  estado=x;  }
    public Color c_red,c_yellow,c_green;
    public SpriteRenderer sr_patineta,sr_vasos,sr_bicicleta,
                          sr_pvMonumento,sr_pvAscensor,sr_pvLancha;
    public async UniTask Esperar(float segundos)
    {
        float tiempoRestante=segundos;  while (tiempoRestante>0)
        {  if(en_pausa){  await UniTask.Yield();  continue;  }  tiempoRestante -= Time.deltaTime;  await UniTask.Yield();  }
    }
}