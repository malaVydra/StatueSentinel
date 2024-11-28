using UnityEngine;

public class AfterAnimationEvents : MonoBehaviour
{
    //Extensible for any needed event after animation
    
    public void DeleteObject()
    {
        Destroy(gameObject);
    }
}
