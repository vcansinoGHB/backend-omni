using System.Collections.Generic;
using System.Threading.Tasks;
using desa.Models;

namespace desa.Interfaces {
  public interface ICompany {
    public Task<List<CompanyModel>> GetCompanies();
  }
}