using System.Collections;
using System.Collections.Generic;
using TurnBasedPractice.Resource;
using TurnBasedPractice.SO;
using UnityEngine;

namespace TurnBasedPractice.AudioSystem
{
    public static class AudioPlayer
    {
        private const int BGM = 0, SFX = 1;

        private static List<AudioSource> audioSources;

        private static Dictionary<BgmName, AudioClip> bgms;
        private static Dictionary<SfxName, AudioClip> sfxes;

        static AudioPlayer(){
            audioSources = new List<AudioSource>();
            var audioPlayer = new GameObject("AudioPlayer");
            audioSources.Add(audioPlayer.AddComponent<AudioSource>());
            audioSources.Add(audioPlayer.AddComponent<AudioSource>());

            audioSources[BGM].loop = true;
            audioSources[BGM].volume = 0.05f;
            audioSources[SFX].volume = 0.1f;

            var audioSettings = Resources.Load<AudioSettingsSO>(SoPath.DEFAULT_AUDIO_PATH);
            SetUpAudioFromAudioSettings(audioSettings);
            Object.DontDestroyOnLoad(audioPlayer);
        }

        private static void SetUpAudioFromAudioSettings(AudioSettingsSO so){
            bgms  = new Dictionary<BgmName, AudioClip>();
            foreach(var bgmData in so.bgmSettings.BgmList){
                bgms.Add(bgmData.Name, bgmData.Clip);
            }

            sfxes = new Dictionary<SfxName, AudioClip>();
            foreach(var sfxData in so.sfxSettings.SfxList){
                sfxes.Add(sfxData.Name, sfxData.Clip);
            }
        }

        public static void PlayBgm(BgmName bgmName){
            audioSources[BGM].clip = bgms[bgmName];
            audioSources[BGM].Play();
        }
        public static void PlaySfx(SfxName sfxName) => audioSources[SFX].PlayOneShot(sfxes[sfxName]);
    }
}
