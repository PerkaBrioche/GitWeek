using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmController : MonoBehaviour
{
    public RawImage RAW_LeftArm;
    public RawImage RAW_RightArm;
    public RawImage RAW_MiddleArm;

    public GameObject OneARm;
    public GameObject TwoARm;
    
    public void ChangeArmsSkin(bool OneHanded, Texture ArmTexture)
    {
        if (OneHanded)
        {
            OneARm .SetActive(true);
            TwoARm .SetActive(false);
            RAW_MiddleArm.texture = ArmTexture;
        }
        else
        {
            OneARm .SetActive(false);
            TwoARm .SetActive(true);
            RAW_RightArm.texture = ArmTexture;
        }

    }
    
    
}
