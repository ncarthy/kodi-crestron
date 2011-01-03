using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace CopyThumbnails
{
    class Program
    {
        private static int newWidth;

        static void Main(string[] args)
        {
            // Arg 1 is the directory to do, recursively
            // Arg 2 is new width, use '0' if not resizing
            // Arg 3 is a list of exclude masks. A good list is "generated,Profiles,Pictures,Programs,LastFM,Bookmarks".
            //
            // Usage:    CopyThumbnails.exe "C:\Users\Admin\AppData\Roaming\XBMC\userdata\Thumbnails",
            //                    120,"generated,Profiles,Pictures,Programs,Fanart,LastFM,Bookmarks"
            if (args.Length <2)
	        {
                if (args.Length >= 1)
                {
                    if (args[0] == "/?" || args[0] == "-?" || args[0].ToLower() == "-h" || args[0].ToLower() == "/h" || args[0].ToLower() == "help")
                    {
                        ShowHelp();
                        return;
                    }
                }
                Console.WriteLine(String.Format("Error: Not enough arguments, need at least 2, {0} provided", args.Length));
                ShowHelp();
                return;
	        }
            if (args.Length > 3)
            {
                Console.WriteLine(String.Format("Error: Too many arguments, no more than 3 allowed, {0} provided", args.Length));
                ShowHelp();
                return;
            }
            string rootDirectory = args[0];
            if (!Directory.Exists(rootDirectory))
            {
                Console.WriteLine(String.Format("Error: Directory not found: {0}", args[0]));
                ShowHelp();
                return;
            }
            
            Int32.TryParse(args[1], out newWidth);
            
            string[] exclude = null;
            if (args.Length == 3)
            {
                exclude = args[2].Split(',');
            }

            ProcessDirectory(rootDirectory, exclude);
        }

        private static void ProcessDirectory(string rootDirectory, string[] excludes)
        {
            foreach (string directory in Directory.GetDirectories(rootDirectory))
            {
                if (TestDir(directory,excludes))
                {
                    //recursive
                    ProcessDirectory(directory, excludes);

                    // Only do directories whose name is one character long
                    DirectoryInfo di = new DirectoryInfo(directory);
                    if (di.Name.Length == 1 || di.Name == "Artists" || di.Name == "Fanart")
                    {
                        DoDir(directory);
                    } 
                }
            }
        }

        //Return false if the directory name contains any of the excludes strings
        private static bool TestDir(string directory, string[] excludes)
        {
            if (excludes == null)
            {
                return true;
            }

            foreach (string s in excludes)
            {
                if (directory.Contains(s))
                {
                    return false;
                }
            }
            return true;
        }

        // Make a copy of each ".tbn" file in the given folder
        static void DoDir(string folder)
        {
            foreach (string fileName in Directory.GetFiles(folder))
            {
                //Resize the image and save it as a JPEG
                ResizeAndSaveAs(fileName, newWidth, ImageFormat.Jpeg);
            }
        }

        private static void ResizeAndSaveAs(string fileName, int width, ImageFormat format)
        {
            FileInfo fileInfo = new FileInfo(fileName);
            if (fileInfo.Extension == ".tbn")
            {
#if DEBUG
                Console.WriteLine(string.Format("Doing: {0}", fileName));
#endif
                string newFileName = fileInfo.DirectoryName + Path.DirectorySeparatorChar
                                            + fileInfo.Name.Replace(".tbn", ".jpg");


                Image i = ResizeImage(fileName, newFileName, width, 9999, false);
                try
                {
                    i.Save(newFileName, format);
                }
                catch (System.Runtime.InteropServices.ExternalException ex)
                {
                    // DO NOTHING
                    Console.WriteLine(ex.Message);
                }
                catch (Exception e)
                {
                    throw new ApplicationException("Error resizing file", e);
                }
            }
        }

        static Image ResizeImage(string OriginalFile, string NewFile, int NewWidth, int MaxHeight, bool OnlyResizeIfWider)
        {
            Image FullsizeImage = Image.FromFile(OriginalFile);

            if (NewWidth == 0) return FullsizeImage;

            // Prevent using images internal thumbnail
            FullsizeImage.RotateFlip(RotateFlipType.Rotate180FlipNone);
            FullsizeImage.RotateFlip(RotateFlipType.Rotate180FlipNone);

            if (OnlyResizeIfWider)
            {
                if (FullsizeImage.Width <= NewWidth)
                {
                    NewWidth = FullsizeImage.Width;
                }
            }

            int NewHeight = FullsizeImage.Height * NewWidth / FullsizeImage.Width;
            if (NewHeight > MaxHeight)
            {
                // Resize with height instead
                NewWidth = FullsizeImage.Width * MaxHeight / FullsizeImage.Height;
                NewHeight = MaxHeight;
            }

            Image NewImage = FullsizeImage.GetThumbnailImage(NewWidth, NewHeight, null, IntPtr.Zero);

            // Clear handle to original file so that we can overwrite it if necessary
            FullsizeImage.Dispose();

            return NewImage;
        }

        private static void ShowHelp()
        {
            Console.WriteLine();
            Console.Write("Recursively copies and resizes all XBMC .tbn thumbnail files in a given directory. ");
            Console.WriteLine("The new files have the same name as the old but with the extension '.jpg'.");
            Console.WriteLine();
            Console.WriteLine("CopyThumbnails.exe [/H] directory width [exclude_masks]");
            Console.WriteLine();
            Console.WriteLine("   /H               Shows this help message.");
            Console.WriteLine("   directory        The XBMC user's thumbnail directory.");
            Console.WriteLine("   width            The new image width, use '0' if not resizing.");
            Console.WriteLine("   exclude_masks    A list of exclude masks.");
            Console.WriteLine();
            Console.WriteLine("usage: ");
            Console.Write("CopyThumbnails.exe \"C:\\Users\\Admin\\AppData\\Roaming\\XBMC\\userdata\\Thumbnails\"");
            Console.WriteLine(" 120 \"generated,Profiles,Pictures,Programs,Artists,Fanart,LastFM,Bookmarks\"");
        }
    }
}
