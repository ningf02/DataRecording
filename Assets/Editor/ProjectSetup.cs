using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class ProjectSetup : EditorWindow
{
    [MenuItem("Setup/Build and Player Settings")]
    public static void SetPlayerAndBuildSettings()
    {
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);
        EditorUserBuildSettings.selectedBuildTargetGroup = BuildTargetGroup.Android;
        EditorUserBuildSettings.androidBuildSubtarget = MobileTextureSubtarget.ASTC;

        PlayerSettings.colorSpace = ColorSpace.Linear;
        PlayerSettings.stereoRenderingPath = StereoRenderingPath.SinglePass;
        PlayerSettings.use32BitDisplayBuffer = true;
        PlayerSettings.bakeCollisionMeshes = true;
        PlayerSettings.gcIncremental = true;
        PlayerSettings.MTRendering = true;
        PlayerSettings.useHDRDisplay = false;
        PlayerSettings.gpuSkinning = false;
        PlayerSettings.SetUseDefaultGraphicsAPIs(BuildTarget.Android, false);
        GraphicsDeviceType[] g = new GraphicsDeviceType[1];
        g[0] = GraphicsDeviceType.OpenGLES3;
        PlayerSettings.SetGraphicsAPIs(BuildTarget.Android, g);
        PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.IL2CPP);

        GraphicsSettings.useScriptableRenderPipelineBatching = true;

        PlayerSettings.Android.disableDepthAndStencilBuffers = false;
        PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARM64;
        PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel29;
        PlayerSettings.Android.targetSdkVersion = AndroidSdkVersions.AndroidApiLevelAuto;

        QualitySettings.antiAliasing = 1;
        QualitySettings.vSyncCount = 0;
        QualitySettings.masterTextureLimit = 0;
        QualitySettings.softParticles = false;
        QualitySettings.billboardsFaceCameraPosition = true;
        QualitySettings.realtimeReflectionProbes = true;
        QualitySettings.shadows = UnityEngine.ShadowQuality.HardOnly;
        QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
        QualitySettings.SetQualityLevel(2);
    }
}