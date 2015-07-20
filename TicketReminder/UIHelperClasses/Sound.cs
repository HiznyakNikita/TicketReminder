using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TicketReminder
{
    public class Sound
    {

        //DEL
        public int Count { get; set; }

        public string PathToFile { get; set; }
        public string Name { get; set; }

        public static bool IsPlay = true;
        private MediaPlayer _player = new MediaPlayer();

        public static string Location { get; set; }

        public Sound() { Sound.Location = Environment.CurrentDirectory + "\\Sounds\\Iphone_Ringtone_freetone.at.ua.mp3"; }
        public Sound(string location)
        {
            Location = location;
        }

        public void PlaySound()
        {
            try
            {
                if (IsPlay == true)
                {
                    _player.Open(new Uri(Location, UriKind.Relative));
                    _player.Play();
                }
                else
                    return;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Information", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
        }

        public List<Sound> SearchSounds()
        {
            string mediaExtensions = ".mp3";
            List<Sound> soundsFile = new List<Sound>();

            string pathToFolder = Environment.CurrentDirectory + "\\Sounds\\";

            if(Directory.Exists(pathToFolder))
            {
                foreach (var file in Directory.GetFiles(pathToFolder, "*.*"))
                    if (mediaExtensions.Contains(Path.GetExtension(file).ToLower()))
                    {
                        int index = file.LastIndexOf("\\");
                        int extension = file.LastIndexOf(".mp3");
                        string nameFile = file.Substring(index + 2, extension - index - 2);

                        soundsFile.Add(new Sound("") { PathToFile = file, Name = nameFile });
                    }
                return soundsFile;
            }
            else
                System.Windows.Forms.MessageBox.Show("The folder is exist!", "Information", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            return null;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
