using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmController : MonoBehaviour
{
    public GameObject OneARm;
    public GameObject TwoARm;
    
    public void ChangeArmsSkin(bool OneHanded)
    {
        if (OneHanded)
        {
            OneARm .SetActive(false);
            TwoARm .SetActive(true);
        }
        else
        {
            OneARm .SetActive(true);
            TwoARm .SetActive(false);
        }

    }
    
    
}
