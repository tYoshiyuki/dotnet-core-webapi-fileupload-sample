namespace DotNetCoreWebApiFileUploadSample.Models
{
    /// <summary>
    /// ファイルアップロードリクエスト
    /// </summary>
    public class FileUploadRequest
    {
        /// <summary>
        /// ファイルのメタデータ
        /// </summary>
        public string? MetaData { get; set; }

        /// <summary>
        /// ファイルデータ
        /// </summary>
        public IFormFile? File { get; set; }
    }
}
