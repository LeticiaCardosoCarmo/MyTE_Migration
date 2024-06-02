namespace MyTE_Migration.Services
{
    public interface ISeedUserRoleInitial
    {
        Task SeedRolesAsync();

        Task SeedUsersAsync();
    }
}
