using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeDirectories
{
    public static class FolderHelper
    {
        public static void OpenFolder(string folderPath)
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    Arguments = folderPath,
                    FileName = "explorer.exe"
                };
                Process.Start(startInfo);   // this opens File Explorer like cmd >start path
            }
            catch (DirectoryNotFoundException dexc)
            {
                // Turn this into an Exception that displays on any UI
                throw new DirectoryNotFoundException(string.Format("{0} Directory does not exist!", folderPath), dexc);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static bool DirectoryExists(string folderPath)
        {
            return Directory.Exists(folderPath);
        }

        // TODO: Add more cases here using an Enum as a second parameter
        public static bool ValidPatternName(string dirNamePattern)
        {
            if (dirNamePattern.Contains('*')) return true;
            return false;
        }

        public static List<string>? MakeDirectories(int numberOfDirectories, string dirNamePattern, string dirFullPath)
        {
            try
            {
                // Fill out the list of directory names
                List<string> dirFullPathWithNames = new List<string>();

                if (numberOfDirectories > 0)
                {
                    for (int i = 0; i < numberOfDirectories; i++)
                    {
                        var dirFullPathWithName = $"{dirFullPath}\\{dirNamePattern.Replace("*", (i+1).ToString())}";
                        // Pad with 0s if needed

                        // Create directories
                        if (!Directory.Exists(dirFullPathWithName))
                        {
                            Directory.CreateDirectory(dirFullPathWithName);
                            dirFullPathWithNames.Add(dirFullPathWithName);
                        }                        
                    }
                }

                return dirFullPathWithNames;
            }
            catch (Exception exc)
            {
                throw new Exception($"The specified directories could not be created because {exc.Message}");
            }
        }
    }
}
