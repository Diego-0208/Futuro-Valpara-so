using UnityEngine;  using UnityEngine.InputSystem;
public class InputController : MonoBehaviour
{
    public InputActionAsset inpAct;  //void OnEnable(){inpAct.FindActionMap("PlayerS").Enable();}  void OnDisable(){inpAct.FindActionMap("PlayerS").Disable();}
    /////////////////////////
    public static InputController Instance;
    void Awake()
    {
        if(Instance==null){  Instance=this;  DontDestroyOnLoad(gameObject);  }else{  Destroy(gameObject);  return;  }
        lUp= InputSystem.actions.FindAction("LUp");  lLeft= InputSystem.actions.FindAction("LLeft");
        lDown= InputSystem.actions.FindAction("LDown");  lRight= InputSystem.actions.FindAction("LRight");
        lUpH= InputSystem.actions.FindAction("LUpHoldable");  lLeftH= InputSystem.actions.FindAction("LLeftHoldable");
        lDownH= InputSystem.actions.FindAction("LDownHoldable");  lRightH= InputSystem.actions.FindAction("LRightHoldable");
        rUp= InputSystem.actions.FindAction("RUp");  rLeft= InputSystem.actions.FindAction("RLeft");
        rDown= InputSystem.actions.FindAction("RDown");  rRight= InputSystem.actions.FindAction("RRight");
        rUpH= InputSystem.actions.FindAction("RUpHoldable");  rLeftH= InputSystem.actions.FindAction("RLeftHoldable");
        rDownH= InputSystem.actions.FindAction("RDownHoldable");  rRightH= InputSystem.actions.FindAction("RRightHoldable");
        lClick= InputSystem.actions.FindAction("LClick");
        space= InputSystem.actions.FindAction("Space");
        escape= InputSystem.actions.FindAction("Escape");
        tab= InputSystem.actions.FindAction("Tab");
    }
    /////////////////////////
    public InputAction wasdAct,arrowsAct;
    public InputAction lUp,lLeft,lDown,lRight;
    public InputAction lUpH,lLeftH,lDownH,lRightH;
    public InputAction rUp,rLeft,rDown,rRight;
    public InputAction rUpH,rLeftH,rDownH,rRightH;
    public InputAction lClick;  public InputAction space;
    public InputAction escape;  public InputAction tab;
}
