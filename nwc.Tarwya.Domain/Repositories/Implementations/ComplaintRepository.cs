using nwc.Tarwya.Domain.Models.Models;
using nwc.Tarwya.Domain.Repositories.Contracts;

namespace nwc.Tarwya.Domain.Repositories.Implementations
{
	public class ComplaintRepository : EfRepository<Complaint>, IComplaintsRepo
	{
		public ComplaintRepository(TarwyaContext context)
			: base(context)
		{

		}
	}
}
