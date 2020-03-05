namespace nwc.Tarwya.Application.ViewModels.Categories
{
	public class CategoryVm
	{
		public int Id { get; set; }
		public string NameEn { get; set; }
		public string NameAr { get; set; }
		public string NameFr { get; set; }
		public string NameFa { get; set; }
		public string NameId { get; set; }
		public string NameUr { get; set; }
		public string NameTr { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public int ItemsCount { get; set; }
	}
	public class SubCategoryVm
	{
		public int Id { get; set; }
		public string Code { get; set; }
		public string NameEn { get; set; }
		public string NameAr { get; set; }
		public string NameFr { get; set; }
		public string NameFa { get; set; }
		public string NameId { get; set; }
		public string NameUr { get; set; }
		public string NameTr { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public int ComplaintsCount { get; set; }
	}
}
