namespace DotNetCoreWebApiFileUploadSample.Models
{
    /// <summary>
    /// ファイルアップロードレスポンス
    /// </summary>
    public class FileUploadResponse
    {
        /// <summary>
        /// ファイル名
        /// </summary>
        public string? FileName { get; set; }

        /// <summary>
        /// ファイルのメタデータ
        /// </summary>
        public string? FileMetaData { get; set; }

        /// <summary>
        /// ファイルサイズ (byte)
        /// </summary>
        public long? FileSize { get; set; }
    }
}
