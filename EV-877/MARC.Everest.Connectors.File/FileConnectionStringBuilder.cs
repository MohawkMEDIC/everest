using System;
using System.Collections.Generic;
using System.Text;

namespace MARC.Everest.Connectors.File
{
    /// <summary>
    /// Abstracts the creation of a connection string to be provided to a 
    /// <see cref="T:MARC.Everest.Connectors.File.FileListenConnector"/> or
    /// <see cref="T:MARC.Everest.Connectors.File.FilePublishConnector"/>
    /// </summary>
    public class FileConnectionStringBuilder
    {

        /// <summary>
        /// Specifies the directory to which files should be published or 
        /// consumed.
        /// </summary>
        /// <remarks>
        /// <para>When passed to a server connector, a file system watch 
        /// will be placed on the directory.</para>
        /// </remarks>
        public string Directory { get; set; }
        /// <summary>
        /// Identifies whether files should be kept after consumption
        /// </summary>
        /// <remarks>
        /// Specifies whether or not the <see cref="T:MARC.Everest.Connectors.File.FileListenConnector"/>
        /// should keep files after successfully formatting them from the disk
        /// or should delete them after consumption. Default is false.
        /// </remarks>
        public bool KeepFiles { get; set; }
        /// <summary>
        /// Identifies the file pattern to listen for.
        /// </summary>
        /// <remarks>A standard windows file pattern should be passed as this parameter. This 
        /// modifies the behavior of the <see cref="T:MARC.Everest.Connectors.File.FileListenConnector"/></remarks>
        public string Pattern { get; set; }
        /// <summary>
        /// Identifies whether or not existing files in the subscribed directory should be processed
        /// </summary>
        /// <remarks>
        /// When false, the <see cref="T:MARC.Everest.Connectors.File.FileListenConnector"/> will only
        /// process new files that are placed into the directory.
        /// </remarks>
        public bool ProcessExisting { get; set; }
        /// <summary>
        /// Identifies the name of the file to write
        /// </summary>
        /// <remarks>Instructs the <see cref="T:MARC.Everest.Connectors.File.FilePublishConnector"/> to write
        /// all published messages to one file (overwriting files)</remarks>
        public string FileName { get; set; }
        /// <summary>
        /// Identifies the naming convention of new files created by the connector
        /// </summary>
        /// <remarks>
        /// Identifies the naming function to call generate file names. The two 
        /// naming conventions are Guid and message identifier
        /// </remarks>
        public FileNamingConventionType? NamingConvention { get; set; }
        /// <summary>
        /// When true instructs the formatter to overwrite files
        /// </summary>
        /// <remarks>
        /// When false, and an existing file is encountered the file is suffixed with 1,2,3... For example,
        /// if the file name is File and File exists, the next publish will be to File1, followed by 
        /// File2, File3
        /// </remarks>
        public bool OverwriteExistingFiles { get; set; }

        /// <summary>
        /// Generate connection string
        /// </summary>
        public string GenerateConnectionString()
        {
            StringBuilder sb = new StringBuilder();
            bool isPublish = false;
            if (!String.IsNullOrEmpty(this.Directory))
                sb.AppendFormat("directory={0};", this.Directory);
            if (!String.IsNullOrEmpty(this.Pattern))
            {
                isPublish = true;
                sb.AppendFormat("pattern={0};", this.Pattern);
            }
            if (isPublish)
                sb.AppendFormat("keepFiles={0};processExisting={1};", this.KeepFiles, this.ProcessExisting);
            else
            {
                if (this.NamingConvention.HasValue)
                    sb.AppendFormat("naming={0};", this.NamingConvention.Value.ToString().ToLower());
                sb.AppendFormat("overwrite={0};", this.OverwriteExistingFiles);
            }
            if (!String.IsNullOrEmpty(this.FileName))
            {
                if (isPublish)
                    throw new InvalidOperationException("Cannot use the FileName property when using Publish connector operations");
                sb.AppendFormat("file={0}", this.FileName);
            }
            return sb.ToString();
        }
    }
}
