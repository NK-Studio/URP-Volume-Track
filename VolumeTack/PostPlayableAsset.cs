using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering.Universal;
using NaughtyAttributes;
using NKaughtyAttributes;
using OptionalClampFloat = NKaughtyAttributes.OptionalClampFloat;


namespace VolumeTrack
{
    public class PostPlayableAsset : PlayableAsset
    {
        #region Bloom

        [Foldout("Bloom")] public bool useBloom;

        [Foldout("Bloom"), ShowIf("useBloom")]
        public OptionalClampFloat bloomThreshold = OptionalClampFloat.WithInit(0.9f, max: float.MaxValue);

        [Foldout("Bloom"), ShowIf("useBloom")]
        public OptionalClampFloat bloomIntensity = OptionalClampFloat.WithInit(0f, max: float.MaxValue);

        [Foldout("Bloom"), ShowIf("useBloom")]
        public OptionalFloatSlider bloomScatter = OptionalFloatSlider.WithInit(0.7f);

        [Foldout("Bloom"), ShowIf("useBloom")]
        public OptionalNoAlphaColor bloomTint = OptionalNoAlphaColor.WithInit(Color.white);

        [Foldout("Bloom"), ShowIf("useBloom")]
        public OptionalClampFloat bloomClamp = OptionalClampFloat.WithInit(65472f, min: 0f, max: float.MaxValue);

        #endregion

        #region ChromaticAberration

        [Foldout("Chromatic Aberration")] public bool useChromaticAberration;

        [Foldout("Chromatic Aberration"), ShowIf("useChromaticAberration")]
        public OptionalFloatSlider CAIntensity = OptionalFloatSlider.WithInit(0f);

        #endregion

        #region Color Adjustments

        [Foldout("Color Adjustments")] public bool useColorAdjustments;

        [Foldout("Color Adjustments"), ShowIf("useColorAdjustments")]
        public Optional<float> CAPostExposure = OptionalFloat.WithInit(0f);

        [Foldout("Color Adjustments"), ShowIf("useColorAdjustments")]
        public OptionalFloatSlider CAConstrast = OptionalFloatSlider.WithInit(0f, min: -100, max: 100);

        [Foldout("Color Adjustments"), ShowIf("useColorAdjustments")]
        public OptionalHDRColor CAColorFilter = OptionalHDRColor.WithInit(Color.white);

        [Foldout("Color Adjustments"), ShowIf("useColorAdjustments")]
        public OptionalFloatSlider CAHueShift = OptionalFloatSlider.WithInit(0f, min: -180, max: 180);

        [Foldout("Color Adjustments"), ShowIf("useColorAdjustments")]
        public OptionalFloatSlider CASaturation = OptionalFloatSlider.WithInit(0f, min: -100, max: 100);

        #endregion

        #region Depth Of Field

        [Foldout("Depth Of Filed")] public DepthOfFieldMode DOFMode;

        #region Gaussian

        [Foldout("Depth Of Filed"), ShowIf("DOFMode", DepthOfFieldMode.Gaussian)]
        public OptionalClampFloat DOFStart = OptionalClampFloat.WithInit(10f, max: float.MaxValue);

        [Foldout("Depth Of Filed"), ShowIf("DOFMode", DepthOfFieldMode.Gaussian)]
        public OptionalClampFloat DOFEnd = OptionalClampFloat.WithInit(30f, max: float.MaxValue);

        [Foldout("Depth Of Filed"), ShowIf("DOFMode", DepthOfFieldMode.Gaussian)]
        public OptionalFloatSlider DOFMaxRadius = OptionalFloatSlider.WithInit(1f, min: 0.5f, max: 1.5f);

        #endregion

        #region Bokeh

        [Foldout("Depth Of Filed"), ShowIf("DOFMode", DepthOfFieldMode.Bokeh)]
        public OptionalClampFloat DOFFocusDistance = OptionalClampFloat.WithInit(10f, min: 0.1f, max: float.MaxValue);

        [Foldout("Depth Of Filed"), ShowIf("DOFMode", DepthOfFieldMode.Bokeh)]
        public OptionalFloatSlider DOFFocalLength = OptionalFloatSlider.WithInit(50, min: 1, max: 300);

