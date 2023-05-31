namespace ElectricityDataApp.Services.Interfaces;

public interface IDataService
{

    public void DownloadData();
    public Task<string?> DownloadDataFromUrlAsync(string url);
}