using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Release
{
    public void FreeBlock(Rigidbody selectedBlock)
    {
        selectedBlock.constraints = RigidbodyConstraints.None;
        selectedBlock = null;
        Debug.Log("Release: FreeBlock");
    }
}
