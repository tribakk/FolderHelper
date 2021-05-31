using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Xml;


namespace TestExport.kernal
{
    class utils
    {
        static public string GetFileDirectory(string fileName)
        {
            int indexPatch = fileName.LastIndexOf("\\");
            string destination = fileName.Substring(0, indexPatch /*+ 1*/);
            return destination;
        }

        static public List<string> GetSubFiles(string directory, bool bStruct, bool bWithSubFolder = true)
        {
            List<string> filesList = new List<string>();
            GetSubFiles(directory, directory, filesList, bStruct, bWithSubFolder);
            return filesList;
        }

        static public void GetSubFiles(string rootdir, string directory, List<string> fileList, bool bStruct, bool bWithSubFolder = true)
        {
            if (bWithSubFolder)
            {
                string[] dirArray = Directory.GetDirectories(directory);
                for (int dir = 0; dir < dirArray.Length; dir++)
                {
                    GetSubFiles(rootdir, dirArray[dir], fileList, bStruct);
                }
            }

            string[] fileArray = Directory.GetFiles(directory);
            if (bStruct)
            {
                bool bNeedWriteDirectory = false;
                string dirName = rootdir;
                int dirLength = dirName.Length;
                for (int fileNo = 0; fileNo < fileArray.Length; fileNo++)
                {
                    if (bNeedWriteDirectory)
                    {
                        fileList.Add(dirName);
                        bNeedWriteDirectory = false;
                    }
                    fileList.Add(fileArray[fileNo].Substring(dirLength));
                }
            }
            else
            {
                for (int fileNo = 0; fileNo < fileArray.Length; fileNo++)
                    fileList.Add(fileArray[fileNo]);
            }
        }

        static public void RemoveDirectory(List<string> fileList, string directory)
        {
            int count = directory.Length;
            for (int i = 0; i < fileList.Count; i++)
            {
                fileList[i] = fileList[i].Substring(count);
            }
        }

        static public void UnZip(string fileName, string destination)
        {
            Process process = new Process();
            process.StartInfo.FileName = "C:\\Program Files\\7-Zip\\7z.exe";
            process.StartInfo.Arguments = "x " + "\"" + fileName + "\" -o\"" + destination + "\" -Y";
            process.Start();
            process.WaitForExit();
        }

        public static string PrintXML(string xml)
        {
            string result = "";

            MemoryStream mStream = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(mStream, Encoding.Unicode);
            //XmlTextWriter writer = new XmlTextWriter(mStream, Encoding.Default);
            XmlDocument document = new XmlDocument();

            try
            {
                // Load the XmlDocument with the XML.
                document.LoadXml(xml);

                writer.Formatting = Formatting.Indented;
                writer.IndentChar = '\t';
                writer.Indentation = 1;


                // Write the XML into a formatting XmlTextWriter
                document.WriteContentTo(writer);
                writer.Flush();
                mStream.Flush();

                // Have to rewind the MemoryStream in order to read
                // its contents.
                mStream.Position = 0;

                // Read MemoryStream contents into a StreamReader.
                StreamReader sReader = new StreamReader(mStream);

                // Extract the text from the StreamReader.
                string formattedXml = sReader.ReadToEnd();

                result = formattedXml;
                mStream.Close();
                writer.Close();
            }
            catch (XmlException xml1)
            {
                // Handle the exception
            }

            result = result.Replace(" /", "/");
            result = result.Replace("<?xml version=\"1.0\" encoding=\"UTF-8\"?>", 
                "<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            return result;
        }


        public static string ReadFile(String path)
        {
            StreamReader sr = new StreamReader(path);
            string text = sr.ReadToEnd();
            sr.Close();
            return text;
        }

        public static void WriteFile(String path, string text)
        {
            StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.Unicode);
            sw.Write(text);
            sw.Close();
        }
    }
}