        [Foldout("Depth Of Filed"), ShowIf("DOFMode", DepthOfFieldMode.Bokeh)]
        public OptionalFloatSlider DOFAperture = OptionalFloatSlider.WithInit(5.6f, min: 1f, max: 32);

        [Foldout("Depth Of Filed"), ShowIf("DOFMode", DepthOfFieldMode.Bokeh)]
        public OptionalIntSlider DOFBladeCount = OptionalIntSlider.WithInit(5, min: 3, max: 9);

        [Foldout("Depth Of Filed"), ShowIf("DOFMode", DepthOfFieldMode.Bokeh)]
        public OptionalFloatSlider DOFBladeCuvature = OptionalFloatSlider.WithInit(1f);

        [Foldout("Depth Of Filed"), ShowIf("DOFMode", DepthOfFieldMode.Bokeh)]
        public OptionalFloatSlider DOFBladeRotation = OptionalFloatSlider.WithInit(0, min: -180, max: 180);

        #endregion

        #endregion

        #region Film Grain

        [Foldout("Film Grain")] public bool useFilmGrain;

        [Foldout("Film Grain"), ShowIf("useFilmGrain")]
        public FilmGrainLookup FGLType = FilmGrainLookup.Thin1;

        [Foldout("Film Grain"), ShowIf("useFilmGrain")]
        public OptionalFloatSlider FGIntensity = OptionalFloatSlider.WithInit(0f);

        [Foldout("Film Grain"), ShowIf("useFilmGrain")]
        public OptionalFloatSlider FGResponse = OptionalFloatSlider.WithInit(0.8f);

        #endregion

        #region Lens Distortion

        [Foldout("Lens Distortion")] public bool useLensDistortion;

        [Foldout("Lens Distortion"), ShowIf("useLensDistortion")]
        public OptionalFloatSlider LDIntensity = OptionalFloatSlider.WithInit(0f, min: -1f, max: 1f);

        [Foldout("Lens Distortion"), ShowIf("useLensDistortion")]
        public OptionalFloatSlider LDXMultiplier = OptionalFloatSlider.WithInit(1f);

        [Foldout("Lens Distortion"), ShowIf("useLensDistortion")]
        public OptionalFloatSlider LDYMultiplier = OptionalFloatSlider.WithInit(1f);

        [Foldout("Lens Distortion"), ShowIf("useLensDistortion")]
        public Optional<Vector2> LDCenter = OptionalVector2.WithInit(new Vector2(0.5f, 0.5f));

        [Foldout("Lens Distortion"), ShowIf("useLensDistortion")]
        public OptionalFloatSlider LDScale = OptionalFloatSlider.WithInit(1f, min: 0.01f, max: 5f);

        #endregion

        #region Motion Blur

        [Foldout("Motion Blur")] public bool useMotionBlur;

        [Foldout("Motion Blur"), ShowIf("useMotionBlur")]
        public OptionalFloatSlider MBIntensity = OptionalFloatSlider.WithInit(0f);

        [Foldout("Motion Blur"), ShowIf("useMotionBlur")]
        public OptionalFloatSlider MBClamp = OptionalFloatSlider.WithInit(0.05f, min: 0f, max: 0.2f);

        #endregion

        #region Panini Projection

        [Foldout("Panini Projection")] public bool usePaniniProjection;

        [Foldout("Panini Projection"), ShowIf("usePaniniProjection")]
        public OptionalFloatSlider PaniniDistance = OptionalFloatSlider.WithInit(0f);

        [Foldout("Panini Projection"), ShowIf("usePaniniProjection")]
        public OptionalFloatSlider PaniniCropToFit = OptionalFloatSlider.WithInit(1f);

        #endregion

        #region Split Toning

        [Foldout("Split Toning")] public bool useSplitToning;

        [Foldout("Split Toning"), ShowIf("useSplitToning")]
        public OptionalNoAlphaColor STShadows = OptionalNoAlphaColor.WithInit(Color.gray);

        [Foldout("Split Toning"), ShowIf("useSplitToning")]
        public OptionalNoAlphaColor STHighlights = OptionalNoAlphaColor.WithInit(Color.gray);

