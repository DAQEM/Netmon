using Lextm.SharpSnmpLib;
using Netmon.SNMPPolling.SNMP.Deserializer;
using Netmon.SNMPPolling.SNMP.Result;

namespace Netmon.SNMPPolling.SNMP.MIB.HostResources.Device.Processor;

public class HrProcessorEntry
{
    public static readonly string OID = "1.3.6.1.2.1.25.3.3.1";

    public Integer32 HrProcessorIndex { get; set; }  = null!;
    public ObjectIdentifier HrProcessorFrwID { get; set; } = null!;
    public Integer32 HrProcessorLoad { get; set; } = null!;
    
    public static ISNMPDeserializer<HrProcessorEntry> Deserializer { get; } = new HrProcessorEntryDeserializer();
    
    private class HrProcessorEntryDeserializer : ISNMPDeserializer<HrProcessorEntry>
    {
        public HrProcessorEntry Deserialize(ISNMPResult isnmpResult)
        {
            Dictionary<string, Variable> variables = isnmpResult.Variables.ToDictionary(v => v.Id.ToString().Split(".").Reverse().Skip(1).First(), v => v);
            
            return new HrProcessorEntry
            {
                HrProcessorIndex = new Integer32(int.Parse(isnmpResult.Variables.First().Id.ToString().Split(".").Last())),
                HrProcessorFrwID = (ObjectIdentifier) variables["1"].Data,
                HrProcessorLoad = (Integer32) variables["2"].Data
            };
        }
    }
}