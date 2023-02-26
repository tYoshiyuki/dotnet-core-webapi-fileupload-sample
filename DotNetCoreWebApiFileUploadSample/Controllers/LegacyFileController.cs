using System.Net.Http.Headers;
using DotNetCoreWebApiFileUploadSample.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreWebApiFileUploadSample.Controllers
{
    /// <summary>
    /// ファイルアップロードコントローラ
    /// (※ 比較のため、ASP.NET Web API時代の方式で敢えて実装)
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LegacyFileController : ControllerBase
    {
        private readonly HttpContext httpContext;
        private readonly string rootPath;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="httpContextAccessor"><see cref="IHttpContextAccessor"/></param>
        /// <param name="environment"><see cref="IHostEnvironment"/></param>
        public LegacyFileController(IHttpContextAccessor httpContextAccessor, IHostEnvironment environment)
        {
            httpContext = httpContextAccessor.HttpContext;
            rootPath = environment.ContentRootPath;
        }

        /// <summary>
        /// ファイルアップロード
        /// </summary>
        /// <returns><see cref="FileUploadResponse"/></returns>
        /// <remarks>サーバサイドでアップロード情報を参照しレスポンスへ返却します。サンプル実装のため実際のアップロード処理は何も行いません。</remarks>
        [HttpPost("Upload")]
        public async Task<IActionResult> Upload()
        {
            var provider = new MultipartFormDataStreamProvider(rootPath);
            var content = new StreamContent(httpContext.Request.Body);
            content.Headers.ContentType = MediaTypeHeaderValue.Parse(httpContext.Request.ContentType);

            // ローカルディスク上にファイルを保存
            await content.ReadAsMultipartAsync(provider);

            var file = provider.FileData.First();
            var fileInfo = new FileInfo(file.LocalFileName);

            var metadata = provider.FormData.Get("MetaData");

            var response = new FileUploadResponse
            {
                FileMetaData = metadata,
                FileName = file.Headers.ContentDisposition?.FileName,
                FileSize = fileInfo.Length
            };

            // サンプル実装のためアップロードしたファイルを削除
            fileInfo.Delete();

            return Ok(response);
        }
    }
}
