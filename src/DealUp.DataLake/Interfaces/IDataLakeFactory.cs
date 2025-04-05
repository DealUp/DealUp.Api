using DealUp.DataLake.Models;

namespace DealUp.DataLake.Interfaces;

public interface IDataLakeFactory
{
    public IDataLake CreateDataLake(DataLakeType dataLakeType);
}