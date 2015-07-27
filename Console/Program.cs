using System;
using System.Drawing;
using System.Text;
using PathFinding;
using PathFinding.Maps;
using PathFinding.Strategies;


namespace Console
{
	class Program
	{
		static void Main(string[] args)
		{
			if ((args.Length == 0) ||
			    (args[0] == "-?") ||
			    (args[0] == "/?") ||
				(args[0].ToLower() == "help"))
			{
				OutputHelp();
				return;
			}
			
			// Forgot to specify solution path.
			if (args.Length < 2)
			{
				System.Console.WriteLine("No destination image path specified.");
				return;
			}

			// Collect file paths.
			string sourcePath = args[0];
			string outputPath = args[1];

			// Attempt to load the image.
			Bitmap sourceImage;
			try
			{
				 sourceImage = new Bitmap(sourcePath);
			}
			catch (Exception)
			{
				System.Console.WriteLine("Source file is not a valid image format.");
				return;
			}
			
			// TODO: Allow other strategies if time allows.
			Map map = new Map(sourceImage);
			MapPath path = PathFinder.FindPath(map, Strategies.BreadthFirst);

			// No solution found.
			if (path == null)
			{
				System.Console.WriteLine("No path could be found to solve the maze.");
				System.Console.WriteLine("Are you using the correct colors?");
				return;
			}

			PathFinder.OutputBitmap(path, map, outputPath);
			System.Console.WriteLine("Solution saved successfully to " + outputPath);
		}

	
		static void OutputHelp()
		{
			string output = new StringBuilder()
				.AppendLine()
				.AppendLine("Usage: maze.exe source destination")
				.Append("  source").AppendLine("\t\tPath to a source map image.")
				.Append("  destination").AppendLine("\t\tPath where solution image will be output.")
				.AppendLine()
				.AppendLine("Valid image formats include BMP, PNG, JPEG, GIF, TIFF, and WMP files.")
				.AppendLine("Maps are solved according to pixel color.")
				.AppendLine("Walls should be black, empty spaces white.")
				.AppendLine("Start points should be red pixels.")
				.AppendLine("End points should be blue.")
				.AppendLine("Maps must use only pure black, white, red, and blue pixels to be solved properly.")
				.AppendLine("Output solution will have path marked in lime green.")
				.ToString();
			
			System.Console.WriteLine(output);
		}
	}
}
