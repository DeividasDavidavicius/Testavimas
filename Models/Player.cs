using Models.Cells;
using System.Drawing;
using System.Text.Json.Serialization;

namespace Models
{
    public class Player: EntityCell
    {
        [JsonPropertyName("id")]
        public string id { get; set; }
        [JsonPropertyName("username")]
        public string username { get; set; }
        [JsonPropertyName("xPos")]
        public int xPos { get; set; }
        [JsonPropertyName("yPos")]
        public int yPos { get; set; }

        public Player(): base(Color.Blue, 1000, 50)
        {
            this.username = "Player";
        }
        [JsonConstructor]
        public Player(string id, int xPos, int yPos, Color color, int healthPoints, string username): base(color, 1000, 50)
        {
            this.id = id;
            this.xPos = xPos;
            this.yPos = yPos;
            this.color = color;
            this.healthPoints = healthPoints;
            this.username = username;
        }

    }
}