        [Foldout("Split Toning"), ShowIf("useSplitToning")]
        public OptionalFloatSlider STBalance = OptionalFloatSlider.WithInit(0f, min: -100f, max: 100f);

        #endregion

        #region Vignette

        [Foldout("Vignette")] public bool useVignette;

        [Foldout("Vignette"), ShowIf("useVignette")]
        public OptionalNoAlphaColor VignetteColor = OptionalNoAlphaColor.WithInit(Color.black);

        [Foldout("Vignette"), ShowIf("useVignette")]
        public OptionalVector2 VignetteCenter = OptionalVector2.WithInit(new Vector2(0.5f, 0.5f));

        [Foldout("Vignette"), ShowIf("useVignette")]
        public OptionalFloatSlider VignetteIntensity = OptionalFloatSlider.WithInit(0f);

        [Foldout("Vignette"), ShowIf("useVignette")]
        public OptionalFloatSlider VignetteSmoothness = OptionalFloatSlider.WithInit(0.2f, min: 0.01f);

        #endregion

        #region White Balance

        [Foldout("White Balance")] public bool useWhiteBalance;

        [Foldout("White Balance"), ShowIf("useWhiteBalance")]
        public OptionalFloatSlider WBTemperature = OptionalFloatSlider.WithInit(0f, min: -100f, max: 100f);

        [Foldout("White Balance"), ShowIf("useWhiteBalance")]
        public OptionalFloatSlider WBTint = OptionalFloatSlider.WithInit(0f, min: -100f, max: 100f);

        #endregion

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            ScriptPlayable<PostBehaviour> _playable = ScriptPlayable<PostBehaviour>.Create(graph);
            PostBehaviour _volumeBehaviour = _playable.GetBehaviour();

            _volumeBehaviour.useBloom = useBloom;
            if (useBloom)
            {
                _volumeBehaviour.bloomThreshold =
                    bloomThreshold.isSet ? bloomThreshold.value : bloomThreshold.initValue;

                _volumeBehaviour.bloomIntensity =
                    bloomIntensity.isSet ? bloomIntensity.value : bloomIntensity.initValue;

                _volumeBehaviour.bloomScatter =
                    bloomScatter.isSet ? bloomScatter.value : bloomScatter.initValue;

                _volumeBehaviour.bloomTint =
                    bloomTint.isSet ? bloomTint.value : bloomTint.initValue;

                _volumeBehaviour.bloomClamp =
                    bloomClamp.isSet ? bloomClamp.value : bloomClamp.initValue;
            }
            else
            {
                _volumeBehaviour.bloomThreshold = bloomThreshold.initValue;
                _volumeBehaviour.bloomIntensity = bloomIntensity.initValue;
                _volumeBehaviour.bloomScatter = bloomScatter.initValue;
                _volumeBehaviour.bloomTint = bloomTint.initValue;
                _volumeBehaviour.bloomClamp = bloomClamp.initValue;
            }

            _volumeBehaviour.useChromaticAberration = useChromaticAberration;
            if (useChromaticAberration)
            {
                _volumeBehaviour.CAIntensity =
                    CAIntensity.isSet ? CAIntensity.value : CAIntensity.initValue;
            }
            else
            {
                _volumeBehaviour.CAIntensity = CAIntensity.initValue;
            }

            _volumeBehaviour.useColorAdjustments = useColorAdjustments;
            if (useColorAdjustments)
            {
                _volumeBehaviour.CAPostExposure =
                    CAPostExposure.isSet ? CAPostExposure.value : CAPostExposure.initValue;

                _volumeBehaviour.CAConstrast =
                    CAConstrast.isSet ? CAConstrast.value : CAConstrast.initValue;

                _volumeBehaviour.CAColorFilter =
                    CAColorFilter.isSet ? CAColorFilter.value : CAColorFilter.initValue;

                _volumeBehaviour.CAHueShift =
                    CAHueShift.isSet ? CAHueShift.value : CAHueShift.initValue;

                _volumeBehaviour.CASaturation =
                    CASaturation.isSet ? CASaturation.value : CASaturation.initValue;
            }
            else
            {
                _volumeBehaviour.CAPostExposure = CAPostExposure.initValue;
                _volumeBehaviour.CAConstrast = CAConstrast.initValue;
                _volumeBehaviour.CAColorFilter = CAColorFilter.initValue;
                _volumeBehaviour.CAHueShift = CAHueShift.initValue;
                _volumeBehaviour.CASaturation = CASaturation.initValue;
            }

