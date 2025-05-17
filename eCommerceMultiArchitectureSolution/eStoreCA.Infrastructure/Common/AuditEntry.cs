
using System;
using eStoreCA.Shared.Enums;

namespace eStoreCA.Infrastructure.Common
{
    public class AuditEntry
    {
        public string UserId { get; set; }
        public string TableName { get; set; }
        public Dictionary<string, object> KeyValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> OldValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> NewValues { get; } = new Dictionary<string, object>();
        public AppEnums.AuditType AuditType { get; set; }
        public List<string> ChangedColumns { get; } = new List<string>();



        #region Custom
        #endregion Custom

    }
}
