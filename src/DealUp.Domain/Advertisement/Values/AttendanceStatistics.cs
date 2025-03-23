namespace DealUp.Domain.Advertisement.Values;

public record AttendanceStatistics
{
    public int ViewCount { get; private set; }
    public int FavoriteCount { get; private set; }
    public int ChatCount { get; private set; }

    private AttendanceStatistics(int viewCount, int favoriteCount, int chatCount)
    {
        ViewCount = viewCount;
        FavoriteCount = favoriteCount;
        ChatCount = chatCount;
    }

    public void IncrementViews()
    {
        ++ViewCount;
    }

    public void IncrementFavorites()
    {
        ++FavoriteCount;
    }

    public void IncrementChats()
    {
        ++ChatCount;
    }

    public void DecrementFavorites()
    {
        FavoriteCount = Math.Max(0, FavoriteCount - 1);
    }

    public static AttendanceStatistics CreateNew()
    {
        return new AttendanceStatistics(0, 0, 0);
    }
}