# dotnet-core-webapi-fileupload-sample
ASP.NET Core の WebAPI で ファイルアップロードを行うサンプル

## Feature
- .NET6
- ASP.NET Core

## Note
- ASP.NET Core では、`IFormFile` を利用することで `multipart/form-data` のファイルをアップロードすることが出来ます。
  - `FileController` にサンプル実装を行っています。
- ASP.NET Web API で利用していた `MultipartFormDataStreamProvider` は使う必要が無くなっています。
  - `LegacyFileController` にサンプル実装を行っています。

## Reference
- ASP.NET Core の ファイルアップロード
    - https://learn.microsoft.com/ja-jp/aspnet/core/mvc/models/file-uploads?view=aspnetcore-6.0
- ASP.NET Web API のファイルアップロード
    - https://learn.microsoft.com/ja-jp/aspnet/web-api/overview/advanced/sending-html-form-data-part-2