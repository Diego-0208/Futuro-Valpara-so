using UnityEngine;  using UnityEngine.InputSystem;
public class InputController : MonoBehaviour
{
    public InputActionAsset inpAct;  //void OnEnable(){inpAct.FindActionMap("PlayerS").Enable();}  void OnDisable(){inpAct.FindActionMap("PlayerS").Disable();}
    /////////////////////////
    public static InputController Instance;
    void Awake()
    {
        if(Instance==null){  Instance=this;  DontDestroyOnLoad(gameObject);  }else{  Destroy(gameObject);  return;  }
        w= InputSystem.actions.FindAction("w");  a= InputSystem.actions.FindAction("a");
        s= InputSystem.actions.FindAction("s");  d= InputSystem.actions.FindAction("d");
        lClick= InputSystem.actions.FindAction("LClick");
        space= InputSystem.actions.FindAction("Space");
        escape= InputSystem.actions.FindAction("Escape");
        tab= InputSystem.actions.FindAction("Tab");
    }
    /////////////////////////
    public InputAction w,a,s,d;
    public InputAction lClick;  public InputAction space;
    public InputAction escape;  public InputAction tab;
}
