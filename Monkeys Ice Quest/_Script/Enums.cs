using UnityEngine;
using System.Collections;

public class Enums : MonoBehaviour {


}

public enum Power
{
    move,
    freeze,
    swap,
    copy,
    hard,
    rotate,
    collect,
    doubled,
    none
}

public enum Block {
    standardIceBlock,
    fruitBlock,
    woodBlock,
    stoneBlock,
    powerIceBlock
}

public enum Sounds
{
    standardButton,
    levelActive,
    levelInactive
}
