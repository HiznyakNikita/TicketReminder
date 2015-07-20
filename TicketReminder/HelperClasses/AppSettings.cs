using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TicketReminder.HelperClasses
{
    /// <summary>
    /// AppSettings realease pattern singletone
    /// </summary>
    public class AppSettings
    {

        private string _pathToFolder = Environment.CurrentDirectory + "\\Settings\\";
        private string _fileName = "Settings.xml";

        private static readonly Object _appLock = new Object();
        private static AppSettings _appSettings = null;

        private AppSettings() { }
        public AppSettings(string user, string pathToSound) 
        {
            Sound.Location = pathToSound;
        }

        public static AppSettings Instance()
        {
            lock (_appLock)
                if (_appSettings == null)
                {
                    _appSettings = new AppSettings();
                    _appSettings.SaveSettings();
                }
            return _appSettings;
        }

        private void SaveSettings()
        {
            if (!Directory.Exists(_pathToFolder))
                Directory.CreateDirectory(_pathToFolder);

            CreateXML();
        }
        
        private void CreateXML()
        {
            XmlDocument doc = new XmlDocument();
            XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(docNode);

            XmlNode settingsNode = doc.CreateElement("Settings");
            doc.AppendChild(settingsNode);

            XmlNode appSettingsNode = doc.CreateElement("AppSettings");
            settingsNode.AppendChild(appSettingsNode);

            XmlNode nameNode = doc.CreateElement("PathSound");
            nameNode.AppendChild(doc.CreateTextNode("This is path"));
            appSettingsNode.AppendChild(nameNode);
            XmlNode priceNode = doc.CreateElement("Price");
            priceNode.AppendChild(doc.CreateTextNode("Free"));
            appSettingsNode.AppendChild(priceNode);

            // Create and add another product node.
            appSettingsNode = doc.CreateElement("UserSettings");
            settingsNode.AppendChild(appSettingsNode);

            nameNode = doc.CreateElement("Name");
            nameNode.AppendChild(doc.CreateTextNode("Pasha"));
            appSettingsNode.AppendChild(nameNode);

            priceNode = doc.CreateElement("Age");
            priceNode.AppendChild(doc.CreateTextNode("18"));
            appSettingsNode.AppendChild(priceNode);

            XmlNode vkId = doc.CreateElement("VkIdUser");
            vkId.AppendChild(doc.CreateTextNode("123412431"));
            appSettingsNode.AppendChild(vkId);

            using (StreamWriter writer = new StreamWriter(_pathToFolder + _fileName, false)) 
            {
                doc.Save(writer);
            }
        }

    }
}
