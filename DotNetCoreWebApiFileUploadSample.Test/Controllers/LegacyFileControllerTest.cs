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
        private HttpClient client;

        [OneTimeSetUp]
        public void Init()
        {
            var factory = new TestWebApplicationFactory();
            client = factory.CreateClient();
        }

        /// <summary>
        /// <see cref="LegacyFileController.Upload"/>が正常に動作することを確認します。
        /// </summary>
        [Test]
        public async Task Upload_OK()
        {
            // Arrange
            string filePath = "SampleFile.txt";

            await using var fs = File.OpenRead(filePath);
            using var fileContent = new StreamContent(fs);
            
            var content = new MultipartFormDataContent
            {
                {new StringContent("This is SampleFile MetaData.", Encoding.UTF8), nameof(FileUploadRequest.MetaData)},
                { fileContent, nameof(FileUploadRequest.File), "SampleFile.txt" }
            };

            // Act
            var response = await client.PostAsync("api/LegacyFile/Upload", content);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            string json = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<FileUploadResponse>(json);
            Assert.Multiple(() =>
            {
                Assert.That(result.FileMetaData, Is.EqualTo("This is SampleFile MetaData."));
                Assert.That(result.FileName, Is.EqualTo("SampleFile.txt"));
                Assert.That(result.FileSize, Is.EqualTo(15L));
            });
        }
    }
}
