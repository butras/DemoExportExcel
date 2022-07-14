using DemoExportExcel.DTO;
using System.Collections.Generic;
using System.Linq;

namespace DemoExportExcel.Service
{
    public class FakeService
    {
        public IQueryable<UserDTO> FillUser()
        {
            List<UserDTO> item = new();

            UserDTO user1 = new()
            {
                UserId = 1,
                Name = "Test1",
                Roles = new List<RoleDTO>()
                {
                    new RoleDTO() { RoleId = 1, RoleName = "Admin" },
                    new RoleDTO() { RoleId = 2, RoleName = "Read" }
                }
            };
            item.Add(user1);

            UserDTO user2 = new()
            {
                UserId = 1,
                Name = "Test2",
                Roles = new List<RoleDTO>()
                {
                    new RoleDTO() { RoleId = 1, RoleName = "Admin" },
                    new RoleDTO() { RoleId = 2, RoleName = "Read" },
                    new RoleDTO() { RoleId = 3, RoleName = "Edit" }
                }
            };
            item.Add(user2);

            return item.AsQueryable();
        }
    }
}
