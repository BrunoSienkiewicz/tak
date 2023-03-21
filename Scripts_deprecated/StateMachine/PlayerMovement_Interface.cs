using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PlayerState
{
    PlayerState DoState(PlayerMovement_BasedClass player);
}
