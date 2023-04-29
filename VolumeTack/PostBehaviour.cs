using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace VolumeTrack
{
    public class PostBehaviour : PlayableBehaviour
    {
        #region Bloom

        public bool useBloom;
        public float bloomThreshold;
        public float bloomIntensity;
        public float bloomScatter;

        [ColorUsage(false, false)] public Color bloomTint;

        public float bloomClamp;

        #endregion

        #region ChromaticAberration

        public bool useChromaticAberration;
        public float CAIntensity;

        #endregion

        #region Color Adjustments

        public bool useColorAdjustments;

        public float CAPostExposure;

        public float CAConstrast;

        public Color CAColorFilter;

        public float CAHueShift;

        public float CASaturation;

        #endregion

        #region Depth Of Field

        public DepthOfFieldMode DOFMode;

        #region Gaussian

        public float DOFStart;

        public float DOFEnd;

        public float DOFMaxRadius;

        #endregion

        #region Bokeh

        public float DOFFocusDistance;

        public float DOFFocalLength;

        public float DOFAperture;

        public int DOFBladeCount;

        public float DOFBladeCuvature;

        public float DOFBladeRotation;

        #endregion

        #endregion

        #region Film Grain

        public bool useFilmGrain;

        public FilmGrainLookup FGLType;

        public float FGIntensity;

        public float FGResponse;

        #endregion

        #region Lens Distortion

        public bool useLensDistortion;

        public float LDIntensity;

        public float LDXMultiplier;

        public float LDYMultiplier;

        public Vector2 LDCenter;

        public float LDScale;

        #endregion

        #region Motion Blur

        public bool useMotionBlur;

        public float MBIntensity;

        public float MBClamp;

        #endregion

        #region Panini Projection

        public bool usePaniniProjection;

        public float PaniniDistance;

        public float PaniniCropToFit;

        #endregion

        #region Split Toning

        public bool useSplitToning;

        public Color STShadows;

        public Color STHighlights;

        public float STBalance;

        #endregion

        #region Vignette

        public bool useVignette;

        public Color VignetteColor;
        
        public Vector2 VignetteCenter;
        
        public float VignetteIntensity;
        
        public float VignetteSmoothness;
        
        #endregion
        
        #region White Balance

        public bool useWhiteBalance;
        
        public float WBTemperature;

        public float WBTint;

        #endregion

        
        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            base.ProcessFrame(playable, info, playerData);
            Volume volume = playerData as Volume;

            if (!volume) return;

            if (volume.profile.TryGet(out Bloom bloom))
            {
                bloom.threshold.value = bloomThreshold;
                bloom.intensity.value = bloomIntensity;
                bloom.scatter.value = bloomScatter;
                bloom.tint.value = bloomTint;
                bloom.clamp.value = bloomClamp;
            }

            if (volume.profile.TryGet(out ChromaticAberration chromatic))
            {
                chromatic.intensity.value = CAIntensity;
            }

            if (volume.profile.TryGet(out ColorAdjustments adjustments))
            {
                adjustments.postExposure.value = CAPostExposure;
                adjustments.contrast.value = CAConstrast;
                adjustments.colorFilter.value = CAColorFilter;
                adjustments.hueShift.value = CAHueShift;
                adjustments.saturation.value = CASaturation;
            }
            if (volume.profile.TryGet(out DepthOfField depthOfField))
            {
                depthOfField.mode.value = DOFMode;

                if (DOFMode == DepthOfFieldMode.Gaussian)
                {
                    depthOfField.gaussianStart.value = DOFStart;
                    depthOfField.gaussianEnd.value = DOFEnd;
                    depthOfField.gaussianMaxRadius.value = DOFMaxRadius;
                }
                else if (DOFMode == DepthOfFieldMode.Bokeh)
                {
                    depthOfField.focusDistance.value = DOFFocusDistance;
                    depthOfField.focalLength.value = DOFFocalLength;
                    depthOfField.aperture.value = DOFAperture;
                    depthOfField.bladeCount.value = DOFBladeCount;
                    depthOfField.bladeCurvature.value = DOFBladeCuvature;
                    depthOfField.bladeRotation.value = DOFBladeRotation;
                }
            }

            if (volume.profile.TryGet(out FilmGrain filmGrain))
            {
                filmGrain.type.value = FGLType;
                filmGrain.intensity.value = FGIntensity;
                filmGrain.response.value = FGResponse;
            }

            if (volume.profile.TryGet(out LensDistortion lensDistortion))
            {
                lensDistortion.intensity.value = LDIntensity;
                lensDistortion.xMultiplier.value = LDXMultiplier;
                lensDistortion.yMultiplier.value = LDYMultiplier;
                lensDistortion.center.value = LDCenter;
                lensDistortion.scale.value = LDScale;
            }

            if (volume.profile.TryGet(out MotionBlur motionBlur))
            {
                motionBlur.intensity.value = MBIntensity;
                motionBlur.clamp.value = MBClamp;
            }

            if (volume.profile.TryGet(out PaniniProjection paniniProjection))
            {
                paniniProjection.distance.value = PaniniDistance;
                paniniProjection.cropToFit.value = PaniniCropToFit;
            }

            if (volume.profile.TryGet(out SplitToning splitToning))
            {
                splitToning.shadows.value = STShadows;
                splitToning.highlights.value = STHighlights;
                splitToning.balance.value = STBalance;
            }
            
            if (volume.profile.TryGet(out Vignette vignette))
            {
                vignette.color.value = VignetteColor;
                vignette.center.value = VignetteCenter;
                vignette.intensity.value = VignetteIntensity;
                vignette.smoothness.value = VignetteSmoothness;
            }

            if (volume.profile.TryGet(out WhiteBalance whiteBalance))
            {
                whiteBalance.temperature.value = WBTemperature;
                whiteBalance.tint.value = WBTint;
            }  
        }
        
        
    }
}