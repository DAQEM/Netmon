using System.Net.NetworkInformation;
using System.Text.Json.Serialization;
using Netmon.Models.Component.Interface;
using Netmon.Models.Json.Converter;

namespace Netmon.SNMPPolling.DTO;

public class InterfaceDTO
{
    public int Index { get; set; }
    public string Name { get; set; }
    public InterfaceType Type { get; set; }
    [JsonConverter(typeof(PhysicalAddressConverter))]
    public PhysicalAddress PhysAddress { get; set; }
    
    public static InterfaceDTO FromInterface(IInterface arg)
    {
        return new InterfaceDTO
        {
            Index = arg.Index,
            Name = arg.Name,
            Type = arg.Type,
            PhysAddress = arg.PhysAddress
        };
    }
}