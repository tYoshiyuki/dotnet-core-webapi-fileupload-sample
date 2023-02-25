namespace DotNetCoreWebApiFileUploadSample.Models
{
    /// <summary>
    /// 複数ファイルアップロードレスポンス
    /// </summary>
    public class FileUploadListResponse
    {
        /// <summary>
        /// ファイル名のリスト
        /// </summary>
        public IEnumerable<string>? FileNameList { get; set; }

        /// <summary>
        /// ファイルのメタデータリスト
        /// </summary>
        public IEnumerable<string>? FileMetaDataList { get; set; }

        /// <summary>
        /// ファイルサイズのリスト (byte)
        /// </summary>
        public IEnumerable<long>? FileSizeList { get; set; }
    }
}
