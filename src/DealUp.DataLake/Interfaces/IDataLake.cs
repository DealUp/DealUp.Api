using DealUp.DataLake.AmazonS3.Models;

namespace DealUp.DataLake.Interfaces;

public interface IDataLake
{
    private const string MediaUploadPath = "uploads";
    private const char PathSeparator = '/';

    public CreatePreSignedPostResponse GeneratePreSignedPost(string filePath, string fileName);
    public Task<string> GeneratePreSignedGet(string objectKey);
    public Task<List<string>> GetKeysByPrefixAsync(string prefix);

    public static string CombinePath(params string[] paths)
    {
        paths = [MediaUploadPath, ..paths];
        return Path.Combine(paths).Replace('\\', PathSeparator);
    }
}