using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAnimation : MonoBehaviour
{
    public Rigidbody2D sea;
    // Start is called before the first frame update
    void Start()
    {
        sea = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        sea.velocity = new Vector2(.1f, sea.velocity.y);

        if(sea.position.x > 7.44f)
        {
            sea.position = new Vector2(-10.5f, .45f);
        }
    }
}
