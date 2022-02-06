using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Core.Audio
{
    public interface IAudioManager
    {
        void PlayEffect(EAudio audio);
        void PlayMusic(EAudio audio, bool isLoop = true);
        void StopMusic(EAudio audio);

        void SetMusicActive(bool isActive);
        void SetEffectsActive(bool isActive);
    }
}