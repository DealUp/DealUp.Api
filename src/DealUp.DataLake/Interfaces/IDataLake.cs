using DealUp.DataLake.AmazonS3.Models;

namespace DealUp.DataLake.Interfaces;

public interface IDataLake
{
    private const string MediaUploadPath = "uploads";
    private const char PathSeparator = '/';

    public Task<List<string>> GetKeysByPrefixAsync(string searchPrefix);
    public Task<string> GeneratePreSignedGetAsync(string objectKey);
    public Task<CreatePreSignedPostResponse> GeneratePreSignedPostAsync(string filePath);

    internal static string CombinePaths(params string[] paths)
    {
        return Path.Combine([MediaUploadPath, ..paths]).Replace('\\', PathSeparator);
    }
}