using Lextm.SharpSnmpLib;
using SNMPPollingService.SNMP.Deserializer;
using SNMPPollingService.SNMP.Result;

namespace SNMPPollingService.SNMP.MIB.HostResources.Device.Processor;

public class HrProcessorEntry
{
    public static readonly string OID = "1.3.6.1.2.1.25.3.3.1";

    public Integer32 HrProcessorOID { get; set; } 
    public ObjectIdentifier HrProcessorFrwID { get; set; }
    public Integer32 HrProcessorLoad { get; set; }
    
    public static ISNMPDeserializer<HrProcessorEntry> Deserializer { get; } = new HrProcessorEntryDeserializer();
    
    private class HrProcessorEntryDeserializer : ISNMPDeserializer<HrProcessorEntry>
    {
        public HrProcessorEntry Deserialize(ISNMPResult isnmpResult)
        {
            Dictionary<string, Variable> variables = isnmpResult.Variables.ToDictionary(v => v.Id.ToString().Split(".").Reverse().Skip(1).First(), v => v);
            
            return new HrProcessorEntry
            {
                HrProcessorOID = new Integer32(int.Parse(isnmpResult.Variables.First().Id.ToString().Split(".").Last())),
                HrProcessorFrwID = (ObjectIdentifier) variables["1"].Data,
                HrProcessorLoad = (Integer32) variables["2"].Data
            };
        }
    }
}