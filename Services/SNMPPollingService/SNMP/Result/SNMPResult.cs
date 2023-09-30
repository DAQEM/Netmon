using Lextm.SharpSnmpLib;

namespace SNMPPollingService.SNMP.Result;

public class SNMPResult : ISNMPResult
{
    public SNMPResult(List<Variable> variables)
    {
        Variables = variables;
    }

    public List<Variable> Variables { get; set; }
    
    public List<List<Variable>> GetEntries(string oid)
    {
        return Variables
            .Where(v => v.Id.ToString().StartsWith(oid))
            .GroupBy(variable => variable.Id.ToString().Split(".").Last())
            .Select(group => group.ToList()).ToList();
    }

    public List<Variable> GetTable(string oid)
    {
        return Variables
            .Where(v => v.Id.ToString().StartsWith(oid))
            .ToList();
    }
}