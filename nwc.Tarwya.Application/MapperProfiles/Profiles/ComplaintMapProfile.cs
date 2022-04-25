using nwc.Tarwya.Application.ViewModels.Complains;
using nwc.Tarwya.Domain.Models.Models;
using nwc.Tarwya.Integrations.Models;

namespace nwc.Tarwya.Application.MapperProfiles.Profiles
{
	public class ComplaintMapProfile : BaseMappingProfile
	{
		public ComplaintMapProfile()
		{
			CreateMap<ComplaintEditableVm, Complaint>()
				.ForMember(i => i.IssuerName, s => s.MapFrom(d => d.IssuarName))
				.ForMember(i => i.IssuerMobileNumber, s => s.MapFrom(d => d.IssuarMobile))
				.ForMember(i => i.Description, s => s.MapFrom(d => d.Description))
				.ForMember(i => i.AgentLocation, s => s.MapFrom(d => d.AgentLocation))
				.ForMember(i => i.AgetOs, s => s.MapFrom(d => d.AgentOs))
				.ForMember(i => i.AgetLanguage, s => s.MapFrom(d => d.AgentLanguage))
				.ForMember(i => i.SubCategoryId, s => s.MapFrom(d => d.CategoryItemId))
				.ForMember(i => i.AssetId, s => s.MapFrom(d => d.AssetNumber))
				.ForMember(i => i.Coordintes, s => s.MapFrom(d => d.UTM))
				.ReverseMap();

			CreateMap<ComplaintEditableVm, WorkOrderCreationRequest>()
				.ForMember(i => i.IssuarName, s => s.MapFrom(d => d.IssuarName))
				.ForMember(i => i.IssuarMobile, s => s.MapFrom(d => d.IssuarMobile))
				.ForMember(i => i.Description, s => s.MapFrom(d => d.Description))
				.ForMember(i => i.utm, s => s.MapFrom(d => d.UTM))
				.ForMember(i => i.SubCategoryCode, s => s.MapFrom(d => d.CategoryItemId))
				.ForMember(i => i.AssetNumber, s => s.MapFrom(d => d.AssetNumber));

			CreateMap<WorkOrderInqueryResponce, ComplaintStatus>()
				.ForMember(i => i.AssetId, s => s.MapFrom(d => d.assetId))
				.ForMember(i => i.CaseClassCode, s => s.MapFrom(d => d.caseClassificationCode))
				.ForMember(i => i.CreationDate, s => s.MapFrom(d => d.creationDate))
				.ForMember(i => i.WorkOrderId, s => s.MapFrom(d => d.workOrderId))
				.ForMember(i => i.WorkOrderStatus, s => s.MapFrom(d => d.workOrderStatus));

			CreateMap<Complaint, ComplaintVm>()
				.ForMember(i => i.AssetNo, s => s.MapFrom(d => d.AssetId))
				.ForMember(i => i.Description, s => s.MapFrom(d => d.Description))
				.ForMember(i => i.Id, s => s.MapFrom(d => d.Id))
				.ForMember(i => i.IssuerMobile, s => s.MapFrom(d => d.IssuerMobileNumber))
				.ForMember(i => i.SubCatigory, s => s.MapFrom(d => d.SubCategory.NameEn))
				.ForMember(i => i.Date, s => s.MapFrom(d => d.CreationDate))
				.ForMember(i => i.IssuerName, s => s.MapFrom(d => d.IssuerName))
				.ForMember(i => i.IsSynced, s => s.MapFrom(d => d.IsSyncedToCcb))
				;
		}
	}
}
