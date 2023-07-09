using DotNetCoreWebApiFileUploadSample.Models;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using DotNetCoreWebApiFileUploadSample.Controllers;

namespace DotNetCoreWebApiFileUploadSample.Test.Controllers
{
    /// <summary>
    /// <see cref="LegacyFileController"/>のテストクラス
    /// </summary>
    internal class LegacyFileControllerTest
    {
        private HttpClient _client = null!;

        [OneTimeSetUp]
        public void Init()
        {
            var factory = new TestWebApplicationFactory();
            _client = factory.CreateClient();
        }

        /// <summary>
        /// <see cref="LegacyFileController.Upload"/>が正常に動作することを確認します。
        /// </summary>
        [Test]
        public async Task Upload_OK()
        {
            // Arrange
            string filePath = "SampleFile.txt";

            await using FileStream fs = File.OpenRead(filePath);
            using var fileContent = new StreamContent(fs);
            
            var content = new MultipartFormDataContent
            {
                {new StringContent("This is SampleFile MetaData.", Encoding.UTF8), nameof(FileUploadRequest.MetaData)},
                { fileContent, nameof(FileUploadRequest.File), "SampleFile.txt" }
            };

            // Act
            HttpResponseMessage response = await _client.PostAsync("api/LegacyFile/Upload", content);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            string json = response.Content.ReadAsStringAsync().Result;
            FileUploadResponse result = JsonConvert.DeserializeObject<FileUploadResponse>(json);
            Assert.Multiple(() =>
            {
                Assert.That(result.FileMetaData, Is.EqualTo("This is SampleFile MetaData."));
                Assert.That(result.FileName, Is.EqualTo("SampleFile.txt"));
                Assert.That(result.FileSize, Is.EqualTo(15L));
            });
        }
    }
}
