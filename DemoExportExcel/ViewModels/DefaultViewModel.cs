using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoExportExcel.DTO;
using DemoExportExcel.Service;
using DotVVM.BusinessPack.Controls;
using DotVVM.Framework.ViewModel;
using DotVVM.BusinessPack.Export.Excel;

namespace DemoExportExcel.ViewModels
{
    public class DefaultViewModel : MasterPageViewModel
    {
        private readonly FakeService _fakeservice;

        public BusinessPackDataSet<UserDTO> UserGridView { get; set; } = new BusinessPackDataSet<UserDTO>()
        {
            PagingOptions = { PageSize = 10 },
            SortingOptions = { SortExpression = nameof(UserDTO.Name) }
        };

        public int CountUserGridView => UserGridView.Items.Count;

        public DefaultViewModel(FakeService fakeService)
		{
			_fakeservice = fakeService;
		}

        public override Task PreRender()
        {
            if(UserGridView.IsRefreshRequired)
            {
                UserGridView.LoadFromQueryable(_fakeservice.FillUser());
            }

            return base.PreRender();
        }

        public async Task ExportGridView()
        {
            // Select GridView
            var gridview = (GridView)Context.View.FindControlByClientId(nameof(UserGridView));

            // Create DataSet for export
            var dataSet = new BusinessPackDataSet<UserDTO>();
            dataSet.SortingOptions = UserGridView.SortingOptions;
            dataSet.FilteringOptions = UserGridView.FilteringOptions;
            dataSet.PagingOptions = UserGridView.PagingOptions;

            dataSet.LoadFromQueryable(_fakeservice.FillUser());
            var settings = GridViewExportExcelSettings<UserDTO>.Default
               .ForColumn("Role", c => c.WithValueTransform<List<RoleDTO>>(value => string.Join("\r\n", value.Select(n => n.RoleName))));
               //.ForColumn("Role", opt => opt.WithValueTransform<List<RoleDTO>>(value => string.Join("\r\n", new List<string>() { "1", "2", "3", "4" })));

            var export = new GridViewExportExcel<UserDTO>(settings);
            using var file = export.Export(gridview, dataSet);

            // Return the file
            await Context.ReturnFileAsync(file, $"ExportUser_{DateTime.Now:yyyyMMddHHmmss}.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
    }
}
