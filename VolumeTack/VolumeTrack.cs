using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Playables;
using UnityEngine.Rendering;
using UnityEngine.Timeline;

namespace VolumeTrack
{
    [TrackClipType(typeof(PostPlayableAsset))]
    [TrackBindingType(typeof(Volume))]
    public class VolumeTrack : TrackAsset
    {
        [InfoBox("SRP Volume을 스크립트로 변경하면 인스턴스화 되며, 이 상태에서 옵션을 On/Off하면 " +
                 "플레이 모드를 종료했을 때 초기화가 되는 문제가 있으므로, On/Off할 때는 새로고침을 눌러서 " +
                 "원본 Volume으로 되돌린 후 설정해야합니다.")]
        [BoxGroup("Option"), Tooltip("인스턴스화된 볼륨을 원본으로 되돌리는 참고용")]
        public VolumeProfile originVolumeProfile;

        [Button("Refresh VolumeProfile")]
        public void RefreshVolumeProfile()
        {
            if (Application.systemLanguage == SystemLanguage.Korean)
                Assert.IsTrue(originVolumeProfile, "originVolumeProfile이 연결되어 있지 않습니다.");
            else
                Assert.IsTrue(originVolumeProfile, "originVolumeProfile is not referenced");
        
            int count = 0;
            PlayableBinding _binding;
            foreach (PlayableBinding _playableAssetOutput in playableDirector.playableAsset.outputs)
            {
                if (_playableAssetOutput.streamName.Contains("Volume Track"))
                {
                    count++;
                    _binding = _playableAssetOutput;
                }
            }
        
            if (Application.systemLanguage == SystemLanguage.Korean)
                Assert.IsTrue(count == 1, "포스트 프로세싱 트랙은 1개만 가능합니다.");
            else
                Assert.IsTrue(count == 1, "You can only have one post-processing track.");
        
            Volume volume = playableDirector.GetGenericBinding(_binding.sourceObject) as Volume;
        
            if (Application.systemLanguage == SystemLanguage.Korean)
                Assert.IsNotNull(volume, "트랙에서 포스트 프로세싱을 찾을 수 없습니다.");
            else
                Assert.IsNotNull(volume, "No post processing found on the track.");
        
            volume.profile = originVolumeProfile;
        }
        
        private VolumeTrack currentTrack;
        private PlayableDirector playableDirector;

        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            //없으면 가져옴
            if (!playableDirector)
                playableDirector = go.GetComponent<PlayableDirector>();

            return ScriptPlayable<PostMixerBehaviour>.Create(graph, inputCount);
        }
    }
}