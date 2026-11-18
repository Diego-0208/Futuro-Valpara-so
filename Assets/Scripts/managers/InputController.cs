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
        lUpH= InputSystem.actions.FindAction("LUpHoldable");  lLeftH= InputSystem.actions.FindAction("LLeftHoldable");
        lDownH= InputSystem.actions.FindAction("LDownHoldable");  lRightH= InputSystem.actions.FindAction("LRightHoldable");
        rUpH= InputSystem.actions.FindAction("RUpHoldable");  rLeftH= InputSystem.actions.FindAction("RLeftHoldable");
        rDownH= InputSystem.actions.FindAction("RDownHoldable");  rRightH= InputSystem.actions.FindAction("RRightHoldable");
        lClick= InputSystem.actions.FindAction("LClick");
        space= InputSystem.actions.FindAction("Space");
        escape= InputSystem.actions.FindAction("Escape");
        tab= InputSystem.actions.FindAction("Tab");
    }
    /////////////////////////
    public InputAction wasdAct,arrowsAct;
    public InputAction w,a,s,d;
    public InputAction lUpH,lLeftH,lDownH,lRightH;
    public InputAction up,left,down,right;
    public InputAction rUpH,rLeftH,rDownH,rRightH;
    public InputAction lClick;  public InputAction space;
    public InputAction escape;  public InputAction tab;
}
