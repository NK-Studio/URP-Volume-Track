using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace VolumeTrack
{
    public class PostMixerBehaviour : PlayableBehaviour
    {
        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            base.ProcessFrame(playable, info, playerData);
            Volume volume = playerData as Volume;

            #region Bloom

            bool useBloom = false;
            float bloomThreshold = 0f;
            float bloomIntensity = 0f;
            float bloomScatter = 0f;
            Color bloomTint = Color.clear;
            float bloomClamp = 0f;

            #endregion

            #region Chromatic Aberration

            bool useChromaticAberration = false;
            float chromaticAberrationIntensity = 0f;

            #endregion

            #region Color Adjustments

            bool useColorAdjustments = false;

            float CAPostExposure = 0f;

            float CAConstrast = 0f;

            Color CAColorFilter = Color.clear;

            float CAHueShift = 0f;

            float CASaturation = 0f;

            #endregion

            #region Depth Of Field

            DepthOfFieldMode DOFMode = DepthOfFieldMode.Off;

            #region Gaussian

            float DOFStart = 0f;

            float DOFEnd = 0f;

            float DOFMaxRadius = 0f;

            #endregion

            #region Bokeh

            float DOFFocusDistance = 0f;

            float DOFFocalLength = 0;

            float DOFAperture = 0f;

            int DOFBladeCount = 0;

            float DOFBladeCuvature = 0f;

            float DOFBladeRotation = 0f;

            #endregion

            #endregion

            #region Film Grain

            FilmGrainLookup FGLType = FilmGrainLookup.Thin1;

            bool useFilmGrain = false;

            float FGIntensity = 0f;

            float FGResponse = 0f;

            #endregion

            #region Lens Distortion

            bool useLensDistortion = false;
            float LDIntensity = 0f;
            float LDXMultiplier = 0f;
            float LDYMultiplier = 0f;
            Vector2 LDCenter = Vector2.zero;
            float LDScale = 0f;

            #endregion

            #region Motion Blur

            bool useMotionBlur = false;

            float MBIntensity = 0f;

            float MBClamp = 0f;

            #endregion

            #region Panini Projection

            bool usePaniniProjection = false;

            float PaniniDistance = 0f;

            float PaniniCropToFit = 0f;

            #endregion

            #region Split Toning

            bool useSplitToning = false;

            Color STShadows = Color.clear;

            Color STHighlights = Color.clear;

            float STBalance = 0f;

            #endregion

            #region Vignette

            bool useVignette = false;

            Color VignetteColor = Color.clear;

            Vector2 VignetteCenter = Vector2.zero;

            float VignetteIntensity = 0f;

            float VignetteSmoothness = 0f;

            #endregion

            #region White Balance

            bool useWhiteBalance = false;

            float WBTemperature = 0f;

            float WBTint = 0f;

            #endregion


            if (!volume) return;

            int inputCount = playable.GetInputCount();
            for (int i = 0; i < inputCount; i++)
            {
                float inputWeight = playable.GetInputWeight(i);
                ScriptPlayable<PostBehaviour> inputPlayable = (ScriptPlayable<PostBehaviour>)playable.GetInput(i);
                PostBehaviour input = inputPlayable.GetBehaviour();

                #region Bloom

                useBloom = input.useBloom;
                bloomThreshold += input.bloomThreshold * inputWeight;
                bloomIntensity += input.bloomIntensity * inputWeight;
                bloomScatter += input.bloomScatter * inputWeight;
                bloomTint += input.bloomTint * inputWeight;
                bloomClamp += input.bloomClamp * inputWeight;

                #endregion

                #region ChromaticAberration

                useChromaticAberration = input.useChromaticAberration;
                chromaticAberrationIntensity += input.CAIntensity * inputWeight;

                #endregion

                #region Color Adjustments

                useColorAdjustments = input.useColorAdjustments;

                CAPostExposure += input.CAPostExposure * inputWeight;

                CAConstrast += input.CAConstrast * inputWeight;

                CAColorFilter += input.CAColorFilter * inputWeight;

                CAHueShift += input.CAHueShift * inputWeight;

                CASaturation += input.CASaturation * inputWeight;

                #endregion

                #region Depth Of Field

                DOFMode = input.DOFMode;

                #region Gaussian

                if (DOFMode == DepthOfFieldMode.Gaussian)
                {
                    DOFStart += input.DOFStart * inputWeight;

                    DOFEnd += input.DOFEnd * inputWeight;

                    DOFMaxRadius += input.DOFMaxRadius * inputWeight;
                }

                #endregion

                #region Bokeh

                if (DOFMode == DepthOfFieldMode.Bokeh)
                {
                    DOFFocusDistance += input.DOFFocusDistance * inputWeight;

                    DOFFocalLength += input.DOFFocalLength * inputWeight;

                    DOFAperture += input.DOFAperture * inputWeight;

                    DOFBladeCount += input.DOFBladeCount;

                    DOFBladeCuvature += input.DOFBladeCuvature * inputWeight;

                    DOFBladeRotation += input.DOFBladeRotation * inputWeight;
                }

                #endregion

                #endregion

                #region Film Grain

                FGLType = input.FGLType;
                useFilmGrain = input.useFilmGrain;
                FGIntensity += input.FGIntensity * inputWeight;
                FGResponse += input.FGResponse * inputWeight;

                #endregion

                #region Lens Distortion

                useLensDistortion = input.useLensDistortion;
                LDIntensity += input.LDIntensity * inputWeight;
                LDXMultiplier += input.LDXMultiplier * inputWeight;
                LDYMultiplier += input.LDYMultiplier * inputWeight;
                LDCenter += input.LDCenter * inputWeight;
                LDScale += input.LDScale * inputWeight;

                #endregion

                #region Motion Blur

                useMotionBlur = input.useMotionBlur;
                MBIntensity += input.MBIntensity * inputWeight;
                MBClamp += input.MBClamp * inputWeight;

                #endregion

                #region Split Toning

                useSplitToning = input.useSplitToning;

                STShadows += input.STShadows * inputWeight;

                STHighlights += input.STHighlights * inputWeight;

                STBalance += input.STBalance * inputWeight;

                #endregion

                #region Panini Projection

                usePaniniProjection = input.usePaniniProjection;
                PaniniDistance += input.PaniniDistance * inputWeight;
                PaniniCropToFit += input.PaniniCropToFit * inputWeight;

                #endregion

                #region Split Toning

                useSplitToning = input.useSplitToning;
                STShadows += input.STShadows * inputWeight;
                STHighlights += input.STHighlights * inputWeight;

                #endregion

                #region Vignette

                useVignette = input.useVignette;
                VignetteColor += input.VignetteColor * inputWeight;
                VignetteCenter += input.VignetteCenter * inputWeight;
                VignetteIntensity += input.VignetteIntensity * inputWeight;
                VignetteSmoothness += input.VignetteSmoothness * inputWeight;

                #endregion

                #region White Balance

                useWhiteBalance = input.useWhiteBalance;

                WBTemperature += input.WBTemperature * inputWeight;

                WBTint += input.WBTint * inputWeight;

                #endregion
            }

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
                chromatic.intensity.value = chromaticAberrationIntensity;
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