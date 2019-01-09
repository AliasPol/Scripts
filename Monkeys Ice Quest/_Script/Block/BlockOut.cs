using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockOut : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IceBlockPhysics block = collision.GetComponent<IceBlockPhysics>();
        if (block != null && block.block == Block.fruitBlock)
        {
            block.DestroyByFalling();
        }
    }
}
