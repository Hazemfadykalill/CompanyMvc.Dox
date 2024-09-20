namespace CompanyMvc.Dox.PL.Services
{
    public interface ITransientService
    {

        public Guid Guid { get; set; }
        string GetGuid();
    }
}
