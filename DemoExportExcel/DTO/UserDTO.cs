using System.Collections.Generic;

namespace DemoExportExcel.DTO;

public class UserDTO
{
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public List<RoleDTO> Roles { get; set; }
}
