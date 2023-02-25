using DotNetCoreWebApiFileUploadSample.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreWebApiFileUploadSample.Controllers
{
    /// <summary>
    /// ファイルアップロードコントローラ
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        /// <summary>
        /// ファイルアップロード
        /// </summary>
        /// <param name="request"><see cref="FileUploadRequest"/></param>
        /// <returns><see cref="FileUploadResponse"/></returns>
        [HttpPost("Upload")]
        public IActionResult Upload([FromForm] FileUploadRequest request)
        {
            return Ok(new FileUploadResponse
            {
                FileName = request.File?.FileName,
                FileMetaData = request.MetaData,
                FileSize = request.File?.Length,
            });
        }

        /// <summary>
        /// 複数ファイルアップロード
        /// </summary>
        /// <param name="request"><see cref="FileUploadListRequest"/></param>
        /// <returns><see cref="FileUploadListResponse"/></returns>
        [HttpPost("UploadList")]
        public IActionResult UploadList([FromForm] FileUploadListRequest request)
        {
            return Ok(new FileUploadListResponse
            {
                FileNameList = request.FileList?.Select(x => x.FileName),
                FileMetaDataList = request.MetaDataList,
                FileSizeList = request.FileList?.Select(x => x.Length)
            });
        }
    }
}
