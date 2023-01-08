using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrollUV : MonoBehaviour
{

    //following https://youtu.be/nGw_UBJQPDY Unity Tutorial: Infinitely Scrolling Background [Starfield for Space Games] by quill18creates

    

    // Update is called once per frame
    void Update()
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();

        Material mat = mr.material;

        Vector2 offset = mat.mainTextureOffset;

        offset.x = transform.position.x / 200;

        offset.y = transform.position.y / 50;

        mat.mainTextureOffset = offset;
    }
}
