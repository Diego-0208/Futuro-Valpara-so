using UnityEngine;
public class PV : MonoBehaviour
{
    public GameObject go_presente,go_futuro;
    public string cual;
    bool primeravez=true;
    void Update()
    {
        if(InputController.Instance.tab.WasPressedThisFrame())
        {
            if(go_presente.activeSelf)
            {   go_presente.SetActive(false);   go_futuro.SetActive(true);   CheckFirst(cual);   }else
            {   go_presente.SetActive(true);   go_futuro.SetActive(false);   }
        }
    }
    void CheckFirst(string x)
    {
        if(!primeravez){   return;   }
        primeravez=false;
        if(x=="ascensor")
        {   GameManager.Instance.SetAscensorDone(true);   }else
        if(x=="monumento")
        {   GameManager.Instance.SetMonumentoDone(true);   }else
        if(x=="lancha")
        {   GameManager.Instance.SetLanchaDone(true);   }
    }
}
