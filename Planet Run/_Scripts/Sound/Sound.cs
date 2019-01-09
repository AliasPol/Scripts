using UnityEngine.Audio;
using UnityEngine;

public class Sound {

    public enum MusicList {
        OpenSpaceAtmosphere,
        RainAtmosphereV2,
        CityEarthAtmosphereLoopV2,

        PlanetAtmosphere
    };
    public enum EffectList {
        JetFiresProjectile,
        ExplosionInSpace,
        AlienCollectHotDog,
        GameOverScreen,
        SpaceGunV1,
        SpaceGunV2,
        FuelCollectV2,
        UIButtonV1,
        UIButtonV2,
        UIButtonV3,

    };
    public enum PlayerEffectList {
        JetMovingUpDown,
        JetMovingLeftRight,
        JetApproachingPlanetSequence,

        AlienSlidev2,
        AlienJumpv2,
        AlienMovev3,
        AlienMovev4
    };

    public enum PlayerEffectLoopList
    {
        JetEngineWhenMoving,
        JetEngineLoop,
        JetEngineLoopV2,

        AlienFootstep1,
        AlienFootstep2,
        AlienFootstep3,
        AlienFootstep4,
        AlienFootstep5,
        AlienFootstep6,
        AlienFootstep7,
        AlienFootstep8,
        AlienFootstep9,
        AlienFootstep10
    }

    public enum MixGroupsName { Master, Music, Effects, PlayerEffects, PlayerEffectsLoop }


    public static MixGroupsName GetMixGroupsName(string name)
    {
        switch (name) {
            case "Master":
                return MixGroupsName.Master;
            case "Music":
                return MixGroupsName.Music;
            case "Effects":
                return MixGroupsName.Effects;
            case "PlayerEffects":
                return MixGroupsName.PlayerEffects;
            case "PlayerEffectsLoop":
                return MixGroupsName.PlayerEffectsLoop;
            default:
                Debug.Log("Something wrong");
                return MixGroupsName.Master;
        }

    }


}
