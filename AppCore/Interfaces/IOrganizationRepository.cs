using AppCore.Models;
using AppCore.Models.Enums;

namespace AppCore.Interfaces
{
    // Repozytorium dla encji Organization.
    // Dziedziczy po generycznym repozytorium asynchronicznym więc ma już podstawowe operacje CRUD (Add, Update, Delete, GetById, List itd.).
    public interface IOrganizationRepository : IGenericRepositoryAsync<Organization>
    {
        // Zwraca wszystkie organizacje o określonym typie (np. NGO, Foundation, Association).
        Task<IEnumerable<Organization>> GetByTypeAsync(OrganizationType type);

        // Zwraca listę członków organizacji o podanym identyfikatorze.
        // organizationId to Guid identyfikujący encję Organization.
        Task<IEnumerable<Person>> GetMembersAsync(Guid organizationId);
    }
}