using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public void FootStep(float theValue)
    {
        //FMODManager.Instance.PlaySound("event:/SFX_Footsteps");
        FMODManager.Instance.PlaySound("event:/SFX_ShotFriend");
    }
}
