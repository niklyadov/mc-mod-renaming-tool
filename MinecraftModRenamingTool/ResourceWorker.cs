using System;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace MinecraftModRenamingTool
{
    public class ResourceWorker
    {
        private string _zipFilePath;

        public ResourceWorker(string zipFilePath)
        {
            _zipFilePath = zipFilePath;
        }

        /// <summary>
        /// Opens file stream inside the archive
        /// </summary>
        /// <param name="filename">Filename inside zip</param>
        /// <returns>Stream of file</returns>
        public Stream GetFile(string filename)
        {
            try
            {
                return ZipFile.OpenRead(_zipFilePath)
                    .Entries.Single(x => x.FullName.Equals(filename, StringComparison.InvariantCulture))
                    .Open();
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}