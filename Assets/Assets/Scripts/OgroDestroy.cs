using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OgroDestroy : MonoBehaviour
{
    [SerializeField] GameObject go;

    private void OnDestroy()
    {
        Destroy(go);
    }
}
