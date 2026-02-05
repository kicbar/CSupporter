using CSupporter.Domain.Enums;

namespace CSupporter.Application.Models.DTOs;

public class ClientWithAuditDto
{
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string PhoneNumber { get; set; }

    public string Address { get; set; }

    public string Email { get; set; }

    public ClientType ClientType { get; set; }

    public DateTime UpdateDate { get; set; }

    public string UpdateUser { get; set; }

    public DateTime InsertDate { get; set; }

    public string InsertUser { get; set; }
}
