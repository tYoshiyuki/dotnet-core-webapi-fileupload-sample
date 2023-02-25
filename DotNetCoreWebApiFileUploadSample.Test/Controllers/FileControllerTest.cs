using DotNetCoreWebApiFileUploadSample.Models;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using DotNetCoreWebApiFileUploadSample.Controllers;

namespace DotNetCoreWebApiFileUploadSample.Test.Controllers
{
    /// <summary>
    /// <see cref="FileController"/>のテストクラス
    /// </summary>
    internal class FileControllerTest
    {
        private HttpClient client;

        [OneTimeSetUp]
        public void Init()
        {
            var factory = new TestWebApplicationFactory();
            client = factory.CreateClient();
        }

        /// <summary>
        /// <see cref="FileController.Upload"/>が正常に動作することを確認します。
        /// </summary>
        [Test]
        public async Task Upload_OK()
        {
            // Arrange
            var filePath = "SampleFile.txt";

            await using var fs = File.OpenRead(filePath);
            using var fileContent = new StreamContent(fs);
            
            var content = new MultipartFormDataContent
            {
                {new StringContent("This is SampleFile MetaData.", Encoding.UTF8), nameof(FileUploadRequest.MetaData)},
                { fileContent, nameof(FileUploadRequest.File), "SampleFile.txt" }
            };

            // Act
            var response = await client.PostAsync("api/File/Upload", content);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var json = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<FileUploadResponse>(json);
            Assert.Multiple(() =>
            {
                Assert.That(result.FileMetaData, Is.EqualTo("This is SampleFile MetaData."));
                Assert.That(result.FileName, Is.EqualTo("SampleFile.txt"));
                Assert.That(result.FileSize, Is.EqualTo(15L));
            });
        }

        /// <summary>
        /// <see cref="FileController.UploadList"/>が正常に動作することを確認します。
        /// </summary>
        [Test]
        public async Task UploadList_Ok()
        {
            // Arrange
            var filePath = "SampleFile.txt";

            await using var fs1 = File.OpenRead(filePath);
            await using var fs2 = File.OpenRead(filePath);
            using var fileContent1 = new StreamContent(fs1);
            using var fileContent2 = new StreamContent(fs2);

            var content = new MultipartFormDataContent
            {
                {new StringContent("This is SampleFile MetaData 1.", Encoding.UTF8), nameof(FileUploadListRequest.MetaDataList)},
                {new StringContent("This is SampleFile MetaData 2.", Encoding.UTF8), nameof(FileUploadListRequest.MetaDataList)},
                { fileContent1, nameof(FileUploadListRequest.FileList), "SampleFile1.txt" },
                { fileContent2, nameof(FileUploadListRequest.FileList), "SampleFile2.txt" }
            };

            // Act
            var response = await client.PostAsync("api/File/UploadList", content);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var json = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<FileUploadListResponse>(json);
            Assert.Multiple(() =>
            {
                Assert.That(result.FileMetaDataList, Is.EqualTo(new[] { "This is SampleFile MetaData 1.", "This is SampleFile MetaData 2." }));
                Assert.That(result.FileNameList, Is.EqualTo(new[] { "SampleFile1.txt", "SampleFile2.txt" }));
                Assert.That(result.FileSizeList, Is.EqualTo(new[] { 15L, 15L }));
            });
        }
    }
}
