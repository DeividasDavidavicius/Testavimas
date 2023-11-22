using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Models.Adapter
{
    internal class AudioPlayer : IAudioPlayer
    {
        private SoundPlayer soundPlayer;

        public AudioPlayer()
        {
            soundPlayer = new SoundPlayer();
        }
        public void Pause()
        {
            soundPlayer.Stop();
        }

        public void Play(string audioFile)
        {
            soundPlayer.SoundLocation = audioFile;
            soundPlayer.Play();
        }

        public void Stop()
        {
            soundPlayer.Stop();
        }
    }
}
