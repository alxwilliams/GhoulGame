using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct GameSettings
{
    //main controller variables
    public const float WALK_SPEED = 30;
    public const float MAX_GROUND_WALK_ANGLE = 55;
    public const float JUMP_FORCE = 85;
    public const float BOTTOM_SWIRL_FADE_DISTANCE = 15f;
    public const float GRAVITY = 3f;
    public const float TERMINALVELOCITY = -2.2f;

}
public enum PixelStates
{
    MaxPixel,
    MiddlePixel,
    MinPixel,
    NoPixel
}

public enum GameStates
{
    WorldState,
    TownState,
    BattleState,
    CutsceneState
}

public class TriggerSwitchPackage
{
    public Vector3 newAngle;
    public float distanceBack;
    public Vector3 offsetFromPlayer;

    public TriggerSwitchPackage(Vector3 angle, float distance, Vector3 offset)
    {
        newAngle = angle;
        distanceBack = distance;
        offsetFromPlayer = offset;
    }
}

public struct GamePadInput {
    public const string LEFT_STICK = "Move";
    public const string RIGHT_STICK = "Look";
    public const string RIGHT_HORIZONTAL = "RHorizontal";
    public const string RIGHT_VERTICAL = "RVertical";
    public const string LEFT_TRIGGER = "LeftTrigger";
    public const string RIGHT_TRIGGER = "RightTrigger";
    public const string ACTION = "Action";
    public const string SPECIAL = "XButton";
    public const string PAUSE = "Pause";
    public const string BACK = "Back";
}

public struct GhoulStatVariables
{
    public const float MAX_STIFFNESS = 100;
    public const float ALPHA_CHANCE_THRESHOLD = .99f;
    public const float ALPHA_STARTING_LIMIT = .9f;
    public const float STARTING_AGE_AVERAGE = 60 * 40; //60 minutes * 40 hours
    public const float STARTING_AGE_MINIMUM = 60 * 15; //keep this on a limit, so they don't start dying instantly
    public const float STARTING_AGE_VARIANCE = 60 * 30;
    public const float LIFESPAN_INCREASERANGE = 60 * 10;
}


