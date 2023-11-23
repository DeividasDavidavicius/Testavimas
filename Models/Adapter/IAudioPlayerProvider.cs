using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Models.Adapter
{
    public interface IAudioPlayerProvider
    {
        SoundPlayer CreateSoundPlayer();
    }
}