            #region Depth Of Field

            _volumeBehaviour.DOFMode = DOFMode;

            switch (DOFMode)
            {
                case DepthOfFieldMode.Gaussian:
                    _volumeBehaviour.DOFStart = DOFStart.isSet ? DOFStart.value : DOFStart.initValue;
                    _volumeBehaviour.DOFEnd = DOFEnd.isSet ? DOFEnd.value : DOFEnd.initValue;
                    _volumeBehaviour.DOFMaxRadius = DOFMaxRadius.isSet ? DOFMaxRadius.value : DOFMaxRadius.initValue;
                    break;
                case DepthOfFieldMode.Bokeh:
                    _volumeBehaviour.DOFFocusDistance =
                        DOFFocusDistance.isSet ? DOFFocusDistance.value : DOFFocusDistance.initValue;
                    _volumeBehaviour.DOFFocalLength =
                        DOFFocalLength.isSet ? DOFFocalLength.value : DOFFocalLength.initValue;
                    _volumeBehaviour.DOFAperture = DOFAperture.isSet ? DOFAperture.value : DOFAperture.initValue;
                    _volumeBehaviour.DOFBladeCount =
                        DOFBladeCount.isSet ? DOFBladeCount.value : DOFBladeCount.initValue;
                    _volumeBehaviour.DOFBladeCuvature =
                        DOFBladeCuvature.isSet ? DOFBladeCuvature.value : DOFBladeCuvature.initValue;
                    _volumeBehaviour.DOFBladeRotation =
                        DOFBladeRotation.isSet ? DOFBladeRotation.value : DOFBladeRotation.initValue;
                    break;
                case DepthOfFieldMode.Off:
                    _volumeBehaviour.DOFStart = DOFStart.initValue;
                    _volumeBehaviour.DOFEnd = DOFEnd.initValue;
                    _volumeBehaviour.DOFMaxRadius = DOFMaxRadius.initValue;
                    _volumeBehaviour.DOFFocusDistance = DOFFocusDistance.initValue;
                    _volumeBehaviour.DOFFocalLength = DOFFocalLength.initValue;
                    _volumeBehaviour.DOFAperture = DOFAperture.initValue;
                    _volumeBehaviour.DOFBladeCount = DOFBladeCount.initValue;
                    _volumeBehaviour.DOFBladeCuvature = DOFBladeCuvature.initValue;
                    _volumeBehaviour.DOFBladeRotation = DOFBladeRotation.initValue;
                    break;
            }

            #endregion

            _volumeBehaviour.useFilmGrain = useFilmGrain;
            if (useFilmGrain)
            {
                _volumeBehaviour.FGLType = FGLType;

                _volumeBehaviour.FGIntensity =
                    FGIntensity.isSet ? FGIntensity.value : FGIntensity.initValue;

                _volumeBehaviour.FGResponse =
                    FGResponse.isSet ? FGResponse.value : FGResponse.initValue;
            }
            else
            {
                _volumeBehaviour.FGLType = FilmGrainLookup.Thin1;
                _volumeBehaviour.FGIntensity = FGIntensity.initValue;
                _volumeBehaviour.FGResponse = FGResponse.initValue;
            }

