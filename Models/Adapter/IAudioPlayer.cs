using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Adapter
{
    internal interface IAudioPlayer
    {
        void Play(string audioFile);
        void Pause();
        void Stop();
    }
}
