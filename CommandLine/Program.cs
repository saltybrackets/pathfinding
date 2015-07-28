using System;
using System.Drawing;
using System.Text;
using PathFinding;
using PathFinding.Maps;
using PathFinding.Strategies;


namespace CommandLine
{
	/// <summary>
	/// Command line interface for PathFinding features.
	/// </summary>
	class Program
	{
		static void Main(string[] args)
		{
			// Display help.
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
				Console.WriteLine("No destination image path specified.");
				return;
			}

			// Collect file paths.
			string sourcePath = args[0];
			string outputPath = args[1];
			
			// Optional strategy option specified.
			string strategyOption = null;
			if (args.Length > 2)
				strategyOption = args[2];

			// Attempt to load the image.
			Bitmap sourceImage;
			try
			{
				 sourceImage = new Bitmap(sourcePath);
			}
			catch (Exception)
			{
				Console.WriteLine("Source file is not a valid image format.");
				return;
			}
			
			// TODO: Allow other strategies if time allows.
			Map map = new Map(sourceImage);
			MapPath path = PathFinder.FindPath(map, Strategies.BreadthFirst, strategyOption);

			// No solution found.
			if (path == null)
			{
				Console.WriteLine("No path could be found to solve the maze.");
				Console.WriteLine("Are you using the correct colors?");
				return;
			}

			PathFinder.OutputBitmap(path, map, outputPath);
			System.Console.WriteLine("Solution saved successfully to " + outputPath);
		}

	
		/// <summary>
		/// Show usage instructions for command line interface.
		/// </summary>
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
				.AppendLine("Maps must use only pure black/white/red/blue pixels to be solved properly.")
				.AppendLine("Output solution will have path marked in lime green.")
				.AppendLine()
				.AppendLine("There is an optional third parameter. Consult your local wizard for details.")
				.ToString();
			
			Console.WriteLine(output);
		}
	}
}
