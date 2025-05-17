
namespace eStoreCA.Shared.Enums
{
    public class AppEnums
    {
        public enum OperationStatus
        {
            Ok = 1,
            Error = 2,
            ValidationError = 3,
        }

        public enum MsgType
        {
            Success = 1,
            Error = 2,
            Warning = 3,
            Info = 4
        }

        public enum DataOrderDirection
        {
            Asc,
            Desc
        }

        public enum AuditType
        {
            None = 0,
            Create = 1,
            Update = 2,
            Delete = 3
        }



        #region Custom
        #endregion Custom


    }
}
