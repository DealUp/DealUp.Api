namespace DealUp.Database.Repositories.User;

public static class Converter
{
    public static Domain.User.User ToDomain(this Dal.User userDal)
    {
        return new Domain.User.User(userDal.Id, userDal.Email, userDal.Password);
    }

    public static Dal.User ToDal(this Domain.User.User userDomain)
    {
        return new Dal.User
        {
            Id = userDomain.Id,
            Email = userDomain.Email,
            Password = userDomain.Password
        };
    }
}