            _volumeBehaviour.useLensDistortion = useLensDistortion;
            if (useLensDistortion)
            {
                _volumeBehaviour.LDIntensity =
                    LDIntensity.isSet ? LDIntensity.value : LDIntensity.initValue;

                _volumeBehaviour.LDXMultiplier =
                    LDXMultiplier.isSet ? LDXMultiplier.value : LDXMultiplier.initValue;

                _volumeBehaviour.LDYMultiplier =
                    LDYMultiplier.isSet ? LDYMultiplier.value : LDYMultiplier.initValue;

                _volumeBehaviour.LDCenter =
                    LDCenter.isSet ? LDCenter.value : LDCenter.initValue;

                _volumeBehaviour.LDScale =
                    LDScale.isSet ? LDScale.value : LDScale.initValue;
            }
            else
            {
                _volumeBehaviour.LDIntensity = LDIntensity.initValue;
                _volumeBehaviour.LDXMultiplier = LDXMultiplier.initValue;
                _volumeBehaviour.LDYMultiplier = LDYMultiplier.initValue;
                _volumeBehaviour.LDCenter = LDCenter.initValue;
                _volumeBehaviour.LDScale = LDScale.initValue;
            }

            _volumeBehaviour.useMotionBlur = useMotionBlur;
            if (useMotionBlur)
            {
                _volumeBehaviour.MBIntensity =
                    MBIntensity.isSet ? MBIntensity.value : MBIntensity.initValue;

                _volumeBehaviour.MBClamp =
                    MBClamp.isSet ? MBClamp.value : MBClamp.initValue;
            }
            else
            {
                _volumeBehaviour.MBIntensity = MBIntensity.initValue;
                _volumeBehaviour.MBClamp = MBClamp.initValue;
            }

            _volumeBehaviour.useSplitToning = useSplitToning;
            if (useSplitToning)
            {
                _volumeBehaviour.STShadows =
                    STShadows.isSet ? STShadows.value : STShadows.initValue;

                _volumeBehaviour.STHighlights =
                    STHighlights.isSet ? STHighlights.value : STHighlights.initValue;

                _volumeBehaviour.STBalance =
                    STBalance.isSet ? STBalance.value : STBalance.initValue;
            }
            else
            {
                _volumeBehaviour.STShadows = STShadows.initValue;
                _volumeBehaviour.STHighlights = STHighlights.initValue;
                _volumeBehaviour.STBalance = STBalance.initValue;
            }

            _volumeBehaviour.usePaniniProjection = usePaniniProjection;
            if (usePaniniProjection)
            {
                _volumeBehaviour.PaniniDistance =
                    PaniniDistance.isSet ? PaniniDistance.value : PaniniDistance.initValue;

                _volumeBehaviour.PaniniCropToFit =
                    PaniniCropToFit.isSet ? PaniniCropToFit.value : PaniniCropToFit.initValue;
            }
            else
            {
                _volumeBehaviour.PaniniDistance = PaniniDistance.initValue;
                _volumeBehaviour.PaniniCropToFit = PaniniCropToFit.initValue;
            }

            _volumeBehaviour.useVignette = useVignette;
            if (useVignette)
            {
                _volumeBehaviour.VignetteColor =
                    VignetteColor.isSet ? VignetteColor.value : VignetteColor.initValue;

                _volumeBehaviour.VignetteCenter =
                    VignetteCenter.isSet ? VignetteCenter.value : VignetteCenter.initValue;

                _volumeBehaviour.VignetteIntensity =
                    VignetteIntensity.isSet ? VignetteIntensity.value : VignetteIntensity.initValue;

                _volumeBehaviour.VignetteSmoothness =
                    VignetteSmoothness.isSet ? VignetteSmoothness.value : VignetteSmoothness.initValue;
            }
            else
            {
                _volumeBehaviour.VignetteColor = VignetteColor.initValue;
                _volumeBehaviour.VignetteCenter = VignetteCenter.initValue;
                _volumeBehaviour.VignetteIntensity = VignetteIntensity.initValue;
                _volumeBehaviour.VignetteSmoothness = VignetteSmoothness.initValue;
            }

            _volumeBehaviour.useWhiteBalance = useWhiteBalance;
            if (useWhiteBalance)
            {
                _volumeBehaviour.WBTemperature =
                    WBTemperature.isSet ? WBTemperature.value : WBTemperature.initValue;

                _volumeBehaviour.WBTint =
                    WBTint.isSet ? WBTint.value : WBTint.initValue;
            }
            else
            {
                _volumeBehaviour.WBTemperature = WBTemperature.initValue;
                _volumeBehaviour.WBTint = WBTint.initValue;
            }

            return _playable;
        }
    }
}