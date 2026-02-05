using CSupporter.Domain.Enums;

namespace CSupporter.Application.Models.DTOs;

public class OrderDto
{
    public int Id { get; set; }

    public string OrderNo { get; set; }

    public DateOnly OrderDate { get; set; }

    public ProducerType ProducerType { get; set; }

    public string AdditionalInfo { get; set; }

    public ClientForOrderDto Client { get; set; }

}
