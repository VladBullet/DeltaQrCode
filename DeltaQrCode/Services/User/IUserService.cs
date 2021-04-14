namespace DeltaQrCode.Services
{
    public interface IUserService
    {
        CaUsers FindUserByLoginAndPass(string login, string password);
    }
}
