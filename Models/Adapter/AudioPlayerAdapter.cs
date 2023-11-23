using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Adapter
{
    public class AudioPlayerAdapter : IAudioPlayer
    {
        private IAudioPlayer audioPlayer;
        public AudioPlayerAdapter(IAudioPlayer player)
        {
            this.audioPlayer = player;
        }
        public void Play(string audioFile)
        {
            audioPlayer.Play(audioFile);
        }
        public void Stop()
        {
            audioPlayer.Stop();
        }
        public void Pause() 
        { 
            audioPlayer.Pause();
        }
    }
}
