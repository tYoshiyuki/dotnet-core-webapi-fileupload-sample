namespace DotNetCoreWebApiFileUploadSample.Models
{
    /// <summary>
    /// 複数ファイルアップロードリクエスト
    /// </summary>
    public class FileUploadListRequest
    {
        /// <summary>
        /// ファイルのメタデータリスト
        /// </summary>
        public IEnumerable<string>? MetaDataList { get; set; }

        /// <summary>
        /// ファイルデータのリスト
        /// </summary>
        public IEnumerable<IFormFile>? FileList { get; set; }
    }
}
