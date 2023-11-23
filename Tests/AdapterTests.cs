using Models.Adapter;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class AdapterTests
    {
        [TestMethod]
        public void TestAudioPlayerAdapterPlay()
        {
            // Arrange
            var soundPlayerMock = new Mock<IAudioPlayer>();
            var audioPlayer = new AudioPlayerAdapter(soundPlayerMock.Object);

            // Act
            audioPlayer.Play("test.mp3");

            // Assert
            soundPlayerMock.Verify(sp => sp.Play("test.mp3"), Times.Once);
        }

        [TestMethod]
        public void TestAudioPlayerAdapterStop()
        {
            // Arrange
            var soundPlayerMock = new Mock<IAudioPlayer>();
            var audioPlayer = new AudioPlayerAdapter(soundPlayerMock.Object);

            // Act
            audioPlayer.Stop();

            // Assert
            soundPlayerMock.Verify(sp => sp.Stop(), Times.Once);
        }

        [TestMethod]
        public void TestAudioPlayerAdapterPause()
        {
            // Arrange
            var soundPlayerMock = new Mock<IAudioPlayer>();
            var audioPlayer = new AudioPlayerAdapter(soundPlayerMock.Object);

            // Act
            audioPlayer.Pause();

            // Assert
            soundPlayerMock.Verify(sp => sp.Pause(), Times.Once);
        }
    }
}
