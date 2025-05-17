using System.Reflection;

namespace eStoreCA.Shared.Common
{

    public static class AppPermissions
    {

        //  public static class Roles {

        //   public const string Administrator = "Administrator";
        //   public const string User = "User";

        #region Custom Roles
        #endregion Custom Roles

        
        //}

        public static List<PermissionModule> GetPermissionsModules()
        {
            List<PermissionModule> permissionModules = new List<PermissionModule>();

            Type type = typeof(AppPermissions);
            foreach (Type nested in type.GetNestedTypes())
            {
                PermissionModule permissionModule = new PermissionModule();
                permissionModule.Name = nested.Name;

                List<PermissionItem> permissionItems = new List<PermissionItem>();
                FieldInfo[] fields = nested.GetFields();
                foreach (var field in fields)
                {
                    PermissionItem permissionItem = new PermissionItem();
                    string name = field.Name;
                    object temp = field.GetValue(null);

                    permissionItem.ActionName = name;
                    permissionItem.ActionValue = temp.ToString();

                    permissionItems.Add(permissionItem);
                }

                permissionModule.PermissionItems = permissionItems;

                permissionModules.Add(permissionModule);
            }


            return permissionModules;
        }



        public static class RolePermissions
        {
            public const string List = "Permissions.Role.List";
            public const string View = "Permissions.Role.View";
            public const string Create = "Permissions.Role.Create";
            public const string Edit = "Permissions.Role.Edit";
            public const string Delete = "Permissions.Role.Delete";
        }


        public static class UserPermissions
        {
            public const string List = "Permissions.User.List";
            public const string View = "Permissions.User.View";
            public const string Create = "Permissions.User.Create";
            public const string Edit = "Permissions.User.Edit";
            public const string Delete = "Permissions.User.Delete";
        }



        public static class CategoryPermissions
        {
            public const string List = "Permissions.Category.List";
            public const string View = "Permissions.Category.View";
            public const string Create = "Permissions.Category.Create";
            public const string Edit = "Permissions.Category.Edit";
            public const string Delete = "Permissions.Category.Delete";
        }

        #region Custom Permissions
        #endregion Custom Permissions



    }
}

