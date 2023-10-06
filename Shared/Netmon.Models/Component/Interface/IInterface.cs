using System.Net.NetworkInformation;
using System.Text.Json.Serialization;
using Netmon.Models.Component.Interface.Metric;
using Netmon.Models.Json.Converter;

namespace Netmon.Models.Component.Interface;

public interface IInterface : IComponent
{
    public int Index { get; set; }
    public string Name { get; set; }
    public InterfaceType Type { get; set; }
    
    [JsonConverter(typeof(PhysicalAddressConverter))]
    public PhysicalAddress PhysAddress { get; set; }
    public List<IInterfaceMetric> Metrics { get; set; }
}