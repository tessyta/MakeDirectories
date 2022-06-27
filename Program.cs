using MakeDirectories;


Console.WriteLine("Hello World! Are you ready to create some directories in your computer? (Y/N) ");
var @continue = Console.ReadLine() ?? String.Empty;

string dirFullPath = string.Empty;

if (@continue.ToUpper() == "Y")
{
    do
    {
        Console.WriteLine("Enter the full path where you want to create the directories: ");
        dirFullPath = Console.ReadLine() ?? String.Empty;

        try
        {
            // Verify new directory full path exists
            if (FolderHelper.DirectoryExists(dirFullPath))
            {
                // Continue if it exists, otherwise ask again
                Console.WriteLine("How many directories do you want to create (1 to Any Number)?");
                string inputNumberOfDirectories = Console.ReadLine() ?? string.Empty;

                if (int.TryParse(inputNumberOfDirectories, out int numberOfDirectories))
                {
                    Console.WriteLine("Do you want to name them with consecutive numbers?");
                    var nameDirsWithConsecutiveNumbers = Console.ReadLine() ?? string.Empty;

                    if (nameDirsWithConsecutiveNumbers.ToUpper() == "Y")
                    {
                        Console.WriteLine("Enter the directory's name pattern using '*' for the number(s) (i.e. Chapter*_4ed) ");
                        var dirNamePattern = Console.ReadLine() ?? string.Empty;

                        if (FolderHelper.ValidPatternName(dirNamePattern))
                        {
                            // Create the folders in the path and open File Explorer to show user
                            var dirsCreated = FolderHelper.MakeDirectories(numberOfDirectories, dirNamePattern, dirFullPath);

                            if (dirsCreated != null && dirsCreated.Count > 0)
                            {
                                Console.WriteLine("The following directories were created: ");

                                foreach (var dir in dirsCreated)
                                {
                                    Console.WriteLine(dir);
                                }

                                FolderHelper.OpenFolder(dirFullPath);
                            }                            
                        }
                        else
                        {
                            throw new Exception($"The directory's name pattern you entered, {dirNamePattern}, is not valid.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("TO DO more here");
                    }                    
                }
                else
                {
                    throw new Exception($"The number of directories you entered, {inputNumberOfDirectories}, is not a valid number");
                }                

                Console.Write("Do you want to create more directories? (Y/N) ");
                @continue = Console.ReadLine() ?? String.Empty;
                dirFullPath = String.Empty;
            }
            else
            {
                throw new Exception(string.Format("{0} Directory does not exist!", dirFullPath));
            }
        }
        catch (Exception e)
        {
            Console.WriteLine();
            Console.WriteLine("******* ERROR ******");
            Console.WriteLine(e.Message);
            Console.WriteLine("********************");
            Console.WriteLine();

            Console.Write("Do you want to try again? (Y/N) ");
            @continue = Console.ReadLine() ?? String.Empty;
            dirFullPath= String.Empty;
        }

        
    }
    while (!FolderHelper.DirectoryExists(dirFullPath) && @continue.ToUpper() == "Y");
}

Console.WriteLine("Bye Bye...hope to see you next time :-)");