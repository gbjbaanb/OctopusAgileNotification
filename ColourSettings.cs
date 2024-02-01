using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;
using ServiceStack;

namespace OctopusAgileNotification
{
	internal class ColourSettings
	{
		public Color textColour { get; set; }
		public Color backColour { get; set; }
		public int threshold { get; set; }
	}

	public class ColorJsonConverter : JsonConverter<Color>
	{
		public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => Color.FromArgb(Int32.Parse(reader.GetString()));

		public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options) => writer.WriteStringValue((value.ToArgb().ToString()));
	}
}
