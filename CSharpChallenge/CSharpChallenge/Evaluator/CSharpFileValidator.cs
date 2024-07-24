using System.Diagnostics;

namespace CSharpChallenge.Evaluator
{
    /// <summary>
    /// Provides methods to validate C# files and check their ability to compile.
    /// </summary>
    public class CSharpFileValidator
    {
        /// <summary>
        /// Checks if the given file is a valid C# file.
        /// </summary>
        /// <param name="file">The file to validate.</param>
        /// <returns>True if the file is a valid C# file; otherwise, false.</returns>
        public bool IsValidCsFile(IFormFile file)
        {
            if (file.Length == 0)
            {
                return false;
            }

            string extension = Path.GetExtension(file.FileName);
            
            // Check if the extension to the file is .cs
            return string.Equals(extension, ".cs", StringComparison.Ordinal);
        }

        /// <summary>
        /// Checks if the C# file at the specified path can be compiled.
        /// </summary>
        /// <param name="filePath">The path to the C# file.</param>
        /// <returns>True if the file can be compiled; otherwise, false.</returns>
        public bool IsFileAbleToCompile(string filePath)
        {
            string projectDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/TempProject");
            string fileName = Path.GetFileName(filePath);
            string projectFilePath = Path.Combine(projectDir, "TempProject.csproj");

            // Ensure the project directory exists
            Directory.CreateDirectory(projectDir);
            
            // Copy the C# file to the project directory
            File.Copy(filePath, Path.Combine(projectDir, fileName), true);
            
            string projectFileContent = @"
                <Project Sdk=""Microsoft.NET.Sdk"">
                  <PropertyGroup>
                    <OutputType>Exe</OutputType>
                    <TargetFramework>net8.0</TargetFramework>
                    <UseAppHost>false</UseAppHost>
                  </PropertyGroup>
                </Project>";

            // Save the project file
            File.WriteAllText(projectFilePath, projectFileContent);

            // Compile the project using dotnet build
            Process compileProcess = new Process();
            compileProcess.StartInfo.FileName = "dotnet";
            compileProcess.StartInfo.Arguments = $"build {projectDir} -c Release";
            compileProcess.StartInfo.RedirectStandardOutput = true;
            compileProcess.StartInfo.RedirectStandardError = true;
            compileProcess.StartInfo.UseShellExecute = false;

            compileProcess.Start();
            compileProcess.WaitForExit();

            // Return true if the exit code is 0, indicating a successful build
            return compileProcess.ExitCode == 0;
        }
    }
}
